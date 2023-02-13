import { Observable } from 'rxjs';

export interface IComboList {
  list: any[];
  loading: boolean;
}

export interface IMsgValidation {
  name: string;
  message: string;
}

export interface IMsgValidations {
  [prop: string]: IMsgValidation[];
}

export interface IFormModelField {
  value: any;
  loading: boolean;
  disabled: boolean;
  valid: boolean;
  errors: any[];
  rules: any[];
  fieldName: string;
  fieldLabel: string;
  parentPrefix: string;
  setValue: (value: any) => void;
  setValidator: (validator: any[]) => void;
  setErrors: (errors: any[]) => void;
  clearErrors: () => void;
  setFieldLabel: (label) => void;
  addDisableWhen: (disableWhen: () => boolean) => void;
  isDisabled: boolean;
  validate: () => void
}

export interface IFormActions {
  onSave?: (formValue) => Observable<any>;
  onUpdate?: (formValue) => Observable<any>;
  onGet?: () => Observable<any>;
}
