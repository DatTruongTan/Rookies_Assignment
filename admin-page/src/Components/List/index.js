import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Table, Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import Endpoints from '../../Constants/endpoints';
import { getBrandsRequest } from '../services/request';
import UpdateProduct from '../Update';
import { GET_ALL_PRODUCTS, DELETE_PRODUCT_ID } from '../../Services/apiService';
import {
    ACCSENDING,
    DECSENDING,
    DEFAULT_BRAND_SORT_COLUMN_NAME,
    DEFAULT_PAGE_LIMIT,
} from '../../Constants/paging';

const ListProduct = () => {
    const [product, setProduct] = useState(null);
    const [isDeleted, setIsDeleted] = useState(false);
    const [query, setQuery] = useState(
        {
            page: 1,
            limit: DEFAULT_PAGE_LIMIT,
            sortOrder: DECSENDING,
            sortColumn: DEFAULT_BRAND_SORT_COLUMN_NAME,
        },
        []
    );

    const handleDeleteProduct = (id) => {
        DELETE_PRODUCT_ID(id)
            .then((response) => {
                console.log('Deleted Product -', response);
                setProduct(product.filter((p) => p.id !== id));
            })
            .catch((err) => {
                console.log(err);
            });
    };

    useEffect(() => {
        GET_ALL_PRODUCTS().then((response) => {
            setProduct(response.data.items);
            console.log(response.data.items);
        });
    }, []);

    if (!product) return null;
    return (
        <div>
            <Link to="/create" type="button" className="btn btn-success">
                Create new Brand
            </Link>
            <Table striped bordered hover responsive>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Price</th>
                        <th>Created Date</th>
                        <th>Image</th>
                        <th>Function</th>
                    </tr>
                </thead>
                <tbody>
                    {product.map((p) => (
                        <tr>
                            <td>{p.name}</td>
                            <td>{p.price}</td>
                            <td>{p.createdDate}</td>
                            <td>
                                <img
                                    src={`${process.env.REACT_APP_BACKEND_URL}${p.imagePath}`}
                                    alt="product"
                                    height="50"
                                    width="50"
                                />
                            </td>
                            <td>
                                <Link
                                    to={`/edit/${p.id}`}
                                    type="button"
                                    className="btn btn-primary"
                                >
                                    Edit
                                </Link>
                                <Button
                                    variant="danger"
                                    type="submit"
                                    onClick={() => handleDeleteProduct(p.id)}
                                >
                                    Delete
                                </Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </div>
    );
};

export default ListProduct;
