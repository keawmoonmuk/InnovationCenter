
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { from, throwError } from 'rxjs';
import { mergeMap } from 'rxjs/operators';
import { OAuthService } from 'angular-oauth2-oidc';

import { LocalStoreManager } from './local-store-manager.service';
import { ConfigurationService } from './configuration.service';
import { Utilities } from './utilities';
import { DBkeys } from './db-keys';
import { LoginResponse } from '../models/login-response.model';

@Injectable()
export class OidcHelperService {

  private get baseUrl() { return this.configurations.baseUrl; }    //get baseUrl
  private clientId = 'cmsapp_spa'
  //private clientId = 'quickapp_spa';
  private scope = 'openid email phone profile offline_access roles cmsapp_api';

  constructor(
    private http: HttpClient,
    private oauthService: OAuthService,
    private configurations: ConfigurationService,
    private localStorage: LocalStoreManager) {

  }

  //-------------login for username and passs----------------------
  loginWithPassword(userName: string, password: string) {
    const header = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' });
    const params = new HttpParams()
      .append('username', userName)
      .append('password', password)
      .append('client_id', this.clientId)
      .append('grant_type', 'password')
      .append('scope', this.scope);

    this.oauthService.issuer = this.baseUrl;

    return from(this.oauthService.loadDiscoveryDocument())
      .pipe(mergeMap(() => {
        return this.http.post<LoginResponse>(this.oauthService.tokenEndpoint, params, { headers: header });
      }));
  }
  //-----------------refreshLogin--------------------------------------
  refreshLogin() {
    const header = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' });
    const params = new HttpParams()
      .append('refresh_token', this.refreshToken)
      .append('client_id', this.clientId)
      .append('grant_type', 'refresh_token');

    this.oauthService.issuer = this.baseUrl;

    return from(this.oauthService.loadDiscoveryDocument())
      .pipe(mergeMap(() => {
        return this.http.post<LoginResponse>(this.oauthService.tokenEndpoint, params, { headers: header });
      }));
  }
  //-------------------loginWithExternalToken---------------------------
  loginWithExternalToken(token: string, provider: string, email?: string, password?: string) {
    const header = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' });
    let params = new HttpParams()
      .append('token', token)
      .append('provider', provider)
      .append('client_id', this.clientId)
      .append('grant_type', 'delegation')
      .append('scope', this.scope);

    if (email) {
      params = params.append('email', email);
    }

    if (password) {
      params = params.append('password', password);
    }

    this.oauthService.issuer = this.baseUrl;

    return from(this.oauthService.loadDiscoveryDocument())
      .pipe(mergeMap(() => {
        return this.http.post<LoginResponse>(this.oauthService.tokenEndpoint, params, { headers: header });
      }));
  }
  //----------------LoginWithGoogle------------------------
  initLoginWithGoogle() {
    this.oauthService.configure({
      issuer: 'https://accounts.google.com',
      redirectUri: this.configurations.baseUrl + '/google-login',
      clientId: this.configurations.googleClientId,
      strictDiscoveryDocumentValidation: false,
      scope: 'openid profile email',
      sessionChecksEnabled: false
    });

    this.oauthService.loadDiscoveryDocument().then(() => {
      this.oauthService.initImplicitFlow();
    });
  }
  //-----------------LoginWithFacebook-------------------
  initLoginWithFacebook() {
    this.oauthService.configure({
      loginUrl: 'https://www.facebook.com/v5.0/dialog/oauth',
      redirectUri: this.configurations.baseUrl + '/facebook-login',
      clientId: this.configurations.facebookClientId,
      scope: 'email',
      oidc: false,
      sessionChecksEnabled: false
    });

    this.oauthService.initImplicitFlow();
  }
  //------------------Login Wtih Twitter-----------------------
  initLoginWithTwitter() {
    this.getTwitterRequestToken()
      .subscribe(response => {
        const tokens = Utilities.getQueryParamsFromString(response);
        if (tokens.oauth_callback_confirmed) {
          this.localStorage.savePermanentData(tokens.oauth_token, DBkeys.TWITTER_OAUTH_TOKEN);
          this.localStorage.savePermanentData(tokens.oauth_token_secret, DBkeys.TWITTER_OAUTH_TOKEN_SECRET);

          this.authenticateTwitterRequestToken(tokens.oauth_token);
        } else {
          throw new Error('Twitter OAuth Callback Not Confirmed');
        }
      });
  }
  //-------------------getTwitterRequestToken-------------------------
  private getTwitterRequestToken() {
    const requestTokenUrl = this.baseUrl + '/oauth/twitter/request_token';
    const requestObject = { oauth_callback: this.baseUrl + '/twitter-login' };
    return this.http.post(requestTokenUrl, requestObject, { responseType: 'text' });
  }
  //-------------------authenticate Twitter RequestToken----------------
  private authenticateTwitterRequestToken(oauthToken: string) {
    window.location.href = 'https://api.twitter.com/oauth/authenticate?oauth_token=' + oauthToken;
  }
  //--------------------getTwitter AccessToken---------------------------
  getTwitterAccessToken(oauthToken: string, oauthVerifier: string) {
    const savedOauthToken = this.localStorage.getDataObject<string>(DBkeys.TWITTER_OAUTH_TOKEN);
    const savedOauthTokenSecret = this.localStorage.getDataObject<string>(DBkeys.TWITTER_OAUTH_TOKEN_SECRET);
    this.localStorage.deleteData(DBkeys.TWITTER_OAUTH_TOKEN);
    this.localStorage.deleteData(DBkeys.TWITTER_OAUTH_TOKEN_SECRET);

    if (oauthToken !== savedOauthToken) {
      return throwError('Invalid or Expired Twitter OAuthToken');
    }

    const requestTokenUrl = this.baseUrl + '/oauth/twitter/access_token';
    const requestObject = {
      oauth_token: oauthToken,
      oauth_token_secret: savedOauthTokenSecret,
      oauth_verifier: oauthVerifier,
    };

    return this.http.post(requestTokenUrl, requestObject, { responseType: 'text' });
  }

  //-------------------accessToken----------------------
  get accessToken(): string {
    return this.localStorage.getData(DBkeys.ACCESS_TOKEN);
  }
  //------------------accessToken Expiry Date-----------
  get accessTokenExpiryDate(): Date {
    return this.localStorage.getDataObject<Date>(DBkeys.TOKEN_EXPIRES_IN, true);
  }
  //-----------------refrestToken-----------------------
  get refreshToken(): string {
    return this.localStorage.getData(DBkeys.REFRESH_TOKEN);
  }
  //----------------Session Expired----------------------
  get isSessionExpired(): boolean {
    if (this.accessTokenExpiryDate == null) {
      return true;
    }

    return this.accessTokenExpiryDate.valueOf() <= new Date().valueOf();
  }
}
