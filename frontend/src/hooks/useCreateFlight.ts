import { useQueryClient, useMutation } from '@tanstack/react-query';
import { createFlight } from '../actions/flightActions';
import toast from 'react-hot-toast';
import axios from 'axios';

export function useCreateFlight() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationKey: ['createFlight'],
    mutationFn: createFlight,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['flights'] });
      toast.success('Flight created!');
    },
    onError: (error: unknown) => {
      if (axios.isAxiosError(error)) {
        const status = error.response?.status;
        const data = error.response?.data;

        if (status === 400 && Array.isArray(data?.errors)) {
          data.errors.forEach(
            (err: { propertyName: string; errorMessage: string }) => {
              toast.error(err.errorMessage);
            }
          );
        } else {
          toast.error(data?.title || error.message || 'Request failed');
        }
      } else {
        toast.error('An unexpected error occurred.');
      }
    },
  });
}
