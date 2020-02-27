import { SortDirection } from '../enums/sort-direction';

export interface SearchDTOBase {
   text: string;
   sortDirection: SortDirection;
   sortColumn: string;
   pageIndex: number;
   pageSize: number;
}
