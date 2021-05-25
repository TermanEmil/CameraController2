import { RouteComponentProps } from 'react-router';

export interface CameraViewParams {
  port: string;
}

export default function CameraView({ match }: RouteComponentProps<CameraViewParams>): JSX.Element {
  return <>Camera: {match.params.port}</>;
}
