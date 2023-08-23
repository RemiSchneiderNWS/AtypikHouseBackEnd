import { Component, OnInit } from '@angular/core';
import { Advert } from '../models/Adverts/Advert';
import { AdvertsService } from '../_services/Adverts/adverts.service';
import { StatusUser } from '../_services/User/statusUser';
import { CriteriaService } from '../_services/Criteria/criteria.service';
import { Criteria } from '../models/Criteria/Criteria';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import * as firebaseAuth from 'firebase/auth';
import { AdvertSet } from '../models/Adverts/AdvertSet';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { AdvertImagesService } from '../_services/AdvertImages/advert-images.service';
import { getDownloadURL, ref } from '@firebase/storage';
import { getStorage } from 'firebase/storage';
import { MatFormField } from '@angular/material/form-field';

@Component({
    selector: 'app-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.scss'],
})
export class SearchComponent implements OnInit {
    listAdverts: Advert[] = [];
    listCriterias: Criteria[] = [];
    imagesURL = [];
    isEmpty:boolean = false;
    idsCriterias: number[] = [];
    CriteriaForm: FormGroup;
    fieldRecherche:string;
    storage = getStorage();
    constructor(
        public statusUser: StatusUser,
        private fB: FormBuilder,
        private advService: AdvertsService,
        private criteriaService: CriteriaService,
        private imgAdv: AdvertImagesService
    ) {}

    ngOnInit(): void {
        this.initForm();
        let auth = firebaseAuth.getAuth();
        firebaseAuth.signInWithEmailAndPassword(
            auth,
            'firebaseadmin@mail.fr',
            'firebase'
        );
        this.advService.getAdvertsByTimestamp().then(async (adverts) => {
            this.listAdverts = adverts;
            await this.loadImages();
        });
        this.criteriaService.getCriteria().then((criterias) => {
            this.listCriterias = criterias;
        });
    }

    onChange(value: MatSlideToggleChange, id: number) {
        if (value.checked === true) {
            this.idsCriterias.push(id);
        } else {
            const index = this.idsCriterias.indexOf(id);
            if (index > -1) {
                this.idsCriterias.splice(index, 1);
            }
        }
    }
    onSetField(e){
        this.fieldRecherche = e.target.value;
    }
    onSearch(){
        this.isEmpty = false;
        console.log(this.fieldRecherche)
        if(this.fieldRecherche.length === 0 ){
            this.advService.getAdvertsByTimestamp().then(async (adverts) => {
                this.listAdverts = await adverts;
                this.loadImages();
            });
        }else {
            this.advService.getAdvertsBySearch(this.fieldRecherche).then(async (result) => {
                if(result.status === 404){
                    this.listAdverts = []
                    this.isEmpty = true
                }else {
                    this.listAdverts = await result.json()
                }
            })
        }
    }   

    loadImages() {
        this.imagesURL = [];
        for (let index = 0; index < this.listAdverts.length; index++) {
            this.imgAdv
                .getImagesByAdvertId(this.listAdverts[index].id)
                .then(async (result) => {
                    if (result.status === 404) {
                        this.imagesURL.push('/assets/images/image 3.png');
                    } else {
                        let images = await result.json();
                        console.log(images);
                        getDownloadURL(
                            ref(this.storage, 'AdvertsImages/' + images[0].name)
                        ).then(async (url) => {
                            await this.imagesURL.push(url);
                        });
                    }
                });
        }
        this.imagesURL = this.imagesURL.reverse();
    }
    onSubmit() {
        this.isEmpty = false;
        const criteriaList = this.idsCriterias;
        if (criteriaList.length > 0) {
           
            this.criteriaService
                .getAdvertByCriteria(criteriaList)
                .then(async (result) => {
                    if(result.status == 404){
                        this.listAdverts = [];
                        this.isEmpty = true;
                    }
                    else{
                        this.listAdverts = await result.json();
                        this.loadImages();
                    }
                    
                });
        } else {
            console.log("normalement c'est là")
            this.advService.getAdvertsByTimestamp().then(async (adverts) => {
                this.listAdverts = await adverts;
                this.loadImages();
            });
        }
    }

    initForm() {
        this.CriteriaForm = this.fB.group({
            checked: [false],
        });
    }
}
