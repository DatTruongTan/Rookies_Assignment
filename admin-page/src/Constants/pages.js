export const LOGIN = '/login';
export const AUTH = '/authentication/:action';
export const HOME = '/';
export const USER = '/user';

export const CREATE_PRODUCT = '/create';
export const EDIT_PRODUCT = '/edit/:id';
export const EDIT_PRODUCT_ID = (id) => `/edit/${id}`;
export const DELETE_PRODUCT_ID = (id) => `/delete/${id}`;

export const UNAUTHORIZE = '/unauthorize';
export const NOTFOUND = '/notfound';
