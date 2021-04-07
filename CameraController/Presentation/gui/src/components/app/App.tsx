import './App.css';

import CameraIcon from '@material-ui/icons/Camera';
import { ErrorHandler, useErrorHandler } from 'components/common/error-handler/ErrorHandler';
import LoadingFab from 'components/common/loading-fab/LoadingFab';
import { useState } from 'react';

import { CameraModel } from './CameraModel';
import getCameras from './GetCameras';

export default function App(): JSX.Element {
  function DummyInnerComponent() {
    const [cameras, setCameras] = useState<ReadonlyArray<CameraModel>>([]);
    const handleError = useErrorHandler();

    async function handleAutoDetect() {
      return getCameras()
        .then((cameras) => setCameras(cameras))
        .catch((error) => handleError(`Failed to autodetect: ${error.message}`));
    }

    return (
      <>
        <LoadingFab onClick={handleAutoDetect}>
          <CameraIcon />
        </LoadingFab>

        {cameras.map((camera, i) => (
          <div key={i}>
            {camera.model} | {camera.port}
          </div>
        ))}
      </>
    );
  }

  return (
    <div>
      <ErrorHandler>
        <DummyInnerComponent />
      </ErrorHandler>
    </div>
  );
}
