import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { HomeComponent } from './home/home.component';
import { MonedasFormComponent } from './monedas/monedas-form/monedas-form.component';
import { MonedasComponent } from './monedas/monedas.component';
import { AuthGuardService } from './services/auth-guard.service';
import { CotizarFormComponent } from './tipocambio/cotizar-form/cotizar-form.component';
import { TipocambioFormComponent } from './tipocambio/tipocambio-form/tipocambio-form.component';
import { TipocambioComponent } from './tipocambio/tipocambio.component';

const routes: Routes = [];

@NgModule({
  imports: [
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'monedas', component: MonedasComponent, canActivate: [AuthGuardService] },
      { path: 'monedas/agregar', component: MonedasFormComponent, canActivate: [AuthGuardService] },
      { path: 'monedas/editar/:id', component: MonedasFormComponent, canActivate: [AuthGuardService] },
      { path: 'tipocambio', component: TipocambioComponent, canActivate: [AuthGuardService] },
      { path: 'tipocambio/agregar', component: TipocambioFormComponent, canActivate: [AuthGuardService] },
      { path: 'tipocambio/editar/:id', component: TipocambioFormComponent, canActivate: [AuthGuardService] },
      { path: 'tipocambio/cotizar', component: CotizarFormComponent, canActivate: [AuthGuardService] },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
    ])
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
