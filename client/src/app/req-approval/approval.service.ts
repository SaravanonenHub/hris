import { Injectable } from "@angular/core";

import { environment } from 'src/environments/environment';
import { IRequest } from "../domain/models/request";
import { HttpClient, HttpParams } from "@angular/common/http";
import { AccountService } from "../account/account.service";
import { firstValueFrom } from "rxjs";
@Injectable({
    providedIn:"root"
})

export class ApprovalService{
    baseAPIURL=environment.apiUrl;

    constructor(private http:HttpClient, private account:AccountService){}
    async getOpenApproval(){
        let params = new HttpParams();
        params = params.append('Status','Created');
        const user = await firstValueFrom(this.account.currentUser$)
        console.log(user);
        const header = {
            Authorization:`Bearer ${user?.token}`
        }
        return this.http.get<IRequest[]>(`${this.baseAPIURL}Approve/approvals`,{headers:header,params:params});
    }
}