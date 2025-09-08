import { Link } from "react-router-dom";
import { useFormData } from "../hooks/forms";
import { authApi } from "../api/authApi";
import { FormEvent } from "react";
import { useAppDispatch, useAppSelector } from "../hooks/redux";
import { RedirectToHome } from "../UI/RedirectToHome";
import { ErrorComponent } from "../UI/ErrorComponent";
import { toProblemDetails } from "../utils/toProblemDetails";
import { Spinner } from "../UI/Spinner";
import { fetchCurrentUser } from "../store/actions/userActionCreators";
import { RedirectToLogin } from "../UI/RedirectToLogin";

interface IRegistrationForm {
  username: string;
  email: string;
  password: string;
}

export const Registration = () => {
  const registrationForm: IRegistrationForm = {
    username: "",
    email: "",
    password: "",
  };

  const [formData, handleChange] = useFormData(registrationForm);

  const { currentUser } = useAppSelector((state) => state.userReducer);

  const [register, { isSuccess, error, isLoading }] =
    authApi.useRegisterUserMutation();

  const onSubmit = async (e: FormEvent) => {
    e.preventDefault();

    await register({
      username: formData.username,
      email: formData.email,
      password: formData.password,
    });
  };

  if (currentUser) {
    return <RedirectToHome />;
  }

  if (isSuccess) {
    return <RedirectToLogin />;
  }

  return (
    <>
      <div className="mt-10 p-4 bg-neutral-700 text-white w-[48vw] min-w-[350px] min-h-[42vh] flex flex-col justify-center space-y-14 items-center border-emerald-500 border-4 rounded-[2rem]">
        <span className="text-4xl font-bold text-emerald-200">Регистрация</span>

        <div>
          <form
            className="flex flex-col space-y-6 w-[30vw] min-w-64 text-[1.7rem]"
            onSubmit={onSubmit}
          >
            <div className="flex flex-row justify-between gap-4">
              <label>Имя</label>
              <input
                className="w-[68%] text-base bg-stone-900 rounded-full px-5 align-middle transition"
                type="text"
                name="username"
                value={formData.username}
                onChange={handleChange}
                minLength={3}
                maxLength={30}
                required
              />
            </div>

            <div className="flex flex-row justify-between gap-4">
              <label>Email</label>
              <input
                className="w-[68%] text-base bg-stone-900 rounded-full px-5 align-middle transition"
                type="email"
                name="email"
                value={formData.email}
                onChange={handleChange}
                required
              />
            </div>

            <div className="flex flex-row justify-between gap-4">
              <label>Пароль</label>
              <input
                className="w-[68%] text-base bg-stone-900 rounded-full px-5 align-middle transition"
                type="password"
                name="password"
                value={formData.password}
                onChange={handleChange}
                minLength={8}
                maxLength={100}
                required
              />
            </div>
            <div className="p-5 flex flex-col items-center self-center space-x-3 gap-2">
              <input
                className="cursor-pointer text-center bg-emerald-600 pt-3 px-8 pb-4 rounded-3xl align-middle text-2xl transition-transform duration-300  hover:scale-110 active:bg-emerald-400 active:scale-105"
                value={"Зарегистрироваться"}
                type="submit"
              />
              <Link className="text-xl" to="/login">
                Есть аккаунт?
              </Link>
            </div>
          </form>
        </div>
      </div>
      {error && <ErrorComponent problemDetails={toProblemDetails(error)} />}
      {isLoading && <Spinner />}
    </>
  );
};
