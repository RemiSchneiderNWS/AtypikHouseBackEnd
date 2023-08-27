import { User } from "../Users/User";

export interface Advert {
    id: number;
    name: string;
    type: string;
    tenants: string;
    user: User;
    status: boolean;
    criLimit: number;
    created_at: Date;
    price: number;
    adress: string;
    city: string;
    postal: string;
    up: boolean;
    describe: string;
}
