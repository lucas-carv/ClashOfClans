import React, { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import { obterDetalhesDaGuerra } from '../services/api';

const WarDetailsPage = () => {
    const { clanTag, oponenteTag } = useParams();
    const [warDetails, setWarDetails] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchDetails = async () => {
            setLoading(true);
            try {
                const decodedClanTag = decodeURIComponent(clanTag);
                const decodedOponenteTag = decodeURIComponent(oponenteTag);
                const data = await obterDetalhesDaGuerra(decodedClanTag, decodedOponenteTag);
                setWarDetails(data);
            } catch (err) {
                console.error(err);
                setError('Falha ao carregar detalhes da guerra.');
            } finally {
                setLoading(false);
            }
        };

        fetchDetails();
    }, [clanTag, oponenteTag]);

    const styles = {
        container: {
            padding: '2rem 1rem',
            maxWidth: '1200px',
            margin: '0 auto',
            color: '#fff',
            paddingBottom: '4rem',
        },
        header: {
            textAlign: 'center',
            marginBottom: '2rem',
            background: 'rgba(22, 27, 34, 0.6)',
            padding: '1.5rem',
            borderRadius: '16px',
            border: '1px solid rgba(255, 255, 255, 0.1)',
            backdropFilter: 'blur(10px)',
        },
        matchup: {
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
            gap: '2rem',
            marginBottom: '1rem',
            flexWrap: 'wrap',
        },
        clanName: {
            fontSize: '1.5rem',
            fontWeight: 'bold',
            color: '#fff',
        },
        vs: {
            fontSize: '2rem',
            fontWeight: '900',
            fontStyle: 'italic',
            color: 'rgba(255,255,255,0.3)',
        },
        columns: {
            display: 'grid',
            gridTemplateColumns: 'repeat(auto-fit, minmax(350px, 1fr))',
            gap: '2rem',
        },
        column: {
            background: 'rgba(22, 27, 34, 0.4)',
            borderRadius: '16px',
            padding: '1.5rem',
            border: '1px solid rgba(255, 255, 255, 0.05)',
        },
        columnHeader: {
            textAlign: 'center',
            marginBottom: '1.5rem',
            paddingBottom: '1rem',
            borderBottom: '1px solid rgba(255, 255, 255, 0.1)',
            fontSize: '1.2rem',
            fontWeight: 'bold',
        },
        memberList: {
            display: 'flex',
            flexDirection: 'column',
            gap: '1rem',
        },
        memberCard: {
            background: 'rgba(0, 0, 0, 0.3)',
            borderRadius: '8px',
            padding: '1rem',
            border: '1px solid rgba(255, 255, 255, 0.05)',
        },
        memberHeader: {
            display: 'flex',
            justifyContent: 'space-between',
            marginBottom: '0.5rem',
            fontSize: '0.9rem',
        },
        attacks: {
            marginTop: '0.5rem',
            paddingTop: '0.5rem',
            borderTop: '1px dashed rgba(255, 255, 255, 0.1)',
        },
        attackRow: {
            display: 'flex',
            justifyContent: 'space-between',
            fontSize: '0.8rem',
            color: 'rgba(255, 255, 255, 0.7)',
            marginBottom: '0.2rem',
        },
        stars: {
            color: '#fccb44',
        },
        backLink: {
            display: 'inline-block',
            marginBottom: '2rem',
            color: '#4cc9f0',
            textDecoration: 'none',
            fontSize: '0.9rem',
        }
    };

    if (loading) return <div className="loading">Carregando detalhes...</div>;
    if (error) return <div className="error">{error}</div>;
    if (!warDetails) return <div className="error">Detalhes não encontrados.</div>;

    return (
        <div style={styles.container}>
            <Link to="/logs" style={styles.backLink}>← Voltar para Logs</Link>

            <div style={styles.header}>
                <div style={styles.matchup}>
                    {warDetails.clansEmGuerra.map((clan, index) => (
                        <React.Fragment key={clan.tag}>
                            <div style={styles.clanName}>{clan.nome}</div>
                            {index === 0 && <div style={styles.vs}>VS</div>}
                        </React.Fragment>
                    ))}
                </div>
                <div style={{ fontSize: '0.9rem', color: 'rgba(255,255,255,0.6)' }}>
                    Início: {new Date(warDetails.inicioGuerra).toLocaleString()} •
                    Fim: {new Date(warDetails.fimGuerra).toLocaleString()}
                </div>
            </div>

            <div style={styles.columns}>
                {warDetails.clansEmGuerra.map(clan => (
                    <div key={clan.tag} style={styles.column}>
                        <div style={styles.columnHeader}>{clan.nome}</div>
                        <div style={styles.memberList}>
                            {clan.membrosEmGuerra.map(membro => (
                                <div key={membro.tag} style={styles.memberCard}>
                                    <div style={styles.memberHeader}>
                                        <span style={{ fontWeight: 'bold' }}>{membro.posicaoMapa}. {membro.nome}</span>
                                        <span style={{ color: 'rgba(255,255,255,0.5)' }}>CV {membro.centroVilaLevel}</span>
                                    </div>

                                    {membro.ataques && membro.ataques.length > 0 && (
                                        <div style={styles.attacks}>
                                            <div style={{ fontSize: '0.75rem', marginBottom: '0.25rem', color: 'rgba(255,255,255,0.4)' }}>ATAQUES:</div>
                                            {membro.ataques.map((ataque, idx) => (
                                                <div key={idx} style={styles.attackRow}>
                                                    <span>{ataque.posicaoMapa} - {ataque.nomeDefensor}</span>
                                                    <span>
                                                        <span style={styles.stars}>{'★'.repeat(ataque.estrelas)}</span> {ataque.percentualDestruicao}%
                                                    </span>
                                                </div>
                                            ))}
                                        </div>
                                    )}
                                </div>
                            ))}
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default WarDetailsPage;
