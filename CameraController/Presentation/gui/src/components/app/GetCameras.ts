import axios from 'axios';

import { CameraModel, getCamerasResponseSchema } from './CameraModel';
import { getCamerasUrl } from './urls';

export default async function getCameras(): Promise<ReadonlyArray<CameraModel>> {
  const response = await axios
    .get<CameraModel[]>(getCamerasUrl())
    .catch(() => { throw new Error('Failed to communicate with the API') });

  await getCamerasResponseSchema
    .validate({ cameras: response.data })
    .catch(() => { throw new Error('Unexpected API response') });

  return response.data;
}
