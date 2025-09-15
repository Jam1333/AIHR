import { useNavigate, useParams } from "react-router-dom";
import { useFormData } from "../hooks/forms";
import { FormEvent, useEffect } from "react";
import { ErrorComponent } from "../UI/ErrorComponent";
import { toProblemDetails } from "../utils/toProblemDetails";
import { Spinner } from "../UI/Spinner";
import { interviewApi } from "../api/interviewApi";

interface IUploadContactInformationForm {
  email: string;
  phoneNumber: string;
}

export const UploadContactInformation = () => {
  const { id } = useParams();

  const uploadContactInformationForm: IUploadContactInformationForm = {
    email: "",
    phoneNumber: "",
  };

  const [formData, handleChange, setFormData] = useFormData(
    uploadContactInformationForm
  );

  const navigate = useNavigate();

  const [
    uploadContactInformation,
    {
      isSuccess,
      isLoading: isContactInformationLoading,
      error: contactInformationError,
    },
  ] = interviewApi.useProvideInterviewContactInformationMutation();

  const {
    data: interview,
    isLoading: isInterviewLoading,
    error: fetchInterviewError,
  } = interviewApi.useFetchInterviewByIdQuery(id ?? "");

  const onSubmit = async (e: FormEvent) => {
    e.preventDefault();

    if (!id) {
      return;
    }

    await uploadContactInformation({ ...formData, id: id });
  };

  useEffect(() => {
    if (!interview || !interview.contactInformation) {
      return;
    }

    setFormData(interview.contactInformation);
  }, [interview, setFormData]);

  useEffect(() => {
    if (!isSuccess) {
      return;
    }

    navigate(`/interviews/${id}`);
  }, [isSuccess, id, navigate]);

  if (fetchInterviewError) {
    return (
      <ErrorComponent problemDetails={toProblemDetails(fetchInterviewError)} />
    );
  }

  if (isInterviewLoading || !interview) {
    return <Spinner />;
  }

  return (
    <>
      <div className="mt-12 p-4 bg-neutral-700 text-white w-[48vw] min-w-80 min-h-[42vh] flex flex-col justify-center space-y-14 items-center border-emerald-500 border-4 rounded-[2rem]">
        <span className="text-4xl text-center font-bold text-emerald-200">
          Контактная информация
        </span>

        <div>
          <form
            className="flex flex-col space-y-6 w-[30vw] min-w-64 text-[1.7rem]"
            onSubmit={onSubmit}
          >
            <div className="flex flex-row justify-between gap-4">
              <label className="text-xl">Email</label>
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
              <label className="text-xl">Номер телефона</label>
              <input
                className="w-[68%] text-base bg-stone-900 rounded-full px-5 align-middle transition"
                type="text"
                name="phoneNumber"
                value={formData.phoneNumber}
                onChange={handleChange}
                maxLength={15}
                required
              />
            </div>
            <div className="p-5 flex flex-row items-center self-center space-x-3">
              <input
                className="cursor-pointer text-center bg-emerald-600 pt-3 px-8 pb-4 rounded-3xl align-middle text-2xl transition-transform duration-300  hover:scale-110 active:bg-emerald-400 active:scale-105"
                value={"Оставить"}
                type="submit"
              />
            </div>
          </form>
        </div>
      </div>
      {contactInformationError && (
        <ErrorComponent
          problemDetails={toProblemDetails(contactInformationError)}
        />
      )}
      {isContactInformationLoading && <Spinner />}
    </>
  );
};
