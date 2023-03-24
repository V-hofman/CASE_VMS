import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { template } from 'lodash';
import * as moment from 'moment';
import { Cursist } from 'src/app/cursist/cursist-model';
import { CursistClientService } from 'src/app/cursist/httpclient/cursist-client.service';

@Component({
  selector: 'BLD-cursus-detail-view',
  templateUrl: './cursus-detail-view.component.html',
  styleUrls: ['./cursus-detail-view.component.css']
})
export class CursusDetailViewComponent implements OnInit {

  @Output("StudentsChanged") StudentUpdate: EventEmitter<{CourseId: number; Amount: number;}> = new EventEmitter();

  constructor(
    private cursistClient: CursistClientService,
    private _route: ActivatedRoute,
    ){}

    addingStudent : boolean = false;
    students: Cursist[] = [];
    courseNumber!: number;

    
    
    ngOnInit(): void
    {
      this._route.queryParams.subscribe(params =>
      {
        if('course' in params)
        {
          this.courseNumber = parseInt(params['course'])               
          
        }      
      });
      this.cursistClient.getStudents(this.courseNumber).subscribe(
        (response) =>
        {
          
          if(response.body instanceof Array)
          {
            response.body.forEach((student) =>
            {
              if("Name" in student && typeof student.Name === 'string'
              && "Surname" in student && typeof student.Surname === 'string')
              {
                
                let tempStudent: Cursist = {
                  SurName: student.Surname,
                  FirstName: student.Name,
                  Id: student.Id,
                  PaymentInfo: student.PaymentInfo
                };
                
                this.students.push(tempStudent)
              }          
            })
          }
        });
    }

    ToggleAddingStudent()
    {    
      this.addingStudent = !this.addingStudent;
    }

    AddedNewStudent(student: Cursist)
    {
      if(!this.students.find(s => s.FirstName == student.FirstName && s.SurName == student.SurName))
      {
        this.students.push(student);
        
        this.StudentUpdate.emit({Amount: this.students.length, CourseId: this.courseNumber });
      }
    }
  }


