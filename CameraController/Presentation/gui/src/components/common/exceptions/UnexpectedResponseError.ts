import ApplicationError from "./ApplicationError";

export default class UnexpectedResponseError extends ApplicationError {
  constructor() {
    super('Unexpected response');
  }
}
