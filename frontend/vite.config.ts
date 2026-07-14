import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import tailwindcss from '@tailwindcss/vite';

/**
 * Configuração do Vite.
 * Define os plugins utilizados, como React e Tailwind, e a configuração e proxy para redirecionar
 * chamadas da API para o servidor backend.
 */
export default defineConfig({
  plugins: [react(), tailwindcss()],
  server: {
    // Proxy para evitar erros de CORS durante o desenvolvimento, direcionando requisições que iniciam com '/api'
    // para o servidor backend.
    proxy: {
      '/api': {
        target: 'http://localhost:5117',
        changeOrigin: true,
      }
    }
  }
});