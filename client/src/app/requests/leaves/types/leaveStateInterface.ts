import { ILeave } from "src/app/domain/models/leave";

export interface LeaveStateInterface{
    IsSucced:boolean,
    Leaves:ILeave[],
    error:string|null
}