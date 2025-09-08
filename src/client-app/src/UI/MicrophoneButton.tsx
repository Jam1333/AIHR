import SpeechRecognition, {
  useSpeechRecognition,
} from "react-speech-recognition";

interface MicrophoneButtonProps {
  onSpeechEnded: (speech: string) => void | Promise<void>;
  language: "Russian" | "English" | "Kazakh" | "Belarussian";
  disabled?: boolean;
}

const languages = {
  Russian: "ru-RU",
  English: "en-US",
  Kazakh: "kk-KZ",
  Belarussian: "be-BY",
};

export const MicrophoneButton = ({
  onSpeechEnded,
  language,
  disabled,
}: MicrophoneButtonProps) => {
  const {
    transcript,
    listening,
    resetTranscript,
    browserSupportsSpeechRecognition,
  } = useSpeechRecognition();

  const handleClick = async () => {
    if (disabled) {
      return;
    }

    if (listening) {
      onSpeechEnded(transcript);

      await SpeechRecognition.stopListening();

      resetTranscript();
    } else {
      await SpeechRecognition.startListening({
        continuous: true,
        language: languages[language],
      });
    }
  };

  if (!browserSupportsSpeechRecognition) {
    return <span>Браузер не поддерживает распознавание речи.</span>;
  }

  return (
    <div className="relative group">
      <button
        id="microphoneButton"
        className={`relative w-16 h-16 bg-gradient-to-r ${
          disabled ? "from-gray-500 to-gray-800" : "from-blue-500 to-purple-600"
        } ${
          listening &&
          "ring-purple-400 outline-none ring-4 ring-opacity-50 scale-95"
        } rounded-full flex items-center justify-center text-white shadow-lg hover:shadow-xl transition-all duration-300`}
        aria-label="Start recording"
        onClick={handleClick}
      >
        <svg
          className="w-8 h-8"
          fill="none"
          stroke="currentColor"
          viewBox="0 0 24 24"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M19 11a7 7 0 01-7 7m0 0a7 7 0 01-7-7m7 7v4m0 0H8m4 0h4m-4-8a3 3 0 01-3-3V5a3 3 0 116 0v6a3 3 0 01-3 3z"
          ></path>
        </svg>

        <span
          id="pulseIndicator"
          className="absolute inset-0 rounded-full bg-purple-400 opacity-0 scale-125 group-hover:opacity-50 transition-opacity duration-300 animate-ping hidden"
        ></span>
      </button>

      <div className="absolute -top-10 left-1/2 transform -translate-x-1/2 bg-gray-800 text-white text-xs py-1 px-2 rounded opacity-0 group-hover:opacity-100 transition-opacity duration-300 pointer-events-none">
        Click to record
      </div>
    </div>
  );
};
