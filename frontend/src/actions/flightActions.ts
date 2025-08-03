import axios from 'axios';
import { CreateFlightDto } from '../types/Flight';

axios.defaults.baseURL = 'http://localhost:5140/api/Flights';

export const fetchFlights = async () => {
  const { data: flights } = await axios.get('');
  return flights;
};

export const createFlight = async (flight: CreateFlightDto) => {
  const response = await axios.post('', flight);
  return response.data;
};

export const deleteFlight = async (id: string) => {
  const response = await axios.delete(`/${id}`);
  if (response.status === 404) {
    console.error(response.data);
  }
};

export const searchFlight = async (params: {
  status?: string;
  destination?: string;
}) => {
  const response = await axios.get('/search', {
    params,
  });
  console.log(response.data);
  return response.data;
};
