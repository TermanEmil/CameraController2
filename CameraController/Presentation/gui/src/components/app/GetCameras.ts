import axios from 'axios';
import * as env from 'env-var';
import { Camera } from "./Camera";

export async function getCameras(): Promise<ReadonlyArray<Camera>> {
  const url = env.get('REACT_APP_ApiCameraControlBasePath').required().asString();
  const cameras = await axios.get<Camera[]>(`${url}/cameras`);
  return cameras.data;
}
