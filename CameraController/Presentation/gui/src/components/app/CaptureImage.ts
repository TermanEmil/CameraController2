import axios from 'axios';
import * as env from 'env-var';
import { saveAs } from 'file-saver';
import mime from 'mime-types'

export default async function captureImage(port: string): Promise<void> {
  const baseUrl = env
    .get('REACT_APP_ApiCameraControlBasePath')
    .required()
    .asString();

  const url = `${ baseUrl }/Cameras/CaptureImageAndDownload`;

  return axios
    .request({
      url: url,
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
