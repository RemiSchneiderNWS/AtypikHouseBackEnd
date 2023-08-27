import { Injectable } from '@angular/core';
@Injectable()
export class StatusUser {
    isAuth: boolean = false;
    isAdmin: boolean = false;
    firstName: string;
    lastName: string;
    id: number;
    acces_token: string;
}
