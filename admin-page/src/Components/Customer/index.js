import React, { useState, useEffect } from 'react';
import { FilterBrandTypeOptions } from '../../Constants/selectOptions';
import CustomerTable from './Table';
import { getCustomersRequest } from '../services/request';
import {
    ACCSENDING,
    DECSENDING,
    DEFAULT_BRAND_SORT_COLUMN_NAME,
    DEFAULT_PAGE_LIMIT,
} from '../../Constants/paging';

const ListUser = () => {
    const [customers, setCustomers] = useState(null);
    const [query, setQuery] = useState(
        {
            page: 1,
            limit: DEFAULT_PAGE_LIMIT,
            sortOrder: DECSENDING,
            sortColumn: DEFAULT_BRAND_SORT_COLUMN_NAME,
        },
        []
    );

    const handlePage = (page) => {
        setQuery({
            ...query,
            page,
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
            let result = await getCustomersRequest(query);
            console.log(result.data);
            setCustomers(result.data);
        }

        fetchDataAsync();
    }, [query, customers]);

    return (
        <div>
            <CustomerTable
                customers={customers}
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

export default ListUser;
