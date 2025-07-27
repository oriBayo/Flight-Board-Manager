import { createSlice } from '@reduxjs/toolkit';
import { FlightSearchState } from '../../types/Flight';
import type { RootState } from '../store';

const initialState: FlightSearchState = {
  search: '',
  status: '',
  submitted: false,
};

const filterSlice = createSlice({
  name: 'flightSearch',
  initialState,
  reducers: {
    updateSearch: (state, action) => {
      state.search = action.payload;
    },
    updateStatus: (state, action) => {
      state.status = action.payload;
    },
    setSubmitted: (state, action) => {
      state.submitted = action.payload;
    },
    resetSearch: (state) => {
      return initialState;
    },
  },
});

export const { updateSearch, updateStatus, setSubmitted, resetSearch } =
  filterSlice.actions;

export const selectFlightSearch = (state: RootState): FlightSearchState =>
  state.search;

export default filterSlice.reducer;
