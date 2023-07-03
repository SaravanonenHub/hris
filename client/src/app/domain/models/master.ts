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
export interface IRole {
    id: number;
    role: string;
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
    Admin = "Admin",
    Manager = "Manager",
    TeamLeader = "Team Leader",
    Member = "Member",
}
export enum OptionalSaturday {
    Y = 'YES',
    N = 'NO'
}
export interface ITeam {
    id: number;
    department: IDepartment;
    teamName: string;
    displayName: string
}
export interface ITeamwithDetails {
    id: number;
    department: IDivision;
    teamName: string;
    displayName: string;
    teamDetails: ITeamDetails[]
}
export interface ITeamDetails {
    id: number;
    department: IDepartment;
    team: ITeam;
    employee: IEmployee
    role: Role
}
export class TeamDetails {
    departmentId: number = 0;
    employeeId: number = 0
    role: string = "";
    sort: number = 1;
}
