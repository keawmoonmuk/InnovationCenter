
//import for angular 
import { Injectable } from '@angular/core';
import { Router, NavigationExtras } from '@angular/router';
import { Observable, Subject, config } from 'rxjs';
import { map } from 'rxjs/operators';

import { LocalStoreManager } from './local-store-manager.service';  // Manage in Local Store
import { OidcHelperService } from './oidc-helper.service';
import { ConfigurationService } from './configuration.service';
import { DBkeys } from './db-keys';
import { JwtHelper } from './jwt-helper';                                   //Jwt
import { Utilities } from './utilities';                                    //Utilies
import { AccessToken, LoginResponse } from '../models/login-response.model';
import { User } from '../models/user.model';                                //import class User
import { Patient } from '../models/patient.model'                           //import class patient

import { PermissionValues } from '../models/permission.model';              //Permisssin Values

@Injectable()
export class AuthService {

  public get loginUrl() { return this.configurations.loginUrl; }      // get status login url
  public get homeUrl() { return this.configurations.homeUrl; }        // get page home url

  public loginRedirectUrl: string;                                    // กำหนด url login url ที่จะ link ไป
  public logoutRedirectUrl: string;                                   // กำหนด url logout url ที่จะ link ไป 
  public reLoginDelegate: () => void;                                 // รับมอบสิทธิ์ authorized  
  private previousIsLoggedInCheck = false;                            // ก่อนเข้าสู่ระบบ (Check)
  private loginStatus = new Subject<boolean>();                       // login status

