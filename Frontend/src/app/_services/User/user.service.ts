import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { LogUSer } from 'src/app/models/Users/LogUser';
import { NewUser } from 'src/app/models/Users/NewUser';
import { User } from 'src/app/models/Users/User';
import { environment } from 'src/environments/environment';
import { StatusUser } from './statusUser';

@Injectable({ providedIn: 'root' })
export class UserService {
    isAuth: boolean = true;

    constructor(
        public rt: Router,
        private http: HttpClient,
        private statusUser: StatusUser
    ) {}

    emitUser() {
        //this.app.user = this.user
    }

    getLocalUser() {
        // let user = this.app.user;
        // return user;
    }

    async register(user: NewUser) { //Ok
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                Accept: 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
            body: JSON.stringify(user),
        };

        const response = await fetch(
            environment.API_URL + '/User',
            requestOptions
        );

        const data = await response.json();
        return data;
    }

    async connectUser(user: LogUSer) {//OK
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },

            body: JSON.stringify(user),
        };

        const response = await fetch(
            environment.API_URL + '/User/login',
            requestOptions
        );
        const data = await response;
        return data;
    }

    async disconnectUser() {//Ok
    /*     const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },

            body: JSON.stringify({
                usr_refresh_token: localStorage.getItem('refresh_token'),
            }),
        };

        await fetch(environment.API_URL + '/User/logout', requestOptions); */
        localStorage.clear();
        this.statusUser.isAuth = false;
        this.statusUser.isAdmin = false;
        this.rt.navigate(['/']);
    }
    
    async refresh() {//Ok
        const token = localStorage.getItem("access_token");
        console.log(token)
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                Authorization: `Bearer ${token}`,
            },

            
        };
        const response = await fetch(
            environment.API_URL + '/User/refresh',
            requestOptions
        );
       
        const data = await response
       
        return data;
    }
    async isAdmin(){//Ok
        const token = localStorage.getItem("access_token");
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                Authorization: `Bearer ${token}`,
            },
 
        };
        const response = await fetch(
            environment.API_URL + '/User/isAdmin',
            requestOptions
        );
        const data = await response.json();
        return data
    }
}
