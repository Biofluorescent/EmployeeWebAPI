import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
readonly APIUrl = "http://localhost:6792/api";
readonly PhotoUrl = "http://localhost:6792/Photos/";

  constructor(private http: HttpClient) { }

  // Department API endpoints
  getDepartmentList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl + '/Department');
  }

  addDepartment(val:any) {
    return this.http.post(this.APIUrl + '/Department', val);
  }

  updateDepartment(val:any) {
    return this.http.put(this.APIUrl + '/Department', val);
  }

  deleteDepartment(val:any) {
    return this.http.delete(this.APIUrl + '/Department/' + val);
  }
  
  // Employee API endpoints
  getEmployeeList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl + '/Employee');
  }

  addEmployee(val:any) {
    return this.http.post(this.APIUrl + '/Employee', val);
  }

  updateEmployee(val:any) {
    return this.http.put(this.APIUrl + '/Employee', val);
  }

  deleteEmployee(val:any) {
    return this.http.delete(this.APIUrl + '/Employee/' + val);
  }


  // Save photo API endpoint
  uploadPhoto(val:any) {
    return this.http.post(this.APIUrl + '/Employee/SaveFile', val);
  }

  // Get all department names
  getAllDepartmentNames():Observable<any[]> {
    return this.http.get<any[]>(this.APIUrl + '/Employee/GetAllDepartmentNames');
  }

}
