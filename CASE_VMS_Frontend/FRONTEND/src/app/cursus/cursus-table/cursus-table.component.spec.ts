import { HttpClientTestingModule } from '@angular/common/http/testing';
import {
  ComponentFixture,
  fakeAsync,
  getTestBed,
  inject,
  tick,
} from '@angular/core/testing';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { CursusTableItem } from '../cursus-model';

import { CursusTableComponent } from './cursus-table.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import * as moment from 'moment';
import { of } from 'rxjs';
import { By } from '@angular/platform-browser';
import { CursusClientService } from '../httpclient/cursus-client.service';
import { Cursist } from 'src/app/cursist/cursist-model';

const TestBed = getTestBed();
let router: Router;
let currentweek = moment(new Date()).isoWeek();
let currentYear = new Date().getFullYear();

describe('CursusTableComponent', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CursusTableComponent],
      imports: [
        HttpClientTestingModule,
        NoopAnimationsModule,
        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        RouterTestingModule.withRoutes([]),
        ReactiveFormsModule,
      ],
    });
    TestBed.compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CursusTableComponent);
    component = fixture.componentInstance;
    router = TestBed.inject(Router);
    component.ngOnInit();
  });

  let component: CursusTableComponent;
  let fixture: ComponentFixture<CursusTableComponent>;

  const testData: CursusTableItem[] = [
    {
      Duration: 5,
      Title: 'TestData',
      NumberOfSignedIn: 4,
      StartDate: new Date('2019-01-16'),
      Id: 5
    },
    {
      Duration: 5,
      Title: 'TestData',
      NumberOfSignedIn: 4,
      StartDate: new Date('2019-01-18'),
      Id: 4
    },
    {
      Duration: 5,
      Title: 'TestData',
      NumberOfSignedIn: 4,
      StartDate: new Date('2020-02-16'),
      Id: 3
    },
    {
      Duration: 5,
      Title: 'TestData',
      NumberOfSignedIn: 4,
      StartDate: new Date('2019-02-16'),
      Id: 2
    },
  ];

  it('Should set up everything', () => {
    fixture.detectChanges();

    const sort = component.dataSource.sort;
    expect(sort).toBeInstanceOf(MatSort);
  });

  it('Should render the proper amount of tables', () => {
    fixture.detectChanges();
    fixture.componentInstance.ngAfterViewInit();
    let tableAmount = fixture.nativeElement.querySelectorAll('table');
    expect(tableAmount.length).toBe(1);
  });

  it('Should click ChangeWeeknumber when pressing button', fakeAsync(() => {
    spyOn(component, 'ChangeWeekNumber');
    component.weekNumberBeingViewed = 12;
    component.yearNumberBeingViewed = 2012;
    fixture.detectChanges();

    let buttons = fixture.debugElement.nativeElement.querySelectorAll('button');
    buttons[1].click();

    expect(component.ChangeWeekNumber).toHaveBeenCalled();
  }));

  it('Should click ChangeYearnumber when pressing button', fakeAsync(() => {
    spyOn(component, 'ChangeYearNumber');
    component.weekNumberBeingViewed = 12;
    component.yearNumberBeingViewed = 2012;
    fixture.detectChanges();

    let buttons = fixture.debugElement.nativeElement.querySelectorAll('button');
    buttons[0].click();

    expect(component.ChangeYearNumber).toHaveBeenCalled();
  }));

  it('Should increase weekNumberBeingViewed', () => {
    fixture.detectChanges();

    component.ChangeWeekNumber(1);

    expect(component.weekNumberBeingViewed).toBe(currentweek + 1);
  });

  it('Should increase yearNumberBeingViewed', () => {
    fixture.detectChanges();

    component.ChangeYearNumber(1);

    expect(component.yearNumberBeingViewed).toBe(currentYear + 1);
  });

  it('Should loop back a year when weeknumber is 0', () => {
    fixture.detectChanges();
    
    
    component.ChangeWeekNumber(-currentweek);
    
    
    expect(component.weekNumberBeingViewed).toBe(52);
    expect(component.yearNumberBeingViewed).toBe(currentYear - 1);
  });

  it('Should loop back a year when weeknumber is very negative', () => {
    fixture.detectChanges();

    component.ChangeWeekNumber(-1000);

    expect(component.weekNumberBeingViewed).toBe(52);
    expect(component.yearNumberBeingViewed).toBe(currentYear - 1);
  });

  it('Should loop to next year when weeknumber is 53', () => {
    fixture.detectChanges();

    component.ChangeWeekNumber((53 - currentweek));

    expect(component.yearNumberBeingViewed).toBe(currentYear + 1);
    expect(component.weekNumberBeingViewed).toBe(1);
  });

  it('Should loop to next year when weeknumber is massive', () => {
    fixture.detectChanges();

    component.ChangeWeekNumber(1000);

    expect(component.yearNumberBeingViewed).toBe(currentYear + 1);
    expect(component.weekNumberBeingViewed).toBe(1);
  });

  it('Should group the dates by weeknumber', () => {
    let expected = [testData[0], testData[1]];

    let filteredGroups = component.FilterByWeek(testData);
    let firstKey = Object.keys(filteredGroups)[0];

    let actual = filteredGroups[firstKey];

    expect(actual).toEqual(expected);
  });

  it('Should group the dates by yearnumber', () => {
    let expected = [testData[0], testData[1], testData[3]];

    let filteredGroups = component.FilterByYear(testData);
    let firstKey = Object.keys(filteredGroups)[0];

    let actual = filteredGroups[firstKey];

    expect(actual).toEqual(expected);
  });

  it('Should group the dates by weeknumber and then by yearnumber', () => {
    let expected = [testData[0], testData[1]];

    let filteredGroups = component.FilterByWeek(testData);
    let firstKey = Object.keys(filteredGroups)[0];

    let filteredByWeekAndYear = component.FilterByYear(
      filteredGroups[firstKey]
    );
    let secondKey = Object.keys(filteredByWeekAndYear)[0];

    let actual = filteredByWeekAndYear[secondKey];

    expect(actual).toEqual(expected);
  });

  it("Should handle manual input for the week and year numbers", fakeAsync(() =>
  {
    fixture.detectChanges();
    fixture.whenStable().then(() =>
    {
      
      let weekInput = fixture.debugElement.nativeElement.querySelector("#week")
      let yearInput = fixture.debugElement.nativeElement.querySelector('#year')
      let form = fixture.debugElement.query(By.css("form"))
      weekInput.value = 20;
      yearInput.value= 2020;
      
      weekInput.dispatchEvent(new Event('input'));
      yearInput.dispatchEvent(new Event('input'));
      form.triggerEventHandler('submit');
  
      fixture.detectChanges();
      
      expect(component.weekNumberBeingViewed).toBe(20)
      expect(component.yearNumberBeingViewed).toBe(2020);
    })  
  }));

  it("Should Go to detailed view when clicking on attendees",inject([Router], (router: Router) =>
  {
    spyOn(router, "navigate");
    component.GoToDetailView(12);

    expect(router.navigate).toHaveBeenCalled();    
  }));

  it("Should update student amount", () =>
  {
    spyOn(component, "UpdateFilter");
    component.UpdateStudentNumber({CourseId: 12, Amount: 5});

    expect(component.UpdateFilter).toHaveBeenCalled();

  });

  it("Get the courses in the ngOnInit", inject([CursusClientService],(client: CursusClientService) =>
  {
    let response: CursusTableItem[] = [ {
      Id: 1,
      Title: "Test",
      StartDate: new Date(),
      Duration: 5,
      NumberOfSignedIn: 5
    }]
    spyOn(client, "getCourses").and.returnValue(of(response));
    component.ngOnInit();
    expect(client.getCourses).toHaveBeenCalled();
  }));

});
