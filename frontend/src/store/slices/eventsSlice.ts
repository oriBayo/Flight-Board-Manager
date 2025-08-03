import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { FlightEventsState } from '../../types/Flight';
import { RootState } from '../store';

const initialState: FlightEventsState = {
  created: [],
  updated: [],
  deleted: [],
};

const eventsSlice = createSlice({
  name: 'FlightEvents',
  initialState,
  reducers: {
    flightCreated: (state, action: PayloadAction<string>) => {
      state.created.push(action.payload);
    },
    flightUpdated: (state, action: PayloadAction<string>) => {
      state.updated.push(action.payload);
    },
    flightDeleted: (state, action: PayloadAction<string>) => {
      state.deleted.push(action.payload);
    },
    clearRealtimeEvents: () => initialState,
    flightCreationHandled: (state, action: PayloadAction<string>) => {
      state.created = state.created.filter((id) => id !== action.payload);
    },
    flightUpdateHandled: (state, action: PayloadAction<string>) => {
      state.updated = state.updated.filter((id) => id !== action.payload);
    },
    flightDeletionHandled: (state, action: PayloadAction<string>) => {
      state.deleted = state.deleted.filter((id) => id !== action.payload);
    },
  },
});

export const {
  flightCreated,
  flightUpdated,
  flightDeleted,
  clearRealtimeEvents,
  flightCreationHandled,
  flightDeletionHandled,
  flightUpdateHandled,
} = eventsSlice.actions;

export const selectFlightEvents = (state: RootState): FlightEventsState =>
  state.events;

export default eventsSlice.reducer;
