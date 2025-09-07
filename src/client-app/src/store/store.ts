import { combineReducers, configureStore } from "@reduxjs/toolkit";
import { userReducer } from "./reducers/userSlice";
import { userApi } from "../api/userApi";
import { authApi } from "../api/authApi";
import { vacancyApi } from "../api/vacancyApi";
import { analysisApi } from "../api/analysisApi";
import { interviewApi } from "../api/interviewApi";

const rootReducer = combineReducers({
  userReducer,
  [userApi.reducerPath]: userApi.reducer,
  [authApi.reducerPath]: authApi.reducer,
  [vacancyApi.reducerPath]: vacancyApi.reducer,
  [analysisApi.reducerPath]: analysisApi.reducer,
  [interviewApi.reducerPath]: interviewApi.reducer,
});

export const setupStore = () => {
  return configureStore({
    reducer: rootReducer,
    middleware: (getDefaultMiddleware) =>
      getDefaultMiddleware().concat(
        userApi.middleware,
        authApi.middleware,
        vacancyApi.middleware,
        analysisApi.middleware,
        interviewApi.middleware
      ),
  });
};

export type RootState = ReturnType<typeof rootReducer>;
export type AppStore = ReturnType<typeof setupStore>;
export type AppDispatch = AppStore["dispatch"];
