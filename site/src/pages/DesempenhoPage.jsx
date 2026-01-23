
import React, { useEffect, useState, startTransition } from 'react';
import DataTable from '../components/DataTable';
import { obterDesempenhoDeMembros } from '../services/api';

const PerformancePage = () => {
    const [performanceData, setPerformanceData] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const clanTag = '#2L0UC9R8P';
    const [quantidadeGuerras, setQuantidadeGuerras] = useState(5);

    const fetchPerformance = async () => {
        setLoading(true);
        setError(null);
        try {
            const data = await obterDesempenhoDeMembros(clanTag, quantidadeGuerras);
            const formattedData = data.map(item => {
                const newItem = {};

                Object.keys(item).forEach(key => {
                    const normalizedKey = key.toLowerCase();

                    if (normalizedKey === 'quantidadeguerras') {
                        newItem['Qtd Guerras'] = item[key];
                    } else if (normalizedKey === 'membrotag') {
                        return;
                    } else if (normalizedKey === 'mediaestrelas') {
                        newItem['Média Estrelas'] = item[key];
                    } else if (normalizedKey === 'mediadestruicao') {
                        newItem['Média Destruição'] = item[key];
                    } else if (normalizedKey === 'totalataques') {
                        newItem['Total Ataques'] = item[key];
                    } else if (normalizedKey === 'totalestrelas') {
                        newItem['Total Estrelas'] = item[key];
                    } else {
                        const readableKey = key.replace(/([A-Z])/g, ' $1').trim();
                        const titleCaseKey = readableKey.charAt(0).toUpperCase() + readableKey.slice(1);
                        newItem[titleCaseKey] = item[key];
                    }
                });
                return newItem;
            });
            setPerformanceData(formattedData);
        } catch (err) {
            console.error(err);
            setError('Falha ao carregar dados de desempenho.');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchPerformance();
    }, [clanTag]);

    return (
        <div className="section-container">
            <h2>Desempenho de membros</h2>
            {/* Premium Filters Section */}
            <div style={{
                display: 'flex',
                flexWrap: 'wrap',
                justifyContent: 'center',
                alignItems: 'flex-end',
                gap: '1rem',
                marginBottom: '2rem',
                background: 'rgba(22, 27, 34, 0.4)',
                padding: '1.5rem',
                borderRadius: '16px',
                border: '1px solid rgba(255, 255, 255, 0.1)',
                backdropFilter: 'blur(10px)',
                maxWidth: '600px',
                margin: '0 auto 2rem auto',
                boxShadow: '0 8px 32px rgba(0, 0, 0, 0.2)',
            }}>
                <div style={{
                    display: 'flex',
                    flexDirection: 'column',
                    gap: '0.5rem',
                    flex: '1 1 200px',
                    minWidth: '200px'
                }}>
                    <label style={{
                        fontSize: '0.9rem',
                        fontWeight: '600',
                        color: 'rgba(255, 255, 255, 0.8)',
                        letterSpacing: '0.5px'
                    }}>
                        QUANTIDADE DE GUERRAS
                    </label>
                    <input
                        type="number"
                        min="1"
                        value={quantidadeGuerras}
                        onChange={(e) => setQuantidadeGuerras(Number(e.target.value))}
                        style={{
                            background: 'rgba(0, 0, 0, 0.3)',
                            border: '1px solid rgba(76, 201, 240, 0.3)',
                            borderRadius: '8px',
                            padding: '0.75rem 1rem',
                            fontSize: '1rem',
                            color: '#fff',
                            outline: 'none',
                            transition: 'all 0.2s ease',
                            width: '100%',
                            fontFamily: 'inherit',
                            boxSizing: 'border-box',
                        }}
                        onFocus={(e) => {
                            e.target.style.border = '1px solid #4cc9f0';
                            e.target.style.boxShadow = '0 0 0 2px rgba(76, 201, 240, 0.2)';
                        }}
                        onBlur={(e) => {
                            e.target.style.border = '1px solid rgba(76, 201, 240, 0.3)';
                            e.target.style.boxShadow = 'none';
                        }}
                    />
                </div>
                <button
                    onClick={fetchPerformance}
                    style={{
                        height: 'fit-content',
                        padding: '0.75rem 1.5rem',
                        fontSize: '0.9rem',
                        flex: '0 0 auto',
                        marginBottom: '1px' // Adjustment for border/shadow alignment
                    }}
                >
                    BUSCAR
                </button>
            </div>
            {loading && <div className="loading">Carregando desempenho...</div>}

            {error && <div className="error">{error}</div>}

            {!loading && !error && (
                <DataTable data={performanceData} />
            )}
        </div>
    );
};

export default PerformancePage;
