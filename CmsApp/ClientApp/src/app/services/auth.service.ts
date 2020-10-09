
import { Injectable } from '@angular/core';
import { Router, NavigationExtras } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

import { LocalStoreManager } from './local-store-manager.service';
import { OidcHelperService } from './oidc-helper.service';
import { ConfigurationService } from './configuration.service';
import { DBkeys } from './db-keys';
import { JwtHelper } from './jwt-helper';
import { Utilities } from './utilities';
import { AccessToken, LoginResponse } from '../models/login-response.model';
import { User } from '../models/user.model';                                //import class User
import { Patient } from '../models/patient.model'                           //import class patient

import { PermissionValues } from '../models/permission.model';

@Injectable() 
export class AuthService {
  public get loginUrl() { return this.configurations.loginUrl; }      //get status login url
  public get homeUrl() { return this.configurations.homeUrl; }        //get page home url
  
  public loginRedirectUrl: string;                    // กำหนด url login url ที่จะ link ไป
  public logoutRedirectUrl: string;                   //  กำหนด url logout url ที่จะ link ไป 
  public reLoginDelegate: () => void;                 // ผู้รับมอบสิทธิ์
  private previousIsLoggedInCheck = false;   
  private loginStatus = new Subject<boolean>();       // login status

  //constuctor ==> for create varible router, oidcHelperService, configurations, localStorage
  constructor(
    private router: Router,
    private oidcHelperService: OidcHelperService,
    private configurations: ConfigurationService,
    private localStorage: LocalStoreManager)

  {
    this.initializeLoginStatus();
  }

  //initialize   Login start
  private initializeLoginStatus() {
    this.localStorage.getInitEvent().subscribe(() => {
      this.reevaluateLoginStatus();
    });
  }

  gotoPage(page: string, preserveParams = true) {

    const navigationExtras: NavigationExtras = {
      queryParamsHandling: preserveParams ? 'merge' : '', preserveFragment: preserveParams
    };

    this.router.navigate([page], navigationExtras);
  }

  gotoHomePage() {
    this.router.navigate([this.homeUrl]);
  }

  //Redirect LoginUser
  redirectLoginUser() {
    const redirect = this.loginRedirectUrl && this.loginRedirectUrl !== '/' && this.loginRedirectUrl !== ConfigurationService.defaultHomeUrl ? this.loginRedirectUrl : this.homeUrl;
    this.loginRedirectUrl = null;

    const urlParamsAndFragment = Utilities.splitInTwo(redirect, '#');
    const urlAndParams = Utilities.splitInTwo(urlParamsAndFragment.firstPart, '?');

    const navigationExtras: NavigationExtras = {
      fragment: urlParamsAndFragment.secondPart,
      queryParams: Utilities.getQueryParamsFromString(urlAndParams.secondPart),
      queryParamsHandling: 'merge'
    };

    this.router.navigate([urlAndParams.firstPart], navigationExtras);
  }

  // Redirect Logout User
  redirectLogoutUser() {
    const redirect = this.logoutRedirectUrl ? this.logoutRedirectUrl : this.loginUrl;
    this.logoutRedirectUrl = null;

    this.router.navigate([redirect]);

  }

  redirectForLogin() {
    this.loginRedirectUrl = this.router.url;
    this.router.navigate([this.loginUrl]);
  }

  reLogin() {
    if (this.reLoginDelegate) {
      this.reLoginDelegate();
    } else {
      this.redirectForLogin();
    }
  }

  refreshLogin() {
    return this.oidcHelperService.refreshLogin()
      .pipe(map(resp => this.processLoginResponse(resp, this.rememberMe)));
  }
  //login username and password
  loginWithPassword(userName: string, password: string, rememberMe?: boolean) {
    if (this.isLoggedIn)
    {
      this.logout();
    }

    return this.oidcHelperService.loginWithPassword(userName, password)
      .pipe(map(resp => this.processLoginResponse(resp, rememberMe)));
  }

    loginWithExternalToken(token: string, provider: string, email?: string, password?: string) {
        if (this.isLoggedIn) {
            this.logout();
        }

        return this.oidcHelperService.loginWithExternalToken(token, provider, email, password)
            .pipe(map(resp => this.processLoginResponse(resp)));
    }

    initLoginWithGoogle(rememberMe?: boolean) {
        if (this.isLoggedIn) {
            this.logout();
        }

        this.localStorage.savePermanentData(rememberMe, DBkeys.REMEMBER_ME);
        this.oidcHelperService.initLoginWithGoogle();
    }

    initLoginWithFacebook(rememberMe?: boolean) {
        if (this.isLoggedIn) {
            this.logout();
        }

        this.localStorage.savePermanentData(rememberMe, DBkeys.REMEMBER_ME);
        this.oidcHelperService.initLoginWithFacebook();
    }

    initLoginWithTwitter(rememberMe?: boolean) {
        if (this.isLoggedIn) {
            this.logout();
        }

        this.localStorage.savePermanentData(rememberMe, DBkeys.REMEMBER_ME);
        this.oidcHelperService.initLoginWithTwitter();
    }

    getTwitterAccessToken(oauthToken: string, oauthVerifier: string) {
        return this.oidcHelperService.getTwitterAccessToken(oauthToken, oauthVerifier);
    }

