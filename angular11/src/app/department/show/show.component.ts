import { Component, OnInit } from '@angular/core';

// Needed to use API method
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show',
  templateUrl: './show.component.html',
  styleUrls: ['./show.component.css']
})
export class ShowComponent implements OnInit {

  constructor(private service: SharedService) { }

  DepartmentList: any = [];
  ModalTitle:string = "";
  ActivateAddEditDepComp:boolean = false;
  dep:any;

  // For use with filtering
  IdFilter:string = "";
  NameFilter:string = "";
  ListWithoutFilter:any = [];

  // First method executed when this component is in scope
  ngOnInit(): void {
    this.refreshDepartmentList();
  }

  addClick(){
    this.dep = {
      Id:0,
      Name:""
    }
    this.ModalTitle = "Add Department";
    this.ActivateAddEditDepComp = true;
  }

  editClick(item:any){
    this.dep = item;
    this.ModalTitle = "Edit Department";
    this.ActivateAddEditDepComp = true;
  }

  deleteClick(item:any){
    if(confirm('Are you sure?')){
      this.service.deleteDepartment(item.Id).subscribe(data => {
        alert(data.toString());
        this.refreshDepartmentList();
      });
    }
  }

  closeClick(){
    this.ActivateAddEditDepComp = false;
    this.refreshDepartmentList();
  }

  refreshDepartmentList() {
    // Subscribe waits until response is received from API call. Asynchronous method
    this.service.getDepartmentList().subscribe(data => {
      this.DepartmentList = data;
      this.ListWithoutFilter = data;
    })
  }

  FilterDep(){
    var IdFilter = this.IdFilter;
    var NameFilter = this.NameFilter;

    // Filter entire list for cases where the id and name match the search terms
    this.DepartmentList = this.ListWithoutFilter.filter(function (el:any){
      return el.Id.toString().toLowerCase().includes(
        IdFilter.toString().trim().toLowerCase()
      ) && 
      el.Name.toString().toLowerCase().includes(
        NameFilter.toString().trim().toLowerCase()
      )
    });
  }

  // Sort the list ascending or descending based on the desired property
  sortResult(prop:string, asc:boolean){
    this.DepartmentList = this.ListWithoutFilter.sort(function (a:any,b:any){
      if(asc){
        return (a[prop]>b[prop]) ? 1 : ((a[prop]<b[prop]) ? -1:0)
      }else {
        return (b[prop]>a[prop]) ? 1 : ((b[prop]<a[prop]) ? -1:0)
      }
    })
  }

}
