import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'BLD-routing-buttons',
  templateUrl: './routing-buttons.component.html',
  styleUrls: ['./routing-buttons.component.css']
})
export class RoutingButtonsComponent {

  constructor(
    private _router : Router,
    private _route: ActivatedRoute
    ){}

  NavigateToSchema()
  {
    this._router.navigate(["Schema"], {relativeTo: this._route })
  }

  NavigateToUpload()
  {
    this._router.navigate(["Upload"], {relativeTo: this._route})
  }
}
