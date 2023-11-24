import { Validators } from "@angular/forms";
import { IEmployee } from "./employee";
import { ILeaveType } from "./master";

export interface ILeave {
    id: number;
    idStr?:string;
    employeeId: number;
    fromDate: Date;
    toDate: Date;
    days: number;
    leaveType: LeaveType;
    session: string;
    createDate: Date;
    reason: string;
    status: string
    employee?: IEmployee;
    fDate?:string;
    tDate?:string;
    cancellationStatus:string;
}
export class Leave implements ILeave {
    reason: string = '';
    createdBy: string = '';
    status: string = 'Submitted';
    id: number = 0;
    employeeId: number = 0;
    fromDate: Date = new Date();
    toDate: Date = new Date();
    days: number = 0;
    leaveType: LeaveType = LeaveType.CASUALLEAVE;
    session: string = 'FULLDAY';
    createDate: Date = new Date();
    templateId:number = 1;
    cancellationStatus:string = 'N';
    
}
export interface ILeaveEntitlement {
    id:Number,
    policyName:string;
    shortName:string;
    details:ILeaveEntitlementDetail[]
}
export interface ILeaveEntitlementDetail{
    leaveType:ILeaveType;
    provided:number;
    taken:number;
}
// export class LeaveType {
//     id?: number;
//     leaveType?: string;
// }
export enum Session {
    FULLDAY = 'FULLDAY',
    FIRSTSESSION = 'FIRST SESSION',
    SECONDSESSION = 'SECOND SESSION'
}
export enum LeaveType {
    CASUALLEAVE = 'Casual Leave',
    EARNEDLEAVE = 'Earned Leave',
    SICKLEAVE = 'Sick Leave'
}