import './App.css';
import { useState } from 'react';
import getCameras from './GetCameras';
import { CameraModel } from './CameraModel';

export default function App(): JSX.Element {
  const [cameras, setCameras] = useState<ReadonlyArray<CameraModel>>([]);
  const [errors, setErrors] = useState<string[]>([]);

  const handleAutoDetect = () =>
    getCameras()
      .then((cameras) => setCameras(cameras))
      .then(() => setErrors([]))
      .catch((error) => setErrors([error.message]));

  return (
    <div>
      <button onClick={handleAutoDetect}>Auto Detect</button>
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
