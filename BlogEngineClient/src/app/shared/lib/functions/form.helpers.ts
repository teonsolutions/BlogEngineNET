export function parseError(e: any) {
  if (e == null)
  return null;

  let errors: any[] = [];

  if (e.hasErrors) {
      errors = e.messages.map((item) => ({ msg: item.message }));
  } else {
    const message = e.developerMessage.Message;
    errors.push(message);
  }

  return errors;
}
