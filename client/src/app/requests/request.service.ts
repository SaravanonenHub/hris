import { Injectable } from "@angular/core";

import { environment } from 'src/environments/environment';
import { IRequest, IRequestDetails } from "../domain/models/request";
import { HttpClient } from "@angular/common/http";
@Injectable({
    providedIn:'root'
})
export class RequestService{
    baseUrl = environment.apiUrl;
    constructor(private http:HttpClient){}
    getRequests(){
        return this.http.get<IRequest[]>(`${this.baseUrl}Request/requests`);
    }
    getRequestDetail(id:number){
        return this.http.get<IRequestDetails>(`${this.baseUrl}Request/request/${id}`);
    }
}