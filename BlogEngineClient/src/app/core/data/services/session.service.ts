import { Injectable } from '@angular/core';
import { ActionCodeEnum } from '../../enums/form.enum';
import { IAuthorizationData, IMenu, IPermissionAction, ISignInData } from '../../store/app.store.interface';
import { PermissionAction } from '../../store/app.store.model';
import { CryptoService } from './crypto.service';

@Injectable({
  providedIn: 'root'
})
export class SessionService {
  private keySignInData = 'sign_in_data';
  private keyAuthorizationData = 'authorization_data';
  private keyCurrentMenuData = 'current_menu_data';
  public permission: IPermissionAction = new PermissionAction();

  constructor(private crypto: CryptoService) { }

  public setSignInData(data: ISignInData) {
    const encrypted = this.crypto.encrypt(JSON.stringify(data));
    sessionStorage.setItem(this.keySignInData, encrypted);
  }
  public getSignInData(): ISignInData {
    const encrypted = sessionStorage.getItem(this.keySignInData);
    return JSON.parse(this.crypto.decrypt(encrypted));
  }

  public setAutorizationData(data: IAuthorizationData) {
    const encrypted = this.crypto.encrypt(JSON.stringify(data));
    sessionStorage.setItem(this.keyAuthorizationData, encrypted);
  }
  public getAutorizationData(): IAuthorizationData {
    const encrypted = sessionStorage.getItem(this.keyAuthorizationData);
    return JSON.parse(this.crypto.decrypt(encrypted));
  }

  public setCurrentMenuData(data: IMenu) {
    const encrypted = this.crypto.encrypt(JSON.stringify(data));
    sessionStorage.setItem(this.keyCurrentMenuData, encrypted);
  }
  public getCurrentMenuData(): IMenu {
    const encrypted = sessionStorage.getItem(this.keyCurrentMenuData);
    return JSON.parse(this.crypto.decrypt(encrypted));
  }

  public getCheckAuthorizationOption(actionCode: string) {
    const currentMenu = this.getCurrentMenuData();
    const authorization = this.getAutorizationData();
    if (authorization == null) return false;
    if (currentMenu == null) return false;
    const access = authorization.permissions.find(x => x.menuGuid == currentMenu.guid && x.actionCode == actionCode);
    if (access == null) return false;
    return true;
  }

  public setPermission = (currentMenu: IMenu) => {
    const authorization = this.getAutorizationData();
    this.permission.access = authorization.permissions.find(x => x.menuGuid == currentMenu.guid && x.actionCode == ActionCodeEnum.ACCESS) != null;
    this.permission.add = authorization.permissions.find(x => x.menuGuid == currentMenu.guid && x.actionCode == ActionCodeEnum.ADD) != null;
    this.permission.update = authorization.permissions.find(x => x.menuGuid == currentMenu.guid && x.actionCode == ActionCodeEnum.UPDATE) != null;
    this.permission.delete = authorization.permissions.find(x => x.menuGuid == currentMenu.guid && x.actionCode == ActionCodeEnum.DELETE) != null;
    this.permission.aproval = authorization.permissions.find(x => x.menuGuid == currentMenu.guid && x.actionCode == ActionCodeEnum.APROVAL) != null;
    this.permission.reject = authorization.permissions.find(x => x.menuGuid == currentMenu.guid && x.actionCode == ActionCodeEnum.REJECT) != null;
    this.permission.view = authorization.permissions.find(x => x.menuGuid == currentMenu.guid && x.actionCode == ActionCodeEnum.VIEW) != null;
    this.permission.submit = authorization.permissions.find(x => x.menuGuid == currentMenu.guid && x.actionCode == ActionCodeEnum.SUBMIT) != null;
  }
}
