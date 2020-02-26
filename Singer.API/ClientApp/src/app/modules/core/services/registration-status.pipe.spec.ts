import { RegistrationStatusPipe } from '../pipes2/registration-status.pipe';

describe('RegistrationStatusPipe', () => {
   it('create an instance', () => {
      const pipe = new RegistrationStatusPipe();
      expect(pipe).toBeTruthy();
   });
});
