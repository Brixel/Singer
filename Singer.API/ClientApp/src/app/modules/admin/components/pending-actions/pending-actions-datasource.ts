import { GenericDataSource } from 'src/app/modules/core/services/generic-data-source';
import { EventRegistrationLogCareUser } from 'src/app/modules/core/models/event-registration-log.model';
import { ActionNotificationsService } from 'src/app/modules/core/services/action-notification.service';
import { EventRegistrationLogCareUserDTO } from 'src/app/modules/core/DTOs/event-registration-log.dto';
import { BehaviorSubject } from 'rxjs';

export class PendingActionsDataSource{
   protected modelsSubject$ = new BehaviorSubject<EventRegistrationLogCareUser[]>([]);
   protected totalSizeSubject$ = new BehaviorSubject<number>(0);
   protected queryCountSubject$ = new BehaviorSubject<number>(0);
   protected loadingSubject$ = new BehaviorSubject<boolean>(false);

   public models$ = this.modelsSubject$.asObservable();
   public totalSize$ = this.totalSizeSubject$.asObservable();
   public queryCount$ = this.queryCountSubject$.asObservable();
   public loading$ = this.loadingSubject$.asObservable();

   constructor(private pendingActionsDataSource: ActionNotificationsService) {   }
   load() {
      this.loadingSubject$.next(true);
      this.pendingActionsDataSource
         .fetch()
         .subscribe(res => {
            const models = res.map(x => this.pendingActionsDataSource.toModel(x));
            console.log(models);
            this.modelsSubject$.next(models);
            this.totalSizeSubject$.next(res.length);
            this.queryCountSubject$.next(res.length);
            this.loadingSubject$.next(false);
         });
   }
}
