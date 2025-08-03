import { useDispatch, useSelector } from 'react-redux';
import {
  selectFlightSearch,
  resetSearch,
  setSearchResults,
  setSearchIsActive,
  setFields,
} from '../store/slices/searchSlice';
import { Search, Eraser } from 'lucide-react';
import { useSearchFlights } from '../hooks/useSearchFlights';
import Title from './ui/Title';
import FormField from './ui/FormField';
import Button from './ui/Button';

const options = [
  { value: '', label: 'All Status' },
  { value: 'Scheduled', label: 'Scheduled' },
  { value: 'Boarding', label: 'Boarding' },
  { value: 'Departed', label: 'Departed' },
  { value: 'Landed', label: 'Landed' },
];

const FlightSearch = () => {
  const dispatch = useDispatch();
  const { destination, status } = useSelector(selectFlightSearch);
  const { isFetching, refetch } = useSearchFlights({ status, destination });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const { data: flights } = await refetch();
    dispatch(setSearchResults(flights));
    dispatch(setSearchIsActive(true));
  };

  const handleResetSearch = async (e: React.FormEvent) => {
    e.preventDefault();
    dispatch(resetSearch());
  };

  const handleChangeFields = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ): void => {
    const { id, value } = e.target;
    dispatch(setFields({ id, value }));
  };

  return (
    <div className='container'>
      <Title text='Search' />
      <div className='card flex items-center space-x-4'>
        <select
          key='status'
          className='select-input'
          value={status}
          onChange={handleChangeFields}
        >
          {options.map((options) => (
            <option key={options.value} value={options.value}>
              {options.label}
            </option>
          ))}
        </select>

        <div className='w-[60%]'>
          <FormField
            id='destination'
            value={destination}
            onChange={handleChangeFields}
            placeholder='search flight...'
          />
        </div>
        <div>
          <Button
            onClick={handleSubmit}
            title={isFetching ? 'Searching...' : 'Search'}
            Icon={Search}
          />
        </div>
        {(status || destination) && (
          <div>
            <Button
              onClick={handleResetSearch}
              title='Clear Filters'
              Icon={Eraser}
            />
          </div>
        )}
      </div>
    </div>
  );
};

export default FlightSearch;
