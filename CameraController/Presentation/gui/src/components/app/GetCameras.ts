import axios from 'axios';
import * as env from 'env-var';
import { CameraModel, getCamerasResponseSchema } from './CameraModel';

export default async function getCameras(): Promise<ReadonlyArray<CameraModel>> {
  const url = env
    .get('REACT_APP_ApiCameraControlBasePath')
    .required()
    .asString();

  const response = await axios
    .get<CameraModel[]>(`${ url }/cameras`)
    .catch(() => { throw new Error('The API did not respond') });

  await getCamerasResponseSchema
    .validate({ cameras: response.data })
    .catch(() => { throw new Error('Unexpected API response') });

  return response.data;
}
