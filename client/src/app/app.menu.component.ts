import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, ElementRef, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NavigationEnd, NavigationStart, Router } from '@angular/router';
import { FilterService } from 'primeng/api';
import { DomHandler } from 'primeng/dom';
import { Subscription } from 'rxjs';
import { AppConfig } from './domain/appconfig';
import { AppConfigService } from './service/appconfigservice';

declare let gtag: Function;

@Component({
    selector: 'app-menu',
    template: `
        <div class="layout-sidebar" [ngClass]="{ active: active}">
            <div class="flex align-items-center justify-content-space-around" style="height: 2.75rem;">
            <a class="menu-button" (click)="onMenuButtonClick($event)">
                <i class="pi pi-bars"></i>
            </a>
           
            </div>
           
            <!-- <div class="layout-sidebar-filter p-fluid">
                <p-autoComplete
                    [group]="true"
                    [(ngModel)]="selectedRoute"
                    [minLength]="2"
                    [suggestions]="filteredRoutes"
                    scrollHeight="300px"
                    (onSelect)="onSelect($event)"
                    placeholder="Search"
                    (completeMethod)="filterGroupedRoute($event)"
                    field="label"
                >
                </p-autoComplete>
            </div> -->
            <div class="layout-menu">
                <div class="menu-category">Employee</div>
                <div class="menu-items">
                    <a [routerLink]="['employee']" routerLinkActive="router-link-exact-active" (click)="itemClick($event)">Profiles</a>
                    <a  [routerLink]="['request']" routerLinkActive="router-link-exact-active" (click)="itemClick($event)">Leave Request</a>
                    <a [routerLink]="['approval']" routerLinkActive="router-link-exact-active" (click)="itemClick($event)">Leave Approvals<span class="p-tag">New</span></a>
                </div>

                <div class="menu-category">Configuration</div>
                <div class="menu-items">
                    <a [routerLink]="['request']" routerLinkActive="router-link-exact-active">Locale</a>
                    <a [routerLink]="['overlay']" routerLinkActive="router-link-exact-active">Overlay <span class="p-tag">New</span></a>
                </div>

                <div class="menu-category">Support</div>
                <div class="menu-items">
                    <a href="https://forum.primefaces.org/viewforum.php?f=35" target="_blank">Community Forum</a>
                    <a href="https://discord.gg/gzKFYnpmCY" target="_blank">Discord Server</a>
                    <!--<a [routerLink]="['lts']" routerLinkActive="router-link-exact-active">Long Term Support</a>-->
                    <a [routerLink]="['support']" routerLinkActive="router-link-exact-active">PRO Support <span class="p-tag">New</span></a>
                </div>

                <div class="menu-category">Resources</div>
                <div class="menu-items">
                    <a href="https://www.youtube.com/channel/UCTgmp69aBOlLnPEqlUyetWw" target="_blank">PrimeTV</a>
                    <a href="https://github.com/primefaces/primeng" target="_blank">Source Code</a>
                    <a href="https://www.primefaces.org/store">Store</a>
                    <a href="https://twitter.com/prime_ng?lang=en">Twitter</a>
                </div>

                
            </div>
        </div>
    `,
    animations: [
        trigger('submenu', [
            state(
                'hidden',
                style({
                    height: '0',
                    opacity: 0
                })
            ),
            state(
                'visible',
                style({
                    height: '*',
                    opacity: 1
                })
            ),
            transition('* <=> *', animate('400ms cubic-bezier(0.86, 0, 0.07, 1)'))
        ])
    ]
})
export class AppMenuComponent implements OnInit {
    @Input() active?: boolean;
    @Output() menuButtonClick: EventEmitter<any> = new EventEmitter();
    activeSubmenus: { [key: string]: boolean } = {};

    filteredRoutes: any[] = [];

    selectedRoute: any;

    submenuRouting: boolean = true;

