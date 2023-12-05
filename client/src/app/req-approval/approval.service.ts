import { Injectable, Injector, Type } from "@angular/core";

import { environment } from 'src/environments/environment';
import { IRequest, IRequestDetails } from "../domain/models/request";
import { HttpClient, HttpParams } from "@angular/common/http";
import { AccountService } from "../account/account.service";
import { firstValueFrom } from "rxjs";
import { LeaveDetailsComponent } from "../requests/leaves/leave-details/leave-details.component";
@Injectable({
    providedIn: "root"
})

export class ApprovalService {
    baseAPIURL = environment.apiUrl;
    token = localStorage.getItem('token');
    header = {
        Authorization: `Bearer ${this.token}`
    };
    constructor(private http: HttpClient, private account: AccountService) { }
    async getOpenApproval() {
        let params = new HttpParams();
        params = params.append('Status', 'Created');
        const user = await firstValueFrom(this.account.currentUser$)
        console.log(user);
        const header = {
            Authorization: `Bearer ${user?.token}`
        }
        return this.http.get<IRequest[]>(`${this.baseAPIURL}Approve/approvals`, { headers: header, params: params });
    }
    getRequestDetail(id: number) {
        return this.http.get<IRequestDetails>(`${this.baseAPIURL}Request/request/${id}`, { headers: this.header });
    }
    getDetailComponent(req: IRequest): { component: Type<any>, inputs: Record<string, unknown> } {
        if (req != undefined) {
            if (req.type.templateName == "Leave") {
                return {
                    component: LeaveDetailsComponent
                    , inputs: { request: req }
                }
            }
        }

        return { component: LeaveDetailsComponent, inputs: { request: req } }
    }
    requestApprove(id: number) {
        // let param = new HttpParams();
        // param = param.append('id', id);
        // param = param.append('status', "Approved");
        const body = { 'id': id, 'status': 'Approved' };
        return this.http.put(this.baseAPIURL + 'Leave/approval', body);
    }
}