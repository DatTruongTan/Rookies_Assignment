import React, { useState, useEffect } from 'react';
import { Table } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { PencilFill, XCircle } from 'react-bootstrap-icons';
import ButtonIcon from '../../sharedComponents/ButtonIcon';

import {
    GET_ALL_CATEGORY,
    DELETE_CATEGORY_ID,
} from '../../Services/CategoryApiService';

const ListCategory = () => {
    const [category, setCategory] = useState(null);

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

    const handleDeleteCategory = async (id) => {
        await DELETE_CATEGORY_ID(id).then((response) => {
            console.log('message:', response);
            setCategory(category.filter((key) => key.id !== id));
        });
    };

    return (
        <div>
            <div className="container d-flex mb-3 intro-x">
                <Link
                    to="/category/create"
                    type="button"
                    className="btn btn-success ms-3"
                >
                    Create new Category
                </Link>
            </div>
            <Table striped bordered hover responsive>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Description</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {category &&
                        category.map((c, index) => (
                            <tr>
                                <td>
                                    <div className="d-flex justify-content-center">
                                        {c.id}
                                    </div>
                                </td>
                                <td>
                                    <div className="d-flex justify-content-center">
                                        {c.name}
                                    </div>
                                </td>
                                <td>
                                    <div className="d-flex justify-content-center">
                                        {c.description}
                                    </div>
                                </td>
                                <td>
                                    <div className="d-flex justify-content-evenly">
                                        <Link to={`category/edit/${c.id}`}>
                                            <ButtonIcon className="btn btn-primary">
                                                <PencilFill className="text-light" />
                                            </ButtonIcon>
                                        </Link>
                                        <ButtonIcon
                                            className="btn btn-danger"
                                            onClick={() =>
                                                handleDeleteCategory(c.id)
                                            }
                                        >
                                            <XCircle className="text-light mx-2" />
                                        </ButtonIcon>
                                    </div>
                                </td>
                            </tr>
                        ))}
                </tbody>
            </Table>
        </div>
    );
};

export default ListCategory;
