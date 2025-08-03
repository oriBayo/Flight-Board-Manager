import axios from 'axios';
import { CreateFlightDto } from '../types/Flight';
import { API_CONFIG } from '../config/api';

axios.defaults.baseURL = `${API_CONFIG.baseURL}/Flights`;

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
