import React from 'react';

/**
 * Propriedade para o componente Input.
 * Estende os atributos nativos de um elemento input HTML.
 */
interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {
    /** O texto da label que aparece acima do campo de entrada. */
    label: string;
}

/**
 * Componente de Input de texto estilizado e reutilizável.
 * 
 * @param label - Rótulo desctitivo para o campo.
 * @param props - Atributos nativos do input HTML (value, onChange, placeholder, type, etc).
 */
export const Input: React.FC<InputProps> = ({ label, ...props }) => {
    return (
        <div className="flex flex-col gap-1">
            <label className="text-sm font-semibold text-gray-700">{label}</label>
            <input
                className="border border-gray-300 rounded p-2 focus:ring-2 focus:ring-blue-500 outline-none"
                {...props}
            />
        </div>
    );
};