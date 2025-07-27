import { useQueryClient, useMutation } from '@tanstack/react-query';
import { createFlight } from '../actions/flightActions';

export function useCreateFlight() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationKey: ['createFlight'],
    mutationFn: createFlight,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['flights'] });
    },
  });
}
