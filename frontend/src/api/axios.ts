import axios from 'axios';

/**
 * Instância confugarada do Axios para comunicação com a API do backend.
 * Define a URL base e cabeçalhos padrão para todas as requisições.
 */
export const api = axios.create({
    baseURL: 'http://localhost:5117/api',
    headers: {
        'Content-Type': 'application/json',
    },
});

/**
 * Interceptador de respostas da API.
 * Gerencia o fluxo de sucesso e tratamento centralizado de erros.
 */
api.interceptors.response.use(
    (response) => {
        // Retorna a resposta original em caso de sucesso.
        return response;
    },
    
    (error) => {
        // Intercepta e rejeita promessas de erro para serem tratadas nos componentes ou serviços.
        return Promise.reject(error);
    }
);