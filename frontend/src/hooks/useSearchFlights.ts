import { useQuery } from '@tanstack/react-query';
import { searchFlight } from '../actions/flightActions';

export const useSearchFlights = (params: {
  status?: string;
  destination?: string;
}) => {
  return useQuery({
    queryKey: ['searchFlights', params],
    queryFn: () => searchFlight(params),
    enabled: false,
  });
};
