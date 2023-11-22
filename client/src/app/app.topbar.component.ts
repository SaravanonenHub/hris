import { Component, EventEmitter, Output, ViewChild, ElementRef, Input, OnInit, OnDestroy } from '@angular/core';
import { trigger, style, transition, animate, AnimationEvent } from '@angular/animations';
import { Router, NavigationEnd } from '@angular/router';
import { AppConfigService } from './service/appconfigservice';
import { AppConfig } from './domain/appconfig';
import { Subscription, filter } from 'rxjs';
import { faUser, faBell, faBookmark } from '@fortawesome/free-regular-svg-icons'
import { AccountService } from './account/account.service';
import { NotificationService } from './shared/services/notification.service';
import { EmployeeService } from './employees/employee.service';
import { IEmployee } from './domain/models/employee';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr'
import { environment } from 'src/environments/environment';
import { Notify } from './shared/models/notify';
import { Role } from './domain/models/master';
@Component({
    selector: 'app-topbar',
    template: `
        <div class="layout-topbar" #containerElement>
            <div class="left-boxes">
                <div class="box">
                    <a class="menu-button" (click)="onMenuButtonClick($event)">
                        <i class="pi pi-bars"></i>
                    </a>
                </div>
                <div class="box">
                    <a [routerLink]="['/']" class="logo">
                        <img alt="logo" [src]="'https://primefaces.org/cdn/primeng/images/' + (config.dark ? 'primeng-logo-light.svg' : 'primeng-logo-dark.svg')" height="35" />
                    </a>
                </div>

            </div>
            <div class="right-boxes">
                <div class="box">
                    <a class="menu-button" (click)="onMenuButtonClick($event)">
                        <fa-icon [icon]="faBell"></fa-icon>
                        <span>{{this.notifyCount}}</span>
                    </a>
                </div>
                <div class="box">
                    <a class="menu-button" (click)="onMenuButtonClick($event)">
                        <fa-icon [icon]="faBookmark"></fa-icon>
                    </a>
                </div>
                <div class="box">
                    <a [routerLink] = "['request']" class="pointer custom-link">
                    My Requests
                    </a> 
                </div>  
                <div>
                <ng-container *ngIf="accountService.currentUser$ | async as user">
                    <div class="box">
                        <a routerLink = './account/login'>
                        <div class="horizontal-container">
                            <div class="vertical-container">
                                    <span>naj2bosch.con</span>
                                    <span>{{user.displayName}}</span>
                                </div>
                                <p-avatar icon="pi pi-user" styleClass="mr-2" size="small"></p-avatar>
                            </div>
                        </a>
                    </div>                            
                </ng-container>
                <ng-container *ngIf="(accountService.currentUser$ | async) === null">
                    <div class="box">             
                        <a (click)="accountService.logout()" class="btn btn-outline-secondary me-2">
                            LogIn
                        </a>           
                    </div>
                </ng-container>
            </div>
            <!-- <div class="flex align-items-center justify-content space-evenly">
              
            </div> -->
<!--          
           <div class="flex align-items-center justify-content space-evenly">
               
              
                
              
               
               
               
           </div> -->
            
            <!-- <div class="app-theme" [pTooltip]="config.theme!" tooltipPosition="bottom">
                <img [src]="'https://primefaces.org/cdn/primeng/images/themes/' + logoMap[config.theme!]" />
            </div> -->
           
        </div>
    `,
    styles:[`
    .left-boxes{
        display:flex;
        
    }
    .box {
        /* width: 100px; */
        /* height: 100px; */
        /* background-color: #ccc; */
        
        margin-right: 10px;
    }
    .right-boxes{
        display:flex;
        align-items:center;
        margin-right:0;
    }
    .vertical-container{
        display: flex;
        flex-direction: column;
    }
    .horizontal-container{
        display: flex;
        flex-direction: row;
        justify-content:center;
        align-items:center;
    }
    .pointer {
    cursor: pointer;
}
.custom-link {
    color: white; /* Set the default font color to white */
    text-decoration: none; /* Remove the underline typically associated with links */
}

.custom-link:hover {
    color: yellow; /* Change the font color to blue on hover */
}
    .vertical-container span 
    {color: white; margin-right: 0.5rem;}`],
        animations: [
        trigger('overlayMenuAnimation', [
            transition(':enter', [style({ opacity: 0, transform: 'scaleY(0.8)' }), animate('.12s cubic-bezier(0, 0, 0.2, 1)', style({ opacity: 1, transform: '*' }))]),
            transition(':leave', [animate('.1s linear', style({ opacity: 0 }))])
        ])
    ]
})
export class AppTopBarComponent implements OnInit, OnDestroy {
    @Output() menuButtonClick: EventEmitter<any> = new EventEmitter();
    faUser = faUser; faBookmark = faBookmark; faBell = faBell;
    @ViewChild('topbarMenu') topbarMenu?: ElementRef;

