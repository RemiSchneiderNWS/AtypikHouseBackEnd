import { Component, OnInit } from '@angular/core';
import { StatusUser } from './_services/User/statusUser';
import { UserService } from './_services/User/user.service';
import { Router, NavigationEnd } from '@angular/router';
import { initializeApp } from 'firebase/app';
import { uploadString } from 'firebase/storage';
declare let gtag: Function;

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
    title = 'atypikhouse';
    count = 0;
    constructor(
        private auth: UserService,
        private statusUser: StatusUser,
        public router: Router
    ) {
        const firebaseConfig = {
            apiKey: 'AIzaSyBZ4uQ6nGktu2mHlynkun25SR-doC7CG0A',
            authDomain: 'atypikhouse-6b7e9.firebaseapp.com',
            projectId: 'atypikhouse-6b7e9',
            storageBucket: 'atypikhouse-6b7e9.appspot.com',
            messagingSenderId: '565208006595',
            appId: '1:565208006595:web:8f9f36edbd1ba4f26ac776',
        };
        // Initialize Firebase
        const app = initializeApp(firebaseConfig);
        this.router.events.subscribe((event) => {
            if (event instanceof NavigationEnd) {
                gtag('config', 'G-E4Z4HHQCSC', {
                    page_path: event.urlAfterRedirects,
                });
            }
        });
    }

    ngOnInit(): void {
        this.statusUser.isAuth = false;
        if (
            localStorage.getItem('refresh_token') !== undefined ||
            localStorage.getItem('refresh_token').length > 0
        ) {
            this.auth.refresh().then(async (response) => {

                console.log('response: ', response);
                if (response.status === 401) {
                    this.auth.disconnectUser();
                } else {
                    let token = await response.text();
                    console.log('token set');
                    localStorage.setItem('access_token', token);
                    localStorage.setItem(
                        'refresh_token',
                        token
                    );
                    this.statusUser.isAuth = true;
                    this.statusUser.acces_token = token;
                    this.count++;

                    this.auth.isAdmin().then(async(response) => {
                         if (response) {
                            this.statusUser.isAdmin = true;
                        }
                    })
                   
                } 
            });
        }
    }
}
