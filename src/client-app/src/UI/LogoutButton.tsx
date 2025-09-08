import { useAppDispatch } from "../hooks/redux";
import { logoutCurrentUser } from "../store/actions/userActionCreators";

export const LogoutButton = () => {
  const dispatch = useAppDispatch();

  const handleClick = async () => {
    await dispatch(logoutCurrentUser());
  };

  return (
    <button
      className="flex-1 rounded-full bg-blue-600 text-white antialiased font-bold hover:bg-blue-800 px-4 py-2"
      onClick={handleClick}
    >
      Выйти
    </button>
  );
};
