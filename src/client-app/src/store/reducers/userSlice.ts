import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IUser } from "../../types/user";
import { IProblemDetails } from "../../types/problemDetails";

interface UserState {
  currentUser: IUser | null;
  isLoading: boolean;
  error: IProblemDetails | null;
}

const initialState: UserState = {
  currentUser: null,
  isLoading: false,
  error: null,
};

export const userSlice = createSlice({
    name: "user",
    initialState,
    reducers: {
      fetchCurrentUser(state) {
        state.isLoading = true;
        state.error = null;
      },
      fetchCurrentUserSuccess(state, action: PayloadAction<IUser>) {
        state.isLoading = false;
        state.currentUser = action.payload;
        state.error = null;
      },
      fetchCurrentUserError(state, action: PayloadAction<IProblemDetails>) {
        state.isLoading = false;
        state.error = action.payload;
      },
      logoutCurrentUser(state) {
        state.isLoading = true;
        state.error = null;
      },
      logoutCurrentUserSuccess(state) {
        state.isLoading = false;
        state.currentUser = null;
        state.error = null;
      },
      logoutCurrentUserError(state, action: PayloadAction<IProblemDetails>) {
        state.isLoading = false;
        state.error = action.payload;
      },
    }
});

export const userReducer = userSlice.reducer;
