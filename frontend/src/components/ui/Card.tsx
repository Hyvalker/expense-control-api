import React from 'react';

interface CardProps {
    title: string;
    value: string | number;
    color?: 'green' | 'red' | 'blue';
}

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