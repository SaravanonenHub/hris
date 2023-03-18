import { Injectable } from "@angular/core";
import { filter, Observable, Subject } from "rxjs";
import { Alert, AlertOptions, AlertType } from "../models/alert";

@Injectable({
    providedIn: 'root'
})
export class AlertService {
    private subject = new Subject<Alert>();
    private defaultId = 'default-alert';

    onAlert(id = this.defaultId): Observable<Alert> {
        return this.subject.asObservable().pipe(filter(x => x && x.id === id));
    }

    successAlert(message: string, options?: AlertOptions) {
        this.alert(new Alert({ ...options, type: AlertType.Success, message: message }))
    }

    errorAlert(message: string, options?: AlertOptions) {
        this.alert(new Alert({ ...options, type: AlertType.Error, message: message }))
    }

    infoAlert(message: string, options?: AlertOptions) {
        this.alert(new Alert({ ...options, type: AlertType.Info, message: message }))
    }

    warningAlert(message: string, options?: AlertOptions) {
        this.alert(new Alert({ ...options, type: AlertType.Warning, message: message }))
    }

    alert(alert: Alert) {
        console.log(alert);
        alert.id == alert.id || this.defaultId;
        this.subject.next(alert);

    }

    clear(id = this.defaultId) {
        this.subject.next(new Alert({ id }))
    }
}