import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NewUser } from '../models/Users/NewUser';
import { SnackBarService } from '../_services/SnackBar/snack-bar.service';
import { UserService } from '../_services/User/user.service';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss'],
})
export class registerComponent implements OnInit {
    hide = true;
    isTouchedMail: number = 0;
    isTouchedPassword: number = 0;
    registerForm: FormGroup;
    errorMessage: string;
    signUpSuccess: boolean = false;
    constructor(
        private fB: FormBuilder,
        private snackbar: SnackBarService,
        private userService: UserService,
        private rt: Router
    ) {}

    private _onError(error) {
        console.error(error);
    }
    ngOnInit(): void {
        this.initForm();
    }

    initForm() {
        this.registerForm = this.fB.group({
            mail: ['', [Validators.required, Validators.email]],
            password: [
                '',
                [
                    Validators.required,
                    Validators.pattern(
                        /^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{4,}$/
                    ),
                ],
            ],
            phone: [
                '',
                [Validators.required, Validators.pattern(/[0-9].{9,12}/)],
            ],
            firstName: ['', [Validators.required]],
            lastName: ['', [Validators.required]],
        });
    }

    handleOnChangeMail() {
        this.isTouchedMail++;
    }
    handleOnChangePassword() {
        this.isTouchedPassword++;
    }
    getErrorMessage() {
        if (this.registerForm.hasError('required')) {
            return 'You must enter a value';
        }

        return this.registerForm.hasError('email') ? 'Not a valid email' : '';
    }

    get mail() {
        return this.registerForm.get('mail');
    }
    get password() {
        return this.registerForm.get('password');
    }

    onSubmit(registerUserFormData) {
        const user: NewUser = {
            mail: registerUserFormData.mail,
            password: registerUserFormData.password,
            phone: registerUserFormData.phone,
            firstName: registerUserFormData.firstName,
            lastName: registerUserFormData.lastName,
        };

        this.snackbar.openSnackBar('Sauvegarde en cours ...', 'ok', 1500);

        this.userService.register(user).then((result) => {
            
            /* if (result.errors !== undefined && result.errors.length > 0) {
                this.snackbar.openSnackBar(result.error, 'ok', 1500);
            } else {
                this.snackbar.openSnackBar('inscription réussi', 'ok', 1500);
                this.rt.navigate(['/connection']);
            } */
            if (result === true){
                this.snackbar.openSnackBar('inscription réussi', 'ok', 1500);
                this.rt.navigate(['/connection']);
            }else{
                this.snackbar.openSnackBar(result, 'ok', 1500);
            }
        });
    }
}
