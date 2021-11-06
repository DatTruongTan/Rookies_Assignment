import axios, { AxiosResponse } from 'axios';
import qs from 'qs';

import RequestService from '../../Services/request';
import EndPoints from '../../Constants/endpoints';

export function createProductRequest(Form) {
    return RequestService.axios.post(EndPoints.products, Form);
}

export function getProductsRequest(query) {
    return RequestService.axios.get(EndPoints.products, {
        params: query,
        paramsSerializer: (params) => qs.stringify(params),
    });
}

export function UpdateProductRequest(Form) {
    const formData = new FormData();

    Object.keys(Form).forEach((key) => {
        formData.append(key, Form[key]);
    });

    return RequestService.axios.put(
        EndPoints.productsId(Form.id ?? -1),
        formData
    );
}

export function DeleteProductRequest(id) {
    return RequestService.axios.delete(EndPoints.productsId(id));
}
