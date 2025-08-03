import { VALIDATION_MESSAGES } from '../constants/validationMessages';
import { CreateFlightDto, FlightFormValidation } from '../types/Flight';

export const validateFlight = (flight: CreateFlightDto) => {
  const errors: FlightFormValidation = {};

  if (flight.flightNumber.trim() === '') {
    errors.flightNumber = VALIDATION_MESSAGES.REQUIRED_FIELD;
  }

  if (flight.destination.trim() === '') {
    errors.destination = VALIDATION_MESSAGES.REQUIRED_FIELD;
  }

  if (flight.gate.trim() === '') {
    errors.gate = VALIDATION_MESSAGES.REQUIRED_FIELD;
  }

  if (flight.departureTime.trim() === '') {
    errors.departureTime = VALIDATION_MESSAGES.REQUIRED_FIELD;
  } else {
    const departureTime = new Date(flight.departureTime);
    const diff = departureTime.getTime() - Date.now();
    if (diff <= 0) {
      errors.departureTime = VALIDATION_MESSAGES.INVALID_DEPARTURE_TIME;
    }
  }
  return errors;
};
