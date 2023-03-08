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
    departmentName: string;
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
export enum Role {
    Admin,
    Manager,
    TeamLeader,
    Member
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
