export interface ISignInData {
  token: string;
  success: boolean;
  rolGuid: string;
  userLogin: string;
  fullName: string;
  rolName: string;
}

export interface IMenu {
  menuID: number;
  menuBaseID: number;
  guid: string;
  url: string;
}

export interface IPermission {
  menuGuid: string;
  rolGuid: string;
  actionCode: string;
}

export interface IAuthorizationData {
  menus: IMenu[];
  permissions: IPermission[];
}

export interface IPermissionAction {
  access: boolean;
  add: boolean;
  update: boolean,
  delete: boolean;
  aproval: boolean;
  reject: boolean;
  view: boolean;
  submit: boolean;
}
