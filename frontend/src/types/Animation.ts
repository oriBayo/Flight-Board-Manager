import { ReactNode } from 'react';

export interface FadeSlideInProps {
  children: ReactNode;
  needToAnimate: boolean;
}

export interface StatusBadgeProps {
  children: string;
  needToAnimate: boolean;
  className: string;
}
