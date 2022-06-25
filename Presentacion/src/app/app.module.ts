import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { MonedasComponent } from './monedas/monedas.component';
import { MonedasFormComponent } from './monedas/monedas-form/monedas-form.component';
import { HomeComponent } from './home/home.component';
import { TipocambioComponent } from './tipocambio/tipocambio.component';
import { TipocambioFormComponent } from './tipocambio/tipocambio-form/tipocambio-form.component';
import { CotizarFormComponent } from './tipocambio/cotizar-form/cotizar-form.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LogInterceptorService } from './services/log-interceptor.service';
import { MonedasService } from './monedas/monedas.service';
import { TipocambioService } from './tipocambio/tipocambio.service';
import { AuthGuardService } from './services/auth-guard.service';
import { AccountService } from './account/account.service';
import { AuthInterceptorService } from './services/auth-interceptor.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    MonedasComponent,
    MonedasFormComponent,
    HomeComponent,
    TipocambioComponent,
    TipocambioFormComponent,
    CotizarFormComponent,
    NavMenuComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule
  ],
  providers: [
    {
      provide: LOCALE_ID, useValue: "en-US"
    },
    {
        provide: HTTP_INTERCEPTORS,
        useClass: LogInterceptorService,
        multi:true
    },
    MonedasService,
    TipocambioService,
    AuthGuardService,
    AccountService,
    {
        provide: HTTP_INTERCEPTORS,
        useClass: AuthInterceptorService,
        multi: true
    }
],
bootstrap: [AppComponent]
})
export class AppModule {

}
