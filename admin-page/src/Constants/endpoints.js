const Endpoints = {
    authorize: 'api/authorize',
    me: 'api/authorize/me',

    products: '/api/products',
    productsId: (id) => `api/products/${id}`,
};

export default Endpoints;
