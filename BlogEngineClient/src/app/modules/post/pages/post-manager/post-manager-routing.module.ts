
    import { NgModule } from '@angular/core';
    import { Routes, RouterModule } from '@angular/router';
    import { PostManagerComponent } from './container/post-manager.component';

    const routes: Routes = [
      { path: '', component: PostManagerComponent }
    ];

    @NgModule({
      imports: [ RouterModule.forChild(routes) ],
      exports: [ RouterModule ]
    })
    export class PostManagerRoutingModule { }
