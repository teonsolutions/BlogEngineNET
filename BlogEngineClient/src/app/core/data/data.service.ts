import { Injectable } from '@angular/core';
import { CommentService } from './services/comment.service';
import { PostService } from './services/post.service';
import { SecurityService } from './services/security.service';
import { SessionService } from './services/session.service';

@Injectable({
  providedIn: "root",
})
export class DataService {
  constructor(
    private postService: PostService,
    private commentService: CommentService,
    private sessionService: SessionService,
    private securityService: SecurityService
  ) { }

  posts(): PostService {
    return this.postService;
  }

  comments(): CommentService {
    return this.commentService;
  }

  session(): SessionService {
    return this.sessionService;
  }

  security(): SecurityService {
    return this.securityService;
  }

}
