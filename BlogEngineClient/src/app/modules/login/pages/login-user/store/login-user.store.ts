
import { Injectable } from '@angular/core';
import { LoginUserModel, initialState } from './login-user.store.model';
import { ContainerLoginUserAction } from './actions/container.action';
import { DataService } from 'src/app/core/data/data.service';
import { Store } from 'src/app/core/store/store';

@Injectable()
export class LoginUserStore extends Store<LoginUserModel> {

  container: ContainerLoginUserAction;

  constructor(private dataService: DataService) {

    super(initialState);

    this.container = new ContainerLoginUserAction(
      this.buildScopedGetState('container'),
      this.buildScopedSetState('container'),
      dataService
    );
  }

}

