import React, { useState, useEffect } from 'react';
import { Form, Button, Row, Col } from 'react-bootstrap';
import { Link, useHistory } from 'react-router-dom';
import { POST_ADD_PRODUCT } from '../../Services/apiService';
import { createProductRequest } from '../services/request';
import { HOME } from '../../Constants/pages';

import {
    GET_ALL_CATEGORY,
    DELETE_CATEGORY_ID,
} from '../../Services/CategoryApiService';

const CreateProduct = () => {
    const [formValue, setformValue] = useState({});
    const [imageName, setImageName] = useState(null);
    const [category, setCategory] = useState(null);
    let history = useHistory();

    useEffect(() => {
        GET_ALL_CATEGORY()
            .then((response) => {
                console.log(
                    'messages from respone UpdateProduct:',
                    response.data
                );
                setCategory(response.data);
            })
            .catch((error) => {
                console.error('messsage from update component:', error);
            });
    }, []);

    const handleSubmit = (event) => {
        event.preventDefault();

        const formData = new FormData();
        formData.append('Name', formValue.Name);
        formData.append('Price', formValue.Price);
        formData.append('CategoryId', formValue.CategoryId);
        formData.append('Gender', formValue.Gender);
        formData.append('Size', formValue.Size);
        formData.append('ImageFile', imageName);

        createProductRequest(formData)
            .then((response) => {
                console.log('This is response -', response);
                console.log('This is request -', formData);
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
                            onChange={({ target }) =>
                                setformValue((state) => ({
                                    ...state,
                                    CategoryId: target.value,
                                }))
                            }
                        >
                            <option>Choose...</option>
                            {category &&
                                category.map((c, index) => (
                                    <option value={c.id}>{c.name}</option>
                                ))}
                            {/* <option value="1">Adidas</option>
                            <option value="2">Nike</option> */}
                        </Form.Select>
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridState">
                        <Form.Label>Gender</Form.Label>
                        <Form.Select
                            defaultValue="Choose..."
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

                <Form.Group controlId="formFile" className="mb-3">
                    <Form.Label>Image</Form.Label>
                    <Form.Control
                        type="file"
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
};

export default CreateProduct;
