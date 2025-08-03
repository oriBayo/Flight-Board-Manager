/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./src/**/*.{js,jsx,ts,tsx}', './public/index.html'],
  theme: {
    extend: {
      fontFamily: {
        archivo: ['"Archivo Black"', 'sans-serif'],
        arimo: ['"Arimo"', 'sans-serif'],
      },
    },
  },
  plugins: [],
};
