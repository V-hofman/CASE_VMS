import { HttpResponse } from '@angular/common/http';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, fakeAsync, flush, inject, TestBed, tick } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { DragDropFilesComponent } from 'src/app/utils/drag-drop-files/drag-drop-files.component';
import { CursistCourseDTO } from '../cursist-model';
import { CursistClientService } from '../httpclient/cursist-client.service';

import { IsEmptyString, NewCursistFormComponent } from './new-cursist-form.component';

describe('NewCursisFormComponent', () => {
  let component: NewCursistFormComponent;
  let fixture: ComponentFixture<NewCursistFormComponent>;
  
  beforeEach(async () => {
      await TestBed.configureTestingModule({
        declarations: [ NewCursistFormComponent ],
        imports: [
          HttpClientTestingModule,
          ReactiveFormsModule,
        RouterTestingModule]
        }).compileComponents();
        fixture = TestBed.createComponent(NewCursistFormComponent);
      
      component = fixture.componentInstance;
      fixture.detectChanges();
  });

    it("Should not send a new student to the server when empty fields", inject([CursistClientService],fakeAsync((client: CursistClientService)=>
    {
        spyOn(component, 'SendStudent').and.callThrough();
        spyOn(client, 'postStudent').and.stub();
        fixture.whenStable().then(() => {
        let buttons = fixture.debugElement.nativeElement.querySelectorAll('button');
        buttons[0].click();
        tick();
        fixture.detectChanges();
        
        expect(component.SendStudent).toHaveBeenCalled();
        expect(client.postStudent).not.toHaveBeenCalled();
        });
    })));

    it("Should send a new student to the server when filled fields", inject([CursistClientService],fakeAsync((client: CursistClientService)=>
    {
        let response : CursistCourseDTO = {CourseId: 1, Cursist: { Id: 1, FirstName: "test", SurName: "test", PaymentInfo: {}}}
        
        spyOn(component, 'SendStudent').and.callThrough();
        spyOn(client, 'postStudent').and.returnValue(of(new HttpResponse<CursistCourseDTO>({body: response, status:200})));

        fixture.whenStable().then(() => {
        component.attendeeForm.controls['firstName'].setValue("test");
        component.attendeeForm.controls['lastName'].setValue("test");
        tick();
        fixture.detectChanges();

        component.SendStudent();
        expect(component.SendStudent).toHaveBeenCalled();
        expect(client.postStudent).toHaveBeenCalled();
        });

    } )));

    

    it("Should return true on empty string", () =>
    {
        expect(IsEmptyString("")).toBeTruthy();
    });
    
    it("Should return false on non-empty string", () =>
    {
        expect(IsEmptyString("test")).toBeFalsy();
    });
});
