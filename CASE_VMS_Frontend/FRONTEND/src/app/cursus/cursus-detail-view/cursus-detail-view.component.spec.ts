import { HttpRequest, HttpResponse } from '@angular/common/http';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, fakeAsync, inject, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';
import { ActivatedRoute, convertToParamMap } from '@angular/router';
import { of } from 'rxjs';
import { Cursist } from 'src/app/cursist/cursist-model';
import { CursistClientService } from 'src/app/cursist/httpclient/cursist-client.service';
import { NewCursistFormComponent } from 'src/app/cursist/new-cursist-form/new-cursist-form.component';

import { CursusDetailViewComponent } from './cursus-detail-view.component';

describe('CursusDetailViewComponent', () => {
  let component: CursusDetailViewComponent;
  let fixture: ComponentFixture<CursusDetailViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CursusDetailViewComponent,
      NewCursistFormComponent, ],
      providers: [
        {provide: ActivatedRoute, useValue: {
          queryParams: of({course: 12})
        }}
      ],
      imports: [ HttpClientTestingModule, ReactiveFormsModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CursusDetailViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    component.ngOnInit();
  });

  it("Should get students from ngOnInt", inject([CursistClientService], (client: CursistClientService) =>
  {

    let response: Cursist[] = 
    [
      {Id: 1, FirstName: "Testy", SurName: "Test", PaymentInfo: {}}
    ]
    spyOn(client, "getStudents").and.returnValue(of(new HttpResponse<Cursist[]>({body: response, status:200})));
    component.ngOnInit();

    expect(client.getStudents).toHaveBeenCalled();
  }))

  it("Should read params from url", fakeAsync(() =>
  {
    expect(component.courseNumber).toBe(12);
  }));

  it("Should Toggle the Adding Student Menu", fakeAsync(() =>
  {
    fixture.detectChanges();
    
    expect(fixture.debugElement.query(By.css(".studentForm"))).toBeNull()
    component.ToggleAddingStudent();
    fixture.whenStable().then(() =>
    {      
      fixture.detectChanges();
      expect(fixture.debugElement.query(By.css(".studentForm"))).not.toBeNull()
    })
  }));

  it("Should add new student and emit event", () => 
  {
    let expected =     
      {Amount: 1, CourseId: 12 }
    let fakeStudent: Cursist = 
    {
      Id: 1,
      FirstName: "Testy",
      SurName: "Test",
      PaymentInfo: {}
    }
    
    spyOn(component.StudentUpdate, "emit");

    component.AddedNewStudent(fakeStudent)
    expect(component.StudentUpdate.emit).toHaveBeenCalledWith(expected)
  });

  it("Should not add new student when he already is enrolled and not emit event", () =>
  {
    let fakeStudent: Cursist = 
    {
      Id: 1,
      FirstName: "Testy",
      SurName: "Test",
      PaymentInfo: {}
    }
    let fakeStudent2 = {...fakeStudent}
    spyOn(component.StudentUpdate, "emit");

    component.AddedNewStudent(fakeStudent)
    component.AddedNewStudent(fakeStudent2)

    expect(component.StudentUpdate.emit).toHaveBeenCalledTimes(1)
  });

});
