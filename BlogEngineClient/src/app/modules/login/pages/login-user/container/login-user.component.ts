
import { Component, OnInit, Input } from '@angular/core';
import { LoginUserStore } from '../store/login-user.store';
import { MESSAGES } from '../../../_utils/messages';
import { IDataGridButtonEvent, IDataGridEvent } from 'src/app/shared/lib/interfaces/data-grid.interface';
import { MapPageRequestFromGridSource } from 'src/app/shared/lib/functions/misc';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Login } from '../store/login-user.store.model';
import { ILogin } from '../../../interfaces/login.interface';
import { catchError, finalize } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { parseError } from 'src/app/shared/lib/functions/form.helpers';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  templateUrl: './login-user.component.html',
  styleUrls: ['./login-user.component.scss'],
  providers: [
    LoginUserStore
  ]
})
export class LoginUserComponent implements OnInit {
  readonly state$ = this.loginUserStore.select(s => s);
  form: FormGroup;

  constructor(private loginUserStore: LoginUserStore,
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.buildForm();
  }

  buildForm = () => {
    this.form = this.formBuilder.group({
      userLogin: [null, Validators.required],
      password: [null, Validators.required],
    });
  };

  prepareData = () => {
    const model = this.form.getRawValue();
    let data: ILogin = new Login();
    data.userLogin = model.userLogin;
    data.password = model.password;
    return data;
  }

  handleClickOnLogin = () => {
    if (!this.form.valid) return;
    const data = this.prepareData();
    this.loginUserStore.container.logIn(data).pipe(
      catchError((e) => {
        return throwError(e);
      }),
      finalize(() => { })
    ).subscribe((resp: any) => {
      this.getAuthorizationInfo(resp);
    },
      (err) => {
        this.loginUserStore.container.postSavedError(parseError(err));
      });
  }

  getAuthorizationInfo = (loginInfo: any) => {
    this.loginUserStore.container.getAuthorizationInfo(loginInfo).pipe(
      catchError((e) => {
        return throwError(e);
      }),
      finalize(() => { })
    ).subscribe((resp: any) => {
      this.router.navigate(['../post'], { relativeTo: this.route });
    },
      (err) => {
        this.loginUserStore.container.postSavedError(parseError(err));
      });
  }

}
