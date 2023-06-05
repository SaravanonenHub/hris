import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Employee } from 'src/app/domain/models/employee';
import { ILeave, Leave } from 'src/app/domain/models/leave';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LeaveService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }
  create(params: any) {
    console.log(params);
    return this.http.post<ILeave>(this.baseUrl + 'Leave/create', params);
  }
  Actioncreate(id: number) {
    let params = new HttpParams().set('requestid', id);
    console.log(`Selected Id is ${id}`);
    // params = params.append('requestid', id);
    return this.http.post<ILeave>(this.baseUrl + 'Leave/approval', {}, { params });
  }
  getRequests(id: number) {
    let params = new HttpParams();
    params = params.append('empid', id);
    return this.http.get<ILeave[]>(this.baseUrl + 'Leave/requests', { params });
  }
}
