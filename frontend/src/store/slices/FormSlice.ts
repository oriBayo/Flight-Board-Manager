import { createSlice } from '@reduxjs/toolkit';
import { FlightFormState } from '../../types/Flight';
import { RootState } from '../store';

const initialState: FlightFormState = {
  flightNumber: '',
  destination: '',
  gate: '',
  departureTime: '',
  isSubmitted: false,
  errors: {},
};

const FormSlice = createSlice({
  name: 'flightForm',
  initialState,
  reducers: {
    updateField: (
      state,
      action: {
        payload: {
          field: keyof FlightFormState;
          value: FlightFormState[keyof FlightFormState];
        };
      }
    ) => {
      const { field, value } = action.payload;
      if (field in state) {
        (state as any)[field] = value;
      }
    },
    setError: (state, action) => {
      state.errors = action.payload;
    },
    setSubmitted: (state, action) => {
      state.isSubmitted = action.payload;
    },
    resetFrom: (state) => {
      return initialState;
    },
  },
});

export const { updateField, setSubmitted, setError, resetFrom } =
  FormSlice.actions;

export const selectFlightForm = (state: RootState): FlightFormState =>
  state.form;

export default FormSlice.reducer;
