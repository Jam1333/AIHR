import { useNavigate, useParams } from "react-router-dom";
import { useFormData } from "../hooks/forms";
import { FormEvent, useEffect } from "react";
import { useAppSelector } from "../hooks/redux";
import { ErrorComponent } from "../UI/ErrorComponent";
import { toProblemDetails } from "../utils/toProblemDetails";
import { Spinner } from "../UI/Spinner";
import { RedirectToLogin } from "../UI/RedirectToLogin";
import { vacancyApi } from "../api/vacancyApi";
import { interviewApi } from "../api/interviewApi";

interface ICreateInterviewForm {
  title: string;
  weights: Record<string, number>;
  resumeFile: File | null;
  maxMessagesCount: number;
  vacancyId: string;
}

export const CreateInterview = () => {
  const { id } = useParams();

  const createInterviewForm: ICreateInterviewForm = {
    title: "",
    weights: {},
    resumeFile: null,
    maxMessagesCount: 3,
    vacancyId: id ?? "",
  };

  const [formData, handleChange, setFormData] =
    useFormData(createInterviewForm);

  const { error: fetchUserError } = useAppSelector(
    (state) => state.userReducer
  );

  const navigate = useNavigate();

  const [createInterview, { isSuccess, error, isLoading }] =
    interviewApi.useCreateInterviewMutation();

  const {
    data: vacancy,
    isLoading: isVacancyLoading,
    error: fetchVacancyError,
  } = vacancyApi.useFetchVacancyByIdQuery(id ?? "");

  const onSubmit = async (e: FormEvent) => {
    e.preventDefault();

    if (!formData.resumeFile) {
      return;
    }

    createInterview({ ...formData, resumeFile: formData.resumeFile });
  };

  const handleWeightChange = (category: string, value: number) => {
    setFormData({
      ...formData,
      weights: { ...formData.weights, [category]: value },
    });
  };

  useEffect(() => {
    if (isSuccess) {
      alert("Собеседоване успешно создано");
      navigate(`/vacancies/${id}`);
    }
  }, [isSuccess, navigate, id]);

  if (fetchUserError) {
    return <RedirectToLogin />;
  }

  if (fetchVacancyError) {
    return (
      <ErrorComponent problemDetails={toProblemDetails(fetchVacancyError)} />
    );
  }

  if (isVacancyLoading || !vacancy) {
    return <Spinner />;
  }

  return (
    <>
      <div className="mt-12 p-4 bg-neutral-700 text-white w-[48vw] min-w-80 min-h-[42vh] flex flex-col justify-center space-y-14 items-center border-emerald-500 border-4 rounded-[2rem]">
        <span className="text-4xl text-center font-bold text-emerald-200">
          Создание собеседования
        </span>

        <div>
          <form
            className="flex flex-col space-y-6 w-[30vw] min-w-64 text-[1.7rem]"
            onSubmit={onSubmit}
          >
            <div className="flex flex-row justify-between gap-4">
              <label className="text-xl">Название</label>
              <input
                className="w-[68%] text-base bg-stone-900 rounded-full px-5 align-middle transition"
                type="text"
                name="title"
                value={formData.title}
                onChange={handleChange}
                maxLength={100}
                required
              />
            </div>
            <div className="flex flex-row justify-between items-center gap-4">
              <label htmlFor="file_input" className="text-xl">
                Файл резюме
              </label>
              <input
                className="block h-[100%] w-[68%] text-sm rounded-lg cursor-pointer bg-stone-900"
                aria-describedby="file_input_help"
                id="file_input"
                name="resumeFile"
                accept=".rtf,.docx"
                type="file"
                onChange={handleChange}
                required
              />
            </div>
            <div className="flex flex-row justify-between gap-4">
              <label className="text-xl">Число вопросов</label>
              <input
                className="w-[68%] text-base bg-stone-900 rounded-full px-5 align-middle transition"
                type="number"
                name="maxMessagesCount"
                value={formData.maxMessagesCount}
                onChange={handleChange}
                min={3}
                max={30}
                required
              />
            </div>
            <div>
              <h3 className="text-center pb-4">Веса</h3>
              <div className="flex flex-col gap-2">
                {Object.entries(vacancy.requirements).map(([category]) => (
                  <div className="flex flex-row justify-between gap-4">
                    <label className="text-xl">{category}</label>
                    <input
                      className="w-[68%] text-base bg-stone-900 rounded-full px-5 align-middle transition"
                      type="number"
                      name={category}
                      min={0}
                      max={1}
                      step={0.01}
                      onChange={(e) =>
                        handleWeightChange(category, Number(e.target.value))
                      }
                      required
                    />
                  </div>
                ))}
              </div>
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
