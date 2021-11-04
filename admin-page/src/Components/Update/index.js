import React, { useState, useEffect, useLocation } from 'react';
import { Form, Button, Row, Col } from 'react-bootstrap';
import { Redirect, Link } from 'react-router-dom';

import {
    GET_PRODUCTS_BY_ID,
    PUT_EDIT_PRODUCT,
} from '../../Services/apiService';

export default function UpdateProduct({ match, location }) {
    // const [formValue, setformValue] = useState({});
    const [checkUpdate, setCheckUpdate] = useState(false);
    const [productID, setProductID] = useState(null);
    const [name, setName] = useState(null);
    const [price, setPrice] = useState(null);
    const [brand, setBrand] = useState(null);
    const [gender, setGender] = useState(null);
    const [size, setSize] = useState(null);
    const [imageName, setImageName] = useState(null);

    // const { id } = props;
    useEffect(() => {
        console.log(location);
        console.log(match.params.id);
        GET_PRODUCTS_BY_ID(match.params.id)
            .then((response) => {
                // setformValue(response.data);
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
                // console.error(response);
            });
    }, []);

    // const handleChangeName = (event) => {
    //     setName(event.target.value);
    // };
    // const handleChangePrice = (event) => {
    //     setPrice(event.target.value);
    // };
    // const handleChangeBrand = (event) => {
    //     setBrand(event.target.value);
    // };
    // const handleChangeGender = (event) => {
    //     setGender(event.target.value);
    // };
    // const handleChangeSize = (event) => {
    //     setSize(event.target.value);
    // };
    // const handleChangeImageName = (event) => {
    //     setImageName(event.target.value);
    // };
    const handleSubmit = (event) => {
        event.preventDefault();

        const formData = new FormData();
        // formData.append('Name', formValue.Name);
        // formData.append('Price', formValue.Price);
        // formData.append('Brand', formValue.Brand);
        // formData.append('Gender', formValue.Gender);
        // formData.append('Size', formValue.Size);
        // formData.append('ImageFile', formValue.ImageName);

        formData.append('Name', name);
        formData.append('Price', price);
        formData.append('Brand', brand);
        formData.append('Gender', gender);
        formData.append('Size', size);
        formData.append('ImageFile', imageName);
        // let formData = {
        //     Name: name,
        //     Price: price,
        //     Brand: brand,
        //     Gender: gender,
        //     Size: size,
        //     ImageFile: imageName,
        // };

        PUT_EDIT_PRODUCT(productID, formData)
            .then((response) => {
                console.log('Message from put Product:', response);
                setCheckUpdate(true);
                // if (response.data === 1) {
                //     setCheckUpdate(true);
                // }
            })
            .catch((error) => {
                console.log(error);
                console.log(formData);
            });
    };

    if (checkUpdate) {
        return <Redirect to="/" />;
    }
    return (
        <div>
            <Form onSubmit={handleSubmit}>
                <Row className="mb-3">
                    <Form.Group as={Col} controlId="formGridEmail">
                        <Form.Label>Name</Form.Label>
                        <Form.Control
                            type="text"
                            placeholder="Enter product's name"
                            // value={formValue.Name}
                            // onChange={({ target }) =>
                            //     setformValue((state) => ({
                            //         ...state,
                            //         Name: target.value,
                            //     }))
                            // }
                            value={name}
                            onChange={(event) => setName(event.target.value)}
                            // onChange={handleChangeName}
                        />
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridPassword">
                        <Form.Label>Price</Form.Label>
                        <Form.Control
                            type="text"
                            placeholder="Enter product's price'"
                            // value={formValue.Price}
                            // onChange={({ target }) =>
                            //     setformValue((state) => ({
                            //         ...state,
                            //         Price: target.value,
                            //     }))
                            // }
                            value={price}
                            onChange={(event) => setPrice(event.target.value)}
                            // onChange={handleChangePrice}
                        />
                    </Form.Group>
                </Row>

                <Row className="mb-3">
                    <Form.Group as={Col} controlId="formGridState">
                        <Form.Label>Brand</Form.Label>
                        <Form.Select
                            // defaultValue="Choose..."
                            // value={formValue.Brand}
                            // onChange={({ target }) =>
                            //     setformValue((state) => ({
                            //         ...state,
                            //         Brand: target.value,
                            //     }))
                            // }
                            value={brand}
                            onChange={(event) => setBrand(event.target.value)}
                            // onchange={handleChangeBrand}
                        >
                            <option>Choose...</option>
                            <option value="1">Adidas</option>
                            <option value="2">Nike</option>
                        </Form.Select>
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridState">
                        <Form.Label>Gender</Form.Label>
                        <Form.Select
                            // defaultValue="Choose..."
                            // value={formValue.Gender}
                            // onChange={({ target }) =>
                            //     setformValue((state) => ({
                            //         ...state,
                            //         Gender: target.value,
                            //     }))
                            // }
                            value={gender}
                            onChange={(event) => setGender(event.target.value)}
                            // onChange={handleChangeGender}
                        >
                            <option>Choose...</option>
                            <option value="0">Male</option>
                            <option value="1">Female</option>
                        </Form.Select>
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridState">
                        <Form.Label>Size</Form.Label>
                        <Form.Select
                            // defaultValue="Choose..."
                            // value={formValue.Size}
                            // onChange={({ target }) =>
                            //     setformValue((state) => ({
                            //         ...state,
                            //         Size: target.value,
                            //     }))
                            // }

                            value={size}
                            onChange={(event) => setSize(event.target.value)}
                            // onChange={handleChangeSize}
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
                        // onChange={({ target }) =>
                        //     setformValue((state) => ({
                        //         ...state,
                        //         ImageName: target.files[0],
                        //     }))
                        // }
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

// export default UpdateProduct;
