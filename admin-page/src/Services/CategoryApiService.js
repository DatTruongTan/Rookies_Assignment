import axios from 'axios';
import EndPoints from '../Constants/endpoints';

export function callApi(endpoint, method, body) {
    return axios({
        url: process.env.REACT_APP_BACKEND_URL + endpoint,
        method,
        data: body,
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    }).catch((e) => {
        console.log(e);
        console.log('apiService here-', body);
    });
}

export function GET_ALL_CATEGORY() {
    return callApi(EndPoints.category, 'GET');
}

export function GET_CATEGORY_BY_ID(id) {
    return callApi(EndPoints.category + '/' + id, 'GET');
}

export function POST_ADD_CATEGORY(data) {
    return callApi(EndPoints.category, 'POST', data);
}

export function PUT_EDIT_CATEGORY(id, data) {
    return callApi(EndPoints.category + '/' + id, 'PUT', data);
}

export function DELETE_CATEGORY_ID(id) {
    return callApi(EndPoints.category + '/' + id, 'DELETE');
}
