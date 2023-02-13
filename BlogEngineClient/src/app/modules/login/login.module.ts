
      import { NgModule } from '@angular/core';
import { DataService } from 'src/app/core/data/data.service';
      import { LoginRoutingModule } from './login-routing.module';

      @NgModule({
        declarations: [],
        imports: [
          LoginRoutingModule
        ],
        providers: [
          DataService
        ]
      })
      export class LoginModule { }
