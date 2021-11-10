import React, { useState, useEffect } from 'react';
import { Search } from 'react-feather';
import ReactMultiSelectCheckboxes from 'react-multiselect-checkboxes';
import { FilterBrandTypeOptions } from '../../Constants/selectOptions';
import ProductTable from './Table';
import { Link } from 'react-router-dom';
import { getProductsRequest } from '../services/request';
import {
    ACCSENDING,
    DECSENDING,
    DEFAULT_BRAND_SORT_COLUMN_NAME,
    DEFAULT_PAGE_LIMIT,
} from '../../Constants/paging';

const ListProduct = () => {
    const [product, setProduct] = useState(null);
    const [search, setSearch] = useState('');
    const [query, setQuery] = useState(
        {
            page: 1,
            limit: DEFAULT_PAGE_LIMIT,
            sortOrder: DECSENDING,
            sortColumn: DEFAULT_BRAND_SORT_COLUMN_NAME,
        },
        []
    );

    const [selectedType, setSelectedType] = useState([
        { id: 1, label: 'All', value: 0 },
    ]);

    const handleType = (selected) => {
        if (selected.length === 0) {
            setQuery({
                ...query,
                types: [],
            });

            setSelectedType([FilterBrandTypeOptions[0]]);
            return;
        }

        const selectedAll = selected.find((item) => item.id === 1);

        setSelectedType((prevSelected) => {
            if (!prevSelected.some((item) => item.id === 1) && selectedAll) {
                setQuery({
                    ...query,
                    types: [],
                });

                return [selectedAll];
            }

            const newSelected = selected.filter((item) => item.id !== 1);
            const types = newSelected.map((item) => item.value);

            setQuery({
                ...query,
                types,
            });

            return newSelected;
        });
    };

    const handleChangeSearch = (e) => {
        e.preventDefault();

        const search = e.target.value;
        setSearch(search);
    };

    const handlePage = (page) => {
        setQuery({
            ...query,
            page,
        });
    };

    const handleSearch = () => {
        setQuery({
            ...query,
            search,
        });
    };

    const handleSort = (sortColumn) => {
        const sortOrder =
            query.sortOrder === ACCSENDING ? DECSENDING : ACCSENDING;

        setQuery({
            ...query,
            sortColumn,
            sortOrder,
        });
    };

    useEffect(() => {
        async function fetchDataAsync() {
            let result = await getProductsRequest(query);
            setProduct(result.data);
        }

        fetchDataAsync();
    }, [query, product]);

    return (
        <div>
            <div className="container d-flex mb-3 intro-x">
                <div className="d-flex align-items-center w-md mr-5">
                    <ReactMultiSelectCheckboxes
                        options={FilterBrandTypeOptions}
                        hideSearch={true}
                        placeholderButtonLabel="Type"
                        value={selectedType}
                        onChange={handleType}
                    />
                </div>
                <div className="d-flex align-items-center w-ld ml-auto">
                    <div className="input-group">
                        <input
                            onChange={handleChangeSearch}
                            value={search}
                            type="text"
                            className="form-control"
                        />
                        <span
                            onClick={handleSearch}
                            className="border p-2 pointer"
                        >
                            <Search />
                        </span>
                    </div>
                </div>
                <Link
                    to="/create"
                    type="button"
                    className="btn btn-success ms-3"
                >
                    Create new Product
                </Link>
            </div>
            <ProductTable
                products={product}
                handlePage={handlePage}
                handleSort={handleSort}
                sortState={{
                    columnValue: query.sortColumn,
                    orderBy: query.sortOrder,
                }}
            />
        </div>
    );
};

export default ListProduct;
