import React from 'react';
import { Modal } from 'react-bootstrap';

import {
    Nike,
    NikeLabel,
    Adidas,
    AdidasLabel,
} from '../../Constants/Brand/BrandConstants';

const Info = ({ product, handleClose }) => {
    const getBrandTypeName = (id) => {
        return id === Adidas ? AdidasLabel : NikeLabel;
    };

    return (
        <>
            <Modal show={true} onHide={handleClose} dialogClassName="modal-90w">
                <Modal.Header closeButton>
                    <Modal.Title id="login-modal">
                        Detailed Brand Infomation
                    </Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    <div>
                        <div className="row -intro-y">
                            <div className="col-12">
                                <strong>Id:</strong> {product.id}
                            </div>
                        </div>
                        <div className="row -intro-y">
                            <div className="col-12">
                                <strong>Name:</strong> {product.name}
                            </div>
                        </div>
                        <div className="row -intro-y">
                            <div className="col-12">
                                <strong>Type:</strong>{' '}
                                {getBrandTypeName(product.type)}
                            </div>
                        </div>

                        <div className="row -intro-y">
                            <div className="col-4">
                                <strong>Image:</strong>
                            </div>
                            <div>
                                <img
                                    src={`${process.env.REACT_APP_BACKEND_URL}${product.imagePath}`}
                                    className="col-12 object-center w-full rounded-md"
                                    alt="product"
                                />
                            </div>
                        </div>
                    </div>
                </Modal.Body>
            </Modal>
        </>
    );
};

export default Info;
