import ApplicationError from "./ApplicationError";

export default class ApiCommunicationError extends ApplicationError {
  constructor() {
    super('Failed to communicate with the API');
  }
}
