import "reflect-metadata";
import { Container } from "inversify";
import { CameraControlConfig } from "services/core/camera-control/CameraControlConfig";
import { AutoDetectCommandHandler } from "./core/camera-control/auto-detect/AutoDetectCommandHandler";
import { EnvCameraControlConfig } from "./EnvCameraControlConfig";
import { types } from "./types";

const container = new Container();

container
  .bind<CameraControlConfig>(types.CameraControlConfig)
  .to(EnvCameraControlConfig);

container
  .bind<AutoDetectCommandHandler>(AutoDetectCommandHandler)
  .toSelf();

export { container };
