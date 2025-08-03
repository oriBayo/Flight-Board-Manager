import { useDeleteFlight } from '../hooks/useDeleteFlight';
import { flightDetailsProps } from '../types/Flight';
import { ClipLoader } from 'react-spinners';
import { Trash2 } from 'lucide-react';
import FadeSlideIn from './ui/animations/FadeSlideIn';
import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import {
  flightCreationHandled,
  flightUpdateHandled,
} from '../store/slices/eventsSlice';
import StatusBadge from './ui/animations/StatusBadge';
import { formattedDateToTime } from '../utils/time';
import { getFlightStatusClass } from '../utils/flightUtils';

const FlightDetails = ({ flight, isNew, isUpdated }: flightDetailsProps) => {
  const dispatch = useDispatch();

  const formattedDepartureTime = formattedDateToTime(flight.departureTime);
  const StatusClass = getFlightStatusClass(flight.statusString);

  const { mutate: deleteFlight, isPending: deletePending } = useDeleteFlight();

  const handleDelete = async (id: string) => {
    await deleteFlight(id);
  };

  useEffect(() => {
    if (isUpdated || isNew) {
      const timer = setTimeout(() => {
        isNew && dispatch(flightCreationHandled(flight.id));
        isUpdated && dispatch(flightUpdateHandled(flight.id));
      }, 3000);
      return () => clearTimeout(timer);
    }
  }, [isUpdated, dispatch, flight.id, isNew]);

  return (
    <FadeSlideIn needToAnimate={isNew}>
      <td>{flight.flightNumber}</td>
      <td>{flight.destination}</td>
      <td>{formattedDepartureTime}</td>
      <td>{flight.gate}</td>
      <td className='px-6 py-4'>
        <StatusBadge needToAnimate={isUpdated} className={StatusClass}>
          {flight.statusString}
        </StatusBadge>
      </td>
      <td>
        <button
          onClick={() => handleDelete(flight.id)}
          className='text-indigo-600 hover:text-indigo-800'
        >
          {deletePending ? <ClipLoader size={35} /> : <Trash2 size={16} />}
        </button>
      </td>
    </FadeSlideIn>
  );
};

export default FlightDetails;
