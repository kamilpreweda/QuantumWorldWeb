import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnemyPopupComponent } from './enemy-popup.component';

describe('EnemyPopupComponent', () => {
  let component: EnemyPopupComponent;
  let fixture: ComponentFixture<EnemyPopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnemyPopupComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EnemyPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
