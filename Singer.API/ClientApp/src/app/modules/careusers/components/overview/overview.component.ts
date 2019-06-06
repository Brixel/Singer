import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { OverviewDataSource } from './overview-datasource';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSource: OverviewDataSource;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = 
  [
    //'id', 
    'lastName',
    'firstName', 
    //'email', 
    //'userName', 
    'birthday', 
    'caseNumber', 
    'ageGroup', 
    'isExtern', 
    'hasTrajectory', 
    'hasNormalDayCare', 
    'hasVacationDayCare', 
    'hasResources'
  ];

  ngAfterViewInit() {
    this.dataSource = new OverviewDataSource(this.paginator, this.sort);
  }
}
