// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { Component, NgZone, OnInit, ViewChild, Input } from '@angular/core';

import { AlertService, MessageSeverity } from '../../services/alert.service';
import { AccountService } from '../../services/account.service';
import { Utilities } from '../../services/utilities';
import { LocalStoreManager } from '../../services/local-store-manager.service';
import { DBkeys } from '../../services/db-keys';
import { User } from '../../models/user.model';
import { UserEdit } from '../../models/user-edit.model';
import { Role } from '../../models/role.model';
import { Permission } from '../../models/permission.model';


@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent implements OnInit {

  public isEditMode = false;
  public isNewUser = false;
  public isSaving = false;
  public isSendingEmail = false;
  public isChangePassword = false;
  public isEditingSelf = false;
  public userHasPassword = true;
  public showValidationErrors = false;
  public uniqueId: string = Utilities.uniqueId();
  public emailConfirmed: boolean;
  public user: User = new User();
  public userEdit: UserEdit;
  public allRoles: Role[] = [];

  public formResetToggle = true;

  public changesSavedCallback: () => void;
  public changesFailedCallback: () => void;
  public changesCancelledCallback: () => void;

  @Input()
  isViewOnly: boolean;

  @Input()
  isGeneralEditor = false;



  @ViewChild('f')
  public form;

  // ViewChilds Required because ngIf hides template variables from global scope
  @ViewChild('userName')
  public userName;

  @ViewChild('userPassword')
  public userPassword;

  @ViewChild('email')
  public email;

  @ViewChild('currentPassword')
  public currentPassword;

  @ViewChild('newPassword')
  public newPassword;

  @ViewChild('confirmPassword')
  public confirmPassword;

  @ViewChild('roles')
  public roles;


  constructor(private ngZone: NgZone, private alertService: AlertService, private accountService: AccountService, private localStorage: LocalStoreManager) {
  }
    
  ngOnInit() {
    if (!this.isGeneralEditor) {
      this.loadCurrentUserData();
    }
  }

  private loadCurrentUserData() {
    this.alertService.startLoadingMessage();

    this.loadCurrentUserPasswordStatus();

    if (this.canViewAllRoles) {
      this.accountService.getUserAndRoles().subscribe(results => this.onCurrentUserDataLoadSuccessful(results[0], results[1]), error => this.onCurrentUserDataLoadFailed(error));
    } else {
      this.accountService.getUser().subscribe(user => this.onCurrentUserDataLoadSuccessful(user, user.roles.map(x => new Role(x))), error => this.onCurrentUserDataLoadFailed(error));
    }
  }

  private onCurrentUserDataLoadSuccessful(user: User, roles: Role[]) {
    this.alertService.stopLoadingMessage();
    this.user = user;
    this.allRoles = roles;
    this.emailConfirmed = this.user.emailConfirmed;

    const verificationEmailSent = this.localStorage.getDataObject<boolean>(this.getDBkey_VERIFICATION_EMAIL_SENT(this.user.id));

    if (!verificationEmailSent && !this.emailConfirmed) {
      const sendVerificationEmailWindowsFuncName = 'userinfo_sendVerificationEmail';
      window[sendVerificationEmailWindowsFuncName] = this.sendVerificationEmail.bind(this);

      const confirmEmailMsg = `Your account email has not been verified. <a href="javascript:;" onclick="window.${sendVerificationEmailWindowsFuncName}()">Click here to resend verification email</a>`;
      this.alertService.showStickyMessage('Email not verified!', confirmEmailMsg, MessageSeverity.info, null, () => window[sendVerificationEmailWindowsFuncName] = null);
    }
  }

  private onCurrentUserDataLoadFailed(error: any) {
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage('Load Error', `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessages(error)}"`,
      MessageSeverity.error, error);

    this.user = new User();
  }

  private loadCurrentUserPasswordStatus() {
    this.accountService.getUserHasPassword()
      .subscribe(hasPassword => {
        this.userHasPassword = hasPassword;
      },
        error => {
          this.alertService.showStickyMessage('Load Error', `Error retrieving user password status from the server.\r\nErrors: "${Utilities.getHttpResponseMessages(error)}"`,
            MessageSeverity.error, error);
        });
  }


  sendVerificationEmail() {
    this.ngZone.run(() => {
      this.isSendingEmail = true;
      this.alertService.resetStickyMessage();
      this.alertService.startLoadingMessage('Sending verification email...');

      this.accountService.sendConfirmEmail()
        .subscribe(result => {
          this.isSendingEmail = false;
          this.alertService.stopLoadingMessage();
          this.alertService.showMessage('Verification Email Sent', 'Please check your email', MessageSeverity.success);
          this.localStorage.saveSyncedSessionData(true, this.getDBkey_VERIFICATION_EMAIL_SENT(this.user.id));
        },
          error => {
            this.isSendingEmail = false;
            this.alertService.stopLoadingMessage();
            this.alertService.showStickyMessage('Verification Email Not Sent', `Unable to send verification email.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
              MessageSeverity.error, error);
          });
    });
  }


  getRoleByName(name: string) {
    return this.allRoles.find((r) => r.name === name);
  }



  showErrorAlert(caption: string, message: string) {
    this.alertService.showMessage(caption, message, MessageSeverity.error);
  }


  deletePasswordFromUser(user: UserEdit | User) {
    const userEdit = user as UserEdit;

    delete userEdit.currentPassword;
    delete userEdit.newPassword;
    delete userEdit.confirmPassword;
  }


  edit() {
    if (!this.isGeneralEditor) {
      this.isEditingSelf = true;
      this.userEdit = new UserEdit();
      Object.assign(this.userEdit, this.user);
    } else {
      if (!this.userEdit) {
        this.userEdit = new UserEdit();
      }

      this.isEditingSelf = this.accountService.currentUser ? this.userEdit.id === this.accountService.currentUser.id : false;
    }

    this.isEditMode = true;
    this.showValidationErrors = true;
    this.isChangePassword = false;
  }


  save() {
    this.isSaving = true;
    this.alertService.startLoadingMessage('Saving changes...');

    if (this.isNewUser) {
      this.accountService.newUser(this.userEdit).subscribe(user => this.saveSuccessHelper(user), error => this.saveFailedHelper(error));
    } else {
      this.accountService.updateUser(this.userEdit).subscribe(response => this.saveSuccessHelper(), error => this.saveFailedHelper(error));
    }
  }


  private saveSuccessHelper(user?: User) {
    this.testIsRoleUserCountChanged(this.user, this.userEdit);

    if (user) {
      Object.assign(this.userEdit, user);
    }

    this.isSaving = false;
    this.alertService.stopLoadingMessage();
    this.isChangePassword = false;
    this.showValidationErrors = false;

    this.deletePasswordFromUser(this.userEdit);
    Object.assign(this.user, this.userEdit);
    this.userEdit = new UserEdit();
    this.resetForm();


    if (this.isGeneralEditor) {
      if (this.isNewUser) {
        this.alertService.showMessage('Success', `User \"${this.user.userName}\" was created successfully`, MessageSeverity.success);
      } else if (!this.isEditingSelf) {
        this.alertService.showMessage('Success', `Changes to user \"${this.user.userName}\" was saved successfully`, MessageSeverity.success);
      }
    }

    if (this.isEditingSelf) {
      this.alertService.showMessage('Success', 'Changes to your User Profile was saved successfully', MessageSeverity.success);
      this.refreshLoggedInUser();
    }

    this.isEditMode = false;


    if (this.changesSavedCallback) {
      this.changesSavedCallback();
    }
  }


  private saveFailedHelper(error: any) {
    this.isSaving = false;
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage('Save Error', 'The below errors occured whilst saving your changes:', MessageSeverity.error, error);
    this.alertService.showStickyMessage(error, null, MessageSeverity.error);

    if (this.changesFailedCallback) {
      this.changesFailedCallback();
    }
  }



  private testIsRoleUserCountChanged(currentUser: User, editedUser: User) {

    const rolesAdded = this.isNewUser ? editedUser.roles : editedUser.roles.filter(role => currentUser.roles.indexOf(role) === -1);
    const rolesRemoved = this.isNewUser ? [] : currentUser.roles.filter(role => editedUser.roles.indexOf(role) === -1);

    const modifiedRoles = rolesAdded.concat(rolesRemoved);

    if (modifiedRoles.length) {
      setTimeout(() => this.accountService.onRolesUserCountChanged(modifiedRoles));
    }
  }



  cancel() {
    if (this.isGeneralEditor) {
      this.userEdit = this.user = new UserEdit();
    } else {
      this.userEdit = new UserEdit();
    }

    this.showValidationErrors = false;
    this.resetForm();

    this.alertService.showMessage('Cancelled', 'Operation cancelled by user', MessageSeverity.default);
    this.alertService.resetStickyMessage();

    if (!this.isGeneralEditor) {
      this.isEditMode = false;
    }

    if (this.changesCancelledCallback) {
      this.changesCancelledCallback();
    }
  }


  close() {
    this.userEdit = this.user = new UserEdit();
    this.showValidationErrors = false;
    this.resetForm();
    this.isEditMode = false;

    if (this.changesSavedCallback) {
      this.changesSavedCallback();
    }
  }



  private refreshLoggedInUser() {
    this.accountService.refreshLoggedInUser()
      .subscribe(user => {
        this.loadCurrentUserData();
      },
        error => {
          this.alertService.resetStickyMessage();
          this.alertService.showStickyMessage('Refresh failed', 'An error occured whilst refreshing logged in user information from the server', MessageSeverity.error, error);
        });
  }


  changePassword() {
    this.isChangePassword = true;
  }


  unlockUser() {
    this.isSaving = true;
    this.alertService.startLoadingMessage('Unblocking user...');


    this.accountService.unblockUser(this.userEdit.id)
      .subscribe(() => {
        this.isSaving = false;
        this.userEdit.isLockedOut = false;
        this.alertService.stopLoadingMessage();
        this.alertService.showMessage('Success', 'User has been successfully unblocked', MessageSeverity.success);
      },
        error => {
          this.isSaving = false;
          this.alertService.stopLoadingMessage();
          this.alertService.showStickyMessage('Unblock Error', 'The below errors occured whilst unblocking the user:', MessageSeverity.error, error);
          this.alertService.showStickyMessage(error, null, MessageSeverity.error);
        });
  }


  resetForm(replace = false) {
    this.isChangePassword = false;

    if (!replace) {
      this.form.reset();
    } else {
      this.formResetToggle = false;

      setTimeout(() => {
        this.formResetToggle = true;
      });
    }
  }


  newUser(allRoles: Role[]) {
    this.isGeneralEditor = true;
    this.isNewUser = true;

    this.allRoles = [...allRoles];
    this.user = this.userEdit = new UserEdit();
    this.userEdit.isEnabled = true;
    this.edit();

    return this.userEdit;
  }

  editUser(user: User, allRoles: Role[]) {
    if (user) {
      this.isGeneralEditor = true;
      this.isNewUser = false;

      this.setRoles(user, allRoles);
      this.user = new User();
      this.userEdit = new UserEdit();
      Object.assign(this.user, user);
      Object.assign(this.userEdit, user);
      this.edit();

      if (this.isEditingSelf) {
        this.loadCurrentUserPasswordStatus();
      }

      return this.userEdit;
    } else {
      return this.newUser(allRoles);
    }
  }


  displayUser(user: User, allRoles?: Role[]) {

    this.user = new User();
    Object.assign(this.user, user);
    this.deletePasswordFromUser(this.user);
    this.setRoles(user, allRoles);

    this.isEditMode = false;
  }



  private setRoles(user: User, allRoles?: Role[]) {

    this.allRoles = allRoles ? [...allRoles] : [];

    if (user.roles) {
      for (const ur of user.roles) {
        if (!this.allRoles.some(r => r.name === ur)) {
          this.allRoles.unshift(new Role(ur));
        }
      }
    }
  }

  private getDBkey_VERIFICATION_EMAIL_SENT(userId: string) {
    return `verification_email_sent:${userId}`;
  }


  get canViewAllRoles() {
    return this.accountService.userHasPermission(Permission.viewRolesPermission);
  }

  get canAssignRoles() {
    return this.accountService.userHasPermission(Permission.assignRolesPermission);
  }
}
