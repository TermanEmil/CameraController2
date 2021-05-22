import ApplicationError from "./ApplicationError";

export default class CommunicationError extends ApplicationError {
  constructor() {
    super('Failed to communicate with the API');
  }
}
