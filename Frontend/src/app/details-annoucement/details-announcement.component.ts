import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Advert } from '../models/Adverts/Advert';
import { Activity } from '../models/Activity/Activity';
import { AdvertsService } from '../_services/Adverts/adverts.service';
import { StatusUser } from '../_services/User/statusUser';
import { ActivityService } from '../_services/Activity/activity.service';
import { Commentary } from '../models/Commentary/Commentary';
import { CommentaryService } from '../_services/Commentary/commentary.service';
import { AdvertSet } from '../models/Adverts/AdvertSet';
import { CommentarySet } from '../models/Commentary/CommentarySet';
import { SnackBarService } from '../_services/SnackBar/snack-bar.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ThrowStmt } from '@angular/compiler';
import { ReserveService } from '../_services/Reserve/reserve.service';
import { AdvertImagesService } from '../_services/AdvertImages/advert-images.service';
import { getDownloadURL, getStorage, ref } from '@firebase/storage';
import * as firebaseAuth from 'firebase/auth';
import { ActivityImagesService } from '../_services/ActivityImages/activity-images.service';
@Component({
    selector: 'app-details-announcement',
    templateUrl: './details-announcement.component.html',
    styleUrls: ['./details-announcement.component.scss'],
})
export class DetailsAnnouncementComponent implements OnInit {
    id: string;
    advert: Advert;
    noCommmentaries: boolean = false;
    imagesURL = [];
    mainImageUrl: string;
    listActivity: Activity[];
    listCommentary: Commentary[];
    mailVisible: boolean = false;
    phoneVisible: boolean = false;
    notOwner: boolean = true;
    usr_id: number;
    isActivitiesEmpty = false;
    usr_firstName: string;
    usr_lastName: string;
    NewCommentary: FormGroup;
    userCanComment: boolean = false;
    mainActivityImagesUrl = [];
    storage = getStorage();

    constructor(
        private route: ActivatedRoute,
        private rt: Router,
        private fB: FormBuilder,
        private snackbar: SnackBarService,
        private adv: AdvertsService,
        private act: ActivityService,
        private com: CommentaryService,
        public statusUser: StatusUser,
        private rs: ReserveService,
        private imgAdv: AdvertImagesService,
        private imgAct: ActivityImagesService
    ) {}

    ngOnInit(): void {
        this.initForm();
        this.id = this.route.snapshot.paramMap.get('id');
        this.usr_id = Number(localStorage.getItem('usr_id'));
        this.usr_firstName = localStorage.getItem('usr_firstName');
        this.usr_lastName = localStorage.getItem('usr_lastName');
        this.adv.getAdverById(Number(this.id)).then(async (data) => {
            this.advert = data;

            let advUsrId: number = await Number(this.advert.user.id);
            let usrId: number = Number(localStorage.getItem('usr_id'));

            if (advUsrId === usrId) {
                this.notOwner = false;
            }
            if (this.statusUser.isAuth) {
                this.checkIfUserCanComment(advUsrId);
            }
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

        this.act.getActivityByAdvert(Number(this.id)).then(async (data) => {
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
    }

    initForm() {
        this.NewCommentary = this.fB.group({
            content: ['', [Validators.required]],
        });
    }

    onSubmit(NewCommentaryFormData) {
        const commentarySet: CommentarySet = {
            com_text: NewCommentaryFormData.content,
            com_adv_id: Number(this.id),
            com_usr_id: this.usr_id,
            com_usr_firstName: this.usr_firstName,
            com_usr_lastName: this.usr_lastName,
        };
        this.snackbar.openSnackBar('Sauvegarde en cours ...', 'ok', 1500);

        this.com.createCommentary(commentarySet).then((result) => {
            if (result.error !== undefined && result.error.length > 0) {
                this.snackbar.openSnackBar(result.error, 'ok', 1500);
            } else {
                this.com
                    .getCommentaryByAdvert(Number(this.id))
                    .then(async (data) => {
                        if (data.status === 404) {
                            this.noCommmentaries = true;
                        } else {
                            this.listCommentary = await data.json();
                        }
                    });
                this.snackbar.openSnackBar(
                    'Commentaire sauvegardé',
                    'ok',
                    1500
                );
            }
        });
    }

    onSelect(url: string) {
        this.mainImageUrl = url;
    }

    back() {
        this.rt.navigate(['/search']);
    }

    setMailVisible() {
        this.mailVisible = true;
    }

    setPhoneVisible() {
        this.phoneVisible = true;
    }

    checkIfUserCanComment(OwnerId: number) {
        let userHasReserved: boolean = false;
        this.rs.userCanComment(Number(this.id)).then((bool) => {
            userHasReserved = bool;
        });

        if (this.notOwner == false && userHasReserved) {
            this.userCanComment = true;
        }

        console.log("user Status" ,this.statusUser.isAuth , " NotOwner", this.notOwner)
    }
}
