import { useSelector } from 'react-redux';
import FlightDetails from './FlightRow';
import { selectFlightSearch } from '../store/slices/searchSlice';
import { useFetchFlights } from '../hooks/useFetchFlights';
import { Flight } from '../types/Flight';
import { useMemo } from 'react';
import { selectFlightEvents } from '../store/slices/eventsSlice';
import { Headers } from '../constants/table';
import { Images } from '../constants/images';
import Title from './ui/Title';

const FlightTable = () => {
  const { searchResults, searchIsActive } = useSelector(selectFlightSearch);
  const { data: flights, isError, isLoading, error } = useFetchFlights();

  const { created, updated } = useSelector(selectFlightEvents);

  const filteredFlights: Flight[] = useMemo(() => {
    const baseFlight = searchIsActive ? searchResults : flights;
    if (!baseFlight) return [];

    return [...baseFlight].sort(
      (a, b) => Date.parse(b.departureTime) - Date.parse(a.departureTime)
    );
  }, [searchIsActive, searchResults, flights]);

  if (isLoading) {
    return <div className='text-center text-gray-500'>Loading flights...</div>;
  }

  if (isError) {
    return (
      <div>
        <p className='text-center text-[#638196] text-5xl -mb-8 font-archivo'>
          {error instanceof Error ? error.message : ' Unknown error'}
        </p>
        <img
          src={Images.UNABLE_TO_LOAD_FLIGHTS_IMG}
          alt='Unable to Load Flights'
          className='mx-auto w-64 max-w-full object-contain'
        />
      </div>
    );
  }

  if (!filteredFlights || filteredFlights.length === 0) {
    return (
      <div className='flex flex-col items-center justify-center p-6 text-gray-600'>
        <h2 className='text-xl font-semibold mb-2'>No Flights Found</h2>
      </div>
    );
  }

  return (
    <div className='container'>
      <Title text='Flights' />
      <div className='overflow-x-auto shadow-md sm:rounded-lg'>
        <table className='w-full text-sm text-left '>
          <thead className='text-xs text-gray-700 uppercase bg-gray-50'>
            <tr>
              {Headers.map((header) => (
                <th key={header} scope='col' className='px-6 py-3 text-center'>
                  {header}
                </th>
              ))}
            </tr>
          </thead>
          <tbody>
            {filteredFlights.map((flight: Flight) => (
              <FlightDetails
                key={flight.id}
                flight={flight}
                isUpdated={updated.includes(flight.id)}
                isNew={created.includes(flight.id)}
              />
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default FlightTable;
