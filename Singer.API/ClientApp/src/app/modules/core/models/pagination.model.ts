export interface PaginationDTO {
   previousPageUrl: string;
   nextPageUrl: string;
   currentPageUrl: string;
   size: number;
   totalSize: number;
   items: any[];
}
