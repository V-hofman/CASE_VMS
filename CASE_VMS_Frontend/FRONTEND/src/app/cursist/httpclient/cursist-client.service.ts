import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Cursist, CursistCourseDTO } from '../cursist-model';

@Injectable({
  providedIn: 'root'
})
export class CursistClientService {

  constructor(private http: HttpClient) { }

  url = "https://localhost:7292/api/students";

  getStudents(id: number)
  {   
    return this.http.get<Cursist[]>(this.url + `/${id}`, {observe: 'response'});
  }

  postStudent(student: CursistCourseDTO)
  {
    return this.http.post<CursistCourseDTO>(this.url, {CourseId: student.CourseId, ...student.Cursist}, {observe: 'response'});
  }
}
