import { Component, OnInit } from '@angular/core';

// Needed to use API method
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-emp',
  templateUrl: './show-emp.component.html',
  styleUrls: ['./show-emp.component.css']
})
export class ShowEmpComponent implements OnInit {

  constructor(private service: SharedService) { }

  EmployeeList: any = [];
  ModalTitle:string = "";
  ActivateAddEditEmpComp:boolean = false;
  emp:any;

  // First method executed when this component is in scope
  ngOnInit(): void {
    this.refreshEmployeeList();
  }

  addClick(){
    this.emp = {
      Id:0,
      Name:"",
      Department:"",
      DateOfJoining:"",
      PhotoFileName:"anonymous.png"
    }
    this.ModalTitle = "Add Employee";
    this.ActivateAddEditEmpComp = true;
  }

  editClick(item:any){
    this.emp = item;
    this.ModalTitle = "Edit Employee";
    this.ActivateAddEditEmpComp = true;
  }

  deleteClick(item:any){
    if(confirm('Are you sure?')){
      this.service.deleteEmployee(item.Id).subscribe(data => {
        alert(data.toString());
        this.refreshEmployeeList();
      });
    }
  }

  closeClick(){
    this.ActivateAddEditEmpComp = false;
    this.refreshEmployeeList();
  }

  refreshEmployeeList() {
    // Subscribe waits until response is received from API call. Asynchronous method
    this.service.getEmployeeList().subscribe(data => {
      this.EmployeeList = data;
    })
  }

}
