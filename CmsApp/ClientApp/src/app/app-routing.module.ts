// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { NgModule, Injectable } from '@angular/core';
import { Routes, RouterModule, DefaultUrlSerializer, UrlSerializer, UrlTree } from '@angular/router';

import { LoginComponent } from './components/login/login.component';
import { AuthCallbackComponent } from './components/auth-callback/auth-callback.component';
import { RegisterComponent } from './components/account/register/register.component';
import { ConfirmEmailComponent } from './components/account/confirm-email/confirm-email.component';
import { RecoverPasswordComponent } from './components/account/recover-password/recover-password.component';
import { ResetPasswordComponent } from './components/account/reset-password/reset-password.component';
import { HomeComponent } from './components/home/home.component';
import { CustomersComponent } from './components/customers/customers.component';
import { ProductsComponent } from './components/products/products.component';
import { OrdersComponent } from './components/orders/orders.component';
import { SettingsComponent } from './components/settings/settings.component';
import { AboutComponent } from './components/about/about.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { AuthService } from './services/auth.service';
import { AuthGuard } from './services/auth-guard.service';
import { Utilities } from './services/utilities';
import { PricelistComponent } from './components/pricelist/pricelist.component';
import { PaymentsComponent } from './components/payments/payments.component';
import { PatientsComponent } from './components/patients/patients.component';



@Injectable()
export class LowerCaseUrlSerializer extends DefaultUrlSerializer {
    parse(url: string): UrlTree {
        const possibleSeparators = /[?;#]/;
        const indexOfSeparator = url.search(possibleSeparators);
        let processedUrl: string;

        if (indexOfSeparator > -1) {
            const separator = url.charAt(indexOfSeparator);
            const urlParts = Utilities.splitInTwo(url, separator);
            urlParts.firstPart = urlParts.firstPart.toLowerCase();

            processedUrl = urlParts.firstPart + separator + urlParts.secondPart;
        } else {
            processedUrl = url.toLowerCase();
        }

        return super.parse(processedUrl);
    }
}

//routing
const routes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard], data: { title: 'Home' } },
    { path: 'login', component: LoginComponent, data: { title: 'Login' } },
    { path: 'google-login', component: AuthCallbackComponent, data: { title: 'Google Login' } },
    { path: 'facebook-login', component: AuthCallbackComponent, data: { title: 'Facebook Login' } },
    { path: 'twitter-login', component: AuthCallbackComponent, data: { title: 'Twitter Login' } },
    { path: 'register', component: RegisterComponent, data: { title: 'Register' } },
    { path: 'confirmemail', component: ConfirmEmailComponent, data: { title: 'Confirm Email' } },
    { path: 'recoverpassword', component: RecoverPasswordComponent, data: { title: 'Recover Password' } },
    { path: 'resetpassword', component: ResetPasswordComponent, data: { title: 'Reset Password' } },
    { path: 'customers', component: CustomersComponent, canActivate: [AuthGuard], data: { title: 'Customers' } },
    { path: 'products', component: ProductsComponent, canActivate: [AuthGuard], data: { title: 'Products' } },
    { path: 'orders', component: OrdersComponent, canActivate: [AuthGuard], data: { title: 'Orders' } },
    { path: 'settings', component: SettingsComponent, canActivate: [AuthGuard], data: { title: 'Settings' } },
  { path: 'about', component: AboutComponent, data: { title: 'About Us' } },
  { path: 'payment', component: PaymentsComponent, data: { title: 'Payments' } },
  { path: 'patient', component: PatientsComponent, data: {title:'Patients'} },
  { path: 'priceLists', component: PricelistComponent, data: {title: 'Price List'} },
    { path: 'home', redirectTo: '/', pathMatch: 'full' },
    { path: '**', component: NotFoundComponent, data: { title: 'Page Not Found' } }
];


@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    providers: [
        AuthService,
        AuthGuard,
        { provide: UrlSerializer, useClass: LowerCaseUrlSerializer }]
})
export class AppRoutingModule { }
