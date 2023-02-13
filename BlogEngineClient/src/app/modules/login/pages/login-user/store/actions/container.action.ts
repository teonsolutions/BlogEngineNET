
import { catchError, map } from 'rxjs/operators';
import { throwError, forkJoin } from 'rxjs';
import { ContainerModel, initialState } from '../login-user.store.model';
import { BaseAction } from 'src/app/core/utils/base-action';
import { IDataGridPageRequest } from 'src/app/shared/lib/interfaces/data-grid.interface';
import { DataService } from 'src/app/core/data/data.service';
import { IAuthorizationData, ISignInData } from 'src/app/core/store/app.store.interface';

export class ContainerLoginUserAction extends BaseAction<ContainerModel> {
  constructor(
    getState: () => ContainerModel,
    setState: (newState: ContainerModel) => void,
    private dataService: DataService
  ) {
    super(getState, setState);
  }

  beginProcess = () => {
    const state = this.getState();
    this.setState({ ...state, loading: true, error: null });
  };

  logIn = (model: any) => {
    this.beginProcess();
    return this.dataService.security().logIn(model).pipe(
      map((r: any) => {
        this.saveSigInData(r.data);
        return r;
      }),
      catchError(e => {
        return throwError(e);
      })
    );
  }

  postSavedError = (error: any) => {
    const state = this.getState();
    this.setState({
      ...state,
      loading: false,
      error,
    });
  };

  private saveSigInData(signInData: ISignInData) {
    this.dataService.session().setSignInData(signInData);
  }

  getAuthorizationInfo = (model: any) => {
    this.beginProcess();
    return this.dataService.security().getAuthorizationInfo(model).pipe(
      map((r: any) => {
        this.setAutorizationData(r);
        return r;
      }),
      catchError(e => {
        return throwError(e);
      })
    );
  }

  private setAutorizationData(authorizationData: IAuthorizationData) {
    this.dataService.session().setAutorizationData(authorizationData);
  }
}
