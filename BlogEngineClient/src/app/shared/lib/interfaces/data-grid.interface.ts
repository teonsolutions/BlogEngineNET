export interface IDataGridButton {
  action: string;
  icon?: string;
  color?: string;
  tooltip?: string;
  disabled?: (item: any, rowIndex: number) => boolean;
  hidden?: (item: any, rowIndex: number) => boolean;
  styleClass?: string;
  label?: string;
}

export interface IDataGridColumnDefinition {
  field: string;
  label?: string;
  template?: string;
  buttons?: IDataGridButton[];
  isDatetime?: boolean;
  sortable?: boolean;
  hidden?: boolean;
  dateTimeFormat?: string;
  alignRight?: boolean;
  headerTemplate?: string;
  headerColspan?: number;
  editable?: any;
  editableOptions?: any;
}

export interface IDataGridSource<T> {
  items: T[];
  page?: number;
  pageSize?: number;
  total?: number;
  orderBy?: string;
  orderDir?: string;
  skip?: number;
  totalPages?: number;
}

export interface IDataGridCustomHeader {
  override: boolean;
  columns: { colspan: number; label: string, align?: 'center' }[][];
}

export interface IDataGridDefinition {
  columns: IDataGridColumnDefinition[];
  editable?: boolean;
  editableOptions?: {
    disabled?: (item: any, rowIndex: number) => boolean;
    hidden?: (item: any, rowIndex: number) => boolean;
  };
  rowClass?: any;
  customHeader?: IDataGridCustomHeader;
}

export interface IDataGridEvent {
  page: number;
  pageSize: number;
  orderBy: string;
  orderDir: string;
  skip?: number;
}

export interface IDataGridPageRequest {
  page: number;
  pageSize: number;
  orderBy?: string;
  orderDir?: string;
  skip?: number;
  sortField?: string;
  sortDir?: string;
  totalPages?: number;
}

export interface IDataGridButtonEvent {
  action: string;
  item: any;
  index: number;
  events?: { openEdit: () => void };
}

export interface IDataGridEditableDefinition {
  columns: IDataGridEditableColumnDefinition[];
}

export interface IDataGridEditableColumnDefinition {
  field: string;
  label?: string;
  editable: boolean;
  controlType?:
    | 'text'
    | 'select'
    | 'multiselect'
    | 'multiline'
    | 'datepicker'
    | 'checkbox';
  buttons?: IDataGridButton[];
  onChange?: (item, setOptsFn: (fieldName, newValue) => void) => void;
  selectConfig?: any;
}

export interface IDataGridEditableSaveEvent {
  newValue: any;
  oldValue: any;
  rowIndex: number;
}
