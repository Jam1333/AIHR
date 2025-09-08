import { MicrophoneButton } from "../UI/MicrophoneButton";

export const Test = () => {
  return (
    <div>
      <MicrophoneButton
        onSpeechEnded={(transcript) => console.log(transcript)}
        language="Russian"
      />
    </div>
  );
};
