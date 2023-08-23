import { Component, OnInit } from '@angular/core';
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AdvertsService } from '../_services/Adverts/adverts.service';
import { ReserveService } from '../_services/Reserve/reserve.service';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { Advert } from '../models/Adverts/Advert';
import { DatesReserve } from '../models/Reserve/datesReserve';
import { SnackBarService } from '../_services/SnackBar/snack-bar.service';
import { ReserveCreated } from '../_services/Reserve/reserveCreated';
import { Reserve } from '../models/Reserve/Reserve';
import { getDownloadURL, getStorage, ref } from '@firebase/storage';
import * as firebaseAuth from 'firebase/auth';
import { AdvertImagesService } from '../_services/AdvertImages/advert-images.service';
import { setReserve } from '../models/Reserve/setReserve';
@Component({
    selector: 'app-reserve',
    templateUrl: './reserve.component.html',
    styleUrls: ['./reserve.component.scss'],
})
export class ReserveComponent implements OnInit {
    id: string;
    advert: Advert;
    datesReserve: FormGroup;
    minDate: Date;
    maxDate: Date;
    errorNbTenants: Boolean = false;
    datesReserved: DatesReserve[];
    datesErrors: boolean = false;
    imagesURL = [];
    mainImageUrl: string;
    storage = getStorage();

    constructor(
        private route: ActivatedRoute,
        private fB: FormBuilder,
        private rt: Router,
        private snackbar: SnackBarService,
        private adv: AdvertsService,
        private res: ReserveService,
        private resCreate: ReserveCreated,
        private imgAdv: AdvertImagesService,
    ) {
        const today = new Date();
        const month = today.getMonth();
        const year = today.getFullYear();
        const currentYear = new Date().getFullYear();

        this.minDate = new Date();
        this.maxDate = new Date(currentYear + 1, 11, 31);

        this.datesReserve = this.fB.group({
            nbTenants: [
                ,
                [Validators.required, Validators.pattern(/^[0-9]\d*$/)],
            ],
            start: [, [Validators.required]],
            end: [, [Validators.required]],
        });
    }

    ngOnInit(): void {
        this.id = this.route.snapshot.paramMap.get('id');
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
        this.res.getDatebyAdvRes(Number(this.id)).then(async (data) => {
            if(data.status === 404){
                this.datesReserved = [];
            }else{
                this.datesReserved =  await data.json();
            }
            
        });
    }
    getFormattedDate(dateReceive: Date) {
        let date = new Date(dateReceive);
        let year = date.getFullYear();
        let month = (1 + date.getMonth()).toString().padStart(2, '0');
        let day = date.getDate().toString().padStart(2, '0');

        return new Date(month + '-' + day + '-' + year);
    }
    getFormattedDateForApi(dateReceive: Date) {
        let date = new Date(dateReceive);
        let year = date.getFullYear();
        let month = (1 + date.getMonth()).toString().padStart(2, '0');
        let day = date.getDate().toString().padStart(2, '0');

        return new Date(year + '-' + month + '-' + day);
    }

    myFilter = (d: Date | null): boolean => {
        if (this.datesReserved.length > 0) {
            for (let i = 0; i < this.datesReserved.length; i++) {
                if (
                    d >=
                        this.getFormattedDate(
                            this.datesReserved[i].dateStart
                        ) &&
                    d <=
                        this.getFormattedDate(
                            this.datesReserved[i].dateEnd
                        )
                ) {
                    return false;
                }
            }
        }
        return true;
    };
    onChangeTenantsFields(datesReserve) {
        if (datesReserve.nbTenants > this.advert.tenants) {
            this.errorNbTenants = true;
        } else {
            this.errorNbTenants = false;
        }
    }
    onSubmit(datesReserve) {
        let dateStartToCompare: Date;
        let dateEndToCompare: Date;

        for (let i = 0; i < this.datesReserved.length; i++) {
            dateStartToCompare = new Date(this.datesReserved[i].dateEnd);
            dateEndToCompare = new Date(this.datesReserved[i].dateStart);
            if (
                datesReserve.start < dateStartToCompare &&
                datesReserve.end >= dateEndToCompare
            ) {
                this.datesErrors = true;
                this.snackbar.openSnackBar(
                    'Vous ne pouvez pas sélectionner une période réservée',
                    'ok',
                    1500
                );
            }
        }
        if (!this.datesErrors) {
            let dateStart: Date = new Date(
                this.getFormattedDateForApi(datesReserve.start)
            );
            let dateEnd: Date = new Date(
                this.getFormattedDateForApi(datesReserve.end)
            );
            let newReserve: setReserve = {
                advertId: Number(this.advert.id),
                price: this.advert.price,
                tenants: datesReserve.nbTenants, 
                dateStart: dateStart,
                dateEnd: dateEnd,
            };
            console.log("Reservation : " , newReserve )
            this.resCreate.reserve = newReserve;
            this.rt.navigate(['/paiement/' + this.id]);
        }
    }
}
