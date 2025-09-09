import { useSpeech } from "react-text-to-speech";
import { MicrophoneButton } from "../UI/MicrophoneButton";
import { useEffect } from "react";

export const Test = () => {
  const { start, pause, stop, speechStatus } = useSpeech({
    text: "Привет, меня зовут Максим",
    lang: "ru-RU"
  });

  useEffect(() => {
    start();
  }, []);

  return (
    <div>
      <MicrophoneButton
        onSpeechEnded={(transcript) => console.log(transcript)}
        language="Russian"
      />
    </div>
  );
};
