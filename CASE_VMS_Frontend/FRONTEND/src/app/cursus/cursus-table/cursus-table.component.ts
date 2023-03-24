import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import {MatPaginator} from '@angular/material/paginator'
import { MatTableDataSource } from '@angular/material/table';
import { Router, ActivatedRoute } from '@angular/router';
import { CursusTableItem } from '../cursus-model';
import { CursusClientService } from '../httpclient/cursus-client.service';
import * as moment from 'moment';
import * as _ from "lodash"
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'BLD-cursus-table',
  templateUrl: './cursus-table.component.html',
  styleUrls: ['./cursus-table.component.css']
})
export class CursusTableComponent implements OnInit, AfterViewInit{
  
constructor(
  private cursusClient: CursusClientService,
    private _route: ActivatedRoute,
    private _router: Router) {}

displayedColumns: string[] = ['Id', 'StartDate', 'Duration', 'Title', 'NumberOfSignedIn'];

  ///Used to cache all the data
  allData!: MatTableDataSource<CursusTableItem>;
  
  ///Data that is actually displayed
  dataSource!: MatTableDataSource<CursusTableItem>;

  /// Week and year number of the data being viewed
  weekNumberBeingViewed!: number;
  yearNumberBeingViewed!: number;
  courseBeingViewed: number | undefined;
  
  @ViewChild('empTbSort') empTbSort = new MatSort();
  @ViewChild('paginator') paginator!: MatPaginator;

  manualDate = new FormGroup({
    manualWeek: new FormControl(''),
    manualYear: new FormControl('')

  })


  

    /// we call the sort and paginator here, because in the ngOnInit the component might not have the data binded, so it will be undefined.
  ngAfterViewInit(): void {
    this.empTbSort.disableClear = true;
    
    this.dataSource.sort = this.empTbSort;
    this.dataSource.paginator = this.paginator
    this.UpdateFilter();
    
  }
  
  ngOnInit(): void {
    this.dataSource = new MatTableDataSource();
    this.allData = new MatTableDataSource();

    // Read the params from the URL
    this._route.queryParams.subscribe(params =>
      {
        if('year' in params)
        {
          this.yearNumberBeingViewed = parseInt(params['year']);
        }else
        {
          this.yearNumberBeingViewed = new Date().getFullYear();
        }
        if('week' in params)
        {
          this.weekNumberBeingViewed = parseInt(params['week']);          
        }else
        {
          this.weekNumberBeingViewed = moment(new Date()).isoWeek();
        }
        if('course' in params)
        {
          this.courseBeingViewed = parseInt(params['day'])      
        }
        
      })

    /// Use the HTTPClient to gather all the data
    this.cursusClient.getCourses().subscribe(
      (response) => {
        
        if(response instanceof Array )
        response.forEach(element => {
          if('Attendees' in element && element.Attendees instanceof Array)
          {            
            element.NumberOfSignedIn = element.Attendees.length;
          }
          this.allData.data.push(element);          
        });

        /// First we filter the groups by week number, if no url params where found, we will use the current week as default
        let defaultWeekFilter = this.FilterByWeek(this.allData.data)
        let firstWeekKey = moment(new Date()).isoWeek().toString();
        if(typeof this.weekNumberBeingViewed === undefined)
        {
          this.weekNumberBeingViewed = parseInt(firstWeekKey); 
        }

        /// Secondly we filter the groups (the week group) by year, so we seperate 1/1/2019 from 1/1/2020, if no params found current year is default
        let defaultYearWeekFilter = this.FilterByYear(defaultWeekFilter[this.weekNumberBeingViewed.toString()]);
        let firstYearKey = (moment(new Date()).year()).toString();
        if(typeof this.yearNumberBeingViewed === undefined)
        {
          this.yearNumberBeingViewed = parseInt(firstYearKey);
        }
        if(defaultYearWeekFilter[firstYearKey] !== undefined)
        {
          this.dataSource.data.push(...defaultYearWeekFilter[firstYearKey.toString()]); 
          this.dataSource.data = this.dataSource.data;
        }
      }
    );
  }

  
  /// We filter the dates by week
  FilterByWeek(dates: CursusTableItem[]) : _.Dictionary<CursusTableItem[]>
  {
    let groupedResults = _.groupBy(dates, (result) => moment(result['StartDate'], 'YYYY-MM-DD').isoWeek())

    return groupedResults;
    
  }

  /// We filter the dates by year
  FilterByYear(dates: CursusTableItem[]) : _.Dictionary<CursusTableItem[]>
  {
    let groupedResults = _.groupBy(dates, (result) => moment(result['StartDate'], 'YYYY-MM-DD').year())
    
    return groupedResults;
  }

  /// Used to add or detract from weeknumber
  ChangeWeekNumber(amount: number)
  {    
    this.weekNumberBeingViewed += amount;
    
    this.UpdateYear();
    this.UpdateFilter();
    
  }

  /// Used to add or detract from year number
  ChangeYearNumber(amount: number)
  {
    this.yearNumberBeingViewed += amount; 
    this.UpdateFilter();
  }

  /// We need to update the data inside the table after filtering
  UpdateFilter()
  {
    let weekFilter = this.FilterByWeek(this.allData.data);
    
    let yearFilter = this.FilterByYear(weekFilter[this.weekNumberBeingViewed])

    this.dataSource.data = yearFilter[this.yearNumberBeingViewed];
    this.dataSource.data = this.dataSource.data;

    /// We put the filter in the URL so user can favorite it
    this._router.navigate([], 
    {
      relativeTo: this._route,
      queryParams: {
        year: this.yearNumberBeingViewed,
        week: this.weekNumberBeingViewed
      },
      queryParamsHandling: "merge",

    })
  }

  HandleManualSearchInput()
  {   
    if(this.manualDate.getRawValue().manualWeek)
    {
      this.weekNumberBeingViewed = parseInt(this.manualDate.getRawValue().manualWeek!);
    }
    if(this.manualDate.getRawValue().manualYear)
    {
      this.yearNumberBeingViewed = parseInt(this.manualDate.getRawValue().manualYear!)
    }
    this.manualDate.reset();
    this.UpdateYear();
    this.UpdateFilter();
  }

  UpdateYear()
  {

   if(this.weekNumberBeingViewed <= 0)
    {
      this.weekNumberBeingViewed = 52;
      this.ChangeYearNumber(-1);
    }
    if(this.weekNumberBeingViewed >= 53)
    {
      this.weekNumberBeingViewed = 1;
      this.ChangeYearNumber(1);
    }
  }

  GoToDetailView(courseId: number)
  {
    if(this.courseBeingViewed === undefined)
    {
      this._route.queryParams.subscribe( (params)=>
      {          
          this._router.navigate([], 
          {
            relativeTo: this._route,
            queryParams: 
            {
              course: courseId,
            },
            queryParamsHandling: "merge",   
          });       
      });       
    }else
    {
      this.courseBeingViewed = undefined;
      this._route.queryParams.subscribe((params) =>
      {
        this._router.navigate([],
          {
            relativeTo: this._route,
            queryParams:
            {
              course: null
            },
            queryParamsHandling: "merge"
          })
      })
    }
  }

  UpdateStudentNumber(update: {CourseId: number, Amount: number;})
  {
    
    let index = this.allData.data.findIndex(c => c.Id == update.CourseId);
    this.allData.data[index] = {...this.allData.data[index], NumberOfSignedIn: update.Amount}
    
    this.UpdateFilter();
  }
}


