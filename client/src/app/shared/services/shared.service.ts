import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { IDepartment } from 'src/app/domain/models/master';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  baseUrl = environment.apiUrl;
  departments: IDepartment[] = [];
  constructor(private http:HttpClient) { }
  getDepartments() {
    if (this.departments.length > 0) return of(this.departments);

    return this.http.get<IDepartment[]>(this.baseUrl + 'Master/departments').pipe(
      map(data => this.departments = data)
    );
  }
}
