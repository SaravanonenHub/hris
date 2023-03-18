export class Alert {
    id?: string = 'default-alert';
    type?: AlertType;
    message?: string;
    autoclose?: boolean = true;
    keepAfterRouteChange?: boolean = false;
    fade?: boolean

    constructor(init?: Partial<Alert>) {
        Object.assign(this, init);
    }

}

export enum AlertType {
    Success,
    Error,
    Info,
    Warning
}

export class AlertOptions {
    id?: string;
    autoClose?: boolean;
    keepAfterRouteChange?: boolean
}