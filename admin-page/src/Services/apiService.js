import axios from 'axios';
import EndPoints from '../Constants/endpoints';

export function callApi(endpoint, method, body) {
    return axios({
        url: process.env.REACT_APP_BACKEND_URL + endpoint,
        method,
        data: body,
        headers: {
            // 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8',
            'Content-Type': 'multipart/form-data',
        },
    }).catch((e) => {
        console.log(e);
        console.log('apiService here-', body);
    });
}

export function GET_ALL_PRODUCTS() {
    return callApi(EndPoints.products, 'GET');
}

export function GET_PRODUCTS_BY_ID(id) {
    return callApi(EndPoints.products + '/' + id, 'GET');
}

export function POST_ADD_PRODUCT(data) {
    return callApi(EndPoints.products, 'POST', data);
}

export function PUT_EDIT_PRODUCT(data) {
    return callApi(EndPoints.products, 'PUT', data);
}

export function DELETE_PRODUCT_ID(id) {
    return callApi(EndPoints.products + '/' + id, 'DELETE');
}
