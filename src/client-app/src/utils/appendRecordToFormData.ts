export function appendRecordToFormData<T>(
  formData: FormData,
  record: Record<string, T>,
  key: string
) {
  Object.entries(record).forEach(([recordKey, recordValue]) => {
    formData.append(`${key}[${recordKey}]`, `${recordValue}`);
  });
}
