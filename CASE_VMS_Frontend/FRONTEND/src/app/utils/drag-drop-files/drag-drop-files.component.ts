import { formatDate } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { CursusTableItem } from 'src/app/cursus/cursus-model';
import { CursusClientService } from 'src/app/cursus/httpclient/cursus-client.service';

@Component({
  selector: 'BLD-drag-drop-files',
  templateUrl: './drag-drop-files.component.html',
  styleUrls: ['./drag-drop-files.component.css']
})
export class DragDropFilesComponent {

  errors: string[] = [];
  feedback: string[] = [];

  minDate: Date = new Date();
  files!: FileList| null;


  manualDate = new FormGroup({
    manualStartDate: new FormControl(""),
    manualEndDate: new FormControl("")

  })
  constructor(private httpClient: CursusClientService)
  {

  }


  checkDate(text: string[])
  {    
    let tempStartDate = new Date(this.manualDate.getRawValue().manualStartDate!);
    let tempEndDate = new Date(this.manualDate.getRawValue().manualEndDate!);

    if(Object.prototype.toString.call(tempStartDate) === "[object Date]")
    {
      if(isNaN(tempStartDate.getTime()))
      {
        this.errors.push("Start date is not a valid date!")
      }
    }

    if(Object.prototype.toString.call(tempEndDate) === "[object Date]")
    {
      if(isNaN(tempEndDate.getTime()))
      {
        this.errors.push("End date is not a valid date!")
      }
    }
    
    
    if(tempEndDate!.getTime() < tempStartDate!.getTime())
    {
      this.errors.push("Ending before it starts? you sure?")
    }
    let differenceInDays = Math.floor((Date.UTC(tempEndDate!.getFullYear(), tempEndDate!.getMonth(), tempEndDate!.getDate()) - Date.UTC(tempStartDate!.getFullYear(), tempStartDate!.getMonth(), tempStartDate!.getDate()) ) /(1000 * 60 * 60 * 24))
    let tempCourse  = {StartDate: tempStartDate, Duration: differenceInDays, Title: text[0].trim(), NumberOfSignedIn: 0}
    return tempCourse;
  }

  updateFile(event: Event)
  {
    this.files = (event.target as HTMLInputElement).files;
  }

  getFile(event: Event)
  {
    
    this.errors = [];
    this.feedback = [];
    
    if(this.files)
    {
      for (let index = 0; index < this.files.length; index++)
      {
        if(this.files.item(index))
        {
          let file = this.files.item(index)
          if(!this.checkFileType(file!))
          {
            this.errors.push(`Bestand ${file?.name} is het verkeerde soort!\nAlleen .txt is toegestaan!`)
          }else
          {
            this.prepareFileForUpload(file!)
          }
        }
      }

      (event.target as HTMLInputElement).files = null;
      (event.target as HTMLInputElement).value = "";
    }else
    {
      this.errors.push("Selecteer AUB een bestand...")
    }
    

  }

  checkFileType(file: File): boolean
  {
    return file.type === "text/plain";
  }

  uploadFile(file: File)
  {

    this.httpClient.postCourses(file).subscribe()
    this.feedback.push(`${file.name} has been send succesfully`)
    
  }

  uploadJson(courses: {}[])
  {
    this.feedback.push(`${File.name} has been uploaded!`)
    this.httpClient.postCourseJson(courses).subscribe();
    
  }

  prepareFileForUpload(file: File)
  {
 
    let fileReader = new FileReader();

    let courseObjectList: {
          StartDate: Date;
          Duration: Number;
          Title: string;
          NumberOfSignedIn: Number;
          CourseCode: string;
         }[] = [];

    let isJson :boolean = false;
    fileReader.onload = (e) =>
    {
      
      let lines = fileReader.result?.toString().split("\n");
      if(typeof lines !== 'undefined' && file.size !== 0)
      {

        for (let index = 0; index < lines.length; index++) 
        {
          const element = lines[index];
          if(lines[index].includes("Titel:"))
          {
            if(typeof lines[index + 1] !== undefined && lines[index + 1].includes("Cursuscode:"))
            {
              if(typeof lines[index + 2] !== undefined && lines[index + 2].match(/^Duur: \d+ dagen/g))
              {
                if(typeof lines[index + 3] !== undefined && lines[index + 3].match(/^Startdatum: [0-9]{1,2}\/[0-9]{1,2}\/[0-9]{4}/g))
                {
                  if(typeof lines[index + 4] === undefined && lines[index + 4].includes("Titel:") )
                  {
                    this.errors.push(`File ${file.name} is missing a whitespace line on line: ${index + 5}!`)
                  }
                }else
                {
                    this.errors.push(`File ${file.name} is missing  "Startdatum: dd/MM/yyyy" on line: ${index + 4}!`)                                              
                }
              }else
              {
                try{
                    let currentCourse = this.checkDate([lines[index], lines[index + 1]])
                    courseObjectList.push({...currentCourse, CourseCode: lines[index + 1]});                   
                    isJson = true;
                    index+= 2;
                    continue;            
                  }
                  catch
                  {
                    this.errors.push(`File ${file.name} is missing "Duur: x dagen" on line ${index + 3}!`)                
                  }
              }
            }else
            {
              this.errors.push(`File ${file.name} is missing "Cursuscode: " on line ${index + 2}!`)              
            }
          }else
          {            
            this.errors.push(`File ${file.name} is missing "Titel: " on line ${index + 1}!`)            
          }
          index += 4;
        }
      }else
      {               
        this.errors.push(`File ${file} is empty and thus ignored!`)
      }
      
      if(this.errors.length === 0)
      {
        if(isJson)
        {
          this.uploadJson(courseObjectList)
        }else
        {                              
          this.uploadFile(file);
        }
      }
      
    }
    fileReader.readAsText(file)
    
  }
}
