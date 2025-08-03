import { motion } from 'framer-motion';
import { StatusBadgeProps } from '../../../types/Animation';

const StatusBadge = ({
  children,
  needToAnimate,
  className,
}: StatusBadgeProps) => {
  return (
    <motion.span
      key={children}
      initial={needToAnimate ? { scale: 0.8, opacity: 0 } : false}
      animate={needToAnimate ? { scale: 1, opacity: 1 } : {}}
      transition={{ duration: 0.4, ease: 'easeOut' }}
      className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${className}`}
    >
      {children}
    </motion.span>
  );
};

export default StatusBadge;
