import { ref } from 'vue';
import { defineStore } from 'pinia'
import type { UserState } from "@/services/models/user-state";

const userStateStore = defineStore('user-state', () => {
  const defaultState: UserState = {
    login: '',
    accessToken: '',
    isAuthenticated: false
  };

  const userState = ref(defaultState);

  function set(login: string, accessToken: string) {
    userState.value = {
      login: login,
      accessToken: accessToken,
      isAuthenticated: true
    };
  }

  function clear() {
    userState.value = {
      login: '',
      accessToken: '',
      isAuthenticated: false
    };
  }

  return { userState, set, clear }
});

export default userStateStore;
