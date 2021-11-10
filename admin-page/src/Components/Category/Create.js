import React, { useState } from 'react';
import { Form, Button, Row, Col } from 'react-bootstrap';
import { Link, useHistory } from 'react-router-dom';
import { POST_ADD_CATEGORY } from '../../Services/CategoryApiService';
import { createProductRequest } from '../services/request';
import { CATEGORY } from '../../Constants/pages';

const CreateCategory = () => {
    const [formValue, setformValue] = useState({});
    let history = useHistory();

    const handleSubmit = (event) => {
        event.preventDefault();

        const formData = new FormData();
        formData.append('Name', formValue.Name);
        formData.append('Description', formValue.Description);

        POST_ADD_CATEGORY(formData)
            .then((response) => {
                console.log('This is response -', response);
                console.log('This is request -', formData);
                history.push(CATEGORY);
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
                        <Form.Label>Description</Form.Label>
                        <Form.Control
                            type="text"
                            placeholder="Enter product's price'"
                            onChange={({ target }) =>
                                setformValue((state) => ({
                                    ...state,
                                    Description: target.value,
                                }))
                            }
                        />
                    </Form.Group>
                </Row>

                <Button variant="primary" type="submit">
                    Submit
                </Button>
                <Link
                    to="/category"
                    type="button"
                    className="btn btn-secondary"
                >
                    Cancel
                </Link>
            </Form>
        </div>
    );
};

export default CreateCategory;
