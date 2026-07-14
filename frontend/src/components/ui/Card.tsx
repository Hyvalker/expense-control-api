import React from 'react';

/**
 * Propriedades para o componente Card.
 */
interface CardProps {
    /** Título ou label que aparece no topo do card. */
    title: string;
    /** O valor numérico ou textual a ser exibido em destaque. */
    value: string | number;
    /** Cor do texto do valor. O padrão é 'blue'.*/
    color?: 'green' | 'red' | 'blue';
}

/**
 * Componente de Card informativo para exibição de métricas e valores.
 * 
 * @param title - O cabeçalho do card que descreve a métrica.
 * @param value - O valor principal a ser exibido.
 * @param color - Define a cor do texto do valor para indicar status (ex: verde para positivo, vermelho para negativo).
 */
export const Card: React.FC<CardProps> = ({title, value, color = 'blue'}) => {
    const colors = {
        green: "text-green-600",
        red: "text-red-600",
        blue: "text-blue-900",
    };
    return (
        <div className="bg-white p-6 rounded-lg shadow-sm border border-gray-100">
            <h3 className="text-sm font-medium text-gray-500 uppercase">{title}</h3>
            <p className={`text-2xl font-bold mt-2 ${colors[color]}`}>{value}</p>
        </div>
    );
};