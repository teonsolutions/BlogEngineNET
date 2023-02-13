
import { NgModule } from '@angular/core';
import { PostManagerComponent } from './container/post-manager.component';
import { PostManagerRoutingModule } from './post-manager-routing.module';
import { CommonModule } from '@angular/common';
import { FormRegisterPostComponent } from './components/form-register-post/form-register-post.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormAddCommentComponent } from './components/form-add-comment/form-add-comment.component';
import { FormRejectPostComponent } from './components/form-reject-post/form-reject-post.component';
import { FormViewRejectedCommentComponent } from './components/form-view-rejected-comment/form-view-rejected-comment.component';

@NgModule({
  declarations: [
    PostManagerComponent,
    FormRegisterPostComponent,
    FormAddCommentComponent,
    FormRejectPostComponent,
    FormViewRejectedCommentComponent
  ],
  imports: [
    PostManagerRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  entryComponents: [],

})
export class PostManagerModule { }
