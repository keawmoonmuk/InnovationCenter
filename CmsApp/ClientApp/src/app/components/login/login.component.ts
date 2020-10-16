
import { Component, OnInit, OnDestroy, Input } from '@angular/core';

import { AlertService, MessageSeverity, DialogType } from '../../services/alert.service';
import { AppTranslationService } from '../../services/app-translation.service';  //import service thanslateion
import { AuthService } from '../../services/auth.service';        //import AuthService
import { ConfigurationService } from '../../services/configuration.service';  //import configurationService
import { Utilities } from '../../services/utilities';
import { UserLogin } from '../../models/user-login.model';    //import class model for user login

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit, OnDestroy {


  userLogin = new UserLogin();        
  isLoading = false;
  isExternalLogin = false;
  formResetToggle = true;
  modalClosedCallback: () => void;
  loginStatusSubscription: any;

  @Input()
  isModal = false;

  gT = (key: string | Array<string>, interpolateParams?: object) => this.translationService.getTranslation(key, interpolateParams);

  constructor(private alertService: AlertService, private translationService: AppTranslationService, private authService: AuthService, private configurations: ConfigurationService) {

  }

  ngOnInit() {
   
    //when (checkbox rememberMe = true) = (authSevice.rememberMe)
    this.userLogin.rememberMe = this.authService.rememberMe;

    if (this.getShouldRedirect()) {
      this.authService.redirectLoginUser();
    } else {
      this.loginStatusSubscription = this.authService.getLoginStatusEvent().subscribe(isLoggedIn => {
        if (this.getShouldRedirect()) {
          this.authService.redirectLoginUser();
        }
      });
    }
  }


  ngOnDestroy() {
    if (this.loginStatusSubscription) {
      this.loginStatusSubscription.unsubscribe();
    }
  }


  getShouldRedirect() {
    return !this.isModal && this.authService.isLoggedIn && !this.authService.isSessionExpired;
  }


  showErrorAlert(caption: string, message: string) {
    if (caption) {
      caption = this.gT(caption);
    }

    if (message) {
      message = this.gT(message);
    }

    this.alertService.showMessage(caption, message, MessageSeverity.error);
  }

  closeModal() {
    if (this.modalClosedCallback) {
      this.modalClosedCallback();
    }
  }

  // ******chagelanguage**********
  changeLanguage(language: string) {
    this.configurations.globalLanguage = language;
    this.configurations.language = language;
  }

  //*****************login page***********************
  login() {
    this.isLoading = true;
    this.isExternalLogin = false;
   
    this.alertService.startLoadingMessage('', this.gT('login.alerts.AttemptingLogin'));

    this.authService.loginWithPassword(this.userLogin.userName, this.userLogin.password, this.userLogin.rememberMe)
      .subscribe(
        user => {
          setTimeout(() => {
            this.alertService.stopLoadingMessage();
            this.isLoading = false;
            this.reset();

            if (!this.isModal) {
              this.alertService.showMessage(this.gT('login.alerts.Login'), this.gT('login.alerts.Welcome', { username: user.userName }), MessageSeverity.success);
            } else {
              this.alertService.showMessage(this.gT('login.alerts.Login'), this.gT('login.alerts.UserSessionRestored', { username: user.userName }), MessageSeverity.success);
              setTimeout(() => {
                this.alertService.showStickyMessage(this.gT('login.alerts.SessionRestored'), this.gT('login.alerts.RetryLastOperation'), MessageSeverity.default);
              }, 500);

              this.closeModal();
            }
          }, 500);
        },

        //error to connect host
        error => {

          this.alertService.stopLoadingMessage();

          if (Utilities.checkNoNetwork(error)) {
            this.alertService.showStickyMessage(this.gT('app.NoNetwork'), this.gT('app.ServerCannotBeReached'), MessageSeverity.error, error);
            this.offerAlternateHost();
          } else {
            const errorMessage = Utilities.getHttpResponseMessage(error);

            if (errorMessage) {
              this.alertService.showStickyMessage(this.gT('login.alerts.UnableToLogin'), this.mapLoginErrorMessage(errorMessage), MessageSeverity.error, error);
            } else {
              this.alertService.showStickyMessage(this.gT('login.alerts.UnableToLogin'), this.gT('login.alerts.LoginErrorOccured', { error: Utilities.getResponseBody(error) }), MessageSeverity.error, error);
            }
          }

          setTimeout(() => {
            this.isLoading = false;
          }, 500);
        });
  }

  //*******login with google**********
  loginWithGoogle() {
    this.isLoading = true;
    this.isExternalLogin = true;
    this.alertService.startLoadingMessage('', this.gT('login.alerts.RedirectingToGoogle'));

    this.authService.initLoginWithGoogle(this.userLogin.rememberMe);
  }

  //*********login with facebook***********
  loginWithFacebook() {
    this.isLoading = true;
    this.isExternalLogin = true;
    this.alertService.startLoadingMessage('', this.gT('login.alerts.RedirectingToFacebook'));

    this.authService.initLoginWithFacebook(this.userLogin.rememberMe);
  }

  //**********login with twitter************
  loginWithTwitter() {
    this.isLoading = true;
    this.isExternalLogin = true;
    this.alertService.startLoadingMessage('', this.gT('login.alerts.RedirectingToTwitter'));

    this.authService.initLoginWithTwitter(this.userLogin.rememberMe);
  }


  offerAlternateHost() {
    if (Utilities.checkIsLocalHost(location.origin) && Utilities.checkIsLocalHost(this.configurations.baseUrl)) {
      this.alertService.showDialog(this.gT('login.alerts.DeveloperDemoApiNotice'), DialogType.prompt, (value: string) => {
        this.configurations.baseUrl = value;
        this.configurations.tokenUrl = value;
        this.alertService.showStickyMessage(this.gT('login.alerts.ApiChanged'), this.gT('login.alerts.ApiChangedTo', { API: value }), MessageSeverity.warn);
      },
        null,
        null,
        null,
        this.configurations.fallbackBaseUrl);
    }
  }

  //map login error message
  mapLoginErrorMessage(error: string) {
    if (error === 'invalid_username_or_password') {
      return this.gT('login.alerts.InvalidUsernameOrPassword');
    }

    if (error === 'invalid_grant') {
      return this.gT('login.alerts.AccountDisabled');
    }

    return error;
  }


  //reset 
  reset() {
    this.formResetToggle = false;

    setTimeout(() => {
      this.formResetToggle = true;
    });
  }
}
