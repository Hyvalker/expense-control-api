import React from 'react';

interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {
    label: string;
}

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