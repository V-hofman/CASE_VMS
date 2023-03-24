import { getTestBed, TestBed } from '@angular/core/testing';
import {HttpClientTestingModule, HttpTestingController} from '@angular/common/http/testing'

import { CursusClientService } from './cursus-client.service';

describe('CursusClientService', () => {
  let service: CursusClientService;
  let injector: TestBed;
  let httpMock: HttpTestingController;
  
  const dummyResponse = [
  {StartDate: new Date(2023,3,19), Duration: 5, Title: "TestingCase", NumberOfSignedIn: 3, Id: 4},
  {StartDate: new Date(2023,3,19), Duration: 3, Title: "TestingCase2", NumberOfSignedIn: 5, Id: 3}
  ];

  const dummyFile = new File([], "foo.txt",
  {
    type: "text/plain",
  });

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CursusClientService]
    });
    injector = getTestBed();
    service = injector.inject(CursusClientService);
    httpMock = injector.inject(HttpTestingController)
  });

  // Make sure that no outstanding requests are open
  afterEach(() =>
  {
    httpMock.verify();
  })

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should return an Observable', () =>
  {
    
    service.getCourses().subscribe(courses => {
      expect(courses.length).toBe(2);
      expect(courses).toEqual(dummyResponse);
    });

    const request = httpMock.expectOne(`${service.url}`);
    expect(request.request.method).toBe("GET");
    request.flush(dummyResponse);

  });

  it('should attempt to POST with correct file type', () =>
  {
    service.postCourses(dummyFile).subscribe(courses =>
      {
        expect(courses.length).toBe(2);
        expect(courses).toEqual(dummyResponse);
      });

    const request = httpMock.expectOne(`${service.url}`);
    expect(request.request.method).toBe("POST");
    request.flush(dummyResponse);
  });

  it("should attempt to POST with Json", () =>
  {
    service.postCourseJson(dummyFile).subscribe(courses =>
      {
        expect(courses.length).toBe(2);
        expect(courses).toEqual(dummyResponse);
      });

    const request = httpMock.expectOne(`${service.url+ "/json"}`);
    expect(request.request.method).toBe("POST");
    request.flush(dummyResponse);
  })

  it("Should get the course Details", () => 
  {
    service.getCourseDetails(1).subscribe();

    const request = httpMock.expectOne(`${service.url+ "/1"}`);
    expect(request.request.method).toBe("GET");
    request.flush(dummyResponse);
  })

});
