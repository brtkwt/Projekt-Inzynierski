export interface Pagination<T> {
    objectList: T
    pageNumber: number
    totalPages: number
    pageSize: number
    objectCount: number
  }
  