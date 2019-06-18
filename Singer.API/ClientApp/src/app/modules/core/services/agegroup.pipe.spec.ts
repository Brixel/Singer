import { AgegroupPipe } from './agegroup.pipe';
import { AgeGroup } from '../models/enum';

describe('AgegroupPipe', () => {
   it('create an instance', () => {
      const pipe = new AgegroupPipe();
      expect(pipe).toBeTruthy();
   });

   it('return correct Dutch values for the respective enums', () => {
      const pipe = new AgegroupPipe();
      let value = pipe.transform(AgeGroup.Toddler);
      expect(value).toBe('Kleuters');
      value = pipe.transform(AgeGroup.Child);
      expect(value).toBe('Kinderen');
      value = pipe.transform(AgeGroup.Youngster);
      expect(value).toBe('Jongeren');
      value = pipe.transform(AgeGroup.Adult);
      expect(value).toBe('Volwassenen');
   });
});
