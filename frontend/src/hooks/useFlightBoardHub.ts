import { HubConnectionBuilder } from '@microsoft/signalr';
import { useQueryClient } from '@tanstack/react-query';
import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import {
  flightCreated,
  flightDeleted,
  flightUpdated,
} from '../store/slices/eventsSlice';
import { Flight } from '../types/Flight';
import { API_CONFIG } from '../config/api';

export const useFlightBoardHub = () => {
  const dispatch = useDispatch();
  const queryClient = useQueryClient();
  useEffect(() => {
    const connection = new HubConnectionBuilder()
      .withUrl(API_CONFIG.signalRHub, {
        withCredentials: true,
      })
      .withAutomaticReconnect()
      .build();

    const startConnection = async () => {
      try {
        await connection.start();
        console.log('Connected to FlightBoardHub');

        connection.on('FlightCreated', (flight: Flight) => {
          dispatch(flightCreated(flight.id));
          console.log('Flight created:', flight);
          queryClient.invalidateQueries({ queryKey: ['flights'] });
        });

        connection.on('FlightDeleted', (flightId) => {
          console.log('Flight deleted:', flightId);
          dispatch(flightDeleted(flightId));
          queryClient.invalidateQueries({ queryKey: ['flights'] });
        });

        connection.on('FlightUpdated', (flight) => {
          console.log('Flight status updated:', flight);
          dispatch(flightUpdated(flight));
          queryClient.setQueryData(['flights'], (old: Flight[]) =>
            old.map((f) => (f.id === flight.id ? flight : f))
          );
        });
      } catch (error) {
        console.error('SignalR connection error:', error);
      }
    };

    startConnection();

    return () => {
      if (connection) {
        connection.off('FlightCreated');
        connection.off('FlightDeleted');
        connection
          .stop()
          .then(() => console.log('SignalR connection stopped'))
          .catch((err) => console.error('Error stopping connection:', err));
      }
    };
  }, [dispatch, queryClient]);

  return null;
};
