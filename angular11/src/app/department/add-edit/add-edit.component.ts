import { Component, OnInit, Input } from '@angular/core';
// To call API methods
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-add-edit',
  templateUrl: './add-edit.component.html',
  styleUrls: ['./add-edit.component.css']
})
export class AddEditComponent implements OnInit {

  constructor(private service:SharedService) { }
  
  @Input() dep:any;
  Id:string="";
  Name:string="";

  ngOnInit(): void {
    this.Id = this.dep.Id;
    this.Name = this.dep.Name;
  }

  addDepartment(){
    var val = {Id:this.Id,
                Name:this.Name
              };
    this.service.addDepartment(val).subscribe(res => {
      alert(res.toString());
    });
  }

  updateDepartment(){
    var val = {Id:this.Id,
      Name:this.Name
    };
    this.service.updateDepartment(val).subscribe(res => {
      alert(res.toString());
    });
  }

}
