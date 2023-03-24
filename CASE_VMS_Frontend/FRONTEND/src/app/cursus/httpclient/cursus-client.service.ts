import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CursusTableItem } from '../cursus-model';

@Injectable({
  providedIn: 'root'
})
export class CursusClientService {

url = 'https://localhost:7292/api/courses'

  constructor(private http: HttpClient) { }

  getCourses()
  {
    return this.http.get<CursusTableItem[]>(this.url);
  }

  getCourseDetails(Id: number)
  {
    return this.http.get(this.url + `/${Id}`);
  }

  postCourses(FileToUpload : File)
  {
    const formData = new FormData();
    formData.append('file', FileToUpload );
    const headers = new HttpHeaders().append('Content-Disposition', 'multipart/form-data')
    
    return this.http.post<CursusTableItem[]>(this.url, formData, {headers});
  }

  postCourseJson(course: {})
  {    
    return this.http.post<CursusTableItem[]>(this.url + "/json", course)
  }
}
