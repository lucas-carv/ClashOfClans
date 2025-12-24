
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
            const formattedData = data.map(item => {
                const newItem = {};
                if (item.name) newItem['Nome'] = item.name;
                if (item.MediaDestruicao) newItem['Média Destruição'] = item.MediaDestruicao;
                if (item.QuantidadeGuerras) newItem['Qtd Guerras'] = item.QuantidadeGuerras;

                Object.keys(item).forEach(key => {
                    const normalizedKey = key.toLowerCase();
                    if (!['name', 'role'].includes(normalizedKey)) {
                        newItem[key] = item[key];
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

            <div className="input-group" style={{ marginBottom: '1rem', display: 'flex', gap: '1rem', flexWrap: 'wrap' }}>
                <div style={{ flex: '1 1 200px' }}>
                    <input
                        type="text"
                        className="input-field"
                        value={clanTag}
                        onChange={(e) => setClanTag(e.target.value)}
                        placeholder="Tag do Clan (ex: #2L0UC9R8P)"
                    />
                </div>
                <div style={{ flex: '1 1 100px' }}>
                    <input
                        type="text"
                        className="input-field"
                        value={qtdMinimoGuerras}
                        onChange={(e) => setQtdMinimoGuerras(e.target.value)}
                        placeholder="Mín Guerras"
                        title="Quantidade Mínima de Guerras"
                    />
                </div>
                <div style={{ flex: '1 1 100px' }}>
                    <input
                        type="text"
                        className="input-field"
                        value={qtdMaximoGuerras}
                        onChange={(e) => setQtdMaximoGuerras(e.target.value)}
                        placeholder="Max Guerras"
                        title="Quantidade Máxima de Guerras"
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
