import { IDepartment } from "src/app/domain/models/master";

export class EmployeeParams{
    status:string="";
    nature:string="";
    search:string="";
    departmentIDs:number[]=[]
}