
import React from 'react';

const DataTable = ({ data, rowStyle }) => {
    if (!data || data.length === 0) {
        return (
            <div className="empty-state">
                <p>Nenhum dado encontrado.</p>
            </div>
        );
    }

    // Extract headers from the first object, assuming uniform data
    const headers = Object.keys(data[0]);

    return (
        <div className="table-container">
            <table className="data-table">
                <thead>
                    <tr>
                        {headers.map((header) => (
                            <th key={header}>{header.toUpperCase()}</th>
                        ))}
                    </tr>
                </thead>
                <tbody>
                    {data.map((row, index) => (
                        <tr key={index} style={rowStyle ? rowStyle(row) : {}}>
                            {headers.map((header) => (
                                <td key={`${index}-${header}`} data-label={header.toUpperCase()}>
                                    {typeof row[header] === 'object' ? JSON.stringify(row[header]) : row[header]}
                                </td>
                            ))}
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default DataTable;
