import * as env from 'env-var';

function getBaseUrl(): string {
  return env
    .get('REACT_APP_ApiCameraControlBasePath')
    .required()
    .asString();
}

export function getCamerasUrl(): string {
  return `${getBaseUrl()}/cameras`;
}

export function getCaptureImageUrl(): string {
  return `${getBaseUrl()}/Cameras/CaptureImageAndDownload`;
}
