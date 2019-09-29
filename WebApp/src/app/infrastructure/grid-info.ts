export interface GridInfo<T> {
    page: number;
    pageSize: number;
    filter: T;
}

export interface GridResult<T> {
    rowCount: number;
    data: T[];
}
