import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import * as firebaseAuth from 'firebase/auth';
import { getDownloadURL, getStorage, ref, uploadBytes } from 'firebase/storage';
import { AdvertSet } from '../models/Adverts/AdvertSet';
import { NewAdvertImage } from '../models/Images/NewAdvertImage';
import { AdvertImagesService } from '../_services/AdvertImages/advert-images.service';
import { AdvertsService } from '../_services/Adverts/adverts.service';
import { SnackBarService } from '../_services/SnackBar/snack-bar.service';

@Component({
    selector: 'app-new-advert',
    templateUrl: './new-advert.component.html',
    styleUrls: ['./new-advert.component.scss'],
})
export class NewAdvertComponent implements OnInit {
    selectedFiles = [];
    filesSelectedURL = [];
    photoEmpty = false;
    fileName = '';
    metadata = {
        contentType: 'image/jpeg',
    };
    NewAdvertForm: FormGroup;
    constructor(
        private fB: FormBuilder,
        private advService: AdvertsService,
        private snackbar: SnackBarService,
        private rt: Router,
        private http: HttpClient,
        private imgAdv: AdvertImagesService
    ) {}

    ngOnInit(): void {
        this.initForm();
    }

    initForm() {
        this.NewAdvertForm = this.fB.group({
            titre: ['', [Validators.required]],
            type: ['', [Validators.required]],
            max: ['', [Validators.required, Validators.pattern(/^[0-9]\d*$/)]],
            adress: ['', [Validators.required]],
            city: ['', [Validators.required]],
            adv_postal: [
                '',
                [Validators.required, Validators.pattern(/[0-9]{5}/)],
            ],
            adv_price: ['', [Validators.required]],
            dispo: [false],
            describe: ['', [Validators.required]],
        });
    }
    onFileSelected(event) {
        this.photoEmpty = false;
        let selectedFile = event.target.files[0];
        let checkFileName: string = selectedFile.name;
        let sameFile: boolean = false;
        for (let index = 0; index < this.selectedFiles.length; index++) {
            if (checkFileName === this.selectedFiles[index].name) {
                sameFile = true;
            }
        }
        if (sameFile) {
            this.snackbar.openSnackBar('image déjà ajoutée', 'ok', 3500);
        } else if (!selectedFile.type.match('image')) {
            this.snackbar.openSnackBar(
                'Format du fichier incorrect',
                'ok',
                3500
            );
        } else {
            let reader = new FileReader();

            this.selectedFiles.push(selectedFile);

            reader.readAsDataURL(selectedFile);
            reader.onload = (event) => {
                this.filesSelectedURL.push(reader.result);
            };

            this.fileName = selectedFile.name;
        }
    }

    onClickDelete(i: number) {
        this.selectedFiles.splice(i, 1);
        this.filesSelectedURL.splice(i, 1);
    }

    onUpload(selectedFile, nbOrder: number, IdAdvertCreated) {
        let image: NewAdvertImage = {
            AdvertId: IdAdvertCreated,
            Order: nbOrder,
            Name:
                selectedFile.name.split('.')[0] +
                'id' +
                localStorage.getItem('usr_id'),
        };
        this.imgAdv.createImagesForAdvert(image).then((data) => {
            console.log(data);
        });

        let auth = firebaseAuth.getAuth();
        firebaseAuth.signInWithEmailAndPassword(
            auth,
            'firebaseadmin@mail.fr',
            'firebase'
        );
        let storage = getStorage();
        let mountainRef = ref(
            storage,
            'AdvertsImages/' +
                selectedFile.name.split('.')[0] +
                'id' +
                localStorage.getItem('usr_id')
        );
        uploadBytes(mountainRef, selectedFile, this.metadata).then((result) => {
            console.log('upload :', result);
        });
    }

    onSubmit(NewAdvertUserFormData) {
        if (this.filesSelectedURL.length === 0) {
            this.photoEmpty = true;
        } else {
            const advert: AdvertSet = {
                Name: NewAdvertUserFormData.titre,
                Type: NewAdvertUserFormData.type,
                Tenants: NewAdvertUserFormData.max,
                Status: NewAdvertUserFormData.dispo,
                Adress: NewAdvertUserFormData.adress,
                City: NewAdvertUserFormData.city,
                Postal: '' + NewAdvertUserFormData.adv_postal,
                Price: NewAdvertUserFormData.adv_price,
                Describe: NewAdvertUserFormData.describe,
                Up: false,
            };
            this.snackbar.openSnackBar('Sauvegarde en cours ...', 'ok', 1500);

            this.advService.postAdvert(advert).then(async (result) => {
                if (result.status === 400) {
                    let errorMessage = await result.json();
                    this.snackbar.openSnackBar(errorMessage, 'ok', 1500);
                } else {
                    let advertCreated = await result.json();
                    for (
                        let index = 0;
                        index < this.selectedFiles.length;
                        index++
                    ) {
                        console.log('Id advert', advertCreated.id);
                        this.onUpload(
                            this.selectedFiles[index],
                            index,
                            advertCreated.id
                        );
                    }
                    this.snackbar.openSnackBar(
                        'Annonce sauvegardé',
                        'ok',
                        1500
                    );
                    this.rt.navigate(['/search']);
                }
            });
        }
    }
}
