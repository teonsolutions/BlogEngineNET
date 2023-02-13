
      import { NgModule } from '@angular/core';
import { DataService } from 'src/app/core/data/data.service';
      import { PostRoutingModule } from './post-routing.module';

      @NgModule({
        declarations: [],
        imports: [
          PostRoutingModule
        ],
        providers: [
          DataService
        ]
      })
      export class PostModule { }
