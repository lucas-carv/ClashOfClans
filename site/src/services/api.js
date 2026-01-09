
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

export const obterDesempenhoDeMembros = async (clanTag) => {
    try {
        const encodedTag = encodeURIComponent(clanTag);
        const response = await api.get(`/api/v1/membro/clanTag/${encodedTag}/desempenho`);
        return response.data;
    } catch (error) {
        console.error('Error fetching member performance:', error);
        throw error;
    }
};
