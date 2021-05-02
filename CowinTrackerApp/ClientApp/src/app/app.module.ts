import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { CowinTrackerComponent } from './pages/cowin-tracker/cowin-tracker.component';
import { AppRouterModule } from './app.router.module';
import { NgbDatepickerModule, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { SessionsInfoComponent } from './components/sessions-info/sessions-info.component';

@NgModule({
  entryComponents: [
    SessionsInfoComponent
  ],
  declarations: [
    AppComponent,
    NavMenuComponent,
    CowinTrackerComponent,
    SessionsInfoComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AppRouterModule,
    NgbDatepickerModule,
    NgbModalModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
