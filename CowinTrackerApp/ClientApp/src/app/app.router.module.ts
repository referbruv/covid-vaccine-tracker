import { NgModule } from "@angular/core";
import { RouterModule } from '@angular/router';
import { CowinTrackerComponent } from "./pages/cowin-tracker/cowin-tracker.component";

let routes = [
    { path: '', component: CowinTrackerComponent, pathMatch: 'full' },
    { path: 'tracker', component: CowinTrackerComponent }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],
    exports: [
        RouterModule
    ]
})
export class AppRouterModule {

}