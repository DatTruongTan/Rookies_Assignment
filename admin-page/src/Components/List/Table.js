import React, { useState } from 'react';
import { PencilFill, XCircle } from 'react-bootstrap-icons';
import { useHistory } from 'react-router';
import ButtonIcon from '../../sharedComponents/ButtonIcon';
import { NotificationManager } from 'react-notifications';

import Table, { SortType } from '../../sharedComponents/Table';
import Info from '../Info';
import { EDIT_PRODUCT_ID } from '../../Constants/pages';
import ConfirmModal from '../../sharedComponents/ConfirmModal';
import { Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';

import {
    All,
    AllLabel,
    Nike,
    NikeLabel,
    Adidas,
    AdidasLabel,
} from '../../Constants/Brand/BrandConstants';
import { DeleteProductRequest } from '../services/request';

const columns = [
    { columnName: 'Name', columnValue: 'Name' },
    { columnName: 'Price', columnValue: 'Price' },
    { columnName: 'Created Date', columnValue: 'CreateDate' },
    { columnName: 'Image', columnValue: 'ImageFile' },
];

const ProductTable = ({
    products,
    handlePage,
    handleSort,
    sortState,
    fetchData,
}) => {
    const [showDetail, setShowDetail] = useState(false);
    const [detail, setDetail] = useState(null);
    const [deleteProduct, setDeleteProduct] = useState({
        isOpen: false,
        id: 0,
        title: '',
        message: '',
        isDisable: true,
    });

    const handleShowInfo = (id) => {
        const product = products?.items.find((item) => item.id === id);

        if (product) {
            setDetail(product);
            setShowDetail(true);
        }
    };

    const getBrandTypeName = (id) => {
        return id === Adidas ? AdidasLabel : NikeLabel;
    };

    const handleDeleteProduct = async (id) => {
        setDeleteProduct({
            id,
            isOpen: true,
            title: 'Are you sure',
            message: 'Do you want to remove this product?',
            isDisable: true,
        });
    };

    const handleCloseDisable = () => {
        setDeleteProduct({
            isOpen: false,
            id: 0,
            title: '',
            message: '',
            isDisable: true,
        });
    };

    const handleResult = async (result, message) => {
        if (result) {
            handleCloseDisable();
            await fetchData();
            NotificationManager.success(
                `Remove Brand Successful`,
                `Remove Successful`,
                2000
            );
        } else {
            setDeleteProduct({
                ...deleteProduct,
                title: 'Can not disable Brand',
                message,
                isDisable: result,
            });
        }
    };

    const handleConfirmDisable = async () => {
        let isSuccess = await DeleteProductRequest(deleteProduct.id);
        if (isSuccess) {
            await handleResult(true, '');
        }
    };

    const handleCloseDetail = () => {
        setShowDetail(false);
    };

    const history = useHistory();
    const handleEdit = (id) => {
        const existProduct = products?.items.find(
            (item) => item.id === Number(id)
        );
        history.push(EDIT_PRODUCT_ID(id), {
            existProduct: existProduct,
        });
    };

    return (
        <>
            <Table
                columns={columns}
                handleSort={handleSort}
                sortState={sortState}
                page={{
                    currentPage: products?.currentPage,
                    totalPage: products?.totalPages,
                    handleChange: handlePage,
                }}
            >
                {products &&
                    products?.items?.map((p, index) => (
                        <tr
                            key={index}
                            className=""
                            onClick={() => handleShowInfo(p.id)}
                        >
                            <td>
                                <div className="d-flex justify-content-center">
                                    {p.name}
                                </div>
                            </td>
                            <td>
                                <div className="d-flex justify-content-center">
                                    {p.price}
                                </div>
                            </td>
                            <td>
                                <div className="d-flex justify-content-center">
                                    {p.createdDate}
                                </div>
                            </td>
                            <td>
                                <div className="d-flex justify-content-center">
                                    <img
                                        src={`${process.env.REACT_APP_BACKEND_URL}${p.imagePath}`}
                                        alt="product"
                                        height="50"
                                        width="50"
                                    />
                                </div>
                            </td>
                            <td>
                                <div className="d-flex justify-content-evenly">
                                    <ButtonIcon
                                        className="btn btn-primary"
                                        onClick={() => handleEdit(p.id)}
                                    >
                                        <PencilFill className="text-light" />
                                    </ButtonIcon>
                                    <ButtonIcon
                                        className="btn btn-danger"
                                        onClick={() =>
                                            handleDeleteProduct(p.id)
                                        }
                                    >
                                        <XCircle className="text-light mx-2" />
                                    </ButtonIcon>
                                </div>
                            </td>
                        </tr>
                    ))}
            </Table>
            {detail && showDetail && (
                <Info product={detail} handleClose={handleCloseDetail} />
            )}
            <ConfirmModal
                title={deleteProduct.title}
                isShow={deleteProduct.isOpen}
                onHide={handleCloseDisable}
            >
                <div>
                    <div className="text-center">{deleteProduct.message}</div>
                    {deleteProduct.isDisable && (
                        <div className="text-center mt-3">
                            <button
                                className="btn btn-danger mr-3"
                                onClick={handleConfirmDisable}
                                type="button"
                            >
                                Delete
                            </button>

                            <button
                                className="btn btn-outline-secondary"
                                onClick={handleCloseDisable}
                                type="button"
                            >
                                Cancel
                            </button>
                        </div>
                    )}
                </div>
            </ConfirmModal>
        </>
    );
};

export default ProductTable;
