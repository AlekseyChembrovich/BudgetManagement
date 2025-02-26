import { sendPost } from "@/services/basics/http-client.ts";
import { jwtDecode } from 'jwt-decode';
import type { JwtPayload } from 'jwt-decode';
import type { Result } from "@/services/models/result";
import userStateStore from '@/services/stores/user-store';

export interface ISignInCommand {
  login: string;
  password: string;
}

export interface ISignUpCommand {
  login: string;
  password: string;
  confirmPassword: string;
}

export interface IAccessTokenResponse {
  accessToken: string;
}

interface CustomJwtPayload extends JwtPayload {
  email: string;
  role: string;
}

export class AuthService {
  async signIn(command: ISignInCommand): Promise<Result<IAccessTokenResponse>> {
    const result: Result<IAccessTokenResponse> = await sendPost<ISignInCommand, IAccessTokenResponse>('/auth/sign-in', command);

    if (result.success) {
      const payload = jwtDecode<CustomJwtPayload>(result.data.accessToken);
      userStateStore().set(payload.email, result.data.accessToken);
    }

    return result;
  }

  async signUp(command: ISignUpCommand): Promise<Result<IAccessTokenResponse>> {
    const result: Result<IAccessTokenResponse> = await sendPost<ISignInCommand, IAccessTokenResponse>('/auth/sign-up', command);

    if (result.success) {
      const payload = jwtDecode<CustomJwtPayload>(result.data.accessToken);
      userStateStore().set(payload.email, result.data.accessToken);
    }

    return result;
  }
}
