import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Employee } from 'src/app/domain/models/employee';
import { ILeave, ILeaveEntitlement, ILeaveRequest, Leave } from 'src/app/domain/models/leave';
import { leavePolicyParams } from 'src/app/shared/models/leavePolicyParams';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LeaveService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }
  create(params: any) {
    console.log(params);
    return this.http.post<ILeaveRequest>(this.baseUrl + 'Leave/create', params);
  }
  cancelRequest(id: number,params:any) {
    console.log(params);
    return this.http.put<ILeaveRequest>(`${this.baseUrl}Leave/cancel/${id}`,params);
  }
  Actioncreate(id: number) {
    let params = new HttpParams().set('requestid', id);
    console.log(`Selected Id is ${id}`);
    // params = params.append('requestid', id);
    return this.http.post<ILeave>(this.baseUrl + 'Leave/approval', {}, { params });
  }
  BulKAction(ids: string) {
    let params = new HttpParams().set('bulkIds', ids)
    return this.http.post<ILeave>(this.baseUrl + 'Leave/bulkapporval', {}, { params });
  }
  getEntitlement(param: leavePolicyParams) {

    let params = new HttpParams();


    if (param.policyName !== "") {
      params = params.append('PolicyName', param.policyName!.toString());
    }
    if (param.leaveType !== "") {
      params = params.append('LeaveType', param.leaveType!.toString());
    }
    if (param.empId! > 0) {
      params = params.append('EmpId', param?.empId!.toString())

    }
    return this.http.get<ILeaveEntitlement>(this.baseUrl + 'Leave/entitlement', { params });
  }
  getRequests(id: number) {
    let params = new HttpParams();
    params = params.append('empid', id);
    return this.http.get<ILeave[]>(this.baseUrl + 'Leave/requests', { params });
  }
  getRequest(id: number) {
    let params = new HttpParams();
    params = params.append('id', id);
    return this.http.get<ILeaveRequest>(`${this.baseUrl}Leave/request/${id}`);
  }
  getPendingRequests(id: number) {
    let params = new HttpParams();
    params = params.append('empid', id);
    return this.http.get<ILeave[]>(`${this.baseUrl}Leave/pendingRequests/${id}`);
  }
}
