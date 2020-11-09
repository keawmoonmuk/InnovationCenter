
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthService } from './auth.service';
import { EndpointBase } from './endpoint-base.service';
import { ConfigurationService } from './configuration.service';

@Injectable()
export class AccountEndpoint extends EndpointBase {

  get usersUrl() { return this.configurations.baseUrl + '/api/account/users'; }                       //get  userUrl 
  get usersPublicUrl() { return this.configurations.baseUrl + '/api/account/public/users'; }          // get usersPublicUrl
  get userByUserNameUrl() { return this.configurations.baseUrl + '/api/account/users/username'; }     //get userByUserNameUrl
  get userHasPasswordUrl() { return this.configurations.baseUrl + '/api/account/users/haspassword'; } //get userHasPasswordUrl
  get currentUserUrl() { return this.configurations.baseUrl + '/api/account/users/me'; }
  get currentUserPreferencesUrl() { return this.configurations.baseUrl + '/api/account/users/me/preferences'; }
  get sendConfirmEmailUrl() { return this.configurations.baseUrl + '/api/account/users/me/sendconfirmemail'; }
  get confirmEmailUrl() { return this.configurations.baseUrl + '/api/account/public/confirmemail'; }
  get recoverPasswordUrl() { return this.configurations.baseUrl + '/api/account/public/recoverpassword'; }
  get resetPasswordUrl() { return this.configurations.baseUrl + '/api/account/public/resetpassword'; }
  get unblockUserUrl() { return this.configurations.baseUrl + '/api/account/users/unblock'; }
  get rolesUrl() { return this.configurations.baseUrl + '/api/account/roles'; }
  get roleByRoleNameUrl() { return this.configurations.baseUrl + '/api/account/roles/name'; }
  get permissionsUrl() { return this.configurations.baseUrl + '/api/account/permissions'; }

  //-----add patient---------
  get patientallUrl() { return this.configurations.baseUrl + '/api/Patients/patientall'; }     //get api url patient all

  //constructor create => configurations , http , authService
  constructor(private configurations: ConfigurationService, http: HttpClient, authService: AuthService) {

    //to path => src/app/service/endpoint-base.service.ts
    super(http, authService);
   
  }

