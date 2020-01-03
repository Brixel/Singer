export interface PaginationDTO<TDTO = any> {
   previousPageUrl: string;
   nextPageUrl: string;
   currentPageUrl: string;
   size: number;
   totalSize: number;
   items: TDTO[];
}
