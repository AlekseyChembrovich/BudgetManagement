import moment from "moment/moment";

export function formatDate(value: string) {
  if (value) {
    return moment(value).format('MM/DD/YYYY hh:mm');
  }
}

export function formatFloat(value: number) {
  return value.toFixed(1);
}

export function formatDescription(value: string) {
  if (value) {
    if (value.length > 90) {
      return value.substring(0, 90) + '...';
    }
    return value;
  }
}
