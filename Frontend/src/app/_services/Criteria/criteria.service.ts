import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Criteria } from 'src/app/models/Criteria/Criteria';
import { CriteriaSet } from 'src/app/models/Criteria/CriteriaSet';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})
export class CriteriaService {
    constructor(public rt: Router) {}

    async getCriteria() {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
        };

        const response = await fetch(
            environment.API_URL + '/Criterias',
            requestOptions
        );
        const data = await response.json();
        return data;
    }

    async getAdvertByCriteria(ids: number[]) {
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
            body: JSON.stringify(    
                ids
        ),
        };

        const response = await fetch(
        environment.API_URL + '/Advert/getAdvertByCriteria',
            requestOptions
        );
        return response;
    }
    async linkCriteriAdvert(linkCriteri) {
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
            body: JSON.stringify({
                ria_adv_id: linkCriteri.adv_id,
                ria_cri_id: linkCriteri.cri_ids,
                adv_cri_limit: linkCriteri.adv_cri_limit,
            }),
        };

        const response = await fetch(
            environment.API_URL + '/linkCriteriAdvert',
            requestOptions
        );
        const data = await response.json();
        return data;
    }

    async createCriteria(criteria: CriteriaSet) {
        const requestOptions = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('access_token'),
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
            body: JSON.stringify({
                cri_name: criteria.cri_name,
            }),
        };

        const response = await fetch(
            environment.API_URL + '/Criterias/createCriteria',
            requestOptions
        );
        const data = await response.json();
        return data;
    }

    async deleteCriteria(criteriaToDelete: Criteria) {
        const requestOptions = {
            method: 'DELETE',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('access_token'),
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
            body: JSON.stringify(criteriaToDelete),
        };

        const response = await fetch(
            environment.API_URL + '/deleteCriteria',
            requestOptions
        );
        const data = await response.json();
        return data;
    }
}
