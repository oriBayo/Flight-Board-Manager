import { configureStore } from '@reduxjs/toolkit';
import formSlice from './slices/FormSlice';
import searchSlice from './slices/SearchSlice';

export const store = configureStore({
  reducer: {
    form: formSlice,
    search: searchSlice,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: false,
    }),
});
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
