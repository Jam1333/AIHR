export const utcStringToLocalDate = (utcString: string) => {
    const date = new Date(utcString);

    return date.toLocaleString();
}