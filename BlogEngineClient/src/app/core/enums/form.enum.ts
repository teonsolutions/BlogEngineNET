export enum FormType {
  CREATE = 1,
  EDIT = 2,
  VIEW = 3,
  DELETE = 4,
  SEARCH = 5,
  COMMENTS = 6,
  REJECT = 7,
  REJECT_COMMENTS = 8
}

export enum ActionCodeEnum {
  ACCESS = 'ACC',
  LIST = 'LST',
  ADD = 'ADD',
  UPDATE = 'UPD',
  DELETE = 'DEL',
  APROVAL = 'APR',
  REJECT = 'REJ',
  VIEW = 'VIW',
  SUBMIT = 'SUB',
  NONE = ''
}

export enum StatusPostEnum {
  PENDING = 1,
  PENDING_APROVAL = 2,
  PUBLISHED = 3,
  REJECTED = 4
}

export enum CommentType
{
    GENERAL= 1,
    REJECTED = 2,
    NONE = 3
}
