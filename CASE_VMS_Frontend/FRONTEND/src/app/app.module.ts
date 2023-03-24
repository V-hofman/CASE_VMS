import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { MatTableModule } from '@angular/material/table'
import { MatSortModule } from '@angular/material/sort'
import { MatPaginatorModule } from '@angular/material/paginator'
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CursusTableComponent } from './cursus/cursus-table/cursus-table.component';
import { DragDropFilesComponent } from './utils/drag-drop-files/drag-drop-files.component';
import { CursusDetailViewComponent } from './cursus/cursus-detail-view/cursus-detail-view.component';
import { NewCursistFormComponent } from './cursist/new-cursist-form/new-cursist-form.component';
import { RoutingButtonsComponent } from './utils/routing-buttons/routing-buttons.component';



@NgModule({
  declarations: [
    AppComponent,
    CursusTableComponent,
    DragDropFilesComponent,
    CursusDetailViewComponent,
    NewCursistFormComponent,
    RoutingButtonsComponent

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  providers: [
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
