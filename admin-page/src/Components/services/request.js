import axios, { AxiosResponse } from 'axios';
import qs from 'qs';

import RequestService from '../../Services/request';
import EndPoints from '../../Constants/endpoints';

export function createBrandRequest(brandForm) {
    const formData = new FormData();

    Object.keys(brandForm).forEach((key) => {
        formData.append(key, brandForm[key]);
    });

    return RequestService.axios.post(EndPoints.brand, formData);
}

export function getBrandsRequest(query) {
    return axios.get(EndPoints.brand, {
        params: query,
        paramsSerializer: (params) => qs.stringify(params),
    });
}

export function UpdateBrandRequest(brandForm) {
    const formData = new FormData();

    Object.keys(brandForm).forEach((key) => {
        formData.append(key, brandForm[key]);
    });

    return RequestService.axios.put(
        EndPoints.brandId(brandForm.id ?? -1),
        formData
    );
}

export function DisableBrandRequest(brandId) {
    return RequestService.axios.delete(EndPoints.brandId(brandId));
}
