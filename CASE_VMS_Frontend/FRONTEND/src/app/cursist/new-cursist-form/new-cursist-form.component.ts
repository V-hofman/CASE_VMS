import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Cursist, CursistCourseDTO } from '../cursist-model';
import { CursistClientService } from '../httpclient/cursist-client.service';

@Component({
  selector: 'BLD-new-cursist-form',
  templateUrl: './new-cursist-form.component.html',
  styleUrls: ['./new-cursist-form.component.css']
})
export class NewCursistFormComponent implements OnInit {
  constructor(
    private cursistClient: CursistClientService,
    private _route: ActivatedRoute
    ){}

  @Output("toggleAddingStudent") toggleAddingStudent: EventEmitter<any> = new EventEmitter();
  @Output("addedNewStudent") addedNewStudent: EventEmitter<any> = new EventEmitter();

  attendeeForm = new FormGroup({
    firstName:new FormControl(""),
    lastName: new FormControl(""),
  });

  courseBeingViewed!: number;
  response: string[] = [];

  ngOnInit()
  {
    
       this._route.queryParams.subscribe(params =>
      {
        if('course' in params)
        {
          this.courseBeingViewed = parseInt(params['course'])               
          
        }      
      });
  }

  SendStudent()
  {
    
    if(!IsEmptyString(this.attendeeForm.getRawValue().firstName) && !IsEmptyString(this.attendeeForm.getRawValue().lastName))
    {      
      let tempCursist: Cursist = {
        FirstName: this.attendeeForm.getRawValue().firstName!,
        SurName: this.attendeeForm.getRawValue().lastName!,
        Id: 0,
        PaymentInfo: {}
      };
  
      let DTO: CursistCourseDTO = {
        Cursist: tempCursist,
        CourseId: this.courseBeingViewed  
      };
        
      this.cursistClient.postStudent(DTO).subscribe(
        (res) =>
         {                 
            if(res.status === 200)
            {
              alert("Student is toegevoegd!");
            }
          
          }
      );

  
      this.toggleAddingStudent.emit();
      this.addedNewStudent.emit(tempCursist)
    }else
    {
      this.response.push("Wel iemand invullen aub")
    }

  }
}

export function IsEmptyString(text:string | null) {
  return text === null || text.match(/^ *$/) !== null;
}
