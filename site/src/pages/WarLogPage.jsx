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
            // Ensure data is an array, if API returns a single object wrapper or something else, handle it.
            // Assuming API returns an array of objects as described.
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
        card: (result) => ({
            background: 'rgba(22, 27, 34, 0.6)',
            backdropFilter: 'blur(12px)',
            borderRadius: '16px',
            border: `1px solid ${result === 'lose' ? '#ff6b6b' : '#4cc9f0'}`,
            padding: '1.5rem',
            display: 'flex',
            flexDirection: 'column',
            gap: '1rem',
            boxShadow: '0 8px 32px rgba(0, 0, 0, 0.3)',
            transition: 'transform 0.2s ease, box-shadow 0.2s ease',
            position: 'relative',
            overflow: 'hidden',
        }),
        header: {
            display: 'flex',
            justifyContent: 'space-between',
            alignItems: 'center',
            marginBottom: '0.5rem',
        },
        badge: (result) => ({
            padding: '0.25rem 0.75rem',
            borderRadius: '20px',
            fontSize: '0.75rem',
            fontWeight: '800',
            textTransform: 'uppercase',
            backgroundColor: result === 'lose' ? 'rgba(255, 107, 107, 0.2)' : 'rgba(76, 201, 240, 0.2)',
            color: result === 'lose' ? '#ff6b6b' : '#4cc9f0',
            border: `1px solid ${result === 'lose' ? '#ff6b6b' : '#4cc9f0'}`,
        }),
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
        idLabel: {
            fontSize: '0.7rem',
            color: 'rgba(255, 255, 255, 0.4)',
            marginTop: 'auto',
            alignSelf: 'flex-end',
        }
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
                        // Normalize result string just in case
                        const result = log.resultado?.toLowerCase() || 'lose';

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
                                        {result === 'win' ? 'Vitória' : 'Derrota'}
                                    </span>
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
