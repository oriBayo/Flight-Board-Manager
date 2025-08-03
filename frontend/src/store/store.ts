import { configureStore } from '@reduxjs/toolkit';
import formSlice from './slices/formSlice';
import searchSlice from './slices/searchSlice';
import eventsSlice from './slices/eventsSlice';

export const store = configureStore({
  reducer: {
    form: formSlice,
    search: searchSlice,
    events: eventsSlice,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: false,
    }),
});
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
