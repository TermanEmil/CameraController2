import './App.css';

import { ListItemSecondaryAction } from '@material-ui/core';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import PhotoCameraIcon from '@material-ui/icons/PhotoCamera';
import { useErrorHandler } from 'components/common/error-handler/ErrorHandler';
import LoadingButton from 'components/common/loading-button/LoadingButton';
import LoadingIconButton from 'components/common/loading-icon-button/LoadingIconButton';
import { useState } from 'react';
import { useHistory } from 'react-router';

import { CameraModel } from './CameraModel';
import captureImage from './captureImage';
import getCameras from './getCameras';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      width: '100%',
      backgroundColor: theme.palette.background.paper,
    },
  }),
);

export default function CameraList(): JSX.Element {
  const [cameras, setCameras] = useState<ReadonlyArray<CameraModel>>([]);
  const handleError = useErrorHandler();

  async function handleAutoDetect() {
    return getCameras()
      .then((cameras) => setCameras(cameras))
      .catch((error) => handleError(`Failed to autodetect: ${error.message}`));
  }

  async function handleCaptureImage(port: string) {
    return captureImage(port).catch((error) => handleError(`Failed to capture image: ${error.message}`));
  }

  const classes = useStyles();
  const history = useHistory();

  function handleCameraOnClick(port: string) {
    return () => history.push(`/cameras/${port}`);
  }

  return (
    <div>
      <div className={classes.root}>
        <List component="nav" aria-label="main mailbox folders">
          {cameras.map((camera, i) => (
            <ListItem key={i} button onClick={handleCameraOnClick(camera.port)}>
              <ListItemText primary={camera.model} secondary={camera.port} />
              <ListItemSecondaryAction>
                <LoadingIconButton edge="end" aria-label="capture" onClick={() => handleCaptureImage(camera.port)}>
                  <PhotoCameraIcon />
                </LoadingIconButton>
              </ListItemSecondaryAction>
            </ListItem>
          ))}
        </List>
      </div>
      <LoadingButton variant="contained" color="primary" onClick={handleAutoDetect}>
        Autodetect
      </LoadingButton>
    </div>
  );
}
