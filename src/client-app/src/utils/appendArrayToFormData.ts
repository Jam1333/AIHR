export function appendArrayToFormData(
  formData: FormData,
  values: (string | Blob)[],
  key: string
) {
  for (let i = 0; i < values.length; i++) {
    formData.append(`${key}[${i}]`, values[i]);
  }
}
