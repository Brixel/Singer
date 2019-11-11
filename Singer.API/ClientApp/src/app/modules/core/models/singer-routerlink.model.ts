export interface SingerRouterLink {
   RouterLinkName: string;
   RouterLinkRequirements: singerRouterLinkRequirements[];
   routerLink: string;
}

export enum singerRouterLinkRequirements {
   none = 'none',
   isAuthenticated= 'isAuthenticated',
   isAdmin = 'isAdmin',
   isNotAuthenticated = 'isNotAuthenticated',
}
