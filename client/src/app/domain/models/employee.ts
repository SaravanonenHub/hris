import { IBranch, IDepartment, IDesignation, IDivision, Role, ITeam } from "./master";


export interface IEmployee {
    id?: number;
    employeeCode: string;
    firstName?: string;
    lastName?: string;
    displayName?: string;
    imagePath?: string;
    branch?: IBranch;
    division?: IDivision;
    department?: IDepartment;
    designation?: IDesignation;
    qualification?: string;
    status?: string;
    birthDate?: string;
    age?: number;
    joinDate?: string;
    emailID?: string;
    gender?: string;
    bloodGroup?: string;
    martialStatus?: string;
    employeeNature?: string;
    optionalSaturday?: string;
    team?: ITeam;
    teamRole?: Role;
}

export class Employee implements IEmployee {

    firstName = '';
    lastName = '';
    employeeCode: string = '';
    branchId: number = 0;
    divisionId: number = 0;
    departmentId: number = 0;
    designationId: number = 0;
    teamId: number = 0
    teamRoleId: number = 0;

    fullName() {
        return this.firstName.concat(this.lastName);
    }

}