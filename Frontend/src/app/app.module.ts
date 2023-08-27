//=============================== ANGULAR ========================================//

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

//============================== ANGULAR - MATERIAL ==============================//

import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDialogModule } from '@angular/material/dialog';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSliderModule } from '@angular/material/slider';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatTreeModule } from '@angular/material/tree';

//==============================    MISC   =========================================//
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavbarComponent } from './navbar/navbar.component';
import { SidenavListComponent } from './navbar/sidenav-list/sidenav-list.component';

import { RouterModule, Routes } from '@angular/router';

import { ReactiveFormsModule } from '@angular/forms';

import { HomeComponent } from './home/home.component';
import { NgStyle } from '@angular/common';
import { registerComponent } from './user-register/register.component';
import { connectionComponent } from './user-connection/connection.component';
import { HttpClientModule } from '@angular/common/http';
import { SearchComponent } from './search/search.component';
import { DetailsAnnouncementComponent } from './details-annoucement/details-announcement.component';
import { NewAdvertComponent } from './new-Advert/new-advert.component';
import { StatusUser } from './_services/User/statusUser';
import { ReserveComponent } from './reserve/reserve.component';

import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule, MAT_DATE_LOCALE } from '@angular/material/core';
import { PaiementComponent } from './paiement/paiement.component';
import { ReserveService } from './_services/Reserve/reserve.service';
import { ReserveCreated } from './_services/Reserve/reserveCreated';
import { NgxPayPalModule } from 'ngx-paypal';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { UserProfilComponent } from './user-profil/user-profil.component';
import { UserAdvertsComponent } from './user-adverts/user-adverts.component';
import { UserReservesComponent } from './user-reserves/user-reserves.component';
import { ReserveByAdvertComponent } from './reserve-by-advert/reserve-by-advert.component';
import { UpdateAdvertComponent } from './update-advert/update-advert.component';

//==============================    GOOGLE ANALYTICS   =========================================//
import {
    NgxGoogleAnalyticsModule,
    NgxGoogleAnalyticsRouterModule,
} from 'ngx-google-analytics';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { MentioncgvComponent } from './mentioncgv/mentioncgv.component';
import { MentionLegaleComponent } from './mention-legale/mention-legale.component';
import { CgvComponent } from './cgv/cgv.component';
import { CguComponent } from './cgu/cgu.component';
import { AdvertPlanningComponent } from './advert-planning/advert-planning.component';
import { UpdateAdvertBoardComponent } from './update-advert-board/update-advert-board.component';
import { NewActivityComponent } from './new-activity/new-activity.component';
import { ActivityDetailComponent } from './activity-detail/activity-detail.component';

@NgModule({
    declarations: [
        AppComponent,
        NavbarComponent,
        SidenavListComponent,
        connectionComponent,
        registerComponent,
        HomeComponent,
        SearchComponent,
        DetailsAnnouncementComponent,
        NewAdvertComponent,
        ReserveComponent,
        PaiementComponent,
        MentioncgvComponent,
        UserProfilComponent,
        UserAdvertsComponent,
        UserReservesComponent,
        ReserveByAdvertComponent,
        UpdateAdvertComponent,
        AdminPanelComponent,
        MentionLegaleComponent,
        CgvComponent,
        CguComponent,
        AdvertPlanningComponent,
        UpdateAdvertBoardComponent,
        NewActivityComponent,
        ActivityDetailComponent,
    ],
    imports: [
        NgxGoogleAnalyticsModule.forRoot('G-E4Z4HHQCSC'),
        NgxGoogleAnalyticsRouterModule,
        FontAwesomeModule,
        NgxPayPalModule,
        MatDatepickerModule,
        MatNativeDateModule,
        ReactiveFormsModule,
        BrowserModule.withServerTransition({ appId: 'serverApp' }),
        AppRoutingModule,
        BrowserAnimationsModule,
        MatButtonModule,
        MatButtonToggleModule,
        MatCardModule,
        MatCheckboxModule,
        MatDialogModule,
        MatExpansionModule,
        MatFormFieldModule,
        MatGridListModule,
        MatIconModule,
        MatInputModule,
        MatListModule,
        MatMenuModule,
        MatPaginatorModule,
        MatProgressBarModule,
        MatProgressSpinnerModule,
        MatSelectModule,
        MatSidenavModule,
        MatSliderModule,
        MatSlideToggleModule,
        MatSnackBarModule,
        MatSortModule,
        MatStepperModule,
        MatTableModule,
        MatToolbarModule,
        MatTabsModule,
        MatTreeModule,
        MatTooltipModule,
        HttpClientModule,
    ],
    providers: [
        StatusUser,
        ReserveCreated,
        NavbarComponent,
        { provide: MAT_DATE_LOCALE, useValue: 'fr-FR' },
    ],
    bootstrap: [AppComponent],
})
export class AppModule {}
