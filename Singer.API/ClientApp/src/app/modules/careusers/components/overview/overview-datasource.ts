import { DataSource } from '@angular/cdk/collections';
import { MatPaginator, MatSort } from '@angular/material';
import { map, takeUntil } from 'rxjs/operators';
import { Observable, of as observableOf, merge } from 'rxjs';

// Care User class
export interface OverviewItem {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  userName: string;
  birthday: Date;
  caseNumber: string;
  ageGroup: string; //Maybe vervangen door eigen klasse?
  isExtern: boolean;
  hasTrajectory: boolean;
  hasNormalDayCare: boolean;
  hasVacationDayCare: boolean;
  hasResources: boolean;
}

// TODO: replace this with real data from your application
const EXAMPLE_DATA: OverviewItem[] = [
  {
    id: '1',
    firstName: 'Joske', 
    lastName:'Vermeulen',
    email: 'joske.vermeulen@gmail.com', 
    userName: 'joske',  
    birthday: new Date('2008-07-06'), 
    caseNumber: '0123456789', 
    ageGroup: 'Child', 
    isExtern: false, 
    hasTrajectory: true, 
    hasNormalDayCare: true,
    hasVacationDayCare: true,
    hasResources: true
  },
  {
    id: '2',
    firstName: 'Kim', 
    lastName:'Janssens',
    email: 'kim.janssens@gmail.com', 
    userName: 'kim', 
    birthday: new Date('2006-07-08'), 
    caseNumber: '9876543210', 
    ageGroup: 'Child', 
    isExtern: true, 
    hasTrajectory: true, 
    hasNormalDayCare: true,
    hasVacationDayCare: true,
    hasResources: true
  },
  {
    id: '3',
    firstName: 'Benjamin', 
    lastName:'Vermeulen',
    email: 'benjamin.vermeulen@gmail.com', 
    userName: 'benjamin', 
    birthday: new Date('2010-08-06'), 
    caseNumber: '091837465', 
    ageGroup: 'Youngster', 
    isExtern: false, 
    hasTrajectory: true, 
    hasNormalDayCare: true,
    hasVacationDayCare: true,
    hasResources: false
  },
];
         
/**
 * Data source for the Overview view. This class should
 * encapsulate all logic for fetching and manipulating the displayed data
 * (including sorting, pagination, and filtering).
 */
export class OverviewDataSource extends DataSource<OverviewItem> {
  data: OverviewItem[] = EXAMPLE_DATA;

  constructor(private paginator: MatPaginator, private sort: MatSort) {
    super();
  }

  /**
   * Connect this data source to the table. The table will only update when
   * the returned stream emits new items.
   * @returns A stream of the items to be rendered.
   */
  connect(): Observable<OverviewItem[]> {
    // Combine everything that affects the rendered data into one update
    // stream for the data-table to consume.
    const dataMutations = [
      observableOf(this.data),
      this.paginator.page,
      this.sort.sortChange
    ];

    // Set the paginator's length
    this.paginator.length = this.data.length;

    return merge(...dataMutations).pipe(map(() => {
      return this.getPagedData(this.getSortedData([...this.data]));
    }));
  }

  /**
   *  Called when the table is being destroyed. Use this function, to clean up
   * any open connections or free any held resources that were set up during connect.
   */
  disconnect() {}

  /**
   * Paginate the data (client-side). If you're using server-side pagination,
   * this would be replaced by requesting the appropriate data from the server.
   */
  private getPagedData(data: OverviewItem[]) {
    const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
    return data.splice(startIndex, this.paginator.pageSize);
  }

  /**
   * Sort the data (client-side). If you're using server-side sorting,
   * this would be replaced by requesting the appropriate data from the server.
   */
  private getSortedData(data: OverviewItem[]) {
    if (!this.sort.active || this.sort.direction === '') {
      return data;
    }

    return data.sort((a, b) => {
      const isAsc = this.sort.direction === 'asc';
      switch (this.sort.active) {
        case 'name': return compare(a.name, b.name, isAsc);
        case 'id': return compare(+a.id, +b.id, isAsc);
        default: return 0;
      }
    });
  }
}

/** Simple sort comparator for example ID/Name columns (for client-side sorting). */
function compare(a, b, isAsc) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}
