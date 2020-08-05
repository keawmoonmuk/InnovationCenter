// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { AlertService, MessageSeverity } from '../../../services/alert.service';
import { AppTranslationService } from '../../../services/app-translation.service';
import { AccountService } from '../../../services/account.service';
import { AuthService } from '../../../services/auth.service';
import { Utilities } from '../../../services/utilities';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss']
})

export class ConfirmEmailComponent implements OnInit {

  message: string;
  isLoading = false;
  isSuccess: boolean;

  gT = (key: string | Array<string>, interpolateParams?: object) => this.translationService.getTranslation(key, interpolateParams);

  constructor(
    private route: ActivatedRoute,
    private alertService: AlertService,
    private translationService: AppTranslationService,
    private accountService: AccountService,
    private authService: AuthService) {

  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const loweredParams: any = Utilities.GetObjectWithLoweredPropertyNames(params);
      const userId = loweredParams.userid;
      const code = loweredParams.code;

      if (!userId || !code) {
        this.authService.gotoHomePage();
      } else {
        this.confirmEmail(userId, code);
      }
    });
  }

  confirmEmail(userId: string, code: string) {
    this.isLoading = true;
    this.alertService.startLoadingMessage('', this.gT('confirmEmail.alerts.ConfirmingEmail'));

    this.accountService.confirmUserAccount(userId, code)
      .subscribe(() => {
        this.alertService.stopLoadingMessage();
        this.isLoading = false;
        this.isSuccess = true;

        this.message = this.gT('confirmEmail.alerts.ThankYouForConfirmingEmail');

        setTimeout(() => {
          this.alertService.showMessage(this.gT('confirmEmail.alerts.EmailConfirmed'), this.gT('confirmEmail.alerts.EmailSuccessfullyConfirmed'), MessageSeverity.success);
        }, 2000);
      },
        error => {
          this.alertService.stopLoadingMessage();
          this.isLoading = false;
          this.isSuccess = false;

          this.message = this.gT('confirmEmail.alerts.EmailConfirmationForUserFailed', { userId });

          setTimeout(() => {
            const errorData = Utilities.getResponseBody(error);

            if (Utilities.checkNotFound(error) && errorData === userId) {
              this.alertService.showStickyMessage(this.gT('confirmEmail.alerts.EmailNotConfirmed'), this.gT('confirmEmail.alerts.UserWithIdNotFound', { userId }), MessageSeverity.error, error);
            } else {
              const errorMessage = Utilities.getHttpResponseMessage(error);

              if (errorMessage) {
                this.alertService.showStickyMessage(this.gT('confirmEmail.alerts.EmailNotConfirmed'), errorMessage, MessageSeverity.error, error);
              } else {
                this.alertService.showStickyMessage(this.gT('confirmEmail.alerts.EmailNotConfirmed'), this.gT('confirmEmail.alerts.EmailConfirmationErrorOccured', { error: errorData }), MessageSeverity.error, error);
              }
            }
          }, 2000);
        });
  }
}
