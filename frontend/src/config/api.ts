const isDevelopment = process.env.NODE_ENV === 'development';

export const API_CONFIG = {
  baseURL: isDevelopment ? 'http://localhost:5140/api' : '/api',
  signalRHub: isDevelopment
    ? 'http://localhost:5140/flightBoardHub'
    : '/hubs/flightBoardHub',
};

console.log('API Configuration:', API_CONFIG);
