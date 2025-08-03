import { motion } from 'framer-motion';
import { FadeSlideInProps } from '../../../types/Animation';

const FadeSlideInComp = ({ children, needToAnimate }: FadeSlideInProps) => {
  return (
    <motion.tr
      initial={needToAnimate ? { opacity: 0, y: -20 } : false}
      animate={{ opacity: 1, y: 0 }}
      exit={{ opacity: 0, y: -20 }}
      transition={{ duration: 2 }}
      className='bg-white border-b hover:bg-blue-50 transition-colors duration-500 text-center'
    >
      {children}
    </motion.tr>
  );
};

export default FadeSlideInComp;
