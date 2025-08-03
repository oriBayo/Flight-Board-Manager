import dayjs from 'dayjs';

export const formattedDateToTime = (date: string) => {
  return dayjs(date).format('HH:mm');
};
