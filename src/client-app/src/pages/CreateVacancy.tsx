import { useNavigate } from "react-router-dom";
import { useFormData } from "../hooks/forms";
import { FormEvent, useEffect } from "react";
import { useAppSelector } from "../hooks/redux";
import { ErrorComponent } from "../UI/ErrorComponent";
import { toProblemDetails } from "../utils/toProblemDetails";
import { Spinner } from "../UI/Spinner";
import { vacancyApi } from "../api/vacancyApi";
import { RedirectToLogin } from "../UI/RedirectToLogin";

interface ICreateVacancyForm {
  title: string;
  language: string;
  categories: string;
  file: File | null;
}

export const CreateVacancy = () => {
  const createVacancyForm: ICreateVacancyForm = {
    title: "",
    language: "Russian",
    categories: "",
    file: null,
  };

  const [formData, handleChange] = useFormData(createVacancyForm);

  const { error: fetchUserError } = useAppSelector(
    (state) => state.userReducer
  );

  const navigate = useNavigate();

  const [createVacancy, { isSuccess, error, isLoading }] =
    vacancyApi.useCreateVacancyMutation();

  const onSubmit = async (e: FormEvent) => {
    e.preventDefault();

    if (!formData.file) {
      return;
    }

    await createVacancy({
      title: formData.title,
      language: formData.language,
      categories: formData.categories.split(",").map((c) => c.trim()),
      file: formData.file,
    });
  };

  useEffect(() => {
    if (isSuccess) {
      alert("Вакансия успешно создана");
      navigate("/vacancies");
    }
  }, [isSuccess, navigate]);

  if (fetchUserError) {
    return <RedirectToLogin />;
  }

  return (
    <>
      <div className="mt-12 p-4 bg-neutral-700 text-white w-[48vw] min-w-80 min-h-[42vh] flex flex-col justify-center space-y-14 items-center border-emerald-500 border-4 rounded-[2rem]">
        <span className="text-4xl text-center font-bold text-emerald-200">
          Создание вакансии
        </span>

        <div>
          <form
            className="flex flex-col space-y-6 w-[30vw] min-w-64 text-[1.7rem]"
            onSubmit={onSubmit}
          >
            <div className="flex flex-row justify-between gap-4">
              <label>Название</label>
              <input
                className="w-[68%] text-base bg-stone-900 rounded-full px-5 align-middle transition"
                type="text"
                name="title"
                value={formData.title}
                onChange={handleChange}
                maxLength={50}
                required
              />
            </div>
            <div className="flex flex-row justify-between items-center gap-4">
              <label>Язык</label>
              <select
                className="w-[68%] bg-stone-900 text-sm rounded-lg block p-2.5 placeholder-gray-400 focus:ring-blue-500 focus:border-blue-500"
                defaultValue={formData.language}
                name="language"
                onChange={handleChange}
                required
              >
                <option value="English">Английский</option>
                <option value="Russian">Русский</option>
                <option value="Kazakh">Казахский</option>
                <option value="Belarusian">Белорусский</option>
              </select>
            </div>
            <div className="flex flex-row justify-between gap-4">
              <label>Категории</label>
              <input
                className="w-[68%] text-base bg-stone-900 rounded-full px-5 align-middle transition"
                type="text"
                name="categories"
                placeholder="Вводите категории через знак ','"
                value={formData.categories}
                onChange={handleChange}
                required
              />
            </div>
            <div className="flex flex-row justify-between items-center gap-4">
              <label htmlFor="file_input">Файл</label>
              <input
                className="block h-[100%] w-[68%] text-sm rounded-lg cursor-pointer bg-stone-900"
                aria-describedby="file_input_help"
                id="file_input"
                name="file"
                onChange={handleChange}
                accept=".rtf,.docx"
                type="file"
              />
            </div>
            <div className="p-5 flex flex-row items-center self-center space-x-3">
              <input
                className="cursor-pointer text-center bg-emerald-600 pt-3 px-8 pb-4 rounded-3xl align-middle text-2xl transition-transform duration-300  hover:scale-110 active:bg-emerald-400 active:scale-105"
                value={"Создать"}
                type="submit"
              />
            </div>
          </form>
        </div>
      </div>
      {error && <ErrorComponent problemDetails={toProblemDetails(error)} />}
      {isLoading && <Spinner />}
    </>
  );
};
