import { useMutation, useQueryClient } from '@tanstack/react-query';
import { deleteFlight } from '../actions/flightActions';
import toast from 'react-hot-toast';
import { Flight } from '../types/Flight';

export const useDeleteFlight = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteFlight,
    onMutate: async (flightId: string) => {
      await queryClient.cancelQueries({ queryKey: ['flights'] });

      const previousFlights = queryClient.getQueryData<Flight[]>(['flights']);

      queryClient.setQueryData(['flights'], (old: Flight[] = []) =>
        old.filter((flight) => flight.id !== flightId)
      );

      return { previousFlights };
    },
    onSuccess: () => {
      toast.success('Flight deleted successfully!');
    },
    onError: (error, flightId, context) => {
      queryClient.setQueryData(['flights'], context?.previousFlights);
      toast.error('Could not delete.');
    },
    onSettled: () => {
      queryClient.invalidateQueries({ queryKey: ['flights'] });
    },
  });
};
