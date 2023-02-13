import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Event, NavigationEnd, Router } from '@angular/router';
import { SessionService } from './core/data/services/session.service';
import { ISignInData } from './core/store/app.store.interface';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'BlogEngineClient';
  userSession: ISignInData;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private sessionService: SessionService) { }

  ngOnInit() {
    this.subscribeToRouter();
    this.userSession = this.sessionService.getSignInData();
  }

  handleStart = () => {
    this.router.navigate(['login'], { relativeTo: this.route });
  }

  protected subscribeToRouter = () => {
    this.router.events.subscribe((event: Event) => {
      if (event instanceof NavigationEnd) {
          let route = this.router.routerState.snapshot.root;
          while (route.firstChild) {
              route = route.firstChild;
          }
          this.setCurrentMenu(event);
      }
  });
  }

  setCurrentMenu = (event: NavigationEnd) => {
    const authorizationInfo = this.sessionService.getAutorizationData();
    if (authorizationInfo != null) {
      const currentMenu = authorizationInfo.menus.find(x => x.url == event.url);
      this.sessionService.setCurrentMenuData(currentMenu);
      this.sessionService.setPermission(currentMenu);
    }
  }
}
