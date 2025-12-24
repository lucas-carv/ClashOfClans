
import React, { useEffect, useState, startTransition } from 'react';
import DataTable from '../components/DataTable';
import { getMemberPerformance } from '../services/api';

const PerformancePage = () => {
    const [performanceData, setPerformanceData] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const [clanTag, setClanTag] = useState('#2L0UC9R8P'); // Default or state driven
    const [qtdMinimoGuerras, setQtdMinimoGuerras] = useState('5');
    const [qtdMaximoGuerras, setQtdMaximoGuerras] = useState('10');

    const fetchPerformance = async () => {
        setLoading(true);
        setError(null);
        try {
            const data = await getMemberPerformance(clanTag, qtdMinimoGuerras, qtdMaximoGuerras);
            const formattedData = data.map(item => {
                const newItem = {};

                Object.keys(item).forEach(key => {
                    const normalizedKey = key.toLowerCase();

                    if (normalizedKey === 'quantidadeguerras') {
                        newItem['Qtd Guerras'] = item[key];
                    } else if (normalizedKey === 'mediaestrelas') {
                        newItem['Média Estrelas'] = item[key];
                    } else if (normalizedKey === 'mediadestruicao') {
                        newItem['Média Destruição'] = item[key];
                    } else if (normalizedKey === 'totalataques') {
                        newItem['Total Ataques'] = item[key];
                    } else if (normalizedKey === 'totalestrelas') {
                        newItem['Total Estrelas'] = item[key];
                    } else {
                        // Generic formatter for other keys (e.g. Tag, etc)
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
            <h2>Desempenho dos Membros</h2>

            <div className="input-group" style={{ marginBottom: '1rem', display: 'flex', gap: '1rem', flexWrap: 'wrap', alignItems: 'flex-end' }}>
                <div style={{ flex: '1 1 200px' }}>
                    <label className="input-label">Tag do Clan</label>
                    <input
                        type="text"
                        className="input-field"
                        value={clanTag}
                        onChange={(e) => setClanTag(e.target.value)}
                        placeholder="#2L0UC9R8P"
                    />
                </div>
                <div style={{ flex: '1 1 100px' }}>
                    <label className="input-label">Mín. Guerras</label>
                    <input
                        type="text"
                        className="input-field"
                        value={qtdMinimoGuerras}
                        onChange={(e) => setQtdMinimoGuerras(e.target.value)}
                        placeholder="5"
                    />
                </div>
                <div style={{ flex: '1 1 100px' }}>
                    <label className="input-label">Max. Guerras</label>
                    <input
                        type="text"
                        className="input-field"
                        value={qtdMaximoGuerras}
                        onChange={(e) => setQtdMaximoGuerras(e.target.value)}
                        placeholder="10"
                    />
                </div>
            </div>

            <button onClick={fetchPerformance}>Buscar</button>
            {loading && <div className="loading">Carregando desempenho...</div>}

            {error && <div className="error">{error}</div>}

            {!loading && !error && (
                <DataTable data={performanceData} />
            )}
        </div>
    );
};

export default PerformancePage;
