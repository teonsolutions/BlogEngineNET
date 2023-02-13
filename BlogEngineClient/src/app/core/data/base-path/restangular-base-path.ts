import { Injectable } from '@angular/core';
import { environment } from 'src/assets/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RestangularBasePath {
  public postApi = environment.blogEngineApiPrefix;
  public commentApi = environment.blogEngineApiPrefix;
  public securityApi = environment.blogEngineApiPrefix;
}
