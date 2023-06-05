import { IEmployee } from "./employee";

export interface ILeave {
    id: number;
    employeeId: number;
    fromDate: Date;
    toDate: Date;
    days: number;
    leaveType: LeaveType;
    session: string;
    createDate: Date;
    reason: string;
    status: string
    employee?: IEmployee
}
export class Leave implements ILeave {
    reason: string = '';
    createdBy: string = '';
    status: string = 'PENDING';
    id: number = 0;
    employeeId: number = 0;
    fromDate: Date = new Date();
    toDate: Date = new Date();
    days: number = 0;
    leaveType: LeaveType = LeaveType.CASUALLEAVE;
    session: string = 'FULLDAY';
    createDate: Date = new Date();

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
    CASUALLEAVE = 'Casual',
    EARNEDLEAVE = 'Earned',
    SICKLEAVE = 'Sick'
}