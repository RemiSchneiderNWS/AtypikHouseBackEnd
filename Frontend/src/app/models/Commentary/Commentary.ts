import { Advert } from "../Adverts/Advert";
import { User } from "../Users/User";

export interface Commentary {
    id: number;
    text: string;
    advert : Advert;
    user: User;
}
