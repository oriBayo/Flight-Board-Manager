import { ValidationError } from './Validation';

export interface Flight {
  id: string;
  flightNumber: string;
  destination: string;
  departureTime: string;
  gate: string;
  statusString: FlightStatus;
}

export type FlightStatus = 'Scheduled' | 'Boarding' | 'Departed' | 'Landed';

export interface FlightFormValidation {
  flightNumber?: string;
  destination?: string;
  gate?: string;
  departureTime?: string;
}

export interface FlightFormState {
  flightNumber: string;
  destination: string;
  gate: string;
  departureTime: string;
  isSubmitted?: boolean;
  errors: FlightFormValidation;
}

export interface FlightSearchState {
  search: string;
  status: FlightStatus | '';
  submitted: boolean;
}

export interface CreateFlightDto {
  flightNumber: string;
  destination: string;
  gate: string;
  departureTime: string;
}
