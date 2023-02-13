

//definición de interfaces correspondientes
//a los ViewModels de las APIs

export interface IPuntoVenta {
  idPuntoVenta: number;
  idEmpresa: number;
  codigo: string;
  descripcion: string;
  esActivo: boolean;
}

export interface IPost {
  postID: number;
  title: string;
  description: string;
  status: number;
  userName: string;
  creationDate: Date;
  comments: IComment[];
}

export interface IComment {
  commentID: number;
  postID: number;
  comments: string;
  creationUser: string;
  creationDate: Date;
  userName: string;
  commentType: number;
}
