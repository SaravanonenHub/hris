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
export interface ILeavePolicy{
    id:number;
    policyName:string;
    shortName:string;

}
export interface ILeavePolicyWithDetails{
    id:number;
    policyName:string;
    shortName:string;
    leavePolicyDetail:ILeavePolicyDetails[];
}
export interface ILeavePolicyDetails{
    id:number;
    leaveType:ILeaveType;
    day:number;
}
export interface ILeaveType{
    id:number;
    leaveName:string;
    shortName:string
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
    TeamLeader = "TeamLeader",
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
    displayName: string;
  
}
export interface ITeamwithDetails {
    id: number;
    departmentId:number;
    department: IDepartment;
    teamName: string;
    displayName: string;
    teamDetails: ITeamDetails[];
    manager:IEmployee;
    teamLeader:IEmployee;
    members:IEmployee[];
}
export interface ITeamDetails {
    id: number;
    department: IDepartment;
    team: ITeam;
    employee: IEmployee
    role: IRole
}
export class TeamDetails {
    departmentId: number = 0;
    employeeId: number = 0
    roleName: string = "";
    sort: number = 1;
    displayName:string="";
    
}
export enum RequestTemplate{
    Leave = 1,
    OnDuty,
    Permission
    
}
