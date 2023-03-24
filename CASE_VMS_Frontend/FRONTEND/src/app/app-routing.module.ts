import { NgModule } from '@angular/core';
import { ActivatedRoute, Router, RouterModule, Routes } from '@angular/router';
import { CursusTableComponent } from './cursus/cursus-table/cursus-table.component';
import { DragDropFilesComponent } from './utils/drag-drop-files/drag-drop-files.component';

const routes: Routes = [  {
    path: 'Schema', component: CursusTableComponent
  },
  {
    path: "Upload", component: DragDropFilesComponent
  }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {


 }
