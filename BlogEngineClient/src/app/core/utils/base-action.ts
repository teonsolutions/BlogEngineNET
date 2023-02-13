export class BaseAction<T> {

  constructor(
    protected getState: () => T,
    protected setState: (newState: T) => void,
  ) {
  }

}
