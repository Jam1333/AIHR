import { Link, useParams } from "react-router-dom";
import { interviewApi } from "../api/interviewApi";
import { MicrophoneButton } from "../UI/MicrophoneButton";
import { Spinner } from "../UI/Spinner";
import { ErrorComponent } from "../UI/ErrorComponent";
import { toProblemDetails } from "../utils/toProblemDetails";
import { utcStringToLocalDate } from "../utils/utcStringToLocalDate";
import { languages } from "../constants/languages";
import { useSpeech } from "react-text-to-speech";
import { useEffect, useRef } from "react";

export const Chat = () => {
  const { id } = useParams();

  const {
    data: interview,
    isLoading: isInterviewLoading,
    error: fetchInterviewError,
  } = interviewApi.useFetchInterviewByIdQuery(id ?? "");

  const [
    answerLastQuestion,
    { isLoading: isAnswerLoading, error: answerError },
  ] = interviewApi.useAnswerInterviewLastQuestionMutation();

  const [
    generateConclusion,
    { isLoading: isConclusionLoading, error: conclusionError },
  ] = interviewApi.useGenerateInterviewConclusionMutation();

  const { start, stop, speechStatus } = useSpeech({
    text:
      interview?.interviewMessages[interview.interviewMessages.length - 1]
        .question ?? "",
    lang: languages[
      (interview?.language ?? "Russian") as
        | "Russian"
        | "Kazakh"
        | "English"
        | "Belarussian"
    ],
  });

  const messagesRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    scrollToBottom();

    if (!interview || interview.hasEnded) {
      return;
    }

    start();
  }, [interview, start]);

  const toggleAudio = () => {
    if (speechStatus === "started") {
      stop();
    } else if (speechStatus === "stopped") {
      start();
    }
  };

  const handleSpeechEnd = async (transcript: string) => {
    if (!id) {
      return;
    }

    await answerLastQuestion({
      id: id,
      answer: transcript,
    });
  };

  const scrollToBottom = () => {
    messagesRef.current?.scrollTo({
      behavior: "smooth",
      top: messagesRef.current?.scrollHeight,
    });
  };

  if (isInterviewLoading) {
    return <Spinner />;
  }

  if (fetchInterviewError) {
    return (
      <ErrorComponent problemDetails={toProblemDetails(fetchInterviewError)} />
    );
  }

  if (!interview) {
    return <></>;
  }

  return (
    <>
      <div className="w-[90vw] h-[80vh] flex flex-col gap-2 items-center justify-center text-center ">
        <div className="w-[60vw] h-[70vh] min-h-[450px] min-w-[350px] flex flex-col rounded-3xl">
          <div className="flex flex-col gap-2 p-4 bg-neutral-900 shadow-sm rounded-t-3xl">
            <h1 className="text-xl font-semibold text-emerald-300">
              {interview.title}
              {interview.hasEnded && " (Окончено)"}
            </h1>
            <Link
              to={`/interviews/${id}/contact-information`}
              className="text-xs underline"
            >
              Заполнить контактную информацию
            </Link>
          </div>

          <div
            className="flex-1 overflow-y-auto p-4 space-y-6 bg-neutral-800"
            ref={messagesRef}
          >
            {interview.interviewMessages.map((m, i) => (
              <div key={i}>
                <div className="flex items-start space-x-2">
                  <div className="flex flex-col items-start">
                    <div className="bg-neutral-950 p-3 rounded-xl rounded-tl-none shadow-sm max-w-xs lg:max-w-md">
                      <div className="flex flex-row items-center gap-5 text-sm text-white">
                        {m.question}
                        {!m.answer && (
                          <button
                            className="bg-blue-500 min-w-6 h-6 rounded-full transition duration-150 hover:scale-110 cursor-pointer disabled:cursor-auto"
                            onClick={toggleAudio}
                          >
                            {speechStatus === "started" ? "-" : "+"}
                          </button>
                        )}
                      </div>
                    </div>
                    <span className="text-xs text-gray-500 mt-1">
                      {utcStringToLocalDate(m.createdOnUtc)}
                    </span>
                  </div>
                </div>

                {m.answer && (
                  <div className="flex items-start justify-end space-x-2">
                    <div className="flex flex-col items-end">
                      <div className="bg-emerald-500 p-3 rounded-xl rounded-tr-none shadow-sm max-w-xs lg:max-w-md">
                        <p className="text-sm text-white">{m.answer}</p>
                      </div>
                      <span className="text-xs text-gray-500 mt-1">
                        {utcStringToLocalDate(m.answeredOnUtc ?? "")}
                      </span>
                    </div>
                  </div>
                )}
              </div>
            ))}
            {interview.conclusion && <div>{interview.conclusion}</div>}
          </div>

          <div className="w-full flex flex-row justify-center items-center gap-2 bg-neutral-900 px-4 py-4 mb-2 rounded-b-3xl">
            <MicrophoneButton
              onSpeechEnded={handleSpeechEnd}
              language="Russian"
              disabled={
                interview.hasEnded ||
                isAnswerLoading ||
                speechStatus === "started"
              }
            />
            {interview.hasEnded &&
              !interview.conclusion &&
              !isConclusionLoading && (
                <button
                  className="rounded-full bg-blue-600 text-white antialiased font-bold hover:bg-blue-800 px-4 py-2"
                  onClick={() => generateConclusion(id ?? "")}
                >
                  Получить совет
                </button>
              )}
            {(isAnswerLoading || isConclusionLoading) && <Spinner />}
          </div>
        </div>
        {answerError && (
          <ErrorComponent problemDetails={toProblemDetails(answerError)} />
        )}
        {conclusionError && (
          <ErrorComponent problemDetails={toProblemDetails(conclusionError)} />
        )}
      </div>
    </>
  );
};
