import axios, { AxiosResponse } from 'axios';
import CameraNotFoundError from 'components/common/exceptions/CameraNotFoundError';
import CommunicationError from 'components/common/exceptions/CommunicationError';
import UnexpectedResponseError from 'components/common/exceptions/UnexpectedResponseError';
import { saveAs } from 'file-saver';
import { StatusCodes } from 'http-status-codes'
import mime from 'mime-types'
import { getCaptureImageUrl } from 'urls';

export default async function captureImage(port: string): Promise<void> {
  let response: AxiosResponse;

  try {
    response = await axios
      .request({
        url: getCaptureImageUrl(),
        responseType: 'blob',
        params: { port: port }
      });
  }
  catch (error) {
    if (error.response?.status == StatusCodes.NOT_FOUND)
      throw new CameraNotFoundError();
    else
      throw new CommunicationError();
  }

  if (!response.data || !response.data.type)
    throw new UnexpectedResponseError();

  const extension = mime.extension(response.data.type);
  if (!extension)
    throw new UnexpectedResponseError();

  saveAs(new Blob([response.data]), `captured-photo.${ extension }`);
}
