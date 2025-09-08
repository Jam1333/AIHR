import { useState } from "react";

export function useFormData<T>(
  initialValue: T,
  multifile: boolean = false
): [
  T,
  (e: React.ChangeEvent) => void,
  React.Dispatch<React.SetStateAction<T>>
] {
  const [formData, setFormData] = useState<T>(initialValue);

  const handleChange = (e: React.ChangeEvent) => {
    if ("name" in e.target && "value" in e.target) {
      const { name, value } = e.target;

      const htmlInput = e.target as HTMLInputElement;

      if (
        "files" in htmlInput &&
        htmlInput.files &&
        htmlInput.files.length > 0
      ) {
        if (multifile) {
          setFormData((prevData) => ({
            ...prevData,
            [name as string]: Array.from(htmlInput.files!),
          }));
        } else {
          setFormData((prevData) => ({
            ...prevData,
            [name as string]: htmlInput.files?.item(0),
          }));
        }
      } else {
        setFormData((prevData) => ({ ...prevData, [name as string]: value }));
      }
    }
  };

  return [formData, handleChange, setFormData];
}
