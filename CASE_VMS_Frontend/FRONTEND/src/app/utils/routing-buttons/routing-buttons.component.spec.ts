import { ComponentFixture, inject, TestBed } from '@angular/core/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';


import { RoutingButtonsComponent } from './routing-buttons.component';

describe('RoutingButtonsComponent', () => {
  let component: RoutingButtonsComponent;
  let fixture: ComponentFixture<RoutingButtonsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RoutingButtonsComponent ],
      imports: [RouterTestingModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RoutingButtonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it("Should click the Navigate to Schema button", () =>
  {
    spyOn(component, 'NavigateToSchema');
    
    let buttons = fixture.debugElement.nativeElement.querySelectorAll('button');
    buttons[0].click();

    expect(component.NavigateToSchema).toHaveBeenCalled();
  });

  it("Should click the Navigate to Upload button", () =>
  {
    spyOn(component, 'NavigateToUpload');
    
    let buttons = fixture.debugElement.nativeElement.querySelectorAll('button');
    buttons[1].click();

    expect(component.NavigateToUpload).toHaveBeenCalled();
  });

  it("Should attempt to navigate to schema", inject([Router], (router: Router) =>
  {
    spyOn(router, "navigate").and.stub();

    component.NavigateToSchema()

    expect(router.navigate).toHaveBeenCalled();
  }))

    it("Should attempt to navigate to upload", inject([Router], (router: Router) =>
  {
    spyOn(router, "navigate").and.stub();

    component.NavigateToUpload()

    expect(router.navigate).toHaveBeenCalled();
  }))

});
