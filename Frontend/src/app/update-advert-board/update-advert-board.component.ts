import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Advert } from '../models/Adverts/Advert';
import { Activity } from '../models/Activity/Activity';
import { AdvertsService } from '../_services/Adverts/adverts.service';
import { StatusUser } from '../_services/User/statusUser';
import { ActivityService } from '../_services/Activity/activity.service';
import { Commentary } from '../models/Commentary/Commentary';
import { CommentaryService } from '../_services/Commentary/commentary.service';
import { ReserveService } from '../_services/Reserve/reserve.service';
import { DatesReserve } from '../models/Reserve/datesReserve';
import { SnackBarService } from '../_services/SnackBar/snack-bar.service';
import { Reserve } from '../models/Reserve/Reserve';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidReserve } from '../models/Reserve/validReserve';
import {
    deleteObject,
    getDownloadURL,
    getStorage,
    ref,
    uploadBytes,
} from 'firebase/storage';
import * as firebaseAuth from 'firebase/auth';
import { AdvertImagesService } from '../_services/AdvertImages/advert-images.service';
import { NewAdvertImage } from '../models/Images/NewAdvertImage';
import { SetImage } from '../models/Images/SetImage';
import { delImage } from '../models/Images/delImage';
import { ActivityImagesService } from '../_services/ActivityImages/activity-images.service';
import { setReserve } from '../models/Reserve/setReserve';
@Component({
    selector: 'app-update-advert-board',
    templateUrl: './update-advert-board.component.html',
    styleUrls: ['./update-advert-board.component.scss'],
})
export class UpdateAdvertBoardComponent implements OnInit {
    @Input() public test: string;
    id: string;
    advert: Advert;
    noCommmentaries: boolean = false;
    imagesURL = [];
    mainImageUrl: string;
    datesReserve: FormGroup;
    isActivitiesEmpty = false;
    datesErrors: boolean = false;
    listActivity: Activity[];
    datesReserved: DatesReserve[];
    listCommentary: Commentary[];
    mailVisible: boolean = false;
    phoneVisible: boolean = false;
    notOwner: boolean = true;
    usr_id: number;
    usr_firstName: string;
    usr_lastName: string;
    storage = getStorage();
    userCanComment: boolean = false;
    images: SetImage[] = [];
    mainActivityImagesUrl = [];
    metadata = {
        contentType: 'image/jpeg',
    };
    currentYear = new Date().getFullYear();
    minDate = new Date(2020, 1, 1);
    minDatePicker = new Date();
    maxDate = new Date(this.currentYear + 1, 11, 31);

    constructor(
        private snackbar: SnackBarService,
        private route: ActivatedRoute,
        private rt: Router,
        private adv: AdvertsService,
        private act: ActivityService,
        private com: CommentaryService,
        private fB: FormBuilder,
        public statusUser: StatusUser,
        private rs: ReserveService,
        private imgAdv: AdvertImagesService,
        private imgAct: ActivityImagesService
    ) {
        this.datesReserve = this.fB.group({
            start: [, [Validators.required]],
            end: [, [Validators.required]],
        });
    }

    ngOnInit(): void {
        this.id = this.route.snapshot.paramMap.get('id');
        this.usr_id = Number(localStorage.getItem('usr_id'));
        this.usr_firstName = localStorage.getItem('usr_firstName');
        this.usr_lastName = localStorage.getItem('usr_lastName');
        let auth = firebaseAuth.getAuth();
        firebaseAuth.signInWithEmailAndPassword(
            auth,
            'firebaseadmin@mail.fr',
            'firebase'
        );

        this.imgAdv.getImagesByAdvertId(this.id).then((result) => {
            if (result.status === 404) {
                this.imagesURL.push('/assets/images/image 3.png');
                this.mainImageUrl = '/assets/images/image 3.png';
            }
            /* this.images = result.json();

            for (let index = 0; index < result.selectedImages.length; index++) {
                getDownloadURL(
                    ref(
                        this.storage,
                        'AdvertsImages/' +
                            result.selectedImages[index].adv_img_name
                    )
                ).then((url) => {
                    if (index === 0) {
                        this.mainImageUrl = url;
                    }
                    this.imagesURL[result.selectedImages[index].adv_img_order] =
                        url;
                });
            } */
            console.log(this.images);
        });

        this.adv.getAdverById(Number(this.id)).then((data) => {
            this.advert = data.selectedAdvert;

            let advUsrId: number = Number(this.advert[0].adv_usr_id);

            let usrId: number = Number(localStorage.getItem('usr_id'));

            if (advUsrId === usrId) {
                this.notOwner = false;
            } else {
                this.rt.navigate(['/home']);
            }
        });

        this.act.getActivityById(Number(this.id)).then(async (data) => {
            if (data.status === 404) {
                this.isActivitiesEmpty = true;
            } else {
                this.listActivity = await data.json();
                for (let index = 0; index < this.listActivity.length; index++) {
                    let element = this.listActivity[index].id;
                    this.imgAct
                        .getImagesByActivityId(element)
                        .then(async (result) => {
                            if (result.status === 404) {
                                this.mainActivityImagesUrl.push(
                                    '/assets/images/girl-918706_1920.jpg'
                                );
                            } else {
                                let images = await result.json();
                                getDownloadURL(
                                    ref(
                                        this.storage,
                                        'ActivitiesImages/' + images[0].name
                                    )
                                ).then((url) => {
                                    this.mainActivityImagesUrl.push(url);
                                });
                            }
                        });
                }
            }
        });

        this.com.getCommentaryByAdvert(Number(this.id)).then(async (data) => {
            if (data.status === 404) {
                this.noCommmentaries = true;
            } else {
                this.listCommentary = await data.json();
            }
        });

        this.rs.getDatebyAdvRes(Number(this.id)).then(async (data) => {
            if(data.status === 404){
                this.datesReserved = [];
            }else{
                this.datesReserved =  await data.json();
            }
        });
    }

