import { Toaster } from 'react-hot-toast';
import FlightForm from './components/FlightForm';
import FlightTable from './components/FlightTable';
import FlightSearch from './components/FlightSearch';
import { useFlightBoardHub } from './hooks/useFlightBoardHub';
import { Images } from './constants/images';

function App() {
  useFlightBoardHub();

  return (
    <div className='pb-10 pt-1 px-4 sm:px-8 lg:px-20 min-h-screen bg-gray-200'>
      <div className='max-w-6xl mx-auto'>
        <Toaster position='top-right' />
        <img
          src={Images.LOGO}
          alt='logo'
          className='mx-auto w-64 max-w-full object-contain'
        />
        <main className='space-y-8'>
          <FlightForm />
          <FlightSearch />
          <FlightTable />
        </main>
      </div>
    </div>
  );
}

export default App;
