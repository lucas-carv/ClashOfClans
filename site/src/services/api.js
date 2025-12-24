
import axios from 'axios';

const api = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL,
    timeout: 60000,
    headers: {
        'Content-Type': 'application/json',
    },
});

export const getClans = async () => {
    try {
        const response = await api.get('/membro/clanTag/%232L0UC9R8P');
        return response.data;
    } catch (error) {
        console.error('Error fetching clans:', error);
        throw error;
    }
};

export const getMemberPerformance = async (clanTag, qtdMinimoGuerras, qtdMaximoGuerras) => {
    try {
        // Ensure the tag is URL encoded properly if it isn't already
        const encodedTag = encodeURIComponent(clanTag);
        const response = await api.get(`/membro/clanTag/${encodedTag}/desempenho?minimoGuerras=${qtdMinimoGuerras}&maximoGuerras=${qtdMaximoGuerras}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching member performance:', error);
        throw error;
    }
};