    @ViewChild('containerElement') containerElement?: ElementRef;

    activeMenuIndex?: number | null;

    outsideClickListener: any;

    config: AppConfig = {};

    subscription?: Subscription;

    logoMap = {
        'bootstrap4-light-blue': 'bootstrap4-light-blue.svg',
        'bootstrap4-light-purple': 'bootstrap4-light-purple.svg',
        'bootstrap4-dark-blue': 'bootstrap4-dark-blue.svg',
        'bootstrap4-dark-purple': 'bootstrap4-dark-purple.svg',
        'md-light-indigo': 'md-light-indigo.svg',
        'md-light-deeppurple': 'md-light-deeppurple.svg',
        'md-dark-indigo': 'md-dark-indigo.svg',
        'md-dark-deeppurple': 'md-dark-deeppurple.svg',
        'mdc-light-indigo': 'md-light-indigo.svg',
        'mdc-light-deeppurple': 'md-light-deeppurple.svg',
        'mdc-dark-indigo': 'md-dark-indigo.svg',
        'mdc-dark-deeppurple': 'md-dark-deeppurple.svg',
        'lara-light-indigo': 'lara-light-indigo.png',
        'lara-light-purple': 'lara-light-purple.png',
        'lara-light-blue': 'lara-light-blue.png',
        'lara-light-teal': 'lara-light-teal.png',
        'lara-dark-indigo': 'lara-dark-indigo.png',
        'lara-dark-purple': 'lara-dark-purple.png',
        'lara-dark-blue': 'lara-dark-blue.png',
        'lara-dark-teal': 'lara-dark-teal.png',
        'saga-blue': 'saga-blue.png',
        'saga-green': 'saga-green.png',
        'saga-orange': 'saga-orange.png',
        'saga-purple': 'saga-purple.png',
        'vela-blue': 'vela-blue.png',
        'vela-green': 'vela-green.png',
        'vela-orange': 'vela-orange.png',
        'vela-purple': 'vela-purple.png',
        'arya-blue': 'arya-blue.png',
        'arya-green': 'arya-green.png',
        'arya-orange': 'arya-orange.png',
        'arya-purple': 'arya-purple.png',
        nova: 'nova.png',
        'nova-alt': 'nova-alt.png',
        'nova-accent': 'nova-accent.png',
        'nova-vue': 'nova-vue.png',
        'luna-blue': 'luna-blue.png',
        'luna-green': 'luna-green.png',
        'luna-pink': 'luna-pink.png',
        'luna-amber': 'luna-amber.png',
        rhea: 'rhea.png',
        'fluent-light': 'fluent-light.png',
        'soho-light': 'soho-light.png',
        'soho-dark': 'soho-dark.png',
        'viva-light': 'viva-light.svg',
        'viva-dark': 'viva-dark.svg',
        mira: 'mira.jpg',
        nano: 'nano.jpg',
        'tailwind-light': 'tailwind-light.png'
    };

