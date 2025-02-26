import axios from 'axios';
import type { AxiosRequestConfig } from 'axios';
import type { AxiosResponse } from 'axios';
import type { Result } from '@/services/models/result';
import userStateStore from "@/services/stores/user-store";
import router from '@/router/index';

const requestTimeout: number = 20_000;

const applicationApiUrl: string = import.meta.env.VITE_API_URL;

const client = axios.create({
  baseURL: applicationApiUrl,
  timeout: requestTimeout
});

client.interceptors.response.use(
  (response) => {
    // Return response if successful
    return response;
  },
  (error) => {
    if (error.response && error.response.status === 401) {
      router.push("/signin");
    }
    else {
      return Promise.reject(error);
    }
  }
);

const mapErrorToResult = <TResponse>(error: unknown): Result<TResponse> => {
  if (axios.isAxiosError(error)) {
    if (error.code === 'ERR_NETWORK') {
      return { success: false, errors: [ 'Some network error occurred...' ], data: null as TResponse };
    }

    return { success: false, errors: [ error.request?.data ], data: null as TResponse };
  }

  return { success: false, errors: ['An unexpected error occurred'], data: null as TResponse };
}

export async function sendGet<TResponse>(url: string) : Promise<Result<TResponse>> {
  let response: AxiosResponse<TResponse>;
  const config: AxiosRequestConfig = {
    headers: {
      'Authorization': 'Bearer ' + userStateStore().userState.accessToken
    },
    timeout: requestTimeout,
    maxRedirects: 0
  };

  try {
    response = await client.get<TResponse>(url, config);

    console.log(`GET method response [${url}]: `, response);

    return { success: true, data: response.data, errors: [] };
  }
  catch (error) {
    return mapErrorToResult(error);
  }
}

export async function loadFile(url: string) : Promise<Result<any>> {
  const config: AxiosRequestConfig = {
    headers: {
      'Authorization': 'Bearer ' + userStateStore().userState.accessToken
    },
    timeout: requestTimeout,
    responseType: 'blob'
  };

  try {
    let response = await client.get(url, config);

    console.log(`GET method response [${url}]: `, response);

    return { success: true, data: response.data, errors: [] };
  }
  catch (error) {
    return mapErrorToResult(error);
  }
}

export async function sendPost<TBody, TResponse>(url: string, body: TBody) : Promise<Result<TResponse>> {
  let response: AxiosResponse<TResponse>;
  const config: AxiosRequestConfig = {
    headers: {
      'Authorization': 'Bearer ' + userStateStore().userState.accessToken
    },
    timeout: requestTimeout
  };

  try {
    response = await client.post<TResponse>(url, body, config);

    console.log(`POST method response [${url}]: `, response);

    return { success: true, data: response.data, errors: [] };
  }
  catch (error) {
    return mapErrorToResult(error);
  }
}

export async function sendForm<TResponse>(url: string, formData: FormData) : Promise<Result<TResponse>> {
  let response: AxiosResponse<TResponse>;
  const config: AxiosRequestConfig = {
    headers: {
      'Authorization': 'Bearer ' + userStateStore().userState.accessToken,
      'Content-Type': 'multipart/form-data'
    },
    timeout: requestTimeout
  };

  try {
    response = await client.post<TResponse>(url, formData, config);

    console.log(`POST method response [${url}]: `, response);

    return { success: true, data: response.data, errors: [] };
  }
  catch (error) {
    return mapErrorToResult(error);
  }
}

export async function sendDelete<TResponse>(url: string) : Promise<Result<TResponse>> {
  let response: AxiosResponse<TResponse>;
  const config: AxiosRequestConfig = {
    headers: {
      'Authorization': 'Bearer ' + userStateStore().userState.accessToken
    },
    timeout: requestTimeout
  };

  try {
    response = await client.delete<TResponse>(url, config);

    console.log(`DELETE method response [${url}]: `, response);

    return { success: true, data: response.data, errors: [] };
  }
  catch (error) {
    return mapErrorToResult(error);
  }
}

export async function sendPut<TBody, TResponse>(url: string, body: TBody) : Promise<Result<TResponse>> {
  let response: AxiosResponse<TResponse>;
  const config: AxiosRequestConfig = {
    headers: {
      'Authorization': 'Bearer ' + userStateStore().userState.accessToken
    },
    timeout: requestTimeout
  };

  try {
    response = await client.put<TResponse>(url, body, config);

    console.log(`PUT method response [${url}]: `, response);

    return { success: true, data: response.data, errors: [] };
  }
  catch (error) {
    return mapErrorToResult(error);
  }
}
