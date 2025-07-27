import { VALIDATION_MESSAGES } from '../constants/validationMessages';
import { CreateFlightDto } from '../types/Flight';
import { ValidationError } from '../types/Validation';

export const validateFlight = (flight: CreateFlightDto) => {
  const errors: ValidationError[] = [];

  if (flight.flightNumber.trim() === '') {
    errors.push({
      fieldName: 'flightNumber',
      errorMessage: VALIDATION_MESSAGES.REQUIRED_FIELD,
    });
  }

  if (flight.destination.trim() === '') {
    errors.push({
      fieldName: 'destination',
      errorMessage: VALIDATION_MESSAGES.REQUIRED_FIELD,
    });
  }

  if (flight.gate.trim() === '') {
    errors.push({
      fieldName: 'gate',
      errorMessage: VALIDATION_MESSAGES.REQUIRED_FIELD,
    });
  }

  if (flight.departureTime.trim() === '') {
    errors.push({
      fieldName: 'gate',
      errorMessage: VALIDATION_MESSAGES.REQUIRED_FIELD,
    });
  } else {
    const departureTime = new Date(flight.departureTime);
    const diff = departureTime.getTime() - Date.now();
    if (diff <= 0) {
      errors.push({
        fieldName: 'departureTime',
        errorMessage: VALIDATION_MESSAGES.INVALID_DEPARTURE_TIME,
      });
    }
  }
  return errors;
};
