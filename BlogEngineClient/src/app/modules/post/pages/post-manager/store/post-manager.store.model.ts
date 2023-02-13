

import { FormType } from 'src/app/core/enums/form.enum';
import { IDataGridDefinition, IDataGridSource } from 'src/app/shared/lib/interfaces/data-grid.interface';
import { IComboList } from 'src/app/shared/lib/interfaces/forms.interface';
import { IComment, IPost, IPuntoVenta } from '../../../interfaces/post.interface';

export class FormSearch {
  rolGuid: string = null;
  userName: string = null;
}


export class Post implements IPost {
  postID: number = null;
  title: string = null;
  description: string = null;
  status: number = null;
  creationDate: Date = new Date();
  userName: string = null;
  comments: IComment[] = [];
}

export class Comment implements IComment {
  commentID: number = 0;
  postID: number = null;
  comments: string = null;
  creationUser: string = null;
  creationDate: Date = new Date();
  userName: string = null;
  commentType: number = null;
}

export class FormRegistroModel {

  title: string = 'Nuevo Punto de venta';

  error: any = null;

  loading: boolean = false;

  formType: FormType = FormType.CREATE;

  model: IPost = new Post();

  id: number;

  modoConsulta: boolean;
}

export class ContainerModel {

  title: string = 'All Posts';

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
    skip: 0,
    totalPages: 0
  };
}

export class GestionPuntoVentaModel {

  container = new ContainerModel();

  formRegister = new FormRegistroModel();

}

export const initialState = new GestionPuntoVentaModel();


