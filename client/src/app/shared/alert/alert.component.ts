import { ThisReceiver } from '@angular/compiler';
import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Alert, AlertType } from '../models/alert';
import { AlertService } from '../services/alertService';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss']
})
export class AlertComponent implements OnInit, OnDestroy {
  @Input() id = 'default-alert';
  @Input() fade = true;

  alerts: Alert[] = [];
  alertSubscription!: Subscription;
  routeSubscription!: Subscription;

  constructor(private router: Router, private alertService: AlertService) { }

  ngOnInit(): void {
    console.log(this.id);
    this.alertSubscription = this.alertService.onAlert(this.id).subscribe(alert => {
      // clear alerts when an empty alert is received
      if (!alert.message) {
        // filter out alerts without 'keepAfterRouteChange' flag
        this.alerts = this.alerts.filter(x => x.keepAfterRouteChange);

        // filter out alerts without 'keepAfterRouteChange' flag
        this.alerts.forEach(x => x.keepAfterRouteChange == false);
        return;
      }

      this.alerts.push(alert);

      if (alert.autoclose) {
        setTimeout(() => {
          this.removeAlert(alert);
        }, 3000);
      }
    });

    this.routeSubscription = this.router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        this.alertService.clear(this.id)
      }
    })

  }
  ngOnDestroy(): void {
    this.alertSubscription.unsubscribe;
    this.routeSubscription.unsubscribe;
  }

  removeAlert(alert: Alert) {
    if (!this.alerts.includes(alert)) return;

    const timeout = this.fade ? 250 : 0;
    alert.fade = this.fade;

    setTimeout(() => {
      this.alerts = this.alerts.filter(x => x !== alert)
    }, timeout);

  }

  cssClass(alert: Alert) {
    if (alert?.type === undefined) return;

    const alertTypeClass = {
      [AlertType.Success]: 'alert-success',
      [AlertType.Error]: 'alert-danger',
      [AlertType.Info]: 'alert-info',
      [AlertType.Warning]: 'alert-warning',
    }

    const classes = [alertTypeClass[alert.type]];

    if (alert.fade) classes.push('fade')
    return classes.join(' ')
  }
}
