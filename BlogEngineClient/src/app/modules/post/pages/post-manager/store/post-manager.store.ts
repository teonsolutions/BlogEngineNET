
import { Injectable } from '@angular/core';
import { GestionPuntoVentaModel, initialState } from './post-manager.store.model';
//se debe crear los actions en la carpeta "actions"
import { FormRegisterAction } from './actions/form-register.action';
import { ContainerGestionPuntoVentaAction } from './actions/container.action';
import { DataService } from 'src/app/core/data/data.service';
import { Store } from 'src/app/core/store/store';

@Injectable()
export class PostManagerStore extends Store<GestionPuntoVentaModel> {

  container: ContainerGestionPuntoVentaAction;

  formRegister: FormRegisterAction;

  constructor(
    private dataService: DataService) {

    super(initialState);

    this.container = new ContainerGestionPuntoVentaAction(
      this.buildScopedGetState('container'),
      this.buildScopedSetState('container'),
      dataService
    );

    this.formRegister = new FormRegisterAction(
      this.buildScopedGetState('formRegister'),
      this.buildScopedSetState('formRegister'),
      dataService
    );

  }

}

