
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';                                     //Form Moduel
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';                          //import HttpClientModule

import { TranslateModule, TranslateLoader } from '@ngx-translate/core';           //Multi language
import { NgxDatatableModule } from '@swimlane/ngx-datatable';                     //DataTable
import { OAuthModule } from 'angular-oauth2-oidc';                                //Oauth2 
import { ToastaModule } from 'ngx-toasta';
import { NgSelectModule } from '@ng-select/ng-select';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { ChartsModule } from 'ng2-charts';                                        //Chart

import { AppRoutingModule } from './app-routing.module';                          //Routing 
import { AppErrorHandler } from './app-error.handler';
import { AppTitleService } from './services/app-title.service';
import { AppTranslationService, TranslateLanguageLoader } from './services/app-translation.service';
import { ConfigurationService } from './services/configuration.service';          //Configuration service
import { AlertService } from './services/alert.service';                          //Alert service
import { ThemeManager } from './services/theme-manager';                          //ThemeManager
import { LocalStoreManager } from './services/local-store-manager.service';       //local storemanager  service
import { OidcHelperService } from './services/oidc-helper.service';               //OidcHelper service
import { NotificationService } from './services/notification.service';            //NotificatinEndpoint service
import { NotificationEndpoint } from './services/notification-endpoint.service';  //Notificatin Endpoint service
import { AccountService } from './services/account.service';                      //Account service
import { AccountEndpoint } from './services/account-endpoint.service';            //Account Endpoint service

import { EqualValidator } from './directives/equal-validator.directive';
import { LastElementDirective } from './directives/last-element.directive';
import { AutofocusDirective } from './directives/autofocus.directive';
import { BootstrapTabDirective } from './directives/bootstrap-tab.directive';
import { BootstrapToggleDirective } from './directives/bootstrap-toggle.directive'; 
import { GroupByPipe } from './pipes/group-by.pipe';

import { AppComponent } from './components/app.component';                        // import app component
import { LoginComponent } from './components/login/login.component';              // import component login 
import { AuthCallbackComponent } from './components/auth-callback/auth-callback.component';  //Callback
import { RegisterComponent } from './components/account/register/register.component';        //Register
import { ConfirmEmailComponent } from './components/account/confirm-email/confirm-email.component';          //ConfirmEmail
import { RecoverPasswordComponent } from './components/account/recover-password/recover-password.component'; //ResetPassword
import { ResetPasswordComponent } from './components/account/reset-password/reset-password.component';
import { HomeComponent } from './components/home/home.component';                 //Home
import { CustomersComponent } from './components/customers/customers.component';  //Customer
import { ProductsComponent } from './components/products/products.component';     //Product
import { OrdersComponent } from './components/orders/orders.component';           //Orders
import { SettingsComponent } from './components/settings/settings.component';     //Settings
import { AboutComponent } from './components/about/about.component';              //About 
import { NotFoundComponent } from './components/not-found/not-found.component';

import { BannerDemoComponent } from './components/controls/banner-demo.component';            //BannerDemo component
import { TodoDemoComponent } from './components/controls/todo-demo.component';                //Todo Demo compponent
import { StatisticsDemoComponent } from './components/controls/statistics-demo.component';    //StatisticsDemo
import { NotificationsViewerComponent } from './components/controls/notifications-viewer.component';  //NotificationsView
import { SearchBoxComponent } from './components/controls/search-box.component';              //SearchBoxm component
import { UserInfoComponent } from './components/controls/user-info.component';                //UserInfo component
import { UserPreferencesComponent } from './components/controls/user-preferences.component';  //UserPreference
import { UsersManagementComponent } from './components/controls/users-management.component';  //UserManagement
import { RolesManagementComponent } from './components/controls/roles-management.component';  //RolesManageMenet
import { RoleEditorComponent } from './components/controls/role-editor.component';            //RolesEdit
import { PaymentsComponent } from './components/payments/payments.component';                 //Payments
import { PricelistComponent } from './components/pricelist/pricelist.component';              //Pricelist
import { PatientsComponent } from './components/patients/patients.component';                 //Payments


@NgModule({
  //imports Module
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,

    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useClass: TranslateLanguageLoader
      }
    }),
    NgxDatatableModule,
    OAuthModule.forRoot(),
    ToastaModule.forRoot(),
    NgSelectModule,
    TooltipModule.forRoot(),
    PopoverModule.forRoot(),
    BsDropdownModule.forRoot(),
    CarouselModule.forRoot(),
    ModalModule.forRoot(),
    ChartsModule
  ],

  //declarations Modules
  declarations: [
    AppComponent,
    LoginComponent,
    AuthCallbackComponent,
    RegisterComponent,
    ConfirmEmailComponent,
    RecoverPasswordComponent,
    ResetPasswordComponent,
    HomeComponent,
    CustomersComponent,
    ProductsComponent,
    OrdersComponent,
    SettingsComponent,
    UsersManagementComponent,
    UserInfoComponent,
    UserPreferencesComponent,
    RolesManagementComponent,
    RoleEditorComponent,
    AboutComponent,
    NotFoundComponent,
    NotificationsViewerComponent,
    SearchBoxComponent,
    StatisticsDemoComponent,
    TodoDemoComponent,
    BannerDemoComponent,
    EqualValidator,
    LastElementDirective,
    AutofocusDirective,
    BootstrapTabDirective,
    BootstrapToggleDirective,
    GroupByPipe,
    PaymentsComponent,
    PricelistComponent,
    PatientsComponent
  ],

  //Providers
  providers: [
    { provide: ErrorHandler, useClass: AppErrorHandler },
    AlertService,
    ThemeManager,
    ConfigurationService,
    AppTitleService,
    AppTranslationService,
    NotificationService,
    NotificationEndpoint,
    AccountService,
    AccountEndpoint,
    LocalStoreManager,
    OidcHelperService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
