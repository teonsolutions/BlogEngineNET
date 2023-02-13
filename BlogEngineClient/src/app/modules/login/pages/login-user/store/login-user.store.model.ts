import { FormType } from "src/app/core/enums/form.enum";
import { IDataGridDefinition, IDataGridSource } from "src/app/shared/lib/interfaces/data-grid.interface";
import { IComboList } from "src/app/shared/lib/interfaces/forms.interface";
import { ILogin } from "../../../interfaces/login.interface";



export class FormSearch {

}

export class Login implements ILogin {
  userLogin: string = null;
  password: string = null;

}

export class ContainerModel {

  title: string = 'Titulo';

  loading: boolean = false;

  error: any = null;

  formSearch = new FormSearch();

  gridSource: IDataGridSource<any> = {
    items: [],
    page: 1,
    pageSize: 10,
    total: 0,
    orderBy: null,
    orderDir: null,
    skip: 0
  };
}

export class LoginUserModel {

  container = new ContainerModel();

}

export const initialState = new LoginUserModel();

