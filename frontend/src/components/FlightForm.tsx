import { useDispatch, useSelector } from 'react-redux';
import {
  updateField,
  setError,
  resetFrom,
  selectFlightForm,
} from '../store/slices/FormSlice';
import { useCreateFlight } from '../hooks/useCreateFlight';
import { CreateFlightDto } from '../types/Flight';
import { validateFlight } from '../utils/validation';

const FlightForm = () => {
  const dispatch = useDispatch();

  const { flightNumber, destination, gate, departureTime } =
    useSelector(selectFlightForm);

  const { mutateAsync, isPending } = useCreateFlight();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    dispatch(
      updateField({ field: name as keyof typeof selectFlightForm, value })
    );
  };
  const handleSubmit = async (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    dispatch(setError(''));

    const flightData: CreateFlightDto = {
      flightNumber,
      destination,
      gate,
      departureTime: departureTime,
    };

    const errors = validateFlight(flightData);
    if (errors.length > 0) {
      dispatch(setError(errors));
    }
    await mutateAsync(flightData);
    dispatch(resetFrom());
  };

  return (
    <div className='p-6 bg-gray-100 mx-auto rounded-lg shadow-md mb-8'>
      <h2 className='text-2xl font-bold mb-2 text-gray-800 '>Add New Flight</h2>
      <div className='bg-white shadow-md rounded-lg p-6'>
        <div className='space-y-4'>
          <div>
            <label
              htmlFor='flightNumber'
              className='block text-sm font-medium text-gray-700'
            >
              flight Number
            </label>
            <input
              type='text'
              id='flightNumber'
              name='flightNumber'
              value={flightNumber}
              onChange={handleChange}
              placeholder='example: LY123'
              className='mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm'
            />
          </div>
          <div>
            <label
              htmlFor='destination'
              className='block text-sm font-medium text-gray-700'
            >
              destination
            </label>
            <input
              type='text'
              id='destination'
              name='destination'
              value={destination}
              onChange={handleChange}
              placeholder='example: New York'
              className='mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm'
            />
          </div>
          <div>
            <label
              htmlFor='gate'
              className='block text-sm font-medium text-gray-700'
            >
              gate
            </label>
            <input
              type='text'
              id='gate'
              name='gate'
              value={gate}
              onChange={handleChange}
              placeholder='example: A12'
              className='mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm'
            />
          </div>
          <div>
            <label
              htmlFor='departureTime'
              className='block text-sm font-medium text-gray-700'
            >
              departure Time
            </label>
            <input
              type='datetime-local'
              id='departureTime'
              name='departureTime'
              value={departureTime}
              onChange={handleChange}
              className='mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm'
            />
          </div>
          <div>
            <h4 className='text-red-500'>error</h4>
          </div>
          <button
            type='submit'
            onClick={handleSubmit}
            className='w-full bg-indigo-600 text-white py-2 px-4 rounded-md hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500'
          >
            {isPending ? 'Loading...' : 'Add Flight'}
          </button>
        </div>
      </div>
    </div>
  );
};

export default FlightForm;
