
import { catchError, map } from 'rxjs/operators';
import { throwError, forkJoin } from 'rxjs';
import { ContainerModel, initialState } from '../post-manager.store.model';
import { BaseAction } from 'src/app/core/utils/base-action';
import { DataService } from 'src/app/core/data/data.service';
import { IDataGridPageRequest } from 'src/app/shared/lib/interfaces/data-grid.interface';

export class ContainerGestionPuntoVentaAction extends BaseAction<ContainerModel> {
  constructor(
    getState: () => ContainerModel,
    setState: (newState: ContainerModel) => void,
    private dataService: DataService
  ) {
    super(getState, setState);
  }



  setSearchForm = (form: any) => {
    const state = this.getState();
    this.setState({ ...state, formSearch: form });
  }

  fetchPageBegin = () => {
    const state = this.getState();
    this.setState({ ...state, loading: true, error: null });
  };

  fetchPageSuccess = (
    items: any[],
    total: number,
    totalPages: number,
    pageRequest: IDataGridPageRequest
  ) => {
    const state = this.getState();
    this.setState({
      ...state,
      loading: false,
      gridSource: {
        ...state.gridSource,
        items: items,
        page: pageRequest.page,
        pageSize: pageRequest.pageSize,
        total: total,
        orderBy: pageRequest.orderBy,
        orderDir: pageRequest.orderDir,
        totalPages: totalPages,
        skip: pageRequest.skip
      },
      error: null
    });
  };

  fetchPageError = (error: any) => {
    const state = this.getState();
    this.setState({ ...state, loading: false, error });
  };

  asyncFetchPage = (pageRequest: IDataGridPageRequest) => {

    this.fetchPageBegin();

    const state = this.getState();

    return this.dataService.posts().getAllPosts(pageRequest, state.formSearch).pipe(map(resp => {
      this.fetchPageSuccess(resp.data, resp.total, resp.totalPages, pageRequest);
      return resp;
    }), catchError(err => {
      this.fetchPageError(err);
      return throwError(err);
    }));

  }

  postSaveSuccess = () => {
    const state = this.getState();
    this.setState({ ...state, loading: false, error: null });
  }

  postSaveError = (error: any) => {
    const state = this.getState();
    this.setState({ ...state, loading: false, error: error, });
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

  submit = (data: any) => {
    this.postSavedBegin();
    return this.dataService.posts().submit(data).pipe(
      map(r => {
        this.postSavedSuccess();
        return r;
      }),
      catchError(e => {
        return throwError(e);
      })
    );
  }

  aprove = (data: any) => {
    this.postSavedBegin();
    return this.dataService.posts().aprove(data).pipe(
      map(r => {
        this.postSavedSuccess();
        return r;
      }),
      catchError(e => {
        return throwError(e);
      })
    );
  }

  setCurrentPage = (skip: number) => {
    const state = this.getState();
    this.setState({
      ...state,
      loading: false,
      gridSource: {
        ...state.gridSource,
        skip: skip
      },
      error: null
    });
  }


}
