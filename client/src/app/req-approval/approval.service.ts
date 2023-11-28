import { Injectable } from "@angular/core";

import { environment } from 'src/environments/environment';
import { IRequest } from "../domain/models/request";
import { HttpClient } from "@angular/common/http";
@Injectable({
    providedIn:"root"
})

export class ApprovalService{
    baseAPIURL=environment.apiUrl;
    constructor(private http:HttpClient){}
    getOpenApproval(id:number){
        return this.http.get<IRequest[]>(`${this.baseAPIURL}Approval/getApproval/${id}`);
    }
}