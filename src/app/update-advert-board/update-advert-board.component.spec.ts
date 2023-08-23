import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateAdvertBoardComponent } from './update-advert-board.component';

describe('UpdateAdvertBoardComponent', () => {
    let component: UpdateAdvertBoardComponent;
    let fixture: ComponentFixture<UpdateAdvertBoardComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [UpdateAdvertBoardComponent],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(UpdateAdvertBoardComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
