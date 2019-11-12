import { Injectable } from '@angular/core';
import { ComponentPortal } from '@angular/cdk/portal';
import { OverlayRef, Overlay } from '@angular/cdk/overlay';
import { LoadingComponent } from '../../shared/components/loading/loading.component';

@Injectable({
   providedIn: 'root',
})
export class LoadingService {
   private overlayRef: OverlayRef = null;

   constructor(private overlay: Overlay) {}

   public show() {
      // Returns an OverlayRef (which is a PortalHost)

      if (!this.overlayRef) {
         this.overlayRef = this.overlay.create({
            positionStrategy: this.overlay
               .position()
               .global()
               .centerHorizontally()
               .centerVertically(),
            hasBackdrop: true,
         });
      }

      // Create ComponentPortal that can be attached to a PortalHost
      const loadingOverlayPortal = new ComponentPortal(LoadingComponent);
      const component = this.overlayRef.attach(loadingOverlayPortal); // Attach ComponentPortal to PortalHost
   }

   public hide() {
      if (!!this.overlayRef) {
         this.overlayRef.detach();
      }
   }
}
