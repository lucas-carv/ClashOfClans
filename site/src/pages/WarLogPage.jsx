import React, { useEffect, useState } from 'react';
import { obterLogsDeGuerra } from '../services/api';

const WarLogPage = () => {
    const [warLogs, setWarLogs] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const clanTag = '#2L0UC9R8P'; // Default tag

    const fetchLogs = async () => {
        setLoading(true);
        setError(null);
        try {
            const data = await obterLogsDeGuerra(clanTag);
            // Ensure data is an array
            setWarLogs(Array.isArray(data) ? data : [data]);
        } catch (err) {
            console.error(err);
            setError('Falha ao carregar o histórico de guerras.');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchLogs();
    }, []);

    const formatDate = (dateString) => {
        if (!dateString) return '01/01/0001';
        const options = { day: '2-digit', month: '2-digit', year: 'numeric', hour: '2-digit', minute: '2-digit' };
        return new Date(dateString).toLocaleDateString('pt-BR', options);
    };

    // Inline Styles for Premium Look
    const styles = {
        container: {
            display: 'grid',
            gridTemplateColumns: 'repeat(auto-fit, minmax(300px, 1fr))',
            gap: '1.5rem',
            padding: '1rem',
            maxWidth: '1200px',
            margin: '0 auto',
        },
        card: (result) => {
            let borderColor = '#4ade80'; // win (green)
            if (result === 'lose') borderColor = '#ff6b6b';
            if (result === 'draw') borderColor = '#a0aec0';

            return {
                background: 'rgba(22, 27, 34, 0.6)',
                backdropFilter: 'blur(12px)',
                borderRadius: '16px',
                border: `1px solid ${borderColor}`,
                padding: '1.5rem',
                display: 'flex',
                flexDirection: 'column',
                gap: '1rem',
                boxShadow: '0 8px 32px rgba(0, 0, 0, 0.3)',
                transition: 'transform 0.2s ease, box-shadow 0.2s ease',
                position: 'relative',
                overflow: 'hidden',
            };
        },
        header: {
            display: 'flex',
            justifyContent: 'space-between',
            alignItems: 'center',
            marginBottom: '0.5rem',
        },
        badge: (result) => {
            let bgColor = 'rgba(74, 222, 128, 0.2)';
            let color = '#4ade80';
            let borderColor = '#4ade80';

            if (result === 'lose') {
                bgColor = 'rgba(255, 107, 107, 0.2)';
                color = '#ff6b6b';
                borderColor = '#ff6b6b';
            } else if (result === 'draw') {
                bgColor = 'rgba(160, 174, 192, 0.2)';
                color = '#a0aec0';
                borderColor = '#a0aec0';
            }

            return {
                padding: '0.25rem 0.75rem',
                borderRadius: '20px',
                fontSize: '0.75rem',
                fontWeight: '800',
                textTransform: 'uppercase',
                backgroundColor: bgColor,
                color: color,
                border: `1px solid ${borderColor}`,
            };
        },
        dateContainer: {
            display: 'flex',
            flexDirection: 'column',
            fontSize: '0.75rem',
            color: 'rgba(255, 255, 255, 0.6)',
            textAlign: 'right',
        },
        dateText: {
            marginBottom: '0.2rem',
        },
        versusContainer: {
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'space-between',
            gap: '1rem',
        },
        clanInfo: {
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
            flex: 1,
            textAlign: 'center',
        },
        clanName: {
            fontWeight: '700',
            fontSize: '1.1rem',
            color: '#fff',
            marginBottom: '0.25rem',
        },
        stars: {
            display: 'flex',
            alignItems: 'center',
            gap: '0.25rem',
            color: '#fccb44', // Clash Gold
            fontWeight: '600',
            fontSize: '1.2rem',
        },
        vs: {
            fontSize: '1.5rem',
            fontWeight: '900',
            fontStyle: 'italic',
            color: 'rgba(255, 255, 255, 0.2)',
        },
    };

    return (
        <div style={{ paddingBottom: '3rem' }}>
            <h2 style={{ marginBottom: '2rem' }}>Histórico de Guerras</h2>

            <div style={{ marginBottom: '2rem', display: 'flex', justifyContent: 'center', gap: '1rem', alignItems: 'center' }}>
                <button onClick={fetchLogs} style={{ margin: 0 }}>
                    Buscar
                </button>
            </div>

            {loading && <div className="loading">Carregando Guerras...</div>}

            {error && !loading && (
                <div className="error">
                    <p>{error}</p>
                    <button onClick={fetchLogs} style={{ background: 'transparent', border: '1px solid currentColor', marginTop: '0.5rem' }}>
                        Tentar Novamente
                    </button>
                </div>
            )}

            {!loading && !error && (
                <div style={styles.container}>
                    {warLogs.map((log, index) => {
                        let result = log.resultado?.toLowerCase() || 'lose';

                        // Check for draw
                        if (log.estrelasClan === log.estrelasOponente) {
                            result = 'draw';
                        }

                        let resultText = 'Vitória';
                        if (result === 'lose') resultText = 'Derrota';
                        if (result === 'draw') resultText = 'Empate';

                        return (
                            <div
                                key={index}
                                style={styles.card(result)}
                                onMouseEnter={(e) => {
                                    e.currentTarget.style.transform = 'translateY(-5px)';
                                    e.currentTarget.style.boxShadow = '0 12px 40px rgba(0,0,0,0.5)';
                                }}
                                onMouseLeave={(e) => {
                                    e.currentTarget.style.transform = 'none';
                                    e.currentTarget.style.boxShadow = '0 8px 32px rgba(0, 0, 0, 0.3)';
                                }}
                            >
                                <div style={styles.header}>
                                    <span style={styles.badge(result)}>
                                        {resultText}
                                    </span>
                                    <div style={styles.dateContainer}>
                                        <span>Início: {formatDate(log.inicioGuerra)}</span>
                                        <span>Fim: {formatDate(log.fimGuerra)}</span>
                                    </div>
                                </div>

                                <div style={styles.versusContainer}>
                                    {/* Ally Clan */}
                                    <div style={styles.clanInfo}>
                                        <span style={styles.clanName}>{log.clanNome}</span>
                                        <div style={styles.stars}>
                                            <span>★</span> {log.estrelasClan}
                                        </div>
                                    </div>

                                    <div style={styles.vs}>VS</div>

                                    {/* Opponent Clan */}
                                    <div style={styles.clanInfo}>
                                        <span style={styles.clanName}>{log.oponenteNome}</span>
                                        <div style={styles.stars}>
                                            <span>★</span> {log.estrelasOponente}
                                        </div>
                                    </div>
                                </div>
                            </div>
                        );
                    })}

                    {warLogs.length === 0 && (
                        <div style={{ gridColumn: '1 / -1', textAlign: 'center', padding: '2rem', color: 'rgba(255,255,255,0.5)' }}>
                            Nenhum registro de guerra encontrado.
                        </div>
                    )}
                </div>
            )}
        </div>
    );
};

export default WarLogPage;
