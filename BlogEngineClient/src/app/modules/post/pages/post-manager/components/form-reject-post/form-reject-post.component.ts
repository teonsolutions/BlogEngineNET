import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { throwError } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { SessionService } from 'src/app/core/data/services/session.service';
import { FormType } from 'src/app/core/enums/form.enum';
import { parseError } from 'src/app/shared/lib/functions/form.helpers';
import { PostManagerStore } from '../../store/post-manager.store';

@Component({
  selector: 'app-form-reject-post',
  templateUrl: './form-reject-post.component.html',
  styleUrls: ['./form-reject-post.component.css']
})
export class FormRejectPostComponent implements OnInit {
  readonly state$ = this.store.select(x => x.formRegister);
  @Output() saveEvent: EventEmitter<any> = new EventEmitter();
  @Output() cancelEvent: EventEmitter<any> = new EventEmitter();
  form: FormGroup;
  action = FormType;

  constructor(private store: PostManagerStore,
    private formBuilder: FormBuilder,
    private sessionInfo: SessionService) { }

  ngOnInit(): void {
    this.buildForm();
    this.loadData();
  }

  buildForm = () => {
    this.form = this.formBuilder.group({
      postID: [0],
      comment: [null, Validators.required],
    });
  };

  handleReject = () => {
    if (!this.form.valid) return;
    const postID = this.store.selectSnapshot(x => x.formRegister.id);
    const model = this.form.getRawValue();
    const data = {
      postID: postID,
      comment: model.comment,
      userName: this.sessionInfo.getSignInData()?.userLogin
    };
    this.store.formRegister.reject(data).pipe(
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

  loadData = () => {
    const id = this.store.selectSnapshot(x => x.formRegister.id);
    this.store.formRegister.getById(id).then((resp: any) => {});
  }
}
