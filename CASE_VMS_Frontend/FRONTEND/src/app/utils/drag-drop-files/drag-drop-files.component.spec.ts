import { formatDate } from '@angular/common';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, fakeAsync, flush, inject, TestBed, tick } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';
import { of } from 'rxjs';
import { CursusTableItem } from 'src/app/cursus/cursus-model';
import { CursusClientService } from 'src/app/cursus/httpclient/cursus-client.service';

import { DragDropFilesComponent } from './drag-drop-files.component';

describe('DragDropFilesComponent', () => {
  let component: DragDropFilesComponent;
  let fixture: ComponentFixture<DragDropFilesComponent>;
  let clientspy
  beforeEach(async () => {
      await TestBed.configureTestingModule({
        declarations: [ DragDropFilesComponent ],
        imports: [
          HttpClientTestingModule,
          ReactiveFormsModule],
          teardown: { destroyAfterEach: false }
        }).compileComponents();
        fixture = TestBed.createComponent(DragDropFilesComponent);
      
      component = fixture.componentInstance;
      fixture.detectChanges();
  });

  it("should detect file input change", () =>
  {
    const dataTransfer = new DataTransfer();
    dataTransfer.items.add(new File([], "foo.txt"));

    const inputDebugElem = fixture.debugElement.query(By.css('form'));
    inputDebugElem.nativeElement.files = dataTransfer.files;

    spyOn(component, 'getFile');
    inputDebugElem.nativeElement.dispatchEvent(new InputEvent('submit'));

    fixture.detectChanges();

    expect(component.getFile).toHaveBeenCalled();
  });


  it("Should return true of .txt", () =>
  {
    let blob = new Blob(["Titel: Nieuwe Cursus 01\nCursuscode: NIEUW01\nDuur: 1 dagen\nStartdatum: 09/07/2020"], {type: 'text/plain'});

    let fakeFile = <File>blob;
    let actual = component.checkFileType(fakeFile)
    expect(actual).toBeTruthy();
  });

  it("Should return false if not .txt", () =>
  {
    let blob = new Blob(["Titel: Nieuwe Cursus 01\nCursuscode: NIEUW01\nDuur: 1 dagen\nStartdatum: 09/07/2020"], {type: 'text/html'});

    let fakeFile = <File>blob;
    let actual = component.checkFileType(fakeFile)
    expect(actual).toBeFalsy();
  });

  

  it("Should not upload empty file", fakeAsync(() =>
  {
    let blob = new Blob([""], {type: 'text/plain'});
    spyOn(component, "uploadJson");
    spyOn(component, "uploadFile");
    let fakeFile = <File>blob;
    component.prepareFileForUpload(fakeFile);

    fixture.whenStable().then(() =>
    {
      fixture.detectChanges();
      
      expect(component.uploadFile).not.toHaveBeenCalled();
      expect(component.uploadJson).not.toHaveBeenCalled();
    })
  }));

  it("Should not upload file when missing courseCode", fakeAsync(() =>
  {
    let blob = new Blob(["Titel: Nieuwe Cursus 01\n"], {type: 'text/plain'});
    spyOn(component, "uploadFile");
    let fakeFile = <File>blob;
    component.prepareFileForUpload(fakeFile);

    fixture.whenStable().then(() =>
    {
      fixture.detectChanges();
      
      expect(component.uploadFile).not.toHaveBeenCalled();
    });
  }));

  it("Should not upload file when missing StartDatum", fakeAsync(() =>
  {
    let blob = new Blob(["Titel: Nieuwe Cursus 01\nCursuscode: TEST01\n"], {type: 'text/plain'});
    spyOn(component, "uploadFile");
    let fakeFile = <File>blob;
    component.prepareFileForUpload(fakeFile);

    fixture.whenStable().then(() =>
    {
      fixture.detectChanges();
      
      expect(component.uploadFile).not.toHaveBeenCalled();
    });
  }));

  it("Should not upload file when missing Duur", fakeAsync(() =>
  {
    let blob = new Blob(["Titel: Nieuwe Cursus 01\nCursuscode: TEST01\nStartdatum: 08/09/2023\n"], {type: 'text/plain'});
    spyOn(component, "uploadFile");
    let fakeFile = <File>blob;
    component.prepareFileForUpload(fakeFile);

    fixture.whenStable().then(() =>
    {
      fixture.detectChanges();
      
      expect(component.uploadFile).not.toHaveBeenCalled();
    });
  }));

  it("Should not get errors", fakeAsync(() =>
  {
    let blob = new Blob(["Titel: Nieuwe Cursus 01\nCursuscode: TEST01\nDuur: 5 dagen\nStartdatum: 08/09/2023\n"], {type: 'text/plain'});
    let fakeFile = new File([blob], "test.txt");

    component.prepareFileForUpload(fakeFile);
    fixture.detectChanges()
    tick();
    fixture.whenStable().then(() =>
    {      
      expect(component.errors.length).toBe(0);

    });
  }));

  it("Should spot error on enddate before startdate", fakeAsync(() =>
  {
    let startDate = new Date("2025-10-20");
    let endDate = new Date("2024-9-19");

    fixture.whenStable().then(() =>
    {
      let dateInputs = fixture.debugElement.nativeElement.querySelectorAll("input[type=date]")
      let startDateInput = dateInputs[0];
      let endDateInput = dateInputs[1];
      
      startDateInput.value = formatDate(startDate.toISOString(), "yyyy-MM-dd", "en-US");
      endDateInput.value = formatDate(endDate.toISOString(), "yyyy-MM-dd", "en-US");
      startDateInput.dispatchEvent(new Event('input'));
      endDateInput.dispatchEvent(new Event('input'))
      tick();
  
      fixture.detectChanges();
      component.checkDate(["test"])
      
      expect(component.errors.length).toBe(1)

    })        
  }));

  it("Should return good courseInstance with correct dates", fakeAsync(() =>
  {
    let startDate = new Date("2025-10-20");
    let endDate = new Date("2025-10-23");

    fixture.whenStable().then(() =>
    {
      let dateInputs = fixture.debugElement.nativeElement.querySelectorAll("input[type=date]")
      let startDateInput = dateInputs[0];
      let endDateInput = dateInputs[1];
      let expected = 
        {
          StartDate: startDate,
          Duration: 3,
          Title: "test",
          NumberOfSignedIn: 0,
        }
      
      startDateInput.value = formatDate(startDate.toISOString(), "yyyy-MM-dd", "en-US");
      endDateInput.value = formatDate(endDate.toISOString(), "yyyy-MM-dd", "en-US");
      startDateInput.dispatchEvent(new Event('input'));
      endDateInput.dispatchEvent(new Event('input'))
      tick();
  
      fixture.detectChanges();
      let actual = component.checkDate(["test"])
      
      expect(component.errors.length).toBe(0);
      expect(actual).toEqual(expected)
    
    })
  }));

  it("Should attempt uploading the file", fakeAsync( inject([CursusClientService], (client: CursusClientService ) =>
  {
    const response: CursusTableItem[] = [{Id: 1, Title: "test", StartDate: new Date(), Duration: 1, NumberOfSignedIn: 0}];
    spyOn(client, "postCourses").and.returnValue(of(response));
    let blob = new Blob(["Titel: Nieuwe Cursus 01\nCursuscode: NIEUW01\nDuur: 1 dagen\nStartdatum: 09/07/2020"], {type: 'text/plain'});
    let fakeFile = <File>blob;
    component.uploadFile(fakeFile);

    expect(client.postCourses).toHaveBeenCalled();
  })));


  it("Should attempt uploading the json", fakeAsync( inject([CursusClientService], (client: CursusClientService ) =>
  {
    const response: CursusTableItem[] = [{Id: 1, Title: "test", StartDate: new Date(), Duration: 1, NumberOfSignedIn: 0}];
    spyOn(client, "postCourseJson").and.returnValue(of(response));
    let fakeJson = [{Title: "test", StartDate: new Date(), Duration: 1, NumberOfSignedIn: 0}];
    component.uploadJson(fakeJson);

    expect(client.postCourseJson).toHaveBeenCalled();
  })));
});

