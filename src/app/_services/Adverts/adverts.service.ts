import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Advert } from 'src/app/models/Adverts/Advert';
import { AdvertDelete } from 'src/app/models/Adverts/AdvertDelete';
import { AdvertSet } from 'src/app/models/Adverts/AdvertSet';
import { AdvertUpdate } from 'src/app/models/Adverts/AdvertUpdate';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})
export class AdvertsService {
    constructor(public rt: Router) {}

    async getAdvertsByTimestamp() {//Ok
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Methods':
                    'GET, POST, PUT, DELETE, OPTIONS',
            },
        };

        const response = await fetch(
            environment.API_URL + '/Advert/getAdvertByTimestamp/1',
            requestOptions
        );
        const data = await response.json();
        return data;
    }

    async getAdverById(id: number) {//OK
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
            
        };

        const response = await fetch(
            environment.API_URL + '/Advert/getAdvertbyId/' + id,
            requestOptions
        );
        const data = await response.json();
        return data;
    }
    async getUserAdvert(id: string) {
        const requestOptions = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('access_token'),
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
            body: JSON.stringify({
                adv_usr_id: id,
            }),
        };

        const response = await fetch(
            environment.API_URL + '/getUserAdvert',
            requestOptions
        );
        const data = await response.json();
        return data;
    }

    async getAdvertsBySearch(indication: string){//OK
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },        
        };

        const response = await fetch(
            environment.API_URL + '/Advert/getAdvertsBySearch/' + indication,
            requestOptions
        );

        return response;
    }

    async postAdvert(advert: AdvertSet) {//OK
        const requestOptions = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('access_token'),
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },

            body: JSON.stringify(advert),
        };
/* {
                adv_name: advert.adv_name,
                adv_type: advert.adv_type,
                adv_tenants: advert.adv_tenants,
                adv_status: advert.adv_status,
                adv_adress: advert.adv_adress,
                adv_city: advert.adv_city,
                adv_postal: advert.adv_postal,
                adv_price: advert.adv_price,
                adv_usr_id: advert.adv_usr_id,
                adv_usr_mail: advert.adv_usr_mail,
                adv_usr_phone: advert.adv_usr_phone,
                adv_describe: advert.adv_describe,
            } */
        const response = await fetch(
            environment.API_URL + '/Advert',
            requestOptions
        );
        const data = await response;/* .json(); */
        return data;
    }

    async updateAdvert(advert: AdvertUpdate) {
        const requestOptions = {
            method: 'PUT',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('access_token'),
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },

            body: JSON.stringify({
                adv_id: advert.adv_id,
                adv_name: advert.adv_name,
                adv_type: advert.adv_type,
                adv_tenants: advert.adv_tenants,
                adv_status: advert.adv_status,
                adv_up: advert.adv_up,
                adv_usr_id: advert.adv_usr_id,
                adv_adress: advert.adv_adress,
                adv_city: advert.adv_city,
                adv_postal: advert.adv_postal,
                adv_price: advert.adv_price,
                adv_describe: advert.adv_describe,
            }),
        };
        const response = await fetch(
            environment.API_URL + '/updateAdvert',
            requestOptions
        );
        const data = await response.json();
        return data;
    }

    async deleteAdvert(advert: AdvertDelete) {
        const requestOptions = {
            method: 'DELETE',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('access_token'),
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },

            body: JSON.stringify({
                adv_id: advert.adv_id,
                adv_usr_id: advert.adv_usr_id,
            }),
        };
        const response = await fetch(
            environment.API_URL + '/deleteAdvert',
            requestOptions
        );
        const data = await response.json();
        return data;
    }
}
