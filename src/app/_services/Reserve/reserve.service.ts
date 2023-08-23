import { Injectable } from '@angular/core';
import { CancelReserve } from 'src/app/models/Reserve/CancelReserve';
import { Reserve } from 'src/app/models/Reserve/Reserve';
import { setReserve } from 'src/app/models/Reserve/setReserve';
import { ValidReserve } from 'src/app/models/Reserve/validReserve';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})
export class ReserveService {
    constructor() {}

    async getDatebyAdvRes(advId: number) {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Accept: 'application/json',
                'Access-Control-Allow-Origin': '*',
                Authorization: 'Bearer ' + localStorage.getItem('access_token'),
            },
            
        };

        const response = await fetch(
            environment.API_URL + '/Reservation/getDatebyAdvRes/' + advId,
            requestOptions
        );
       // const dates = await response.json();
        return response;
    }

    async createReserve(reserve: setReserve) {
        const requestOptions = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('access_token'),
                'Content-Type': 'application/json',
                Accept: 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
            body: JSON.stringify(reserve),
        };

        const response = await fetch(
            environment.API_URL + '/Reservation',
            requestOptions
        );

        const data = await response.json();
        return data;
    }

    async getUserReserve() {
        

        const requestOptions = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('access_token'),
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
        };

        const response = await fetch(
            environment.API_URL + '/Reservation/getUserReserve',
            requestOptions
        );

        return response;
    }
    async getReservebyAdvert(advId: number) {
        const requestOptions = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('access_token'),
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
  
        };
        const response = await fetch(
            environment.API_URL + 'Reservation/' + advId,
            requestOptions
        );

        return response;
    }
    
    async userCanComment(advId : number ){
        const requestOptions = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('access_token'),
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
  
        };
        const response = await fetch(
            environment.API_URL + '/Reservation/canComment/' + advId,
            requestOptions
        );

        return response.json();
    }

    async cancelReserve(cancelReserve: CancelReserve) {
        const requestOptions = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('access_token'),
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
            body: JSON.stringify({
                res_usr_id: cancelReserve.res_usr_id,
                res_adv_id: cancelReserve.res_adv_id,
                res_date_start: cancelReserve.res_date_start,
                res_date_end: cancelReserve.res_date_end,
            }),
        };
        const response = await fetch(
            environment.API_URL + '/cancelReserve',
            requestOptions
        );
        const data = await response.json();
        return data;
    }
    async deleteReserve(cancelReserve: CancelReserve) {
        const requestOptions = {
            method: 'DELETE',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('access_token'),
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
            body: JSON.stringify({
                res_id: cancelReserve.res_id,
                res_adv_id: cancelReserve.res_adv_id,
                res_usr_id: cancelReserve.res_usr_id,
            }),
        };
        const response = await fetch(
            environment.API_URL + '/deleteReserve',
            requestOptions
        );
        const data = await response.json();
        return data;
    }

    async validReserve(validReserve: ValidReserve) {
        const requestOptions = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('access_token'),
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
            body: JSON.stringify(validReserve),
        };
        const response = await fetch(
            environment.API_URL + '/Reservation/validReserve',
            requestOptions
        );
        
    }
}
