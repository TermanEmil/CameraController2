import './App.css';

import { IconButton, ListItemSecondaryAction } from '@material-ui/core';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import PhotoCameraIcon from '@material-ui/icons/PhotoCamera';
import { ErrorHandler, useErrorHandler } from 'components/common/error-handler/ErrorHandler';
import LoadingButton from 'components/common/loading-button/LoadingButton';
import React, { useState } from 'react';

import { CameraModel } from './CameraModel';
import captureImage from './CaptureImage';
import getCameras from './GetCameras';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      width: '100%',
      backgroundColor: theme.palette.background.paper,
    },
  }),
);

export default function App(): JSX.Element {
  function DummyInnerComponent() {
    const [cameras, setCameras] = useState<ReadonlyArray<CameraModel>>([]);
    const handleError = useErrorHandler();

    async function handleAutoDetect() {
      return getCameras()
        .then((cameras) => setCameras(cameras))
        .catch((error) => handleError(`Failed to autodetect: ${error.message}`));
    }

    async function handleCaptureImage(port: string) {
      return captureImage(port).catch((error) => handleError(`Failed to capture image: ${error}`));
    }

    const classes = useStyles();

    return (
      <>
        <div className={classes.root}>
          <List component="nav" aria-label="main mailbox folders">
            {cameras.map((camera, i) => (
              <ListItem key={i}>
                <ListItemText primary={camera.model} secondary={camera.port} />
                <ListItemSecondaryAction>
                  <IconButton edge="end" aria-label="capture" onClick={() => handleCaptureImage(camera.port)}>
                    <PhotoCameraIcon />
                  </IconButton>
                </ListItemSecondaryAction>
              </ListItem>
            ))}
          </List>
        </div>
        <LoadingButton onClick={handleAutoDetect}>Autodetect</LoadingButton>
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