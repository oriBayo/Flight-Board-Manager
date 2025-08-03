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
    updateField: (state, action) => {
      const { name, value } = action.payload;
      if (name in state) {
        (state as any)[name] = value;
      }
    },
    setError: (state, action) => {
      state.errors = action.payload;
    },
    setSubmitted: (state, action) => {
      state.isSubmitted = action.payload;
    },
    resetForm: (state) => {
      return initialState;
    },
  },
});

export const { updateField, setSubmitted, setError, resetForm } =
  FormSlice.actions;

export const selectFlightForm = (state: RootState): FlightFormState =>
  state.form;

export default FormSlice.reducer;
