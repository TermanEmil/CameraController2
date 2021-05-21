import axios from 'axios';
import CameraNotFoundError from 'components/common/exceptions/CameraNotFoundError';
import CommunicationError from 'components/common/exceptions/CommunicationError';
import { saveAs } from 'file-saver';
import { StatusCodes } from 'http-status-codes'
import mime from 'mime-types'

import { getCaptureImageUrl } from './urls';

export default async function captureImage(port: string): Promise<void> {
  return axios
    .request({
      url: getCaptureImageUrl(),
      responseType: 'blob',
      params: { port: port }
    })
    .then((response) => {
      const extension = mime.extension(response.data.type);
      saveAs(new Blob([response.data]), `captured-photo.${ extension }`);
    })
    .catch((error) => {
      if (error.response?.status == StatusCodes.NOT_FOUND)
        throw new CameraNotFoundError();
      else
        throw new CommunicationError();
    });
}
