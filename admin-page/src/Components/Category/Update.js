import React, { useState, useEffect } from 'react';
import { Form, Button, Row, Col } from 'react-bootstrap';
import { Link, useHistory } from 'react-router-dom';
import { CATEGORY } from '../../Constants/pages';

import {
    GET_CATEGORY_BY_ID,
    PUT_EDIT_CATEGORY,
} from '../../Services/CategoryApiService';

export default function UpdateProduct({ match, location }) {
    const history = useHistory();
    const [categoryID, setCategoryID] = useState(null);
    const [name, setName] = useState(null);
    const [description, setDescription] = useState(null);

    useEffect(() => {
        console.log(location);
        console.log(match.params.id);
        GET_CATEGORY_BY_ID(match.params.id)
            .then((response) => {
                console.log(
                    'messages from respone UpdateProduct:',
                    response.data
                );
                setCategoryID(response.data.id);
                setName(response.data.name);
                setDescription(response.data.description);
            })
            .catch((error) => {
                console.error('messsage from update component:', error);
            });
    }, []);

    const handleSubmit = (event) => {
        event.preventDefault();

        const formData = new FormData();
        formData.append('Name', name);
        formData.append('Description', description);

        PUT_EDIT_CATEGORY(categoryID, formData)
            .then((response) => {
                console.log('Message from put Product:', response);
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
                            value={name}
                            onChange={(event) => setName(event.target.value)}
                        />
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridPassword">
                        <Form.Label>Description</Form.Label>
                        <Form.Control
                            type="text"
                            placeholder="Enter product's price'"
                            value={description}
                            onChange={(event) =>
                                setDescription(event.target.value)
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
}
