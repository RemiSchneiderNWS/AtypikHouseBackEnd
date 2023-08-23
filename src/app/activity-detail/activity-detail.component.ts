import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { getDownloadURL, ref } from '@firebase/storage';
import { getStorage } from 'firebase/storage';
import { Activity } from '../models/Activity/Activity';
import { ActivityService } from '../_services/Activity/activity.service';
import { ActivityImagesService } from '../_services/ActivityImages/activity-images.service';

import * as firebaseAuth from 'firebase/auth';

@Component({
    selector: 'app-activity-detail',
    templateUrl: './activity-detail.component.html',
    styleUrls: ['./activity-detail.component.scss'],
})
export class ActivityDetailComponent implements OnInit {
    id: string;
    advId: string;
    activity: Activity;
    mainImageUrl: string;
    imagesURL = [];
    storage = getStorage();
    constructor(
        private route: ActivatedRoute,
        private rt: Router,
        private atcS: ActivityService,
        private imgAct: ActivityImagesService
    ) {}

    ngOnInit(): void {
        this.id = this.route.snapshot.paramMap.get('id');
        this.advId = this.route.snapshot.paramMap.get('advId');

        let auth = firebaseAuth.getAuth();
        firebaseAuth.signInWithEmailAndPassword(
            auth,
            'firebaseadmin@mail.fr',
            'firebase'
        );
        this.imgAct.getImagesByActivityId(this.id).then(async (result) => {
            if (result.status === 404) {
                this.imagesURL.push('/assets/images/girl-918706_1920.jpg');
                this.mainImageUrl = '/assets/images/girl-918706_1920.jpg';
            } else {
                let images = await result.json();
                for (let index = 0; index < images.length; index++) {
                    getDownloadURL(
                        ref(
                            this.storage,
                            'ActivitiesImages/' + images[index].name
                        )
                    ).then((url) => {
                        if (index === 0) {
                            this.mainImageUrl = url;
                        }
                        this.imagesURL.push(url);
                    });
                }
            }
        });
        this.atcS
            .getOneActivityByID(Number(this.id), Number(this.advId))
            .then((activity) => {
                this.activity = activity.result;
            });
    }

    back() {
        this.rt.navigate(['/detail/' + this.activity[0].act_adv_id]);
    }

    onSelect(url: string) {
        this.mainImageUrl = url;
    }
}
