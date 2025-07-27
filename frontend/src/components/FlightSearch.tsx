import { useDispatch, useSelector } from 'react-redux';
import {
  selectFlightSearch,
  setSubmitted,
  updateSearch,
  updateStatus,
  resetSearch,
} from '../store/slices/SearchSlice';

const options = [
  { value: '', label: 'All Status' },
  { value: 'Scheduled', label: 'Scheduled' },
  { value: 'Boarding', label: 'Boarding' },
  { value: 'Departed', label: 'Departed' },
  { value: 'Landed', label: 'Landed' },
];

const FlightSearch = () => {
  const dispatch = useDispatch();
  const { search, status, submitted } = useSelector(selectFlightSearch);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    dispatch(setSubmitted(true));
    dispatch(resetSearch());
  };

  return (
    <div className='mx-auto p-6 rounded-lg shadow-md mb-8'>
      <h1 className='text-2xl font-bold mb-4 text-gray-800'>Search</h1>
      <div className='bg-white shadow-md rounded-lg py-6 px-8'>
        <div className='flex items-center space-x-4'>
          <select
            className='px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500'
            value={status}
            onChange={(e) => dispatch(updateStatus(e.target.value))}
          >
            {options.map((options) => (
              <option key={options.value} value={options.value}>
                {options.label}
              </option>
            ))}
          </select>

          <div className='w-[50%]'>
            <input
              type='text'
              value={search}
              onChange={(e) => dispatch(updateSearch(e.target.value))}
              placeholder='search flight...'
              className=' mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm'
              required
            />
          </div>
          <div>
            <button
              onClick={handleSubmit}
              className='w-full bg-indigo-600 text-white py-2 px-4 rounded-md hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500'
            >
              {submitted ? 'Searching...' : 'Search'}
            </button>
          </div>
          {(status || search) && (
            <div>
              <button
                className='w-full bg-indigo-600 text-white py-2 px-4 rounded-md hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500'
                onClick={() => dispatch(resetSearch())}
              >
                Clear Filters
              </button>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default FlightSearch;
