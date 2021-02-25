import * as env from 'env-var';
import { injectable } from 'inversify';
import { CameraControlConfig } from './core/camera-control/CameraControlConfig';

@injectable()
export class EnvCameraControlConfig implements CameraControlConfig {
  get apiBasePath(): string {
    // return env.get('apiCameraControlBasePath').required().asString();
    return 'http://localhost:7000/api';
  }
}

const kek = env.get('REACT_APP_apiCameraControlBasePath').required().asString();
