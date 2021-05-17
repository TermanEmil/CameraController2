import axios from 'axios';
import { saveAs } from 'file-saver';
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
      if (error.response?.status == 404)
        throw new Error('The API responded with Not Found');
      else
        throw new Error('Failed to communicate with the API');
    });
}
