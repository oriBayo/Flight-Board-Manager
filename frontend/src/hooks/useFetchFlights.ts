import { useQuery } from '@tanstack/react-query';
import { fetchFlights } from '../actions/flightActions';

export function useFetchFlights() {
  return useQuery({
    queryKey: ['flights'],
    queryFn: fetchFlights,
    retry: false,
  });
}
