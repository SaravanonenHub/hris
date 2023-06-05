import { ITeam } from "src/app/domain/models/master";

export class Notify {
    type?: NotifyType;
    team?: ITeam
    message?: string;
    routeUrl?: string;
    autoclose?: boolean = true;
    keepAfterRouteChange?: boolean = false;
    fade?: boolean

    constructor(init?: Partial<Notify>) {
        Object.assign(this, init);
    }

}
export enum NotifyType {
    Request,
    Approval,
    Info
}