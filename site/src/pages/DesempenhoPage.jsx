
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
            <h2>Desempenho dos Membros nas últimas {quantidadeGuerras} guerras</h2>
            <div className="filters">
                <label>
                    Quantidade de guerras:
                    <input
                        type="number"
                        min="1"
                        value={quantidadeGuerras}
                        onChange={(e) => setQuantidadeGuerras(Number(e.target.value))}
                    />
                </label>
                <button onClick={fetchPerformance}>Buscar</button>
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
