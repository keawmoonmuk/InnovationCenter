// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { Component, OnInit, OnDestroy } from '@angular/core';

import { AlertService, MessageSeverity } from '../../../services/alert.service';  //import alertService
import { AppTranslationService } from '../../../services/app-translation.service'; 
import { AuthService } from '../../../services/auth.service';  // import authServerice
import { AccountService } from '../../../services/account.service'; // import AccountService
import { Utilities } from '../../../services/utilities';  
import { User } from '../../../models/user.model';  //import User
import { UserEdit } from '../../../models/user-edit.model'; // import UserEdit

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})

export class RegisterComponent implements OnInit, OnDestroy {

  userEdit = new UserEdit();
  isLoading = false;
  formResetToggle = true;
  loginStatusSubscription: any;

  gT = (key: string | Array<string>, interpolateParams?: object) => this.translationService.getTranslation(key, interpolateParams);

  constructor(private alertService: AlertService, private authService: AuthService, private translationService: AppTranslationService, private accountService: AccountService) {

  }


  ngOnInit() {

    if (this.getShouldRedirect()) {
      this.authService.redirectLoginUser();
    } else {
      this.loginStatusSubscription = this.authService.getLoginStatusEvent().subscribe(() => {
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
    return this.authService.isLoggedIn && !this.authService.isSessionExpired;
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


  register() {
    this.isLoading = true;
    this.alertService.startLoadingMessage(this.gT('register.alerts.RegisteringUser'));

    this.accountService.newUser(this.userEdit, true).subscribe(user => this.saveSuccessHelper(user), error => this.saveFailedHelper(error));
  }


  private saveSuccessHelper(user: User) {
    this.alertService.stopLoadingMessage();
    this.alertService.showMessage(this.gT('register.alerts.Success'), this.gT('register.alerts.UserCreatedSuccessfully', { username: this.userEdit.userName }), MessageSeverity.success);

    this.login();
  }


  private saveFailedHelper(error: any) {
    this.isLoading = false;
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage(this.gT('register.alerts.SaveError'), this.gT('register.alerts.BelowRegistrationErrorsOccured'), MessageSeverity.error, error);
    this.alertService.showStickyMessage(error, null, MessageSeverity.error);
  }


  login() {
    this.isLoading = true;
    this.alertService.startLoadingMessage('', this.gT('register.alerts.AttemptingLogin'));

    this.authService.loginWithPassword(this.userEdit.userName, this.userEdit.newPassword, false)
      .subscribe(
        user => {
          setTimeout(() => {
            this.alertService.stopLoadingMessage();
            this.isLoading = false;
            this.reset();

            this.alertService.showStickyMessage(this.gT('register.alerts.Login'), this.gT('register.alerts.WelcomeUser', { username: user.userName }), MessageSeverity.success);
            this.alertService.showStickyMessage('', this.gT('register.alerts.YourAccountCreatedSuccessfully'), MessageSeverity.success);
          }, 500);
        },
        error => {

          this.alertService.stopLoadingMessage();

          if (Utilities.checkNoNetwork(error)) {
            this.alertService.showStickyMessage(Utilities.noNetworkMessageCaption, Utilities.noNetworkMessageDetail, MessageSeverity.error, error);
          } else {
            const errorMessage = Utilities.getHttpResponseMessage(error);

            if (errorMessage) {
              this.alertService.showStickyMessage(this.gT('register.alerts.UnableToLogin'), this.mapLoginErrorMessage(errorMessage), MessageSeverity.error, error);
            } else {
              this.alertService.showStickyMessage(this.gT('register.alerts.UnableToLogin'), this.gT('register.alerts.LoginErrorOccured', { error: Utilities.getResponseBody(error) }), MessageSeverity.error, error);
            }
          }

          setTimeout(() => {
            this.isLoading = false;
          }, 500);
        });
  }


  mapLoginErrorMessage(error: string) {
    if (error === 'invalid_username_or_password') {
      return this.gT('register.alerts.InvalidUsernameOrPassword');
    }

    if (error === 'invalid_grant') {
      return this.gT('register.alerts.AccountDisabled');
    }

    return error;
  }


  reset() {
    this.formResetToggle = false;

    setTimeout(() => {
      this.formResetToggle = true;
    });
  }
}
