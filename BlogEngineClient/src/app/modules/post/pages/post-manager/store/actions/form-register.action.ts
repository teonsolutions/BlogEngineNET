
import { catchError, map } from 'rxjs/operators';
import { throwError, forkJoin } from 'rxjs';
import { FormRegistroModel, initialState, Post } from '../post-manager.store.model';

import { IComment, IPuntoVenta } from '../../../../interfaces/post.interface';
import update from "immutability-helper";
import { DataService } from 'src/app/core/data/data.service';
import { BaseAction } from 'src/app/core/utils/base-action';
import { FormType } from 'src/app/core/enums/form.enum';

export class FormRegisterAction extends BaseAction<FormRegistroModel> {
  constructor(
    getState: () => FormRegistroModel,
    setState: (newState: FormRegistroModel) => void,
    private dataService: DataService
  ) {
    super(getState, setState);
  }

  reset = () => {
    this.setState(initialState.formRegister);
  }

  postSaveSuccess = () => {
    const state = this.getState();
    this.setState({ ...state, loading: false, error: null });
  }

  postSaveError = (error: any) => {
    const state = this.getState();
    this.setState({
      ...state,
      loading: false,
      error: error,
    });
  }

  postSavedSuccess = () => {
    const state = this.getState();
    this.setState({ ...state, loading: false, error: null });
  };

  postSavedBegin = () => {
    const state = this.getState();
    this.setState({ ...state, loading: false, error: null, });
  };

  postSavedError = (error: any) => {
    const state = this.getState();
    this.setState({
      ...state,
      loading: false,
      error,
    });
  };

  setProperties = (model: Post) => {
    const state = this.getState();
    let data = state.model;
    data.postID = model.postID;
    data.title = model.title;
    data.description = model.description;
    data.status = 0;
    data.userName = model.userName;
    this.setState(update(state, { model: { $set: data } }));
  };

  create = () => {
    const model = this.getState().model;
    this.postSavedBegin();
    return this.dataService.posts().create(model).pipe(
      map(r => {
        this.postSavedSuccess();
        return r;
      }),
      catchError(e => {
        return throwError(e);
      })
    );
  }

  setNew = () => {
    const state = this.getState();
    this.setState({
      ...state,
      title: "New Post",
      id: 0,
      model: new Post(),
      formType: FormType.CREATE,
    });
  };

  setUpdate = (id: number) => {
    const state = this.getState();
    this.setState({
      ...state,
      title: "Edit Post",
      id: id,
      formType: FormType.EDIT,
    });
  };

  setView = (id: number) => {
    const state = this.getState();
    this.setState({
      ...state,
      title: "View Post",
      id: id,
      formType: FormType.VIEW,
    });
  };

  update = () => {
    const model = this.getState().model;
    this.postSavedBegin();
    return this.dataService.posts().update(model).pipe(
      map(r => {
        this.postSavedSuccess();
        return r;
      }),
      catchError(e => {
        return throwError(e);
      })
    );
  }

  setDataSuccess = (value: any) => {
    const state = this.getState();
    const newState = update(state, {
      loading: { $set: false },
      model: { $set: value },
    });
    this.setState(newState);
  };

  setDatosError = (error: any) => {
    const state = this.getState();
    const newState = update(state, {
      loading: { $set: false },
      error: { $set: error },
    });
    this.setState(newState);
  };

  getById = (id: number) => {
    return new Promise((resolve, reject) => {
      this.dataService
        .posts()
        .getById(id)
        .subscribe(
          (resp) => {
            const data = {
              ...resp,
            };
            this.setDataSuccess(data);
            resolve(resp);
          },
          (error) => {
            this.setDatosError(error);
          }
        );
    });
  };

  addComment = (model: IComment) => {
    const id = this.getState().id;
    console.log("postID...", id)
    model.postID = id;
    this.postSavedBegin();
    return this.dataService.comments().create(model).pipe(
      map(r => {
        this.postSavedSuccess();
        return r;
      }),
      catchError(e => {
        return throwError(e);
      })
    );
  }

  reject = (data: any) => {
    this.postSavedBegin();
    return this.dataService.posts().reject(data).pipe(
      map(r => {
        this.postSavedSuccess();
        return r;
      }),
      catchError(e => {
        return throwError(e);
      })
    );
  }

}