    routes = [
        {
            label: 'Employee',
            value: 'general',
            items: [
                { label: 'Setup', value: '/setup' },
                { label: 'Locale', value: '/i18n' },
                { label: 'Overlay', value: '/overlay' }
            ]
        },
        {
            label: 'Support',
            value: 'support',
            items: [
                { label: 'Long Term Support', value: '/lts' },
                { label: 'PRO Support', value: '/support' }
            ]
        },
        {
            label: 'Theming',
            value: 'theming',
            items: [
                { label: 'Guide', value: '/theming' },
                { label: 'Colors', value: '/colors' }
            ]
        },
        {
            label: 'UI Kit',
            value: 'uikit',
            items: [{ label: 'Figma', value: '/uikit' }]
        },
        {
            label: 'Accessibility',
            value: 'accessibility',
            items: [{ label: 'Overview', value: '/accessibility' }]
        },
        {
            label: 'PrimeIcons',
            value: 'primeicons',
            items: [{ label: 'Icons v6', value: '/icons' }]
        },

    ];

    scrollable = true;

    config: AppConfig;

    subscription: Subscription;

    constructor(private el: ElementRef, private router: Router, private filterService: FilterService, private configService: AppConfigService) {
        this.config = this.configService.config;
        this.subscription = this.configService.configUpdate$.subscribe((config) => (this.config = config));
        router.events.subscribe((routerEvent) => {
            if (routerEvent instanceof NavigationStart && (routerEvent.navigationTrigger === 'popstate' || this.scrollable)) {
                let routeUrl = routerEvent.url;

                if (this.isSubmenu(routeUrl) && !this.isSubmenuActive('/' + routeUrl.split('/')[1])) {
                    this.submenuRouting = true;
                }

                if (routerEvent.navigationTrigger === 'popstate') {
                    this.scrollable = true;
                }
            }

            if (routerEvent instanceof NavigationEnd && !this.submenuRouting && this.scrollable) {
                setTimeout(() => {
                    this.scrollToSelectedRoute();
                }, 1);
            }
        });

    }
    ngOnInit() {
        // this.active = true;
    }
    filterGroupedRoute(event: any) {
        let query = event.query;
        let filteredGroups = [];

        for (let optgroup of this.routes) {
            let filteredSubOptions = this.filterService.filter(optgroup.items, ['value'], query, 'contains');
            if (filteredSubOptions && filteredSubOptions.length) {
                filteredGroups.push({
                    label: optgroup.label,
                    value: optgroup.value,
                    items: filteredSubOptions
                });
            }
        }

        this.filteredRoutes = filteredGroups;
    }

    onSelect(event: any) {
        if (this.router.url !== event.value) {
            this.scrollable = true;
            this.router.navigate([event.value]);
        }

        this.selectedRoute = null;
    }
    itemClick(event: Event) {
        this.menuButtonClick.emit();
        event.preventDefault();

    }
    onAnimationDone() {
        if (this.submenuRouting) {
            this.scrollToSelectedRoute();
            this.submenuRouting = false;
        }
    }
    onMenuButtonClick(event: Event) {
        this.menuButtonClick.emit();
        event.preventDefault();
    }
    scrollToSelectedRoute() {
        let routeEl = DomHandler.findSingle(this.el.nativeElement, '.router-link-exact-active');

        if (routeEl) routeEl.scrollIntoView({ inline: 'start' });

        this.scrollable = false;
    }

    toggleSubmenu(event: Event, name: string) {
        this.activeSubmenus[name] = this.activeSubmenus[name] ? false : true;
        event.preventDefault();
    }

    isSubmenu(route: any) {
        return route.includes('table') || route.includes('treetable') || route.includes('tree') || route.includes('galleria');
    }

    isSubmenuActive(name: string) {
        if (this.activeSubmenus.hasOwnProperty(name)) {
            return this.activeSubmenus[name];
        } else if (this.router.isActive(name, false)) {
            this.activeSubmenus[name] = true;
            return true;
        }

        return false;
    }
}
