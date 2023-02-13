import { IPermissionAction } from "./app.store.interface";

export class PermissionAction implements IPermissionAction {
  access: boolean = false;
  add: boolean = false;
  update: boolean = false;
  delete: boolean = false;
  aproval: boolean = false;
  reject: boolean = false;
  view: boolean = false;
  submit: boolean = false;
}
