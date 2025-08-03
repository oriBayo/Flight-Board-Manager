export type FlightStatus = 'Scheduled' | 'Boarding' | 'Departed' | 'Landed';

export type FlightFormField =
  | 'flightNumber'
  | 'destination'
  | 'gate'
  | 'departureTime';

export interface Flight {
  id: string;
  flightNumber: string;
  destination: string;
  departureTime: string;
  gate: string;
  statusString: FlightStatus;
}

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
  destination: string;
  status: FlightStatus | '';
  searchResults: Flight[];
  searchIsActive: boolean;
}

export interface CreateFlightDto {
  flightNumber: string;
  destination: string;
  gate: string;
  departureTime: string;
}

export interface FlightEventsState {
  created: string[];
  updated: string[];
  deleted: string[];
}

export interface flightDetailsProps {
  flight: Flight;
  isNew: boolean;
  isUpdated: boolean;
}
