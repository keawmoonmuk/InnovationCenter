
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

import { AppTranslationService } from './app-translation.service';
import { ThemeManager } from './theme-manager';
import { LocalStoreManager } from './local-store-manager.service';
import { DBkeys } from './db-keys';                             //DB keys
import { Utilities } from './utilities';
import { environment } from '../../environments/environment';   //enviroment

interface UserConfiguration {

  language: string;
  homeUrl: string;
  themeId: number;
  showDashboardStatistics: boolean;
  showDashboardNotifications: boolean;
  showDashboardTodo: boolean;
  showDashboardBanner: boolean;
}

@Injectable()
export class ConfigurationService {

  //Constructor Create => localStorage , thanslateionService , themeManger
  constructor(
    private localStorage: LocalStoreManager,
    private translationService: AppTranslationService,
    private themeManager: ThemeManager) {

    this._globalLanguage = this.localStorage.getDataObject<string>(DBkeys.GLOBAL_LANGUAGE);  //get global language  ==> en
    this.loadLocalChanges();                                                                 //load local
  }

  set language(value: string) {
    this._language = value;
    this.saveToLocalStore(value, DBkeys.LANGUAGE);
    this.translationService.changeLanguage(value);
  }

  get language() {
    return this._language || ConfigurationService.defaultLanguage;
  }

  //set globalLanguage
  set globalLanguage(value: string) {
    this._globalLanguage = value;
    this.saveToLocalStore(value, DBkeys.GLOBAL_LANGUAGE);
  }
  // get globalLanguage
  get globalLanguage() {
    return this._globalLanguage;
  }

  set themeId(value: number) {
    value = +value;
    this._themeId = value;
    this.saveToLocalStore(value, DBkeys.THEME_ID);
    this.themeManager.installTheme(this.themeManager.getThemeByID(value));
  }
  get themeId() {
    return this._themeId || ConfigurationService.defaultThemeId;
  }

  //set homeUrl
  set homeUrl(value: string) {
    this._homeUrl = value;
    this.saveToLocalStore(value, DBkeys.HOME_URL);
  }
  // get home Url
  get homeUrl() {
    return this._homeUrl || ConfigurationService.defaultHomeUrl;
  }

  set showDashboardStatistics(value: boolean) {
    this._showDashboardStatistics = value;
    this.saveToLocalStore(value, DBkeys.SHOW_DASHBOARD_STATISTICS);
  }
  get showDashboardStatistics() {
    return this._showDashboardStatistics != null ? this._showDashboardStatistics : ConfigurationService.defaultShowDashboardStatistics;
  }

  set showDashboardNotifications(value: boolean) {
    this._showDashboardNotifications = value;
    this.saveToLocalStore(value, DBkeys.SHOW_DASHBOARD_NOTIFICATIONS);
  }
  get showDashboardNotifications() {
    return this._showDashboardNotifications != null ? this._showDashboardNotifications : ConfigurationService.defaultShowDashboardNotifications;
  }

  set showDashboardTodo(value: boolean) {
    this._showDashboardTodo = value;
    this.saveToLocalStore(value, DBkeys.SHOW_DASHBOARD_TODO);
  }
  get showDashboardTodo() {
    return this._showDashboardTodo != null ? this._showDashboardTodo : ConfigurationService.defaultShowDashboardTodo;
  }

  set showDashboardBanner(value: boolean) {
    this._showDashboardBanner = value;
    this.saveToLocalStore(value, DBkeys.SHOW_DASHBOARD_BANNER);
  }
  get showDashboardBanner() {
    return this._showDashboardBanner != null ? this._showDashboardBanner : ConfigurationService.defaultShowDashboardBanner;
  }

  public static readonly appVersion: string = '4.0.0';
  // ***Specify default configurations here --- ระบุการกำหนดค่าเริ่มต้นที่นี่***
  public static readonly defaultLanguage: string = 'en';
  public static readonly defaultHomeUrl: string = '/';
  public static readonly defaultThemeId: number = 1;
  public static readonly defaultShowDashboardStatistics: boolean = true;
  public static readonly defaultShowDashboardNotifications: boolean = true;
  public static readonly defaultShowDashboardTodo: boolean = false;
  public static readonly defaultShowDashboardBanner: boolean = true;

  public baseUrl = environment.baseUrl || Utilities.baseUrl();                          //*******base Url********
  public tokenUrl = environment.tokenUrl || environment.baseUrl || Utilities.baseUrl(); //*******token Url*******
  public loginUrl = environment.loginUrl;                                               //*******log Url*********
  public googleClientId = environment.googleClientId;                                   //*******google********** 
  public facebookClientId = environment.facebookClientId;                               //*******fecebook********
  public fallbackBaseUrl = null;                                                        //*******base url ทางเลือก**
  //public fallbackBaseUrl = 'https://quickapp-standard.ebenmonney.com';
  // ***End of defaults***

  private _language: string = null;
  private _globalLanguage: string = null;
  private _homeUrl: string = null;
  private _themeId: number = null;
  private _showDashboardStatistics: boolean = null;
  private _showDashboardNotifications: boolean = null;
  private _showDashboardTodo: boolean = null;
  private _showDashboardBanner: boolean = null;
  private onConfigurationImported: Subject<boolean> = new Subject<boolean>();

  configurationImported$ = this.onConfigurationImported.asObservable();

