import axios from 'axios';
import { inject, injectable } from 'inversify';
import { types } from 'services/types';
import { CameraControlConfig } from 'services/core/camera-control/CameraControlConfig';
import { AutoDetectCommand } from './AutoDetectCommand';

class Camera {
  private _model: string;
  private _port: string;

  constructor(model: string, port: string) {
    this._model = model;
    this._port = port;
  }

  get model(): string {
    return this._model;
  }

  get port(): string {
    return this._port;
  }
}

@injectable()
export class AutoDetectCommandHandler {
  @inject(types.CameraControlConfig)
  private readonly config!: CameraControlConfig;

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  async Handle(request: AutoDetectCommand): Promise<Iterable<Camera>> {
    const cameras = await axios.get<Camera[]>(`${this.config.apiBasePath}/cameras`);
    return cameras.data;
  }
}
