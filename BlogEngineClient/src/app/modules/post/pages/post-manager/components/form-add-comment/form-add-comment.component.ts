import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { throwError } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { SessionService } from 'src/app/core/data/services/session.service';
import { FormType } from 'src/app/core/enums/form.enum';
import { IComment } from 'src/app/modules/post/interfaces/post.interface';
import { parseError } from 'src/app/shared/lib/functions/form.helpers';
import { PostManagerStore } from '../../store/post-manager.store';
import { Comment } from '../../store/post-manager.store.model';

@Component({
  selector: 'app-form-add-comment',
  templateUrl: './form-add-comment.component.html',
  styleUrls: ['./form-add-comment.component.css']
})
export class FormAddCommentComponent implements OnInit {
  readonly state$ = this.store.select(x => x.formRegister);
  @Output() returnEvent: EventEmitter<any> = new EventEmitter();
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
      commentID: [0],
      comments: [null, Validators.required],
    });
  };

  loadData = () => {
    const id = this.store.selectSnapshot(x => x.formRegister.id);
    this.store.formRegister.getById(id).then((resp: any) => {});
  }

  prepareData = () => {
    const model = this.form.getRawValue();
    let data: IComment = new Comment();
    data.commentID = 0;
    data.comments = model.comments;
    data.userName = this.sessionInfo.getSignInData()?.userLogin;
    return data;
  }

  handleClickOnSave = () => {
    if (!this.form.valid) return;
    const data = this.prepareData();
    this.store.formRegister.addComment(data).pipe(
      catchError((e) => {
        return throwError(e);
      }),
      finalize(() => { })
    ).subscribe((resp: any) => {
      this.loadData();
      this.form.get('comments').setValue(null);
    },
      (err) => {
        this.store.formRegister.postSavedError(parseError(err));
      });
  }

  handleReturn = () => {
    this.returnEvent.emit();
  }

}
