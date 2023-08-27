import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { getDownloadURL, getStorage, ref } from 'firebase/storage';
import { Advert } from '../models/Adverts/Advert';
import { AdvertDelete } from '../models/Adverts/AdvertDelete';
import { AdvertsService } from '../_services/Adverts/adverts.service';
import { SnackBarService } from '../_services/SnackBar/snack-bar.service';
import * as firebaseAuth from 'firebase/auth';
import { AdvertImagesService } from '../_services/AdvertImages/advert-images.service';
@Component({
    selector: 'app-user-adverts',
    templateUrl: './user-adverts.component.html',
    styleUrls: ['./user-adverts.component.scss'],
})
export class UserAdvertsComponent implements OnInit {
    listAdverts: Advert[];
    imagesURL = [];
    noAdverts: boolean = false;
    id: string = localStorage.getItem('usr_id');
    storage = getStorage();
    constructor(
        private advService: AdvertsService,
        public rt: Router,
        private snackbar: SnackBarService,
        private imgAdv: AdvertImagesService
    ) {}

    ngOnInit(): void {
        let auth = firebaseAuth.getAuth();
        firebaseAuth.signInWithEmailAndPassword(
            auth,
            'firebaseadmin@mail.fr',
            'firebase'
        );
        this.advService.getUserAdvert(this.id).then((adverts) => {
            this.listAdverts = adverts.userAdvert;
            if (adverts.userAdvert.length === 0) {
                this.noAdverts = true;
            }
            for (let index = 0; index < this.listAdverts.length; index++) {
                this.imgAdv
                    .getImagesByAdvertId(this.listAdverts[index].id)
                    .then(async (result) => {
                        if (result.status === 404) {
                            this.imagesURL.push('/assets/images/image 3.png');
                        }else{
                          /*  getDownloadURL(
                            ref(
                                this.storage,
                                'AdvertsImages/' +
                                    result.selectedImages[0].adv_img_name
                            )
                        ).then((url) => {
                            this.imagesURL.push(url);
                        });  */
                        }

                        
                    });
            }
        });
    }

    deleteAdvert(id: number) {
        let message = 'Voulez-vous vraiment supprimer cette annonce ?';
        let resultat = window.confirm(message);
        if (resultat) {
            let advertToDelete: AdvertDelete;
            advertToDelete = {
                adv_id: id,
                adv_usr_id: Number(localStorage.getItem('usr_id')),
            };
            this.advService.deleteAdvert(advertToDelete).then((result) => {
                if (result.error !== undefined && result.error.length > 0) {
                    this.snackbar.openSnackBar(result.error, 'ok', 1500);
                } else {
                    this.advService.getUserAdvert(this.id).then((adverts) => {
                        this.listAdverts = adverts.userAdvert;
                        if (adverts.userAdvert.length === 0) {
                            this.noAdverts = true;
                        }
                    });
                    this.snackbar.openSnackBar(
                        'Modification sauvegardé',
                        'ok',
                        1500
                    );
                }
            });
        }
    }
}
