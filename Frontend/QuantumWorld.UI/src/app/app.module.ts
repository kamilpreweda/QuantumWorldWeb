import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderResourcesComponent } from './components/header-resources/header-resources.component';
import { BuildingsComponent } from './components/buildings/buildings.component';
import { MenuComponent } from './components/menu/menu.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { OverviewComponent } from './components/overview/overview.component';
import { ResearchComponent } from './components/research/research.component';
import { ShipyardComponent } from './components/shipyard/shipyard.component';
import { MapComponent } from './components/map/map.component';
import { EnemyPopupComponent } from './components/enemy-popup/enemy-popup.component';
import { MessagesComponent } from './components/messages/messages.component';
import { LoginComponent } from './components/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserInterceptor } from './services/user.interceptor';
import { CountdownModule } from 'ngx-countdown';

const appRoute: Routes = [
  { path: '', component: LoginComponent },
  { path: 'Overview', component: OverviewComponent },
  { path: 'Buildings', component: BuildingsComponent },
  { path: 'Research', component: ResearchComponent },
  { path: 'Shipyard', component: ShipyardComponent },
  { path: 'Map', component: MapComponent },
  { path: 'Messages', component: MessagesComponent },
  { path: 'Login', component: LoginComponent }
]
@NgModule({
  declarations: [
    AppComponent,
    HeaderResourcesComponent,
    BuildingsComponent,
    MenuComponent,
    OverviewComponent,
    ResearchComponent,
    ShipyardComponent,
    MapComponent,
    EnemyPopupComponent,
    MessagesComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(appRoute),
    FormsModule,
    ReactiveFormsModule,
    CountdownModule,
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: UserInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
