import React from 'react';

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> { 
    variant?: 'primary' | 'danger';
}

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