  // load local change
  private loadLocalChanges() {
    // show language
    if (this.localStorage.exists(DBkeys.LANGUAGE)) {
      this._language = this.localStorage.getDataObject<string>(DBkeys.LANGUAGE);
      this.translationService.changeLanguage(this._language);
    } else {
      this.resetLanguage();
    }
    // show theme
    if (this.localStorage.exists(DBkeys.THEME_ID)) {
      this._themeId = this.localStorage.getDataObject<number>(DBkeys.THEME_ID);
      this.themeManager.installTheme(this.themeManager.getThemeByID(this._themeId));
    } else {
      this.resetTheme();
    }
    //show home
    if (this.localStorage.exists(DBkeys.HOME_URL)) {
      this._homeUrl = this.localStorage.getDataObject<string>(DBkeys.HOME_URL);
    }
    //show dashboard
    if (this.localStorage.exists(DBkeys.SHOW_DASHBOARD_STATISTICS)) {
      this._showDashboardStatistics = this.localStorage.getDataObject<boolean>(DBkeys.SHOW_DASHBOARD_STATISTICS);
    }
    //show dashboard notifications
    if (this.localStorage.exists(DBkeys.SHOW_DASHBOARD_NOTIFICATIONS)) {
      this._showDashboardNotifications = this.localStorage.getDataObject<boolean>(DBkeys.SHOW_DASHBOARD_NOTIFICATIONS);
    }
    //show dashboard todo
    if (this.localStorage.exists(DBkeys.SHOW_DASHBOARD_TODO)) {
      this._showDashboardTodo = this.localStorage.getDataObject<boolean>(DBkeys.SHOW_DASHBOARD_TODO);
    }
    //show dashboard banner
    if (this.localStorage.exists(DBkeys.SHOW_DASHBOARD_BANNER)) {
      this._showDashboardBanner = this.localStorage.getDataObject<boolean>(DBkeys.SHOW_DASHBOARD_BANNER);
    }
  }

  //SaveTolocalStore
  private saveToLocalStore(data: any, key: string) {
    setTimeout(() => this.localStorage.savePermanentData(data, key));
  }

  //import json value
  public import(jsonValue: string) {
    this.clearLocalChanges();

    if (jsonValue) {
      const importValue: UserConfiguration = Utilities.JsonTryParse(jsonValue);

      if (importValue.language != null) {
        this.language = importValue.language;
      }

      if (importValue.themeId != null) {
        this.themeId = +importValue.themeId;
      }

      if (importValue.homeUrl != null) {
        this.homeUrl = importValue.homeUrl;
      }

      if (importValue.showDashboardStatistics != null) {
        this.showDashboardStatistics = importValue.showDashboardStatistics;
      }

      if (importValue.showDashboardNotifications != null) {
        this.showDashboardNotifications = importValue.showDashboardNotifications;
      }

      if (importValue.showDashboardTodo != null) {
        this.showDashboardTodo = importValue.showDashboardTodo;
      }

      if (importValue.showDashboardBanner != null) {
        this.showDashboardBanner = importValue.showDashboardBanner;
      }
    }

    this.onConfigurationImported.next();
  }

  //export change Only
  public export(changesOnly = true): string {
    const exportValue: UserConfiguration = {
      language: changesOnly ? this._language : this.language,
      themeId: changesOnly ? this._themeId : this.themeId,
      homeUrl: changesOnly ? this._homeUrl : this.homeUrl,
      showDashboardStatistics: changesOnly ? this._showDashboardStatistics : this.showDashboardStatistics,
      showDashboardNotifications: changesOnly ? this._showDashboardNotifications : this.showDashboardNotifications,
      showDashboardTodo: changesOnly ? this._showDashboardTodo : this.showDashboardTodo,
      showDashboardBanner: changesOnly ? this._showDashboardBanner : this.showDashboardBanner
    };

    return JSON.stringify(exportValue);
  }

  //clear localchanges
  public clearLocalChanges() {
    this._language = null;
    this._themeId = null;
    this._homeUrl = null;
    this._showDashboardStatistics = null;
    this._showDashboardNotifications = null;
    this._showDashboardTodo = null;
    this._showDashboardBanner = null;

    this.localStorage.deleteData(DBkeys.LANGUAGE);
    this.localStorage.deleteData(DBkeys.THEME_ID);
    this.localStorage.deleteData(DBkeys.HOME_URL);
    this.localStorage.deleteData(DBkeys.SHOW_DASHBOARD_STATISTICS);
    this.localStorage.deleteData(DBkeys.SHOW_DASHBOARD_NOTIFICATIONS);
    this.localStorage.deleteData(DBkeys.SHOW_DASHBOARD_TODO);
    this.localStorage.deleteData(DBkeys.SHOW_DASHBOARD_BANNER);

    this.resetLanguage();
    this.resetTheme();
  }

  //reset Language
  private resetLanguage() {

    if (this._globalLanguage) {
      this._language = this.translationService.changeLanguage(this._globalLanguage);
    } else {
      const language = this.translationService.useBrowserLanguage();

      if (language) {
        this._language = language;
      } else {
        this._language = this.translationService.useDefaultLangage();
      }
    }
  }
  //reset theme
  private resetTheme() {
    this.themeManager.installTheme();
    this._themeId = null;
  }
}
