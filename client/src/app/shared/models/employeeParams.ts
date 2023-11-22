import { IDepartment } from "src/app/domain/models/master";

export class EmployeeParams {
    code:string = ""
    status: string = "";
    nature: string = "";
    search: string = "";
    role: string = "";
    departmentIDs: number[] = []
}