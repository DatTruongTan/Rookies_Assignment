import React, { useState, useEffect } from 'react';
import { Form, Button, Row, Col } from 'react-bootstrap';
import { Link, useHistory } from 'react-router-dom';
import { HOME } from '../../Constants/pages';

import {
    GET_PRODUCTS_BY_ID,
    PUT_EDIT_PRODUCT,
} from '../../Services/apiService';

export default function UpdateProduct({ match, location }) {
    const history = useHistory();
    const [productID, setProductID] = useState(null);
    const [name, setName] = useState(null);
    const [price, setPrice] = useState(null);
    const [brand, setBrand] = useState(null);
    const [gender, setGender] = useState(null);
    const [size, setSize] = useState(null);
    const [imageName, setImageName] = useState(null);

    useEffect(() => {
        console.log(location);
        console.log(match.params.id);
        GET_PRODUCTS_BY_ID(match.params.id)
            .then((response) => {
                console.log(
                    'messages from respone UpdateProduct:',
                    response.data
                );
                setProductID(response.data.id);
                setName(response.data.name);
                setPrice(response.data.price);
                setBrand(response.data.brand);
                setGender(response.data.gender);
                setSize(response.data.size);
                setImageName(response.data.imagePath);
            })
            .catch((error) => {
                console.error('messsage from update component:', error);
            });
    }, []);

    const handleSubmit = (event) => {
        event.preventDefault();

        const formData = new FormData();
        formData.append('Name', name);
        formData.append('Price', price);
        formData.append('Brand', brand);
        formData.append('Gender', gender);
        formData.append('Size', size);
        formData.append('ImageFile', imageName);

        PUT_EDIT_PRODUCT(productID, formData)
            .then((response) => {
                console.log('Message from put Product:', response);
                history.push(HOME);
            })
            .catch((error) => {
                console.log(error);
                console.log(formData);
            });
    };

    return (
        <div>
            <Form onSubmit={handleSubmit}>
                <Row className="mb-3">
                    <Form.Group as={Col} controlId="formGridEmail">
                        <Form.Label>Name</Form.Label>
                        <Form.Control
                            type="text"
                            placeholder="Enter product's name"
                            value={name}
                            onChange={(event) => setName(event.target.value)}
                        />
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridPassword">
                        <Form.Label>Price</Form.Label>
                        <Form.Control
                            type="text"
                            placeholder="Enter product's price'"
                            value={price}
                            onChange={(event) => setPrice(event.target.value)}
                        />
                    </Form.Group>
                </Row>

                <Row className="mb-3">
                    <Form.Group as={Col} controlId="formGridState">
                        <Form.Label>Brand</Form.Label>
                        <Form.Select
                            value={brand}
                            onChange={(event) => setBrand(event.target.value)}
                        >
                            <option>Choose...</option>
                            <option value="1">Adidas</option>
                            <option value="2">Nike</option>
                        </Form.Select>
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridState">
                        <Form.Label>Gender</Form.Label>
                        <Form.Select
                            value={gender}
                            onChange={(event) => setGender(event.target.value)}
                        >
                            <option>Choose...</option>
                            <option value="0">Male</option>
                            <option value="1">Female</option>
                        </Form.Select>
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridState">
                        <Form.Label>Size</Form.Label>
                        <Form.Select
                            value={size}
                            onChange={(event) => setSize(event.target.value)}
                        >
                            <option>Choose...</option>
                            <option value="38">38</option>
                            <option value="39">39</option>
                            <option value="40">40</option>
                            <option value="41">41</option>
                        </Form.Select>
                    </Form.Group>
                </Row>

                <Form.Group controlId="formFile" className="mb-3">
                    <Form.Label>Image</Form.Label>
                    <Form.Control
                        type="file"
                        defaultvalue={imageName}
                        onChange={(event) =>
                            setImageName(event.target.files[0])
                        }
                    />
                </Form.Group>

                <Button variant="primary" type="submit">
                    Submit
                </Button>
                <Link to="/" type="button" className="btn btn-secondary">
                    Cancel
                </Link>
            </Form>
        </div>
    );
}

// import React, { useEffect, useState } from 'react';
// import { Redirect, useParams, useLocation } from 'react-router';

// import FormProduct from '../Form';

// const UpdateProduct = () => {
//     const [product, setProduct] = useState(undefined);
//     const { state } = useLocation();
//     const { existProduct } = state;

//     useEffect(() => {
//         if (existProduct) {
//             setProduct({
//                 id: existProduct.id,
//                 name: existProduct.name,
//                 price: existProduct.price,
//                 brand: existProduct.brand,
//                 gender: existProduct.gender,
//                 size: existProduct.size,
//                 imagePath: existProduct.imagePath,
//             });
//         }
//     }, [existProduct]);

//     return (
//         <div className="ml-5">
//             <div className="primaryColor text-title intro-x">
//                 Update Brand {existProduct?.name}
//             </div>

//             <div className="row">
//                 {product && <FormProduct initialBrandForm={product} />}
//             </div>
//         </div>
//     );
// };

// export default UpdateProduct;
