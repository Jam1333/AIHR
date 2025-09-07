import { useState } from "react";

export function useFormData<T>(
  initialValue: T
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
        htmlInput.files.length !== 0
      ) {
        setFormData((prevData) => ({
          ...prevData,
          [name as string]: htmlInput.files?.item(0),
        }));
      } else {
        setFormData((prevData) => ({ ...prevData, [name as string]: value }));
      }
    }
  };

  return [formData, handleChange, setFormData];
}

