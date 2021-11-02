import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Form, Button, Row, Col } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { POST_ADD_PRODUCT } from '../../Services/apiService';

const CreateProduct = () => {
    const [formValue, setformValue] = useState({});

    const handleSubmit = (event) => {
        event.preventDefault();

        const formData = new FormData();
        formData.append('Name', formValue.Name);
        formData.append('Price', formValue.Price);
        formData.append('Brand', formValue.Brand);
        formData.append('Gender', formValue.Gender);
        formData.append('Size', formValue.Size);
        formData.append('ImageFile', formValue.ImageFile);

        POST_ADD_PRODUCT(formData)
            .then((response) => {
                console.log('Message:', response);
                // setformValue(response.data);
            })
            .catch((error) => {
                console.log(error);
                console.log(formData);
            });

        // axios
        //     .post(process.env.REACT_APP_BACKEND_API, formData, {
        //         headers: {
        //             'Content-Type':
        //                 'application/x-www-form-urlencoded; charset=UTF-8',
        //         },
        //     })
        //     .then((response) => {
        //         console.log('respone here -', response);
        //     })
        //     .catch((error) => {
        //         console.log(error.message);
        //         console.log('error here -', formData);
        //     });
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
                            value={formValue.Name}
                            onChange={({ target }) =>
                                setformValue((state) => ({
                                    ...state,
                                    Name: target.value,
                                }))
                            }
                        />
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridPassword">
                        <Form.Label>Price</Form.Label>
                        <Form.Control
                            type="text"
                            placeholder="Enter product's price'"
                            value={formValue.Price}
                            onChange={({ target }) =>
                                setformValue((state) => ({
                                    ...state,
                                    Price: target.value,
                                }))
                            }
                        />
                    </Form.Group>
                </Row>

                <Row className="mb-3">
                    <Form.Group as={Col} controlId="formGridState">
                        <Form.Label>Brand</Form.Label>
                        <Form.Select
                            defaultValue="Choose..."
                            value={formValue.Brand}
                            onChange={({ target }) =>
                                setformValue((state) => ({
                                    ...state,
                                    Brand: target.value,
                                }))
                            }
                        >
                            <option>Choose...</option>
                            <option value="1">Adidas</option>
                            <option value="2">Nike</option>
                        </Form.Select>
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridState">
                        <Form.Label>Gender</Form.Label>
                        <Form.Select
                            defaultValue="Choose..."
                            value={formValue.Gender}
                            onChange={({ target }) =>
                                setformValue((state) => ({
                                    ...state,
                                    Gender: target.value,
                                }))
                            }
                        >
                            <option>Choose...</option>
                            <option value="0">Male</option>
                            <option value="1">Female</option>
                        </Form.Select>
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridState">
                        <Form.Label>Size</Form.Label>
                        <Form.Select
                            defaultValue="Choose..."
                            value={formValue.Size}
                            onChange={({ target }) =>
                                setformValue((state) => ({
                                    ...state,
                                    Size: target.value,
                                }))
                            }
                        >
                            <option>Choose...</option>
                            <option value="38">38</option>
                            <option value="39">39</option>
                            <option value="40">40</option>
                            <option value="41">41</option>
                        </Form.Select>
                    </Form.Group>
                </Row>

                <Form.Group
                    controlId="formFile"
                    className="mb-3"
                    value={formValue.ImageFile}
                    onChange={({ target }) =>
                        setformValue((state) => ({
                            ...state,
                            ImageFile: target.value,
                        }))
                    }
                >
                    <Form.Label>Image</Form.Label>
                    <Form.Control type="file" />
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
};

export default CreateProduct;
