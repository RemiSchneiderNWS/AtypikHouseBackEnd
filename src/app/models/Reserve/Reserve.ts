import { Advert } from "../Adverts/Advert";
import { User } from "../Users/User";

export interface Reserve {
    user: User;
    advert: Advert;
    dateStart: Date;
    dateEnd: Date;
    price: number;
    tenants: number;
    CreatedAt:Date;
    payment: boolean;
    PaymentTimer: string;
    AdvTenants: number;
    DelTenant: boolean;
    DelOwner: boolean;
}
