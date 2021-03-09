import { useState } from 'react';
import CameraIcon from '@material-ui/icons/Camera';
import LoadingFab from 'components/common/loading-fab/LoadingFab';

import './App.css';
import getCameras from './GetCameras';
import { CameraModel } from './CameraModel';

export default function App(): JSX.Element {
  const [cameras, setCameras] = useState<ReadonlyArray<CameraModel>>([]);
  const [errors, setErrors] = useState<string[]>([]);

  const handleAutoDetect = async () => {
    return getCameras()
      .then((cameras) => {
        setCameras(cameras);
        setErrors([]);
      })
      .catch((error) => {
        setCameras([]);
        setErrors([error.message]);
      });
  };

  return (
    <div>
      <LoadingFab onClick={handleAutoDetect}>
        <CameraIcon />
      </LoadingFab>

      {cameras.map((camera, i) => (
        <div key={i}>
          {camera.model} | {camera.port}
        </div>
      ))}

      <span className="Errors-area">
        {errors.map((error, i) => (
          <div key={i}>{error}</div>
        ))}
      </span>
    </div>
  );
}
