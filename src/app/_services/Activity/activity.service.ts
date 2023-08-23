import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Activity } from 'src/app/models/Activity/Activity';
import { SetActivity } from 'src/app/models/Activity/setActivity';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})
export class ActivityService {
    constructor(public rt: Router) {}

    async getActivityById(id: number) {//ok
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
           
        };

        const response = await fetch(
            environment.API_URL + '/Activity/GetActivityById/' + id,
            requestOptions
        );
        
        return response;
    }
    async getActivityByAdvert(id: number) {//ok
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
           
        };

        const response = await fetch(
            environment.API_URL + '/Activity/getActivityByAdvert/' + id,
            requestOptions
        );
        
        return response;
    }


    async getOneActivityByID(id: number, advId: number) {
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
            body: JSON.stringify({
                act_id: id,
                act_adv_id: advId,
            }),
        };

        const response = await fetch(
            environment.API_URL + '/getOneActivityByID',
            requestOptions
        );
        const data = await response.json();
        return data;
    }

    async createActivity(activity: SetActivity) {
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
            body: JSON.stringify({
                act_name: activity.act_name,
                act_adv_id: activity.act_adv_id,
                act_usr_id: activity.act_usr_id,
                act_adress: activity.act_adress,
                act_city: activity.act_city,
                act_postal: activity.act_postal,
                act_describe: activity.act_describe,
                act_price: activity.act_price,
                act_type: activity.act_type,
            }),
        };

        console.log(requestOptions);
        const response = await fetch(
            environment.API_URL + '/createActivity',
            requestOptions
        );
        const result = await response.json();
        return result;
    }
}
