
import { Observable } from "rxjs";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IDataGridPageRequest } from "src/app/shared/lib/interfaces/data-grid.interface";
import { PostRestangularService } from "./resources/post-restangular.service";

@Injectable({
  providedIn: 'root'
})

export class PostService {
  constructor(private restangular: PostRestangularService) { }

  getListItem = (): Observable<any> => {
    return this.restangular.all('puntosventa/listitem').get();
  }

  getAllPosts = (pageRequest: IDataGridPageRequest, request: any): Observable<any> => {
    let queryParam = new HttpParams();
    if (pageRequest.pageSize != null) {
      queryParam = queryParam.set('pageSize', pageRequest.pageSize);
    }

    if (pageRequest.skip != null) {
      queryParam = queryParam.set('skip', pageRequest.skip);
    }

    if (request.rolGuid != null) {
      queryParam = queryParam.set('filter.rolGuid', request.rolGuid);
    }

    if (request.userName != null) {
      queryParam = queryParam.set('filter.userName', request.userName);
    }


    return this.restangular.all("posts").get(queryParam);
  };

  getById = (id: any): Observable<any> => {
    return this.restangular.one("posts", id).get();
  };

  create = (data: any) => {
    return this.restangular.all("posts").post(data);
  };

  update = (data: any) => {
    return this.restangular.all("posts").put(data);
  };

  /*
  delete = (id: any): Observable<any> => {
    const data = { postID: id };
    return this.restangular.all("posts").patch(data);
  };
  */

  submit = (data: any) => {
    return this.restangular.all("posts/submit").post(data);
  };

  aprove = (data: any) => {
    return this.restangular.all("posts/aprove").post(data);
  };

  reject = (data: any) => {
    return this.restangular.all("posts/reject").post(data);
  };

}
