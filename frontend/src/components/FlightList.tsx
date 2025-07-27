import { useSelector } from 'react-redux';
import FlightDetails from './FlightDetails';
import { selectFlightSearch } from '../store/slices/SearchSlice';
import { useFlights } from '../hooks/useFlights';
import { Flight } from '../types/Flight';

const TableHeaders = [
  'flight Number',
  'destination',
  'departureTime',
  'gate',
  'status',
  'action',
];

const FlightList = () => {
  const { status } = useSelector(selectFlightSearch);
  const { data: flights, isError, isLoading, error } = useFlights();

  const filteredFlights: Flight[] = status
    ? (flights as Flight[]).filter(
        (flight: Flight) => flight.statusString === status
      )
    : (flights as Flight[]);

  if (isLoading) {
    return <div className='text-center text-gray-500'>Loading flights...</div>;
  }

  if (isError) {
    console.log(error);
    return (
      <div className='text-center text-red-500'>Error loading flights.</div>
    );
  }

  return (
    <div className='mx-auto p-6 rounded-lg shadow-md'>
      <h1 className='text-2xl font-bold mb-4 text-gray-800 '>Flight List</h1>
      <div className='overflow-x-auto shadow-md sm:rounded-lg'>
        <table className='w-full text-sm text-left '>
          <thead className='text-xs text-gray-700 uppercase bg-gray-50'>
            <tr>
              {TableHeaders.map((header) => (
                <th key={header} scope='col' className='px-6 py-3 text-center'>
                  {header}
                </th>
              ))}
            </tr>
          </thead>
          <tbody>
            {filteredFlights.map((flight) => (
              <FlightDetails key={flight.id} {...flight} />
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default FlightList;
