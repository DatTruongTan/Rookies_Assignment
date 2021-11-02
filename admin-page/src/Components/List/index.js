import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Table, Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import Endpoints from '../../Constants/endpoints';
import { getBrandsRequest } from '../services/request';
import { GET_ALL_PRODUCTS } from '../../Services/apiService';
import {
    ACCSENDING,
    DECSENDING,
    DEFAULT_BRAND_SORT_COLUMN_NAME,
    DEFAULT_PAGE_LIMIT,
} from '../../Constants/paging';

const ListProduct = () => {
    const [product, setProduct] = useState(null);
    const [query, setQuery] = useState({
        page: 1,
        limit: DEFAULT_PAGE_LIMIT,
        sortOrder: DECSENDING,
        sortColumn: DEFAULT_BRAND_SORT_COLUMN_NAME,
    });

    useEffect(() => {
        GET_ALL_PRODUCTS().then((response) => {
            setProduct(response.data.items);
        });
        // axios
        //     .get(process.env.REACT_APP_BACKEND_URL + Endpoints.products)
        //     .then((response) => {
        //         setProduct(response.data.items);
        //     });
    }, []);

    // useEffect(() => {
    //     async function fetchDataAsync() {
    //         let result = await getBrandsRequest(query);
    //         setProduct(result.data);
    //     }

    //     fetchDataAsync();
    // }, [query, product]);

    if (!product) return null;
    return (
        <div>
            <Link to="/create" type="button" className="btn btn-success">
                Create new Brand
            </Link>
            {/* <Button variant="success">Create</Button> */}
            <Table striped bordered hover responsive>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Price</th>
                        <th>Function</th>
                    </tr>
                </thead>
                <tbody>
                    {product.map((p) => (
                        <tr>
                            <td>{p.name}</td>
                            <td>{p.price}</td>
                            <td>
                                <Button variant="primary">Edit</Button>
                                <Button variant="danger">Delete</Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </div>
    );
};

export default ListProduct;