    myFilter = (d: Date | null): boolean => {
        if (this.datesReserved != null && this.datesReserved.length > 0) {
            for (let i = 0; i < this.datesReserved.length; i++) {
                if (
                    d >=
                        this.getFormattedDate(
                            this.datesReserved[i].dateEnd
                        ) &&
                    d <=
                        this.getFormattedDate(
                            this.datesReserved[i].dateStart
                        )
                ) {
                    return false;
                }
            }
        }
        return true;
    };
    onSelect(url: string) {
        this.mainImageUrl = url;
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
                advertId: Number(this.advert[0].adv_id),
                price: this.advert[0].adv_price,
                tenants: 0,          
                dateStart: dateStart,
                dateEnd: dateEnd,
            };
            this.rs.createReserve(newReserve).then(async (reserve) => {
               

                this.rs.validReserve(reserve.id).then(() => {
                    this.snackbar.openSnackBar('période bloqué', 'ok', 1500);
                });
            });
        }
    }

    deleteImage(id: number) {
        console.log('IDDDDDDD:', id);
        let imageToDelete: delImage = {
            adv_img_id: this.images[id].adv_img_id,
            adv_img_name: this.images[id].adv_img_name,
            adv_img_order: this.images[id].adv_img_order,
            object_id: this.images[id].adv_id,
            usr_token: localStorage.getItem('access_token'),
        };
        console.log(imageToDelete);
        if (window.confirm('supprimer cette image ?')) {
            this.imgAdv.deleteImage(imageToDelete).then((result) => {
                if (result !== 'error') {
                    console.log('delete confimed');
                    deleteObject(
                        ref(
                            this.storage,
                            'AdvertsImages/' + this.images[id].adv_img_name
                        )
                    ).then(() => {
                        this.imagesURL.splice(id, 1);
                        this.mainImageUrl = this.imagesURL[0];
                        this.images.splice(id, 1);
                    });
                }
            });
        }
    }

    onFileSelected(event) {
        let selectedFile = event.target.files[0];

        let checkFileName: string = selectedFile.name;
        let sameFile: boolean = false;
        if (this.images !== undefined) {
            for (let index = 0; index < this.images.length; index++) {
                if (
                    checkFileName.split('.')[0] +
                        'id' +
                        localStorage.getItem('usr_id') ===
                    this.images[index].adv_img_name
                ) {
                    sameFile = true;
                }
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
            this.onUpload(selectedFile);
            reader.readAsDataURL(selectedFile);
            reader.onload = (event) => {
                this.imagesURL.push(reader.result);
            };
        }
    }
    onUpload(selectedFile) {
        let image: NewAdvertImage = {
            AdvertId: Number(this.id),
            Order: this.imagesURL.length,
            Name:
                selectedFile.name.split('.')[0] +
                'id' +
                localStorage.getItem('usr_id'),
        };
        this.imgAdv.createImagesForAdvert(image).then((data) => {});

        let mountainRef = ref(
            this.storage,
            'AdvertsImages/' +
                selectedFile.name.split('.')[0] +
                'id' +
                localStorage.getItem('usr_id')
        );
        uploadBytes(mountainRef, selectedFile, this.metadata).then(() => {
            this.imgAdv.getImagesByAdvertId(this.id).then(async (result) => {
                /* this.images = result.json(); */
            });
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

    back() {
        this.rt.navigate(['/profil']);
    }

    setMailVisible() {
        this.mailVisible = true;
    }

    setPhoneVisible() {
        this.phoneVisible = true;
    }
}
