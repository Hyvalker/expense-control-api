import React from 'react';

/**
 * Proprieadade para o componente Button
 */
interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> { 
    /** Define o estilo visual do botão. O padrão é 'primary'. */
    variant?: 'primary' | 'danger';
}

/**
 * Componente de botão reutilizável com variações de estilo.
 *
 * @param children - Conteúdo interno do botão (texto ou ícones). 
 * @param variant - O estilo visual do botão ('primary' ou 'danger').
 * @param props - Atributos nativos do elemento button (onClick, disabled, etc).
 */
export const Button: React.FC<ButtonProps> = ({ children, variant = 'primary', ...props }) => {
    const baseClasses = "py-2 px-4 rounded font-bold transation-all";
    const variants = {
        primary: "bg-blue-600 text-white hover:bg-blue-700",
        danger: "bg-red-500 text-white hover:bg-red-600",
    };
    return (
        <button className={`${baseClasses} ${variants[variant]}`} {...props}>
            {children}
        </button>
    )
}