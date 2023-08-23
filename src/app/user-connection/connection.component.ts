import { Component, OnInit } from '@angular/core';
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { LogUSer } from '../models/Users/LogUser';
import { NavbarComponent } from '../navbar/navbar.component';
import { StatusUser } from '../_services/User/statusUser';

import { UserService } from '../_services/User/user.service';

@Component({
    selector: 'app-connection',
    templateUrl: './connection.component.html',
    styleUrls: ['./connection.component.scss'],
})
export class connectionComponent implements OnInit {
    hide = true;
    isAuth: Boolean;
    registerForm: FormGroup;
    errorMessage: string;
    errorWhenLogin = false;
    constructor(
        private fB: FormBuilder,
        public auth: UserService,
        private rt: Router,
        private statusUser: StatusUser,
        public nv: NavbarComponent
    ) {}

    ngOnInit(): void {
        this.initForm();
    }

    initForm() {
        this.registerForm = this.fB.group({
            email: ['', [Validators.required, Validators.email]],
            password: [
                '',
                [Validators.required, Validators.pattern(/[0-9a-zA-Z]{4,}/)],
            ],
        });
    }

    onSubmit(registerForm) {
        const user: LogUSer = {
            mail: registerForm.email,
            password: registerForm.password,
        };
        this.auth.connectUser(user).then(async (data) => {

            if(data.status === 400 ){
                this.errorWhenLogin = true;
            }else {
                let userLog = await data.json();
                localStorage.setItem('usr_mail', userLog.mail);
                localStorage.setItem('usr_phone', userLog.phone);
                localStorage.setItem('usr_id', userLog.id);
                localStorage.setItem('usr_firstName', userLog.firstName);
                localStorage.setItem('usr_lastName', userLog.lastName);
                localStorage.setItem('access_token', userLog.accessToken);
                localStorage.setItem('refresh_token', userLog.accessToken);
                this.isAuth = true;
                this.nv.firstName = userLog.firstName;
                this.nv.lastName = userLog.lastName;
                this.statusUser.isAuth = true;
                this.statusUser.firstName = userLog.firstName;
                this.statusUser.lastName = userLog.lastName;

                if (Number(userLog.role) === 1) {
                    this.statusUser.isAdmin = true;
                }

                this.rt.navigate(['/']);
            } /* else if (userLog.error.length > 0) {
                this.errorWhenLogin = true;
            } */
        });
    }

    disconnect() {
        this.auth.disconnectUser();
    }
}
