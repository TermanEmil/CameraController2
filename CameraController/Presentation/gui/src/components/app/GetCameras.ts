import axios from 'axios';
import CommunicationError from 'components/common/exceptions/CommunicationError';
import UnexpectedResponseError from 'components/common/exceptions/UnexpectedResponseError';
import { getCamerasUrl } from 'urls';

import { CameraModel, getCamerasResponseSchema as camerasResponseSchema } from './CameraModel';

export default async function getCameras(): Promise<ReadonlyArray<CameraModel>> {
  const response = await axios
    .get<CameraModel[]>(getCamerasUrl())
    .catch(() => { throw new CommunicationError() });

  await camerasResponseSchema
    .validate({ cameras: response.data })
    .catch(() => { throw new UnexpectedResponseError() });

  return response.data;
}