  private processLoginResponse(response: LoginResponse, rememberMe?: boolean) {
    const accessToken = response.access_token;

    if (accessToken == null) {
      throw new Error('accessToken cannot be null');
    }

    rememberMe = rememberMe || this.rememberMe;

    const refreshToken = response.refresh_token || this.refreshToken;
    const expiresIn = response.expires_in;
    const tokenExpiryDate = new Date();
    tokenExpiryDate.setSeconds(tokenExpiryDate.getSeconds() + expiresIn);
    const accessTokenExpiry = tokenExpiryDate;
    const jwtHelper = new JwtHelper();
    const decodedAccessToken = jwtHelper.decodeToken(accessToken) as AccessToken;

    const permissions: PermissionValues[] = Array.isArray(decodedAccessToken.permission) ? decodedAccessToken.permission : [decodedAccessToken.permission];

    if (!this.isLoggedIn) {
      this.configurations.import(decodedAccessToken.configuration);
    }

    const user = new User(
      decodedAccessToken.sub,
      decodedAccessToken.name,
      decodedAccessToken.fullname,
      decodedAccessToken.email,
      decodedAccessToken.jobtitle,
      decodedAccessToken.phone_number,
      Array.isArray(decodedAccessToken.role) ? decodedAccessToken.role : [decodedAccessToken.role]);
    user.isEnabled = true;

    this.saveUserDetails(user, permissions, accessToken, refreshToken, accessTokenExpiry, rememberMe);

    this.reevaluateLoginStatus(user);

    return user;
  }

  private saveUserDetails(user: User, permissions: PermissionValues[], accessToken: string, refreshToken: string, expiresIn: Date, rememberMe: boolean) {
    if (rememberMe) {
      this.localStorage.savePermanentData(accessToken, DBkeys.ACCESS_TOKEN);
      this.localStorage.savePermanentData(refreshToken, DBkeys.REFRESH_TOKEN);
      this.localStorage.savePermanentData(expiresIn, DBkeys.TOKEN_EXPIRES_IN);
      this.localStorage.savePermanentData(permissions, DBkeys.USER_PERMISSIONS);
      this.localStorage.savePermanentData(user, DBkeys.CURRENT_USER);
    } else {
      this.localStorage.saveSyncedSessionData(accessToken, DBkeys.ACCESS_TOKEN);
      this.localStorage.saveSyncedSessionData(refreshToken, DBkeys.REFRESH_TOKEN);
      this.localStorage.saveSyncedSessionData(expiresIn, DBkeys.TOKEN_EXPIRES_IN);
      this.localStorage.saveSyncedSessionData(permissions, DBkeys.USER_PERMISSIONS);
      this.localStorage.saveSyncedSessionData(user, DBkeys.CURRENT_USER);
    }

    this.localStorage.savePermanentData(rememberMe, DBkeys.REMEMBER_ME);
  }

  logout(): void {
    this.localStorage.deleteData(DBkeys.ACCESS_TOKEN);
    this.localStorage.deleteData(DBkeys.REFRESH_TOKEN);
    this.localStorage.deleteData(DBkeys.TOKEN_EXPIRES_IN);
    this.localStorage.deleteData(DBkeys.USER_PERMISSIONS);
    this.localStorage.deleteData(DBkeys.CURRENT_USER);

    this.configurations.clearLocalChanges();

    this.reevaluateLoginStatus();
  }

  //-------------------current User-----------------
  private reevaluateLoginStatus(currentUser?: User) {
    const user = currentUser || this.localStorage.getDataObject<User>(DBkeys.CURRENT_USER);
    const isLoggedIn = user != null;

    if (this.previousIsLoggedInCheck !== isLoggedIn) {
      setTimeout(() => {
        this.loginStatus.next(isLoggedIn);
      });
    }

    this.previousIsLoggedInCheck = isLoggedIn;
  }

  //------------------current patient----------------------
  private showvaluepatient(currentPatient?: Patient) {
    const patient = this.currentPatient || this.localStorage.getDataObject<Patient>(DBkeys.CURRENT_PATIENT);
    const ispatient = patient != null;
    if (this.previousIsLoggedInCheck !== ispatient) {
      setTimeout(() => {
        this.loginStatus.next(ispatient);
      });
    }
    this.previousIsLoggedInCheck = this.isLoggedIn;
  }
  

  getLoginStatusEvent(): Observable<boolean> {
    return this.loginStatus.asObservable();
  }

  //get currentUser
  get currentUser(): User {
    const user = this.localStorage.getDataObject<User>(DBkeys.CURRENT_USER);
    this.reevaluateLoginStatus(user);
   
    return user;
  }

  //get patient
  get currentPatient(): Patient {
    const patient = this.localStorage.getDataObject<Patient>(DBkeys.CURRENT_PATIENT);
    this.showvaluepatient(patient);
    console.log(patient)
    return patient;
  }

  get userPermissions(): PermissionValues[] {
    return this.localStorage.getDataObject<PermissionValues[]>(DBkeys.USER_PERMISSIONS) || [];
  }

  get accessToken(): string {
    return this.oidcHelperService.accessToken;
  }

  get accessTokenExpiryDate(): Date {
    return this.oidcHelperService.accessTokenExpiryDate;
  }

  get refreshToken(): string {
    return this.oidcHelperService.refreshToken;
  }

  get isSessionExpired(): boolean {
    return this.oidcHelperService.isSessionExpired;
  }

  get isLoggedIn(): boolean {
    return this.currentUser != null;
  }

  get rememberMe(): boolean {
    return this.localStorage.getDataObject<boolean>(DBkeys.REMEMBER_ME) === true;
  }
}
