// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { Component } from '@angular/core';

import { AlertService, MessageSeverity } from '../../../services/alert.service';
import { AppTranslationService } from '../../../services/app-translation.service';
import { AccountService } from '../../../services/account.service';
import { Utilities } from '../../../services/utilities';

@Component({
  selector: 'app-recover-password',
  templateUrl: './recover-password.component.html',
  styleUrls: ['./recover-password.component.scss']
})

export class RecoverPasswordComponent {

  isLoading = false;
  isSuccess: boolean;
  formResetToggle = true;
  usernameOrEmail: string;

  gT = (key: string | Array<string>, interpolateParams?: object) => this.translationService.getTranslation(key, interpolateParams);

  constructor(private alertService: AlertService, private translationService: AppTranslationService, private accountService: AccountService) {

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


  recoverPassword() {
    this.isLoading = true;
    this.alertService.startLoadingMessage('', this.gT('recoverPassword.alerts.GeneratingPasswordEmail'));

    this.accountService.recoverPassword(this.usernameOrEmail)
      .subscribe(() => {
        this.alertService.stopLoadingMessage();
        this.isLoading = false;
        this.isSuccess = true;
        this.alertService.showMessage(this.gT('recoverPassword.alerts.RecoverPassword'), this.gT('recoverPassword.alerts.PasswordEmailSent'), MessageSeverity.success);
      },
        error => {
          this.alertService.stopLoadingMessage();
          this.isLoading = false;
          this.isSuccess = false;

          const errorMessage =
            Utilities.findHttpResponseMessage(Utilities.noNetworkMessageCaption, error) ||
            Utilities.findHttpResponseMessage(Utilities.noNetworkMessageCaption, error) ||
            Utilities.findHttpResponseMessage('error_description', error) ||
            Utilities.findHttpResponseMessage('error', error);

          if (errorMessage) {
            this.alertService.showStickyMessage(this.gT('recoverPassword.alerts.PasswordRecoveryFailed'), errorMessage, MessageSeverity.error, error);
          } else {
            this.alertService.showStickyMessage(this.gT('recoverPassword.alerts.PasswordRecoveryFailed'), this.gT('recoverPassword.alerts.PasswordRecoveryErrorOccured', { error: Utilities.getResponseBody(error) }), MessageSeverity.error, error);
          }
        });
  }
}
