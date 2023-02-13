
    import { NgModule } from '@angular/core';
    import { LoginUserRoutingModule } from './login-user-routing.module';
    import { LoginUserComponent } from './container/login-user.component';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

    @NgModule({
      declarations: [
        LoginUserComponent
      ],
      imports: [
        LoginUserRoutingModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule
      ],
      entryComponents: [],

    })
    export class LoginUserModule { }
