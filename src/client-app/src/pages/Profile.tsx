import { useAppSelector } from "../hooks/redux";
import { LogoutButton } from "../UI/LogoutButton";
import { RedirectToLogin } from "../UI/RedirectToLogin";
import { utcStringToLocalDate } from "../utils/utcStringToLocalDate";

export const Profile = () => {
  const { currentUser } = useAppSelector((state) => state.userReducer);

  if (!currentUser) {
    return <RedirectToLogin />;
  }

  return (
    <div className="max-w-sm min-w-80 mx-auto bg-gray-700 rounded-lg overflow-hidden shadow-lg">
      <div className="px-4 pb-6">
        <div className="text-center my-4">
          <img
            className="h-32 w-32 rounded-full border-4 border-gray-600 bg-gray-800 mx-auto my-4"
            src={`https://api.dicebear.com/8.x/pixel-art/svg?seed=${currentUser.username}`}
            alt="avatar"
          />
          <div className="py-2">
            <h3 className="font-bold text-2xl text-gray-800 dark:text-white mb-1">
              {currentUser.username}
            </h3>
          </div>
          <div className="flex flex-col justify-center items-center gap-2">
            <div className="text-gray-700 dark:text-gray-300 items-center">
              {currentUser.email}
            </div>
            <div className="text-gray-700 dark:text-gray-300 items-center">
              {utcStringToLocalDate(currentUser.createdOnUtc)}
            </div>
          </div>
        </div>
        <div className="flex gap-2 px-2">
          <LogoutButton />
        </div>
      </div>
    </div>
  );
};
