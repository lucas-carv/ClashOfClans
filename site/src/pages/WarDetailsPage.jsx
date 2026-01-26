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

    if (loading) return <div className="loading">Carregando detalhes...</div>;
    if (error) return <div className="error">{error}</div>;
    if (!warDetails) return <div className="error">Detalhes não encontrados.</div>;

    const styles = {
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
        }
    };

    return (
        <div className="war-details-container">
            <Link to="/logs" className="war-back-link">← Voltar para Logs</Link>

            <div className="war-header-card">
                <div className="war-matchup">
                    {warDetails.clansEmGuerra.map((clan, index) => (
                        <React.Fragment key={clan.tag}>
                            <div className="war-clan-name">{clan.nome}</div>
                            {index === 0 && <div className="war-vs-badge">VS</div>}
                        </React.Fragment>
                    ))}
                </div>
                <div style={{ fontSize: '0.9rem', color: 'rgba(255,255,255,0.6)' }}>
                    Início: {new Date(warDetails.inicioGuerra).toLocaleString()} <br />
                    Fim: {new Date(warDetails.fimGuerra).toLocaleString()}
                </div>
            </div>

            <div className="war-columns">
                {warDetails.clansEmGuerra.map(clan => (
                    <div key={clan.tag} className="war-column">
                        <div className="war-column-header">{clan.nome}</div>
                        <div className="war-member-list">
                            {clan.membrosEmGuerra.map(membro => (
                                <div key={membro.tag} className="war-member-card">
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
