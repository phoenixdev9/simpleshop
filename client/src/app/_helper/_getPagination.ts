/**
 * Format the data when the data pass from server to the client
 */
export const _getPagination = (response: any, paginatedResult: any) => {
  paginatedResult.result = response.body; //pass get developers data to result: IDeveloper[]
  if (response.headers.get('Pagination') != null) {
    paginatedResult.pagination = JSON.parse(response.headers.get('Pagination')); //Pass the Pagination Header information to the object
  }
  return paginatedResult;
};
