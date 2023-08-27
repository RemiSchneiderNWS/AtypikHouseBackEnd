import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Activity } from '../models/Activity/Activity';
import { SetActivity } from '../models/Activity/setActivity';
import * as firebaseAuth from 'firebase/auth';
import { AdvertSet } from '../models/Adverts/AdvertSet';
import { NewActivityImage } from '../models/Images/NewActivityImage';
import { ActivityService } from '../_services/Activity/activity.service';
import { ActivityImagesService } from '../_services/ActivityImages/activity-images.service';
import { AdvertsService } from '../_services/Adverts/adverts.service';
import { SnackBarService } from '../_services/SnackBar/snack-bar.service';
import { getDownloadURL, getStorage, ref, uploadBytes } from 'firebase/storage';
@Component({
    selector: 'app-new-activity',
    templateUrl: './new-activity.component.html',
    styleUrls: ['./new-activity.component.scss'],
})
export class NewActivityComponent implements OnInit {
    selectedFiles = [];
    filesSelectedURL = [];
    photoEmpty = false;
    fileName = '';
    metadata = {
        contentType: 'image/jpeg',
    };
    NewActivityForm: FormGroup;
    constructor(
        private route: ActivatedRoute,
        private fB: FormBuilder,
        private actService: ActivityService,
        private snackbar: SnackBarService,
        private rt: Router,
        private imgAct: ActivityImagesService
    ) {}
    id = this.route.snapshot.paramMap.get('id');
    usrId = localStorage.getItem('usr_id');
    ngOnInit(): void {
        this.initForm();
    }

    initForm() {
        this.NewActivityForm = this.fB.group({
            name: ['', [Validators.required]],
            adress: ['', [Validators.required]],
            city: ['', [Validators.required]],
            act_type: ['', [Validators.required]],
            act_price: [
                '',
                [Validators.required, Validators.pattern(/^[0-9]\d*$/)],
            ],

            act_postal: [
                '',
                [Validators.required, Validators.pattern(/[0-9]{5}/)],
            ],
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

    onUpload(selectedFile, nbOrder: number) {
        this.imgAct.getLastActivityId().then((data) => {
            let image: NewActivityImage = {
                ActivityId: data.id,
                Order: nbOrder,
                Name:
                    selectedFile.name.split('.')[0] +
                    'id' +
                    localStorage.getItem('usr_id'),
            };
            this.imgAct.createImagesForActivity(image).then((data) => {
                console.log(data);
            });
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
            'ActivitiesImages/' +
                selectedFile.name.split('.')[0] +
                'id' +
                localStorage.getItem('usr_id')
        );
        uploadBytes(mountainRef, selectedFile, this.metadata).then((result) => {
            console.log('upload :', result);
        });
    }

    onSubmit(NewActivityFormData) {
        if (this.filesSelectedURL.length === 0) {
            this.photoEmpty = true;
        } else {
            const activity: SetActivity = {
                act_name: NewActivityFormData.name,
                act_adv_id: Number(this.id),
                act_adress: NewActivityFormData.adress,
                act_city: NewActivityFormData.adress,
                act_describe: NewActivityFormData.describe,
                act_postal: NewActivityFormData.act_postal,
                act_price: NewActivityFormData.act_price,
                act_type: NewActivityFormData.act_type,
                act_usr_id: Number(this.usrId),
            };
            console.log(activity);
            this.snackbar.openSnackBar('Sauvegarde en cours ...', 'ok', 1500);

            this.actService.createActivity(activity).then((result) => {
                if (result.error !== undefined && result.error.length > 0) {
                    this.snackbar.openSnackBar(result.error, 'ok', 1500);
                } else {
                    for (
                        let index = 0;
                        index < this.selectedFiles.length;
                        index++
                    ) {
                        this.onUpload(this.selectedFiles[index], index);
                    }
                    this.snackbar.openSnackBar(
                        'Activité sauvegardé',
                        'ok',
                        1500
                    );
                    this.rt.navigate(['/search']);
                }
            });
        }
    }
}
