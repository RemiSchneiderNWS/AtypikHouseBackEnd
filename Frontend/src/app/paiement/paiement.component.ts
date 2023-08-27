import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ReserveComponent } from '../reserve/reserve.component';
import { ReserveService } from '../_services/Reserve/reserve.service';
import { HttpClientModule } from '@angular/common/http';
import { ReserveCreated } from '../_services/Reserve/reserveCreated';
import { StatusUser } from '../_services/User/statusUser';
import { Advert } from '../models/Adverts/Advert';
import { AdvertsService } from '../_services/Adverts/adverts.service';
import { IPayPalConfig } from 'ngx-paypal';
import { Reserve } from '../models/Reserve/Reserve';
import { CancelReserve } from '../models/Reserve/CancelReserve';
import { ValidReserve } from '../models/Reserve/validReserve';
import { setReserve } from '../models/Reserve/setReserve';
import { getDownloadURL, getStorage, ref } from '@firebase/storage';
import * as firebaseAuth from 'firebase/auth';
import { AdvertImagesService } from '../_services/AdvertImages/advert-images.service';
@Component({
    selector: 'app-paiement',
    templateUrl: './paiement.component.html',
    styleUrls: ['./paiement.component.scss'],
})
export class PaiementComponent implements OnInit {
    public payPalConfig?: IPayPalConfig;
    imagesURL = [];
    mainImageUrl: string;
    storage = getStorage();
    test = '100';
    advert: Advert;
    reservePassed: Reserve;
    total: number;
    price: number;
    day: number;
    constructor(
        private rt: Router,
        private route: ActivatedRoute,
        private reserve: ReserveCreated,
        private adv: AdvertsService,
        public rs: ReserveService,
        private imgAdv: AdvertImagesService,
    ) {}

    id: string;
    @ViewChild('paypal', { static: true }) paypalElement: any;
    ngOnInit(): void {
        if (this.reserve.reserve === undefined) {
            this.rt.navigate(['/search']);
        }
        this.id = this.route.snapshot.paramMap.get('id');

        this.price = this.reserve.reserve.price;
        this.total = this.getTotalPrice(this.reserve.reserve.price);

        window.addEventListener('beforeunload', function (e) {
            let confirmationMessage = 'o/';
            e.returnValue = confirmationMessage;

            return confirmationMessage;
        });
        let reserveCreated: setReserve = this.reserve.reserve;
        this.adv.getAdverById(Number(this.id)).then((data) => {
            this.advert = data;
        });

        let auth = firebaseAuth.getAuth();
        firebaseAuth.signInWithEmailAndPassword(
            auth,
            'firebaseadmin@mail.fr',
            'firebase'
        );

        this.imgAdv.getImagesByAdvertId(this.id).then(async (result) => {
            if (result.status === 404) {
                this.imagesURL.push('/assets/images/image 3.png');
            } else {
                let images = await result.json();
                for (let index = 0; index < images.length; index++) {
                    getDownloadURL(
                        ref(this.storage, 'AdvertsImages/' + images[index].name)
                    ).then((url) => {
                        if (index === 0) {
                            this.mainImageUrl = url;
                        }
                        this.imagesURL[images[index].order] = url;
                    });
                }
            }
        });

        let totalOrder = this.total;
        let rsp: ReserveService = new ReserveService();
        let router: Router = this.rt;
        window.paypal
            .Buttons({
                style: {
                    size: 'small',
                    layout: 'horizontal',
                    color: 'blue',
                },
                createOrder: function (data, actions) {
                    return actions.order.create({
                        purchase_units: [
                            {
                                amount: {
                                    value: totalOrder,
                                    currency_code: 'EUR',
                                },
                            },
                        ],
                    });
                },

                onApprove: function (data, actions) {
                    let reserveToApprove: ValidReserve = {
                        advertId: reserveCreated.advertId,
                        dateStart: reserveCreated.dateStart,
                        dateEnd: reserveCreated.dateEnd,
                    };

                    rsp.validReserve(reserveToApprove).then(() => {
                        router.navigate(['/profil']);
                    });
                },
                onCancel: function (data) {
                    let reserveToCancel: CancelReserve = {
                        res_adv_id: reserveCreated.advertId,
                        res_id: 0,
                        res_date_start: reserveCreated.dateStart,
                        res_date_end: reserveCreated.dateEnd,
                        res_usr_id: Number(localStorage.getItem('usr_id')),
                    };
                    rsp.cancelReserve(reserveToCancel);
                },
                onClick: function () {
                    rsp.createReserve(reserveCreated).then( async (reserve) => {
                        this.reservePassed = reserve
                    });
                },
            })
            .render(this.paypalElement.nativeElement);
    }

    getTotalPrice(price: number) {
        let totalPrice: number;
        let start: Date = this.reserve.reserve.dateStart;
        let end: Date = this.reserve.reserve.dateEnd;
        let time = end.getTime() - start.getTime();
        let dayTime = time / (1000 * 3600 * 24);
        dayTime++;
        this.day = dayTime;
        totalPrice = dayTime * price;

        return totalPrice;
    }
}
