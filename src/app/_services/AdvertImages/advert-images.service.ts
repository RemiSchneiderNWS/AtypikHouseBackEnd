import { Injectable } from '@angular/core';
import { delImage } from 'src/app/models/Images/delImage';
import { NewAdvertImage } from 'src/app/models/Images/NewAdvertImage';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})
export class AdvertImagesService {
    constructor() {}

    async getImagesByAdvertId(adv_id) {//ok
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
            environment.API_URL + '/AdvertImage/' + adv_id,
            requestOptions
        );

        const data = await response;//.json();
        return data;
    }
    async createImagesForAdvert(image: NewAdvertImage) {//OK
        const requestOptions = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('access_token'),
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Methods':
                    'GET, POST, PUT, DELETE, OPTIONS',
            },
            body: JSON.stringify(image),
        };
        const response = await fetch(
            environment.API_URL + '/AdvertImage',
            requestOptions
        );
        const data = await response.json();
        return data;
    }

/*     async getLastAdvertId() {
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Methods':
                    'GET, POST, PUT, DELETE, OPTIONS',
            },
            body: JSON.stringify({}),
        };
        const response = await fetch(
            environment.API_URL + '/getLastId',
            requestOptions
        );
        const data = await response.json();
        return data;
    }
 */
    async deleteImage(delImage: delImage) {
        console.log(delImage);
        const requestOptions = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('access_token'),
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Methods':
                    'GET, POST, PUT, DELETE, OPTIONS',
            },
            body: JSON.stringify({
                adv_img_id: delImage.adv_img_id,
                adv_img_name: delImage.adv_img_name,
                adv_img_order: delImage.adv_img_order,
                object_id: delImage.object_id,
                usr_token: delImage.usr_token,
            }),
        };
        const response = await fetch(
            environment.API_URL + '/deleteAdvertImage',
            requestOptions
        );
        if (response.status !== 200) {
            return 'error';
        }
        const result = await response.json();
        return result;
    }
}
