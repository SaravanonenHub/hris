import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { AccountService } from './account/account.service';
import { AppConfig } from './domain/appconfig';
import { AppConfigService } from './service/appconfigservice';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private configService: AppConfigService, private accountService: AccountService) { }

  config: AppConfig = {};

  public subscription?: Subscription;

  // public announcement: any = Announcement;

  public newsActive: boolean = false;

  storageKey = 'primeng';

  ngOnInit() {
    this.loadCurrentUser();
    this.config = { theme: 'lara-light-blue', dark: false };

    this.subscription = this.configService.configUpdate$.subscribe((config) => {
      const linkElement = document.getElementById('theme-link');
      this.replaceLink(linkElement, config.theme);
      this.config = config;
    });

    const itemString = localStorage.getItem(this.storageKey);
    if (itemString) {
      const item = JSON.parse(itemString);
      if (item.hiddenNews) {
        this.newsActive = true;
      }
    } else {
      this.newsActive = true;
    }
  }

  onNewsClose() {
    this.newsActive = false;

    const item = {
      // hiddenNews: this.announcement.id
    };

    localStorage.setItem(this.storageKey, JSON.stringify(item));
  }

  replaceLink(linkElement: any, theme: any) {
    const id = linkElement.getAttribute('id');
    const cloneLinkElement = linkElement.cloneNode(true);

    cloneLinkElement.setAttribute('href', linkElement.getAttribute('href').replace(this.config.theme, theme));
    cloneLinkElement.setAttribute('id', id + '-clone');

    linkElement.parentNode.insertBefore(cloneLinkElement, linkElement.nextSibling);

    cloneLinkElement.addEventListener('load', () => {
      linkElement.remove();
      cloneLinkElement.setAttribute('id', id);
    });
  }
  loadCurrentUser() {
    const token = localStorage.getItem('token');
    this.accountService.loadCurrentUser(token!).subscribe();
  }
  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
