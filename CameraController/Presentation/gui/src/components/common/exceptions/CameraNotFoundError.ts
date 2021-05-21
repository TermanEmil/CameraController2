import ApplicationError from "./ApplicationError";

export default class CameraNotFoundError extends ApplicationError {
  constructor() {
    super('Camera not found');
  }
}