  //constuctor ==> start for create varible router, oidcHelperService, configurations, localStorage
  constructor(
    private router: Router,
    private oidcHelperService: OidcHelperService,
    private configurations: ConfigurationService,
    private localStorage: LocalStoreManager) {
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

  //to homepage
  gotoHomePage() {
    console.log("auth.service.ts for gotoHomepage...");
    this.router.navigate([this.homeUrl]);
  }

  //Redirect LoginUser
  redirectLoginUser() {
    console.log("auth.service.ts for redirectLoginUser...");
    const redirect = this.loginRedirectUrl && this.loginRedirectUrl !== '/' && this.loginRedirectUrl !== ConfigurationService.defaultHomeUrl ? this.loginRedirectUrl : this.homeUrl;
    console.log("auth.service.ts func redirctLoginUser  (redirect) =>" + redirect);
    this.loginRedirectUrl = null;

    const urlParamsAndFragment = Utilities.splitInTwo(redirect, '#');
    console.log("auth.service.ts func redirectLoginUser (urlParamsAndFragment) =>" + urlParamsAndFragment);

    const urlAndParams = Utilities.splitInTwo(urlParamsAndFragment.firstPart, '?');
    console.log("auth.service.ts func redirectLoginUser (urlAndParams) =>" + urlAndParams);

    const navigationExtras: NavigationExtras = {
      fragment: urlParamsAndFragment.secondPart,
      queryParams: Utilities.getQueryParamsFromString(urlAndParams.secondPart),
      queryParamsHandling: 'merge'
    };

    this.router.navigate([urlAndParams.firstPart], navigationExtras);
  }

  // Redirect Logout User
  redirectLogoutUser() {
    console.log("auth.service.ts func redirectLogoutUser...");
    const redirect = this.logoutRedirectUrl ? this.logoutRedirectUrl : this.loginUrl;
    console.log("auth.service.ts func redirectLogoutUser (redirect) =>" + redirect);

    this.logoutRedirectUrl = null;
   
    this.router.navigate([redirect]);

  }

  redirectForLogin() {
    console.log("auth.service.ts func redirectForLogin.....");

    this.loginRedirectUrl = this.router.url;
    console.log("auth.service.ts func redirectForLogin (loginRedirectUrl) => " + this.loginRedirectUrl);

    this.router.navigate([this.loginUrl]);
  }

  reLogin() {
    console.log("auth.service.ts func reLogin....");
    if (this.reLoginDelegate) {
      console.log("auth.service.ts func reLogin whern recive Deleage Login ได้รับสิทธิ์ login...");
      this.reLoginDelegate();
    } else {
      console.log("auth.service.ts func reLogin when not recive Deleage Login ไม่ได้รับสิทธิ์ login....");
      console.log("redirect to forlogin (func redirectForLogin)...");
      this.redirectForLogin();
    }
  }

  refreshLogin() {
    console.log("auth.service.ts func refreshLogin....");
    return this.oidcHelperService.refreshLogin()
      .pipe(map(resp => this.processLoginResponse(resp, this.rememberMe)));
  }
  //login username and password
  loginWithPassword(userName: string, password: string, rememberMe?: boolean) {

    console.log("auth.service.ts func loginWithPassword...");
    console.log("auth.service.ts func loginWithPassword   userName => " + userName);
    console.log("auth.service.ts func loginWithPassword   password => " + password);
    console.log("auth.service.ts func loginwithPassword   rememberMe  => " + rememberMe);

    if (this.isLoggedIn) {

      console.log("auth.service.ts func loginWithPassword When isLoggedIn = true...");

      this.logout();
    }

    return this.oidcHelperService.loginWithPassword(userName, password)
      .pipe(map(resp => this.processLoginResponse(resp, rememberMe)));
  }

  loginWithExternalToken(token: string, provider: string, email?: string, password?: string) {

    console.log("auth.service.ts func loginWintExternalToken.....");
    console.log("auth.service.ts func loginWithPassword...");
    console.log("auth.service.ts func loginWithPassword   token => " + token);
    console.log("auth.service.ts func loginWithPassword   provider => " + provider);
    console.log("auth.service.ts func loginwithPassword   email  => " + email);
    console.log("auth.service.ts func loginwithPassword   password  => " + password);

    if (this.isLoggedIn) {

      console.log("auth.service.ts func loginWithExternalToken When isLoggedIn = true...");

      this.logout();
    }

    return this.oidcHelperService.loginWithExternalToken(token, provider, email, password)
      .pipe(map(resp => this.processLoginResponse(resp)));
  }

  initLoginWithGoogle(rememberMe?: boolean) {

    console.log("auth.service.ts func initLoginWithGoogle...");
    console.log("auth.service.ts func initLoginWithGoogle  rememberMe => " + rememberMe);

    if (this.isLoggedIn) {

      console.log("auth.service.ts func initLoginWithGoogle When isLoggedIn = true...");

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

  //*******************Response Login********************* 
  private processLoginResponse(response: LoginResponse, rememberMe?: boolean) {

    const accessToken = response.access_token;

    console.log("auth.service.ts func processLoginResponse  accessToken => " + accessToken);

    if (accessToken == null) {

      console.log("auth.service.ts func processLoginResponse when accessToken == null....");

      throw new Error('accessToken cannot be null');
    }

    console.log("auth.service.ts func processLoginResponse when accessToken != null...");

    rememberMe = rememberMe || this.rememberMe;

    const refreshToken = response.refresh_token || this.refreshToken;
    const expiresIn = response.expires_in;
    const tokenExpiryDate = new Date();
    tokenExpiryDate.setSeconds(tokenExpiryDate.getSeconds() + expiresIn);
    const accessTokenExpiry = tokenExpiryDate;
    const jwtHelper = new JwtHelper();
    const decodedAccessToken = jwtHelper.decodeToken(accessToken) as AccessToken;

    const permissions: PermissionValues[] = Array.isArray(decodedAccessToken.permission) ? decodedAccessToken.permission : [decodedAccessToken.permission];

    console.log("auth.service.ts func processLoginResponse rememberMe =>  " + rememberMe);
    console.log("auth.service.ts func processLoginResponse refreshToken =>  " + refreshToken);
    console.log("auth.service.ts func processLoginResponse expiresIn =>  " + expiresIn);
    console.log("auth.service.ts func processLoginResponse tokenExpiryDate =>  " + tokenExpiryDate);
    console.log("auth.service.ts func processLoginResponse accessTokenExpiry =>  " + accessTokenExpiry);
    console.log("auth.service.ts func processLoginResponse jwtHelper =>  " + jwtHelper);
    console.log("auth.service.ts func processLoginResponse decodeAccessToken =>  " + decodedAccessToken);
    console.log("auth.service.ts func processLoginResponse token expire for set secords =>  " + tokenExpiryDate.setSeconds(tokenExpiryDate.getSeconds() + expiresIn));

    if (!this.isLoggedIn) {

      console.log("auth.service.ts func processLoginrResponse  when isLoggedIn == null....");

      this.configurations.import(decodedAccessToken.configuration);
    }
    // Class User
    const user = new User(
      decodedAccessToken.sub,
      decodedAccessToken.name,
      decodedAccessToken.fullname,
      decodedAccessToken.email,
      decodedAccessToken.jobtitle,
      decodedAccessToken.phone_number,
      Array.isArray(decodedAccessToken.role) ? decodedAccessToken.role : [decodedAccessToken.role]);
    user.isEnabled = true;

    //*******saveUserDetail******** is paramenter => user ,permission,accessToken ,refreshToken, accessTokenExpiry, rememberMe
    console.log("auth.service.ts func processLoginrResponse for saveUserDetails...." );
    this.saveUserDetails(user, permissions, accessToken, refreshToken, accessTokenExpiry, rememberMe);

    this.reevaluateLoginStatus(user);

    console.log("user  => " + user);

    return user;
  }
  //save User Detail
  private saveUserDetails(user: User, permissions: PermissionValues[], accessToken: string, refreshToken: string, expiresIn: Date, rememberMe: boolean) {

    console.log("auth.service.ts funct processLoginResponse  user => " + user);
    console.log("auth.service.ts funct processLoginResponse  permissions => " + permissions);
    console.log("auth.service.ts funct processLoginResponse  accessToken => " + accessToken);
    console.log("auth.service.ts funct processLoginResponse  refreshToken => " + refreshToken);
    console.log("auth.service.ts funct processLoginResponse  expiresIn => " + expiresIn);
    console.log("auth.service.ts funct processLoginResponse  rememberMe => " + rememberMe);

    if (rememberMe) {
      console.log("auth.service.ts func processLoginResponse when rememberMe == true ....");

      this.localStorage.savePermanentData(accessToken, DBkeys.ACCESS_TOKEN);
      this.localStorage.savePermanentData(refreshToken, DBkeys.REFRESH_TOKEN);
      this.localStorage.savePermanentData(expiresIn, DBkeys.TOKEN_EXPIRES_IN);
      this.localStorage.savePermanentData(permissions, DBkeys.USER_PERMISSIONS);
      this.localStorage.savePermanentData(user, DBkeys.CURRENT_USER);

    } else
    {
      console.log("auth.service.ts func processLoginResponse when rememberMe == false");

      this.localStorage.saveSyncedSessionData(accessToken, DBkeys.ACCESS_TOKEN);
      this.localStorage.saveSyncedSessionData(refreshToken, DBkeys.REFRESH_TOKEN);
      this.localStorage.saveSyncedSessionData(expiresIn, DBkeys.TOKEN_EXPIRES_IN);
      this.localStorage.saveSyncedSessionData(permissions, DBkeys.USER_PERMISSIONS);
      this.localStorage.saveSyncedSessionData(user, DBkeys.CURRENT_USER);
    }
    console.log("auth.service.ts func processLoginResponse for savePermanentData save ถาวร...");
    this.localStorage.savePermanentData(rememberMe, DBkeys.REMEMBER_ME);
  }
  //----------------logout--------------------
  logout(): void {
    console.log("auth.service.ts func logout...");
    this.localStorage.deleteData(DBkeys.ACCESS_TOKEN);
    this.localStorage.deleteData(DBkeys.REFRESH_TOKEN);
    this.localStorage.deleteData(DBkeys.TOKEN_EXPIRES_IN);
    this.localStorage.deleteData(DBkeys.USER_PERMISSIONS);
    this.localStorage.deleteData(DBkeys.CURRENT_USER);

    this.configurations.clearLocalChanges();

    this.reevaluateLoginStatus();
  }

  //-------------------ประเมิน user ใหม่-----------------
  private reevaluateLoginStatus(currentUser?: User) {

    const user = currentUser || this.localStorage.getDataObject<User>(DBkeys.CURRENT_USER);
    //log user = Object {id: "2a432ac8-139b-409b-88ac-450c23704bbf", userName: "Kreangkrai", fullName: "kreangkria keaw", …}
    console.log("auth.service.ts func reevaluateLoginStatus   user => " + user);

    const isLoggedIn = user != null;

    console.log("auth.service.ts func reevaluateLoginStatus when isLoggedIn for user != null");

    if (this.previousIsLoggedInCheck !== isLoggedIn) {

      setTimeout(() => {
        this.loginStatus.next(isLoggedIn);
      });
    }

    this.previousIsLoggedInCheck = isLoggedIn;
  }

  //------------------current patient----------------------
  private showvaluepatient(currentPatient?: Patient) {
    const patient = currentPatient || this.localStorage.getDataObject<Patient>(DBkeys.CURRENT_PATIENT);
    const ispatient = patient != null;
    if (this.previousIsLoggedInCheck !== ispatient) {
      setTimeout(() => {
        this.loginStatus.next(ispatient);
      });
    }
    this.previousIsLoggedInCheck = this.isLoggedIn;
  }

  //get Status Login Event
  getLoginStatusEvent(): Observable<boolean> {
    return this.loginStatus.asObservable();
  }

  //*****get currentUser*******
  get currentUser(): User {
    const user = this.localStorage.getDataObject<User>(DBkeys.CURRENT_USER);
    //return ==> Object {id: "2a432ac8-139b-409b-88ac-450c23704bbf", userName: "Kreangkrai", fullName: "kreangkria keaw", …}

    console.log("auth.service.ts func currentUser user => " + user);
    console.log("auth.service.ts func currentUser for reevaluatLoginStatus");
    this.reevaluateLoginStatus(user);

    return user;
  }

  //--------get patient-------
  get currentPatient(): Patient {

    const patient = this.localStorage.getDataObject<Patient>(DBkeys.CURRENT_PATIENT);
    console.log(patient);
    this.showvaluepatient(patient);

    return patient;
  }

  //get Permissions
  get userPermissions(): PermissionValues[] {
    console.log("auth.service.ts func userPermission...");
    return this.localStorage.getDataObject<PermissionValues[]>(DBkeys.USER_PERMISSIONS) || [];
  }

  //get accessToken
  get accessToken(): string {

    console.log("auth.service.ts func accessToken....");
    console.log("auth.service.ts func accessToken for get accessToken  =>" + this.oidcHelperService.accessToken);
    return this.oidcHelperService.accessToken;
  }

  //get for ExpirryDate accessToken
  get accessTokenExpiryDate(): Date {

    console.log("auth.service.ts func accessTokenExpiryDate  for get accessTokenExpiryDate => " + this.oidcHelperService.accessTokenExpiryDate);
    return this.oidcHelperService.accessTokenExpiryDate;
  }

  //get refreshToken
  get refreshToken(): string {
    console.log("auth.service.ts func refreshToken for get return refreshToken" + this.oidcHelperService.refreshToken);
    return this.oidcHelperService.refreshToken;
  }

  //get Session Expired
  get isSessionExpired(): boolean {
    console.log("auth.service.ts func isSessionExpired for get return isSessionExpired " + this.oidcHelperService.isSessionExpired);
    return this.oidcHelperService.isSessionExpired;
  }

  //get LoggendIn
  get isLoggedIn(): boolean {

    console.log("auth.service.ts func isLoggedIn for currentUser != Null....");

    return this.currentUser != null;
  }

  //get rememberMe
  get rememberMe(): boolean {

    console.log("auth.service.ts func rememberMe for get return rememberMe (localStorage.getDataObject)" );

    return this.localStorage.getDataObject<boolean>(DBkeys.REMEMBER_ME) === true;
  }
}
