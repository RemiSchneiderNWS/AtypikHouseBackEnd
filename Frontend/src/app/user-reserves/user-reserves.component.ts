import { Component, OnInit } from '@angular/core';
import { Reserve } from '../models/Reserve/Reserve';
import { ReserveReceive } from '../models/Reserve/ReserveReceive';
import { ReserveService } from '../_services/Reserve/reserve.service';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { CancelReserve } from '../models/Reserve/CancelReserve';
import { StatusUser } from '../_services/User/statusUser';
import { AdvertImagesService } from '../_services/AdvertImages/advert-images.service';
import { getDownloadURL, getStorage, ref } from '@firebase/storage';
import * as firebaseAuth from 'firebase/auth';
@Component({
    selector: 'app-user-reserves',
    templateUrl: './user-reserves.component.html',
    styleUrls: ['./user-reserves.component.scss'],
})
export class UserReservesComponent implements OnInit {
    listReserves: ReserveReceive[];
    noReserves: boolean = false;
    imagesURL = [];
    id: string = localStorage.getItem('usr_id');
    mail: string = localStorage.getItem('usr_mail');
    dateStart: string;
    dateEnd: string;
    storage = getStorage();
    constructor(
        private rs: ReserveService,
        public dialog: MatDialog,
        private rt: Router,
        private imgAdv: AdvertImagesService
    ) {}

    ngOnInit(): void {
        let auth = firebaseAuth.getAuth();
        firebaseAuth.signInWithEmailAndPassword(
            auth,
            'firebaseadmin@mail.fr',
            'firebase'
        );
        setTimeout(() => {
            this.rs.getUserReserve().then(async (reserves) => {
                this.listReserves = await reserves.json();

                for (let index = 0; index < this.listReserves.length; index++) {
                    this.imgAdv
                        .getImagesByAdvertId(this.listReserves[index].advert.id)
                        .then(async (result) => {
                            if (result.status === 404) {
                                this.imagesURL.push(
                                    '/assets/images/image 3.png'
                                );
                            }
                            let image = await result.json();
                            getDownloadURL(
                                ref(
                                    this.storage,
                                    'AdvertsImages/' + image[0].name
                                )
                            ).then((url) => {
                                this.imagesURL.push(url);
                            });
                        });
                }
                if (reserves.status === 404) {
                    this.noReserves = true;
                } else {
                    for (let i = 0; i < this.listReserves.length; i++) {
                        this.listReserves[i].res_date_start_formated =
                            this.getFormattedDate(
                                this.listReserves[i].dateStart
                            );
                        this.listReserves[i].res_date_end_formated =
                            this.getFormattedDate(this.listReserves[i].dateEnd);
                    }
                }
            });
        }, 500);
    }

    getFormattedDate(dateReceive: Date) {
        let date = new Date(dateReceive);
        let year = date.getFullYear();
        let month = (1 + date.getMonth()).toString().padStart(2, '0');
        let day = date.getDate().toString().padStart(2, '0');

        return '' + day + '-' + month + '-' + year;
    }

    deleteReserve(index) {
        let message =
            'Voulez-vous vraiment annuler la réservation du locataire ?';
        let resultat = window.confirm(message);
        if (resultat) {
            let dateEnd: Date;
            let dateStart: Date;
            let cancelReserve: CancelReserve = {
                res_id: this.listReserves[index].id,
                res_adv_id: this.listReserves[index].advert.id,
                res_date_end: dateEnd,
                res_date_start: dateStart,
                res_usr_id: Number(localStorage.getItem('usr_id')),
            };
            this.rs
                .deleteReserve(cancelReserve)
                .then(() => this.rt.navigate(['/profil']));
        }
    }
}
