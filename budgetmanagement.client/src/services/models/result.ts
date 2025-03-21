export interface Result<T> {
  success: boolean;
  data: T;
  errors: string[];
}
