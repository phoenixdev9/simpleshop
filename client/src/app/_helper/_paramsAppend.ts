import { ShopParams } from './../_models/ShopParams';
import { HttpParams } from '@angular/common/http';

export const _paramsAppend = (paramsData?: ShopParams) => {
  let params = new HttpParams();
  const { pageNumber, pageSize } = paramsData;
  if (pageNumber != null && pageSize != null) {
    params = params.append('PageIndex', pageNumber.toString());
    params = params.append('PageSize', pageSize.toString());
  }
  if (!paramsData) {
    return params;
  }
  Object.keys(paramsData).forEach((element) => {
    if (paramsData[element]) {
      params = params.append(element, paramsData[element]);
    }
  });
  return params;
};
