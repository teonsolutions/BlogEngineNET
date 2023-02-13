
import { Observable } from "rxjs";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IDataGridPageRequest } from "src/app/shared/lib/interfaces/data-grid.interface";
import { CommentRestangularService } from "./resources/comment-restangular.service";

@Injectable({
  providedIn: 'root'
})

export class CommentService {
  constructor(private restangular: CommentRestangularService) { }

  create = (data: any) => {
    return this.restangular.all("comments").post(data);
  };
}
