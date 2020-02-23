import { RegistrationStatusPipe } from '../Pipes/registration-status.pipe';

describe('RegistrationStatusPipe', () => {
   it('create an instance', () => {
      const pipe = new RegistrationStatusPipe();
      expect(pipe).toBeTruthy();
   });
});
