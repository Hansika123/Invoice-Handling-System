export class AuthStatusNotifierService {

  private isAuthenticateStatusSet: boolean = true;

  setAuthStatus(is_authenticated: boolean) {
    this.isAuthenticateStatusSet = is_authenticated;
  }

  getAuthStatus() {
    return this.isAuthenticateStatusSet;
  }
}
