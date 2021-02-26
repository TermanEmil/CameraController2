export class Camera {
  private model: string;
  private port: string;

  constructor(model: string, port: string) {
    this.model = model;
    this.port = port;
  }

  get Model(): string {
    return this.model;
  }

  get Port(): string {
    return this.port;
  }
}
