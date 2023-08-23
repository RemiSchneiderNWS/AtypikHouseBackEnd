import { Advert } from "../Adverts/Advert";
import { User } from "../Users/User";

export interface ReserveReceive {
    id: number;
    user: User;
    advert: Advert;
    res_adv_name: string;
    dateStart: Date;
    dateEnd: Date;
    res_date_start_formated: string;
    res_date_end_formated: string;
    tenants: number;
    CreatedAt:Date;
    payment: boolean;
    PaymentTimer: string;
    AdvTenants: number;
    DelTenant: boolean;
    DelOwner: boolean;
}
