import { LucideProps } from 'lucide-react';
import React from 'react';

interface ButtonProps {
  Icon?: React.ForwardRefExoticComponent<
    Omit<LucideProps, 'ref'> & React.RefAttributes<SVGSVGElement>
  >;
  title: string;
  onClick: (e: React.FormEvent) => Promise<void>;
}

const Button = ({ Icon, title, onClick }: ButtonProps) => {
  return (
    <button
      className={`btn ${Icon && 'flex items-center gap-2'}`}
      onClick={onClick}
    >
      {Icon && <Icon size={18} />}
      {title}
    </button>
  );
};

export default Button;
