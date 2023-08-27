import { Injectable } from '@angular/core';
import { Reserve } from 'src/app/models/Reserve/Reserve';
import { setReserve } from 'src/app/models/Reserve/setReserve';
@Injectable()
export class ReserveCreated {
    reserve: setReserve;
}
