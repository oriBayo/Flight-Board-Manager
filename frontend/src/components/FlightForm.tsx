import { useDispatch, useSelector } from 'react-redux';
import {
  updateField,
  setError,
  resetForm,
  selectFlightForm,
} from '../store/slices/formSlice';
import { useCreateFlight } from '../hooks/useCreateFlight';
import { CreateFlightDto, FlightFormField } from '../types/Flight';
import { validateFlight } from '../utils/validation';
import FormField from './ui/FormField';
import Title from './ui/Title';
import Button from './ui/Button';

const FlightForm = () => {
  const dispatch = useDispatch();

  const { flightNumber, destination, gate, departureTime, errors } =
    useSelector(selectFlightForm);

  const { mutateAsync: createFlight, isPending: createPending } =
    useCreateFlight();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    dispatch(updateField({ name: name as FlightFormField, value }));
  };
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const flightData: CreateFlightDto = {
      flightNumber,
      destination,
      gate,
      departureTime,
    };

    const errors = validateFlight(flightData);
    if (Object.keys(errors).length > 0) {
      dispatch(setError(errors));
      return;
    }
    try {
      await createFlight(flightData);
    } catch (err) {
      console.error('Create flight failed:', err);
    } finally {
      dispatch(resetForm());
    }
  };

  return (
    <div className='container'>
      <Title text='New Flight' />
      <div className='card space-y-4'>
        <FormField
          id='flightNumber'
          label='Flight Number'
          value={flightNumber}
          onChange={handleChange}
          placeholder='Example: LY123'
          error={errors.flightNumber}
        />
        <FormField
          id='destination'
          label='Destination'
          value={destination}
          onChange={handleChange}
          placeholder='Example: New York'
          error={errors.destination}
        />
        <FormField
          id='gate'
          label='Gate'
          value={gate}
          onChange={handleChange}
          placeholder='Example: A12'
          error={errors.gate}
        />
        <FormField
          id='departureTime'
          label='Departure Time'
          value={departureTime}
          onChange={handleChange}
          type='datetime-local'
          error={errors.departureTime}
        />

        <Button
          onClick={handleSubmit}
          title={createPending ? 'Loading...' : 'Add Flight'}
        />
      </div>
    </div>
  );
};

export default FlightForm;
