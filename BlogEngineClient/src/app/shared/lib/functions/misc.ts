import { IDataGridPageRequest, IDataGridSource } from "../interfaces/data-grid.interface";

export const MapPageRequestFromGridSource = (
  gridSource: IDataGridSource<any>,
): IDataGridPageRequest => {
  return {
    page: gridSource.page,
    pageSize: gridSource.pageSize,
    sortField: gridSource.orderBy,
    sortDir: gridSource.orderDir,
    skip: gridSource.skip,
  };
};
