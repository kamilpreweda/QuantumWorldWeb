import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
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
import { LogoutComponent } from './components/logout/logout.component';
import { EnemyPopupComponent } from './components/enemy-popup/enemy-popup.component';

const appRoute: Routes = [
  { path: '', component: OverviewComponent },
  { path: 'Overview', component: OverviewComponent },
  { path: 'Buildings', component: BuildingsComponent },
  { path: 'Research', component: ResearchComponent },
  { path: 'Shipyard', component: ShipyardComponent },
  { path: 'Map', component: MapComponent },
  { path: 'Logout', component: LogoutComponent }
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
    LogoutComponent,
    EnemyPopupComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(appRoute)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
