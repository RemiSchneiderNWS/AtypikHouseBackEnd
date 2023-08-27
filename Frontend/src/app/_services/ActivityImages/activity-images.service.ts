import { Injectable } from '@angular/core';
import { delImage } from 'src/app/models/Images/delImage';
import { NewActivityImage } from 'src/app/models/Images/NewActivityImage';
import { environment } from 'src/environments/environment';
@Injectable({
    providedIn: 'root',
})
export class ActivityImagesService {
    constructor() {}

    async getImagesByActivityId(act_id) {
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
            environment.API_URL + '/ActivityImage/'+ act_id,
            requestOptions
        );

    
        return response;
    }

    async createImagesForActivity(image: NewActivityImage) {
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
            environment.API_URL + '/createActivityImage',
            requestOptions
        );
        const data = await response.json();
        return data;
    }

    async getLastActivityId() {
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
            environment.API_URL + '/getLastActivityId',
            requestOptions
        );
        const data = await response.json();
        return data;
    }

    async deleteImage(delImage: delImage) {
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
                act_img_id: delImage.adv_img_id,
                act_img_name: delImage.adv_img_name,
                act_img_order: delImage.adv_img_order,
                object_id: delImage.object_id,
                usr_token: delImage.usr_token,
            }),
        };
        const response = await fetch(
            environment.API_URL + '/deleteActivityImage',
            requestOptions
        );
        if (response.status !== 200) {
            return 'error';
        }
        const result = await response.json();
        return result;
    }
}
