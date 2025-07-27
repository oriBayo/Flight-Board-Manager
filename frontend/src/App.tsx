import FlightForm from './components/FlightForm';
import FlightList from './components/FlightList';
import FlightSearch from './components/FlightSearch';

function App() {
  return (
    <div className='py-10 px-20 min-h-screen bg-gray-100'>
      <h1 className='text-5xl font-bold text-gray-800 mb-8 text-center'>
        Real-Time Flight Board
      </h1>

      <FlightForm />
      <FlightSearch />
      <FlightList />
    </div>
  );
}

export default App;
