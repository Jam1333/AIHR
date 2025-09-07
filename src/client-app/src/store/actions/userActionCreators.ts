import { axiosInstance } from "../../axiosInstance";
import { IUser } from "../../types/user";
import { toProblemDetails } from "../../utils/toProblemDetails";
import { userSlice } from "../reducers/userSlice";
import { AppDispatch } from "../store";

export const fetchCurrentUser = () => async (dispatch: AppDispatch) => {
    try {
        dispatch(userSlice.actions.fetchCurrentUser());

        const response = await axiosInstance.get<IUser>("/users/current");

        dispatch(userSlice.actions.fetchCurrentUserSuccess(response.data));
    } catch(e) {
        dispatch(userSlice.actions.fetchCurrentUserError(toProblemDetails(e)));
    }
};


export const logoutCurrentUser = () => async (dispatch: AppDispatch) => {
    try {
        dispatch(userSlice.actions.logoutCurrentUser());

        await axiosInstance.get<IUser>("/auth/logout");

        dispatch(userSlice.actions.logoutCurrentUserSuccess());
    } catch(e) {
        dispatch(userSlice.actions.logoutCurrentUserError(toProblemDetails(e)));
    }
};