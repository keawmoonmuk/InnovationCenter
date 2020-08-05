// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { AlertService, MessageSeverity } from '../../services/alert.service';
import { AppTranslationService } from '../../services/app-translation.service';
import { AuthService } from '../../services/auth.service';
import { Utilities } from '../../services/utilities';
import { JwtHelper } from '../../services/jwt-helper';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.scss']
})

export class AuthCallbackComponent implements OnInit, OnDestroy {

  message: string;
  isLoading = false;
  provider: string;
  externalAuthToken: string;
  email: string;
  foundEmail: string;
  userPassword: string;
  loginStatusSubscription: any;

  urlFragmentProcessed = false;
  urlQueryParamsProcessed = false;
  tokenProcessed = false;

  googleProvider = 'google';
  facebookProvider = 'facebook';
  twitterProvider = 'twitter';

  get providerName() {
    return { provider: this.gT(`authCallback.${this.provider}Provider`) };
  }

  gT = (key: string | Array<string>, interpolateParams?: object) => this.translationService.getTranslation(key, interpolateParams);

  constructor(
    private route: ActivatedRoute,
    private alertService: AlertService,
    private translationService: AppTranslationService,
    private authService: AuthService) {

  }

  ngOnInit() {
    if (this.getShouldRedirect()) {
      this.authService.redirectLoginUser();
      return;
    } else {
      this.loginStatusSubscription = this.authService.getLoginStatusEvent().subscribe(() => {
        if (this.getShouldRedirect()) {
          this.authService.redirectLoginUser();
        }
      });
    }

    this.setProvider(this.route.snapshot.url[0].path);

    this.route.fragment.subscribe(frag => {
      const fragParams: any = Utilities.getQueryParamsFromString(frag);
      this.processTokens(fragParams);
      this.urlFragmentProcessed = true;
    });

    this.route.queryParams.subscribe(params => {
      const queryParams: any = Utilities.GetObjectWithLoweredPropertyNames(params);
      this.processTokens(queryParams);
      this.urlQueryParamsProcessed = true;
    });
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

  setProvider(url: string) {
    if (url.includes(this.googleProvider)) {
      this.provider = this.googleProvider;
    } else if (url.includes(this.facebookProvider)) {
      this.provider = this.facebookProvider;
    } else if (url.includes(this.twitterProvider)) {
      this.provider = this.twitterProvider;
    } else {
      throw new Error('Unknown login provider');
    }
  }

  processTokens(tokensObject: any) {
    if (this.tokenProcessed) {
      return;
    }

    if (tokensObject) {
      if (tokensObject.access_token) {
        if (tokensObject.id_token) {
          const decodedIdToken = new JwtHelper().decodeToken(tokensObject.id_token);
          this.email = decodedIdToken.email || decodedIdToken.emailAddress;
        } else {
          this.email = null;
        }

        this.tokenProcessed = true;
        this.loginWithToken(tokensObject.access_token, this.provider, this.email);
      } else if (tokensObject.oauth_token && tokensObject.oauth_verifier) {
        if (this.provider === this.twitterProvider) {
          this.tokenProcessed = true;
          this.isLoading = true;
          this.message = this.gT('authCallback.ConnectingToTwitter');
          this.authService.getTwitterAccessToken(tokensObject.oauth_token, tokensObject.oauth_verifier)
            .subscribe(accessToken => {
              this.isLoading = true;
              this.message = this.gT('authCallback.Processing');
              this.loginWithToken(accessToken, this.provider);
            },
              error => {
                this.isLoading = false;
                this.message = null;
                this.showLoginErrorMessage(error);
              });
        }
      }
    }

    if (!this.tokenProcessed && (this.urlFragmentProcessed || this.urlQueryParamsProcessed)) {
      setTimeout(() => {
        this.alertService.showMessage(this.gT('authCallback.alerts.InvalidLogin'), this.gT('authCallback.alerts.NoValidTokensFound'), MessageSeverity.error);
      }, 500);

      this.message = this.gT('authCallback.Error');
      this.authService.redirectLogoutUser();
    }
  }

  loginWithToken(token: string, provider: string, email?: string) {
    this.externalAuthToken = token;
    this.isLoading = true;
    this.message = this.gT('authCallback.Processing');
    this.alertService.startLoadingMessage('', this.gT('authCallback.alerts.SigningIn'));

    this.authService.loginWithExternalToken(token, provider, email)
      .subscribe(
        user => {
          setTimeout(() => {
            this.alertService.stopLoadingMessage();
            this.isLoading = false;

            this.alertService.showMessage(this.gT('login.alerts.Login'), this.gT('login.alerts.Welcome', { username: user.userName }), MessageSeverity.success);
          }, 500);
        },
        error => {
          this.alertService.stopLoadingMessage();
          this.isLoading = false;
          this.message = this.gT('authCallback.Error');
          this.foundEmail = Utilities.findHttpResponseMessage('email', error);

          if (this.foundEmail) {
            const errorMessage = Utilities.getHttpResponseMessage(error);
            this.alertService.showStickyMessage(this.gT('authCallback.alerts.UserAlreadyExists'), this.mapLoginErrorMessage(errorMessage), MessageSeverity.default, error);
          } else {
            this.showLoginErrorMessage(error);
          }
        });
  }


  linkAccountAndLogin() {
    this.isLoading = true;
    this.alertService.startLoadingMessage('', this.gT('login.alerts.AttemptingLogin'));

    this.authService.loginWithExternalToken(this.externalAuthToken, this.provider, this.email, this.userPassword)
      .subscribe(
        user => {
          setTimeout(() => {
            this.alertService.stopLoadingMessage();
            this.isLoading = false;
            this.userPassword = null;

            this.alertService.showMessage(this.gT('login.alerts.Login'), this.gT('login.alerts.Welcome', { username: user.userName }), MessageSeverity.success);
          }, 500);
        },
        error => {
          this.alertService.stopLoadingMessage();
          this.showLoginErrorMessage(error, false);

          setTimeout(() => {
            this.isLoading = false;
          }, 500);
        });
  }

  showLoginErrorMessage(error, redirect = true) {
    setTimeout(() => {
      if (Utilities.checkNoNetwork(error)) {
        this.alertService.showStickyMessage(this.gT('app.NoNetwork'), this.gT('app.ServerCannotBeReached'), MessageSeverity.error, error);
      } else {
        const errorMessage = Utilities.getHttpResponseMessage(error);
        if (errorMessage) {
          this.alertService.showStickyMessage(this.gT('login.alerts.UnableToLogin'), this.mapLoginErrorMessage(errorMessage), MessageSeverity.error, error);
        } else {
          this.alertService.showStickyMessage(this.gT('login.alerts.UnableToLogin'), this.gT('login.alerts.LoginErrorOccured', { error: Utilities.getResponseBody(error) }), MessageSeverity.error, error);
        }
      }

    }, 500);

    if (redirect) {
      this.authService.redirectLogoutUser();
    }
  }

  mapLoginErrorMessage(error: string) {
    if (error === 'invalid_username_or_password') {
      return this.gT('login.alerts.InvalidUsernameOrPassword');
    }

    return error;
  }
}
