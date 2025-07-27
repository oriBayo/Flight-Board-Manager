import axios from 'axios';
import { CreateFlightDto } from '../types/Flight';

axios.defaults.baseURL = 'http://localhost:5140';

export const fetchFlights = async () => {
  const { data: flights } = await axios.get('/api/Flights');
  return flights;
};

export const createFlight = async (flight: CreateFlightDto) => {
  const { data: newFlight } = await axios.post('/api/Flights', flight);
  return newFlight;
};
