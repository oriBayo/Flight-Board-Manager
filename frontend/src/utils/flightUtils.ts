export function getFlightStatusClass(status: string): string {
  switch (status) {
    case 'Scheduled':
      return 'bg-green-100 text-green-800';
    case 'Boarding':
      return 'bg-yellow-100 text-yellow-800';
    case 'Departed':
      return 'bg-blue-100 text-blue-800';
    default:
      return 'bg-gray-100 text-gray-800';
  }
}
