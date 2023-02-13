
import { Component, OnInit, Input } from '@angular/core';
import { PostManagerStore } from '../store/post-manager.store';
import { MESSAGES } from '../../../_utils/messages';
import { FormSearch } from '../store/post-manager.store.model';
import { Router, ActivatedRoute } from '@angular/router';
import { IDataGridButtonEvent, IDataGridEvent } from 'src/app/shared/lib/interfaces/data-grid.interface';
import { MapPageRequestFromGridSource } from 'src/app/shared/lib/functions/misc';
import { ActionCodeEnum, FormType, StatusPostEnum } from 'src/app/core/enums/form.enum';
import { IPost } from '../../../interfaces/post.interface';
import { SessionService } from 'src/app/core/data/services/session.service';
import { IPermissionAction, ISignInData } from 'src/app/core/store/app.store.interface';
import { catchError, finalize } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { parseError } from 'src/app/shared/lib/functions/form.helpers';

@Component({
  templateUrl: './post-manager.component.html',
  styleUrls: ['./post-manager.component.scss'],
  providers: [
    PostManagerStore
  ]
})
export class PostManagerComponent implements OnInit {
  readonly state$ = this.store.select(s => s.container);
  action = FormType;
  formType: number = FormType.SEARCH;
  actionCode = ActionCodeEnum;
  permision: IPermissionAction;
  userSession: ISignInData;
  statusEnum = StatusPostEnum;;
  pageActiveStyle = 'active ';

  constructor(
    private store: PostManagerStore,
    private router: Router,
    private route: ActivatedRoute,
    private sessionInfo: SessionService) { }

  ngOnInit() {
    this.refreshGrid();
    this.permision = this.sessionInfo.permission;
    this.userSession = this.sessionInfo.getSignInData();
  }

  setSearchForm = () => {
    const userInfo = this.sessionInfo.getSignInData();
    const data = {
      rolGuid: userInfo?.rolGuid,
      userName: userInfo?.userLogin
    };
    this.store.container.setSearchForm(data);
  }
  private refreshGrid = () => {
    this.setSearchForm();
    const source = this.store.selectSnapshot(x => x.container.gridSource);
    this.store.container.asyncFetchPage(MapPageRequestFromGridSource(source)).subscribe();
  };

  handleSearch = () => {
    this.setSearchForm();
    const gridSource = this.store.selectSnapshot(x => x.container.gridSource);
    this.store.container.asyncFetchPage({
      page: 1,
      skip: 0,
      orderBy: gridSource.orderBy,
      orderDir: gridSource.orderDir,
      pageSize: gridSource.pageSize,
      totalPages: gridSource.totalPages
    }).subscribe();
  }

  handleClickNew = () => {
    this.formType = this.action.CREATE;
    this.store.formRegister.setNew();
  }

  handleClickEdit = (row: any) => {
    this.formType = this.action.CREATE;
    this.store.formRegister.setUpdate(row.postID);
  }

  handleClickSubmit = (row: any) => {
    const data = {
      postID: row.postID,
      userName: this.sessionInfo.getSignInData()?.userLogin
    };
    this.store.container.submit(data).pipe(
      catchError((e) => {
        return throwError(e);
      }),
      finalize(() => { })
    ).subscribe((resp: any) => {
      this.refreshGrid();
    },
      (err) => {
        this.store.container.postSavedError(parseError(err));
      });
  }

  handleClickAprove = (row: any) => {
    const data = {
      postID: row.postID,
      userName: this.sessionInfo.getSignInData()?.userLogin
    };
    this.store.container.aprove(data).pipe(
      catchError((e) => {
        return throwError(e);
      }),
      finalize(() => { })
    ).subscribe((resp: any) => {
      this.refreshGrid();
    },
      (err) => {
        this.store.container.postSavedError(parseError(err));
      });
  }

  handleClickReject = (row: any) => {
    this.store.formRegister.setView(row.postID);
    this.formType = this.action.REJECT;
  }

  handleClickView = (row: any) => {
    this.formType = this.action.VIEW;
    this.store.formRegister.setView(row.postID);
  }

  handleSave = () => {
    this.formType = this.action.SEARCH;
    this.refreshGrid();
  }

  handleCancel = () => {
    this.formType = this.action.SEARCH;
  }

  handleAddComment = (row: any) => {
    this.store.formRegister.setView(row.postID);
    this.formType = this.action.COMMENTS;
  }

  handleResultComment = () => {
    this.formType = this.action.SEARCH;
  }

  handleRejectConfim = () => {
    this.formType = this.action.SEARCH;
    this.refreshGrid();
  }

  handleRejectCancel = () => {
    this.formType = this.action.SEARCH;
  }

  handleViewRejectedComment = (row: any) => {
    this.store.formRegister.setView(row.postID);
    this.formType = this.action.REJECT_COMMENTS;
  }

  handleRejectCommentHide = () => {
    this.formType = this.action.SEARCH;
  }

  // pagination
  getCurrentPageStyle = (page: number) => {
    const gridSource = this.store.selectSnapshot(x => x.container.gridSource);
    let style = '';
    if (page == gridSource.skip) {
      style = this.pageActiveStyle;
    }
    return style;
  }

  loadCurrentPage = (page: number) => {
    this.store.container.setCurrentPage(page);
    this.refreshGrid();
  }

  loadNextPage = () => {
    const gridSource = this.store.selectSnapshot(x => x.container.gridSource);
    let page = gridSource.totalPages;
    if (gridSource.skip < gridSource.totalPages) {
      page = gridSource.skip + 1;
    }

    this.store.container.setCurrentPage(page);
    this.refreshGrid();
  }

  loadPreviousPage = () => {
    const gridSource = this.store.selectSnapshot(x => x.container.gridSource);
    let page = 1;
    if (gridSource.skip > 1) {
      page = gridSource.skip - 1;
    }

    this.store.container.setCurrentPage(page);
    this.refreshGrid();
  }
}
