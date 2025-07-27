import { Flight } from '../types/Flight';
import dayjs from 'dayjs';

const FlightDetails = (flight: Flight) => {
  const formattedDepartureTime = dayjs(flight.departureTime).format('HH:mm');
  return (
    <tr
      key={flight.id}
      className='bg-white border-b hover:bg-gray-50 text-center'
    >
      <td className='px-6 py-4'>{flight.flightNumber}</td>
      <td className='px-6 py-4'>{flight.destination}</td>
      <td className='px-6 py-4'>{formattedDepartureTime}</td>
      <td className='px-6 py-4'>{flight.gate}</td>
      <td className='px-6 py-4'>
        <span
          className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${
            flight.statusString === 'Scheduled'
              ? 'bg-green-100 text-green-800'
              : flight.statusString === 'Boarding'
              ? 'bg-yellow-100 text-yellow-800'
              : flight.statusString === 'Departed'
              ? 'bg-blue-100 text-blue-800'
              : 'bg-gray-100 text-gray-800'
          }`}
        >
          {flight.statusString}
        </span>
      </td>
      <td className='px-6 py-4'>
        <button className='text-white bg-red-500 p-[0.5rem] rounded-lg'>
          DELETE
        </button>
      </td>
    </tr>
  );
};

export default FlightDetails;