  //http    ==> userEndpoint 
  getUserEndpoint<T>(userId?: string): Observable<T> {
    const endpointUrl = userId ? `${this.usersUrl}/${userId}` : this.currentUserUrl;
    console.log("get user endpoint ==>" + endpointUrl);

    return this.http.get<T>(endpointUrl, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getUserEndpoint(userId));
      }));
  }

  //http  ==> get User by Username endpoint 
  getUserByUserNameEndpoint<T>(userName: string): Observable<T> {
    const endpointUrl = `${this.userByUserNameUrl}/${userName}`;

    return this.http.get<T>(endpointUrl, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getUserByUserNameEndpoint(userName));
      }));
  } 

  //http ==> get user has password endpoint
  getUserHasPasswordEndpoint<T>(userId: string): Observable<T> {
    const endpointUrl = `${this.userHasPasswordUrl}/${userId}`;

    return this.http.get<T>(endpointUrl, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getUserEndpoint(userId));
      }));
  }

  //http  ==> get users endpoint
  getUsersEndpoint<T>(page?: number, pageSize?: number): Observable<T> {
    const endpointUrl = page && pageSize ? `${this.usersUrl}/${page}/${pageSize}` : this.usersUrl;

    return this.http.get<T>(endpointUrl, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getUsersEndpoint(page, pageSize));
      }));
  }

  //http ==> post new user endpoint
  getNewUserEndpoint<T>(userObject: any, isPublicRegistration?: boolean): Observable<T> {
    const endpointUrl = isPublicRegistration ? this.usersPublicUrl : this.usersUrl;

    return this.http.post<T>(endpointUrl, JSON.stringify(userObject), this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getNewUserEndpoint(userObject));
      }));
  }
  //http ==> put update user endpoint
  getUpdateUserEndpoint<T>(userObject: any, userId?: string): Observable<T> {
    const endpointUrl = userId ? `${this.usersUrl}/${userId}` : this.currentUserUrl;

    return this.http.put<T>(endpointUrl, JSON.stringify(userObject), this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getUpdateUserEndpoint(userObject, userId));
      }));
  }

  getPatchUpdateUserEndpoint<T>(patch: {}, userId?: string): Observable<T>;   //get Pactch Update
  getPatchUpdateUserEndpoint<T>(value: any, op: string, path: string, from?: any, userId?: string): Observable<T>;  //getPatchtUpdate

  //get pactch update
  getPatchUpdateUserEndpoint<T>(valueOrPatch: any, opOrUserId?: string, path?: string, from?: any, userId?: string): Observable<T> {
    let endpointUrl: string;
    let patchDocument: {};

    if (path) {
      endpointUrl = userId ? `${this.usersUrl}/${userId}` : this.currentUserUrl;
      patchDocument = from ?
        [{ value: valueOrPatch, path, op: opOrUserId, from }] :
        [{ value: valueOrPatch, path, op: opOrUserId }];
    } else {
      endpointUrl = opOrUserId ? `${this.usersUrl}/${opOrUserId}` : this.currentUserUrl;
      patchDocument = valueOrPatch;
    }

    return this.http.patch<T>(endpointUrl, JSON.stringify(patchDocument), this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getPatchUpdateUserEndpoint(valueOrPatch, opOrUserId, path, from, userId));
      }));
  }

  //http ===> get user preferences endpoint
  getUserPreferencesEndpoint<T>(): Observable<T> {

    return this.http.get<T>(this.currentUserPreferencesUrl, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getUserPreferencesEndpoint());
      }));
  }
  //http ==> put update user prefernces endpoint
  getUpdateUserPreferencesEndpoint<T>(configuration: string): Observable<T> {
    return this.http.put<T>(this.currentUserPreferencesUrl, JSON.stringify(configuration), this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getUpdateUserPreferencesEndpoint(configuration));
      }));
  }
  //http ==> post send confirm email endpoint 
  getSendConfirmEmailEndpoint<T>(): Observable<T> {
    const endpointUrl = this.sendConfirmEmailUrl;

    return this.http.post<T>(endpointUrl, null, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getSendConfirmEmailEndpoint());
      }));
  }
  //http  ==> put confirm user account endpoint 
  getConfirmUserAccountEndpoint<T>(userId: string, confirmationCode: string): Observable<T> {
    const endpointUrl = `${this.confirmEmailUrl}?userid=${userId}&code=${confirmationCode}`;

    return this.http.put<T>(endpointUrl, null, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getConfirmUserAccountEndpoint(userId, confirmationCode));
      }));
  }
  //http ==> post recover password end point 
  getRecoverPasswordEndpoint<T>(usernameOrEmail: string): Observable<T> {
    const endpointUrl = this.recoverPasswordUrl;

    return this.http.post<T>(endpointUrl, JSON.stringify({ usernameOrEmail }), this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getRecoverPasswordEndpoint(usernameOrEmail));
      }));
  }
  //http ==> post reset password endpoint 
  getResetPasswordEndpoint<T>(usernameOrEmail: string, newPassword: string, resetCode: string): Observable<T> {
    const endpointUrl = this.resetPasswordUrl;

    return this.http.post<T>(endpointUrl, JSON.stringify({ usernameOrEmail, password: newPassword, resetcode: resetCode }), this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getResetPasswordEndpoint(usernameOrEmail, newPassword, resetCode));
      }));
  }
  //http ==> put un block user endpoint
  getUnblockUserEndpoint<T>(userId: string): Observable<T> {
    const endpointUrl = `${this.unblockUserUrl}/${userId}`;

    return this.http.put<T>(endpointUrl, null, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getUnblockUserEndpoint(userId));
      }));
  }
  //http ==> get delete uesr endpoint
  getDeleteUserEndpoint<T>(userId: string): Observable<T> {
    const endpointUrl = `${this.usersUrl}/${userId}`;

    return this.http.delete<T>(endpointUrl, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getDeleteUserEndpoint(userId));
      }));
  }

//http ==> get role endpoint 
  getRoleEndpoint<T>(roleId: string): Observable<T> {
    const endpointUrl = `${this.rolesUrl}/${roleId}`;

    return this.http.get<T>(endpointUrl, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getRoleEndpoint(roleId));
      }));
  }

  //http ==> get role by role name endpoint 
  getRoleByRoleNameEndpoint<T>(roleName: string): Observable<T> {
    const endpointUrl = `${this.roleByRoleNameUrl}/${roleName}`;

    return this.http.get<T>(endpointUrl, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getRoleByRoleNameEndpoint(roleName));
      }));
  }

//http ==> get roles endpoint 
  getRolesEndpoint<T>(page?: number, pageSize?: number): Observable<T> {
    const endpointUrl = page && pageSize ? `${this.rolesUrl}/${page}/${pageSize}` : this.rolesUrl;

    return this.http.get<T>(endpointUrl, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getRolesEndpoint(page, pageSize));
      }));
  }
  //http ==> post new role endponit
  getNewRoleEndpoint<T>(roleObject: any): Observable<T> {

    return this.http.post<T>(this.rolesUrl, JSON.stringify(roleObject), this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getNewRoleEndpoint(roleObject));
      }));
  }
  //http ==> put update role endpoint 
  getUpdateRoleEndpoint<T>(roleObject: any, roleId: string): Observable<T> {
    const endpointUrl = `${this.rolesUrl}/${roleId}`;

    return this.http.put<T>(endpointUrl, JSON.stringify(roleObject), this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getUpdateRoleEndpoint(roleObject, roleId));
      }));
  }
  // http ===> method delete for delete role endpoint 
  getDeleteRoleEndpoint<T>(roleId: string): Observable<T> {
    const endpointUrl = `${this.rolesUrl}/${roleId}`;

    return this.http.delete<T>(endpointUrl, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getDeleteRoleEndpoint(roleId));
      }));
  }

  //http ==> get permission endpoint 
  getPermissionsEndpoint<T>(): Observable<T> {

    return this.http.get<T>(this.permissionsUrl, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getPermissionsEndpoint());
      }));
  }
}
