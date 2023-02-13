
import { Observable } from "rxjs";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IDataGridPageRequest } from "src/app/shared/lib/interfaces/data-grid.interface";
import { SecurityRestangularService } from "./resources/security-restangular.service";

@Injectable({
  providedIn: 'root'
})

export class SecurityService {
  constructor(private restangular: SecurityRestangularService) { }

  logIn = (data: any) => {
    return this.restangular.all("securities/login").post(data);
  };

  getAuthorizationInfo = (data: any) => {
    return this.restangular.all("securities/authorizationInfo").get();
  };
}
