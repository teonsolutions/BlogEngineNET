import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { throwError } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { SessionService } from 'src/app/core/data/services/session.service';
import { FormType } from 'src/app/core/enums/form.enum';
import { IPost } from 'src/app/modules/post/interfaces/post.interface';
import { parseError } from 'src/app/shared/lib/functions/form.helpers';
import { PostManagerStore } from '../../store/post-manager.store';
import { Post } from '../../store/post-manager.store.model';

@Component({
  selector: 'app-form-register-post',
  templateUrl: './form-register-post.component.html',
  styleUrls: ['./form-register-post.component.css']
})
export class FormRegisterPostComponent implements OnInit {
  readonly state$ = this.store.select(x => x.formRegister);
  @Output() saveEvent: EventEmitter<any> = new EventEmitter();
  @Output() cancelEvent: EventEmitter<any> = new EventEmitter();
  form: FormGroup;
  action = FormType;

  constructor(private store: PostManagerStore,
    private formBuilder: FormBuilder,
    private sessionInfo: SessionService) { }

  ngOnInit(): void {
    const formType = this.store.selectSnapshot(x => x.formRegister.formType);
    this.buildForm();
    if (formType != FormType.CREATE) {
      const id = this.store.selectSnapshot(x => x.formRegister.id);
      this.loadData(id);
      if (formType == FormType.VIEW) {
        this.disabledFiels();
      }
    }
  }

  buildForm = () => {
    this.form = this.formBuilder.group({
      postID: [0],
      title: [null, Validators.required],
      description: [null, Validators.required],
    });
  };

  prepareData = () => {
    const model = this.form.getRawValue();
    let data: IPost = new Post();
    data.postID = model.postID;
    data.title = model.title;
    data.description = model.description;
    data.userName = this.sessionInfo.getSignInData()?.userLogin;
    return data;
  }

  handleClickOnSave = () => {
    const formType = this.store.selectSnapshot(x => x.formRegister.formType);
    if (formType == FormType.CREATE)
      this.handleOnSave();
    else
      this.handleOnUpdate();
  }

  handleOnSave = () => {
    if (!this.form.valid) return;
    const data = this.prepareData();
    this.store.formRegister.setProperties(data);
    this.store.formRegister.create().pipe(
      catchError((e) => {
        return throwError(e);
      }),
      finalize(() => { })
    ).subscribe((resp: any) => {
      this.saveEvent.emit(data);
    },
      (err) => {
        this.store.formRegister.postSavedError(parseError(err));
      });
  }

  handleOnUpdate = () => {
    if (!this.form.valid) return;
    const data = this.prepareData();
    this.store.formRegister.setProperties(data);
    this.store.formRegister.update().pipe(
      catchError((e) => {
        return throwError(e);
      }),
      finalize(() => { })
    ).subscribe((resp: any) => {
      this.saveEvent.emit(data);
    },
      (err) => {
        this.store.formRegister.postSavedError(parseError(err));
      });
  }

  handleCancel = () => {
    this.cancelEvent.emit();
  }

  loadData = (id: number) => {
    this.store.formRegister.getById(id).then((resp: any) => {
      this.form.get("postID").setValue(resp.postID);
      this.form.get("title").setValue(resp.title);
      this.form.get("description").setValue(resp.description);
    });
  }

  disabledFiels = () => {
    this.form.get("title").disable();
    this.form.get("description").disable();
  }
}
