import { User } from "../Users/User";

export interface Activity {
    id: number;
    name: string;
    adv_id: number;
    adress: string;
    city: string;
    postal: string;
    describe: string;
    user: User;
    price: number;
    type: string;
}
