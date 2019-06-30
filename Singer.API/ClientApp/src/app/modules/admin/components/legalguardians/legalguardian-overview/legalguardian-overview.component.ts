import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { LegalguardianOverviewDataSource } from './legalguardian-overview-datasource';

@Component({
  selector: 'app-legalguardian-overview',
  templateUrl: './legalguardian-overview.component.html',
  styleUrls: ['./legalguardian-overview.component.css']
})
export class LegalguardianOverviewComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSource: LegalguardianOverviewDataSource;

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['id', 'name'];

  ngAfterViewInit() {
    this.dataSource = new LegalguardianOverviewDataSource(this.paginator, this.sort);
  }
}
