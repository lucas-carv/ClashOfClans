
import React, { useEffect, useState, startTransition } from 'react';
import DataTable from '../components/DataTable';
import { getMemberPerformance } from '../services/api';

const PerformancePage = () => {
    const [performanceData, setPerformanceData] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const [clanTag, setClanTag] = useState('#2L0UC9R8P'); // Default or state driven
    const [qtdGuerras, setQtdGuerras] = useState('10');
    const [qtdMinimoGuerras, setQtdMinimoGuerras] = useState('5');
    const [qtdMaximoGuerras, setQtdMaximoGuerras] = useState('10');

    const fetchPerformance = async () => {
        setLoading(true);
        setError(null);
        try {
            const data = await getMemberPerformance(clanTag, qtdGuerras, qtdMinimoGuerras, qtdMaximoGuerras);
            setPerformanceData(data);
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

            <div className="input-group" style={{ marginBottom: '1rem' }}>
                <input
                    type="text"
                    value={clanTag}
                    onChange={(e) => setClanTag(e.target.value)}
                    placeholder="Tag do Clan (ex: #2L0UC9R8P)"
                    style={{ padding: '0.5rem', marginRight: '0.5rem', borderRadius: '4px', border: '1px solid #ccc', color: 'black' }}
                />
            </div>
            <div className="input-group" style={{ marginBottom: '1rem' }}>
                <input
                    type="text"
                    value={qtdMinimoGuerras}
                    onChange={(e) => setQtdMinimoGuerras(e.target.value)}
                    placeholder="0"
                    style={{ padding: '0.5rem', marginRight: '0.5rem', borderRadius: '4px', border: '1px solid #ccc', color: 'black' }}
                />
            </div>
            <div className="input-group" style={{ marginBottom: '1rem' }}>
                <input
                    type="text"
                    value={qtdMaximoGuerras}
                    onChange={(e) => setQtdMaximoGuerras(e.target.value)}
                    placeholder="0"
                    style={{ padding: '0.5rem', marginRight: '0.5rem', borderRadius: '4px', border: '1px solid #ccc', color: 'black' }}
                />
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
