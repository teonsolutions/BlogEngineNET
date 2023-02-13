import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SessionService } from 'src/app/core/data/services/session.service';
import { CommentType, FormType } from 'src/app/core/enums/form.enum';
import { PostManagerStore } from '../../store/post-manager.store';

@Component({
  selector: 'app-form-view-rejected-comment',
  templateUrl: './form-view-rejected-comment.component.html',
  styleUrls: ['./form-view-rejected-comment.component.css']
})
export class FormViewRejectedCommentComponent implements OnInit {

  readonly state$ = this.store.select(x => x.formRegister);
  @Output() returnEvent: EventEmitter<any> = new EventEmitter();
  form: FormGroup;
  action = FormType;
  commentType = CommentType;

  constructor(private store: PostManagerStore,
    private formBuilder: FormBuilder,
    private sessionInfo: SessionService) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData = () => {
    const id = this.store.selectSnapshot(x => x.formRegister.id);
    this.store.formRegister.getById(id).then((resp: any) => {});
  }

  handleReturn = () => {
    this.returnEvent.emit();
  }
}
