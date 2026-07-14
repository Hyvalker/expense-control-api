import axios from 'axios';

export const api = axios.create({
    baseURL: 'http://localhost:5117/api',
    headers: {
        'Content-Type': 'application/json',
    },
});
api.interceptors.response.use(
    (response) => {
        return response;
    },
    
    (error) => {
        
        return Promise.reject(error);
    }
);