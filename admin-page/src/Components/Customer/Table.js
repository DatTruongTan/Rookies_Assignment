import React, { useState } from 'react';
import Table, { SortType } from '../../sharedComponents/Table';

const columns = [
    { columnName: 'User Name', columnValue: 'UserName' },
    { columnName: 'Email', columnValue: 'Email' },
];

const CustomerTable = ({ customers, handlePage, handleSort, sortState }) => {
    return (
        <>
            <Table
                columns={columns}
                handleSort={handleSort}
                sortState={sortState}
                page={{
                    currentPage: customers?.currentPage,
                    totalPage: customers?.totalPages,
                    handleChange: handlePage,
                }}
            >
                {customers &&
                    customers?.map((c, index) => (
                        <tr key={index}>
                            <td>
                                <div className="d-flex justify-content-center">
                                    {c.userName}
                                </div>
                            </td>
                            <td>
                                <div className="d-flex justify-content-center">
                                    {c.email}
                                </div>
                            </td>
                        </tr>
                    ))}
            </Table>
        </>
    );
};

export default CustomerTable;
