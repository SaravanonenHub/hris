import { IEmployee } from "./employee";

export interface IBranch {
    id: number;
    branchName: string;
}
export interface IDivision {
    id: number,
    divisionName: string
}
export interface IDepartment {
    id: number;
    division: IDivision;
    departmentName: string;
}
export interface IDesignation {
    id: number;
    designationName: string;
}
export enum Gender {
    Male = 'Male',
    Female = 'Female'
}
export enum EmpContentType {
    GetStarted = 'GetStarted',
    Personal = 'Personal',
    Experience = 'Experience'
}
export enum MartialStatus {
    Single = 'Single',
    Married = 'Married'
}
export enum EmployeeNature {
    OnRoll = 'OnRoll',
    Contract = 'Contract'
}
export enum Status {
    Live = "Live",
    NotWorking = "Not Working"
}
export enum Role {
    Admin = 1,
    Manager = 2,
    TeamLeader = 3,
    Member = 4,
}
export enum OptionalSaturday {
    Y = 'YES',
    N = 'NO'
}
export interface ITeam {
    id: number;
    department: IDivision;
    teamName: string;
    displayName: string
}
export interface ITeamDetails {
    id: number;
    department: IDivision;
    team: ITeam;
    employee: IEmployee
    teamRole: Role
}
