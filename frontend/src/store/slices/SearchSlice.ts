import { createSlice } from '@reduxjs/toolkit';
import { FlightSearchState } from '../../types/Flight';
import type { RootState } from '../store';

const initialState: FlightSearchState = {
  destination: '',
  status: '',
  searchResults: [],
  searchIsActive: false,
};

const searchSlice = createSlice({
  name: 'flightSearch',
  initialState,
  reducers: {
    setSearchResults: (state, action) => {
      state.searchResults = action.payload;
    },
    resetSearch: (state) => {
      return initialState;
    },
    setSearchIsActive: (state, action) => {
      state.searchIsActive = action.payload;
    },
    setFields: (state, action) => {
      const { id, value } = action.payload;
      return { ...state, [id]: value };
    },
  },
});

export const { resetSearch, setSearchResults, setSearchIsActive, setFields } =
  searchSlice.actions;

export const selectFlightSearch = (state: RootState): FlightSearchState =>
  state.search;

export default searchSlice.reducer;
