import {
  AbstractControl,
  ValidationErrors,
  ValidatorFn,
} from '@angular/forms';

export const confirmPasswordValidator: ValidatorFn = (
  control: AbstractControl
): ValidationErrors | null => {
  let password = control.get('password');
  let confpassword = control.get('confPass');
  if(password && confpassword && password.value !== confpassword.value){
    return{
      passwordMatchError : true
    }
  }
  return  null;
};