    versions: any[] = [];
    empCode?: string;
    employee?: IEmployee;
    notifymessage!: string;
    notifyCount: number = 0;
    scrollListener: any;
    baseUrl = environment.apiUrl;
    private hubConnectionBuilder!: HubConnection;
    constructor(private router: Router, private configService: AppConfigService
        , public accountService: AccountService
        , private empService: EmployeeService
        , private notificationService: NotificationService) { }

    ngOnInit() {
        this.config = this.configService.config;
        this.subscription = this.configService.configUpdate$.subscribe((config) => (this.config = config));
        this.hubConnectionBuilder = new HubConnectionBuilder().withUrl('https://localhost:7114/notify').configureLogging(LogLevel.Information).build();
        this.hubConnectionBuilder.start().then(() => console.log('Connection started...')).catch(err => console.log('Error while connect with server'));
        this.accountService.currentUser$.subscribe((emp) => {
            console.log(emp);
            this.empService.getEmployeesBaseByCode(emp?.email!).subscribe((emp) => {
                // debugger;
                //console.log(emp.team?.teamName);
                this.hubConnectionBuilder.on('BrodcastMessage', (result: Notify) => {
                    console.log(this.notifyCount);
                    console.log(`${emp.employeeCode};${emp.team?.id}: ${result.team?.teamName}`);
                    if (result.team?.teamName === 'ETB' && emp.teamRole == Role.Manager) {
                        this.notifyCount++;
                        console.log(`${emp.employeeCode}: ${result}`);
                    }

                })
            })
        });



        this.router.events.subscribe((event) => {
            if (event instanceof NavigationEnd) {
                this.activeMenuIndex = null;
                // this.activeMenuIndex = 0
            }
        });
        // this.notificationService.notify$.subscribe(() => {
        //     console.log(`Notify count: ${this.notifyCount}`);
        //     this.notifyCount++;
        // });



        this.bindScrollListener();
    }

    bindScrollListener() {
        if (!this.scrollListener) {
            this.scrollListener = () => {
                if (window.scrollY > 0) {
                    this.containerElement?.nativeElement.classList.add('layout-topbar-sticky');
                } else {
                    this.containerElement?.nativeElement.classList.remove('layout-topbar-sticky');
                }
            };
        }

        window.addEventListener('scroll', this.scrollListener);
    }

    onMenuButtonClick(event: Event) {
        this.menuButtonClick.emit();
        event.preventDefault();
    }

    changeTheme(event: Event, theme: string, dark: boolean) {
        this.configService.updateConfig({ ...this.config, ...{ theme, dark } });
        this.activeMenuIndex = null;
        // this.activeMenuIndex = 0;
        event.preventDefault();
    }

    bindOutsideClickListener() {
        if (!this.outsideClickListener) {
            this.outsideClickListener = (event: any) => {
                if (this.isOutsideTopbarMenuClicked(event)) {
                    this.activeMenuIndex = null;
                    // this.activeMenuIndex = 0;
                }
            };

            document.addEventListener('click', this.outsideClickListener);
        }
    }

    unbindOutsideClickListener() {
        if (this.outsideClickListener) {
            document.removeEventListener('click', this.outsideClickListener);
            this.outsideClickListener = null;
        }
    }

    unbindScrollListener() {
        if (this.scrollListener) {
            window.removeEventListener('scroll', this.scrollListener);
            this.scrollListener = null;
        }
    }

    toggleMenu(event: Event, index: number) {
        this.activeMenuIndex = this.activeMenuIndex === index ? null : index;
        event.preventDefault();
    }

    isOutsideTopbarMenuClicked(event: any): boolean {
        return !(this.topbarMenu?.nativeElement.isSameNode(event.target) || this.topbarMenu?.nativeElement.contains(event.target));
    }

    onOverlayMenuEnter(event: AnimationEvent) {
        switch (event.toState) {
            case 'visible':
                this.bindOutsideClickListener();
                break;

            case 'void':
                this.unbindOutsideClickListener();
                break;
        }
    }

    ngOnDestroy() {
        if (this.subscription) {
            this.subscription.unsubscribe();
        }

        this.unbindScrollListener();
    }
}
