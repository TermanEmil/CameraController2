import './App.css';
import { useState } from 'react';
import getCameras from './GetCameras';
import Camera from './Camera';

export default function App(): JSX.Element {
  const [cameras, setCameras] = useState<ReadonlyArray<Camera>>([]);
  const handleAutoDetect = async () => setCameras(await getCameras());

  return (
    <div>
      <button onClick={handleAutoDetect}>Auto Detect</button>
      {cameras.map((camera, i) => (
        <div key={i}>
          {camera.model} | {camera.port}
        </div>
      ))}
    </div>
  );
}
