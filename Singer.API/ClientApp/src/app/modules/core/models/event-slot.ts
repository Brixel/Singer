export interface EventSlot {
   id: string;
   startDateTime: Date;
   endDateTime: Date;
}
export interface EventSlotDTO {
   id: string;
   startDateTime: Date;
   endDateTime: Date;
   currentRegistrants: number;
}
