
import axios from 'axios';

const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
    timeout: 60000,
    headers: {
        'Content-Type': 'application/json',
    },
});


export const obterResumoDeMembros = async (clanTag) => {
    try {
        const encodedTag = encodeURIComponent(clanTag);
        const response = await api.get(`/api/v1/membro/clanTag/${encodedTag}/resumo`);
        return response.data;
    } catch (error) {
        console.error('Error fetching clans:', error);
        throw error;
    }
};

export const obterDesempenhoDeMembros = async (clanTag, quantidadeGuerras) => {
    try {
        const encodedTag = encodeURIComponent(clanTag);
        const response = await api.get(`/api/v1/membro/clanTag/${encodedTag}/desempenho?quantidadeGuerras=${quantidadeGuerras}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching member performance:', error);
        throw error;
    }
};

export const obterLogsDeGuerra = async (clanTag) => {
    try {
        const encodedTag = encodeURIComponent(clanTag);
        const response = await api.get(`/api/v1/guerra/log/clanTag/${encodedTag}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching war logs:', error);
        throw error;
    }
};

export const obterDetalhesDaGuerra = async (clanTag, oponenteTag) => {
    try {
        const encodedClanTag = encodeURIComponent(clanTag);
        const encodedOponenteTag = encodeURIComponent(oponenteTag);
        const response = await api.get(`/api/v1/guerra/detalhe/clan-tag/${encodedClanTag}/oponente-tag/${encodedOponenteTag}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching war details:', error);
        throw error;
    }
};
