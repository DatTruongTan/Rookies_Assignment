const Endpoints = {
    authorize: 'api/authorize',
    me: 'api/authorize/me',

    products: '/api/products',
    productsId: (id) => `api/products/${id}`,
    users: '/api/user',
    usersId: (id) => `/api/user/${id}`,
};

export default Endpoints;
