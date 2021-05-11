import './App.css';

import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import CameraIcon from '@material-ui/icons/Camera';
import { ErrorHandler, useErrorHandler } from 'components/common/error-handler/ErrorHandler';
import LoadingFab from 'components/common/loading-fab/LoadingFab';
import { useState } from 'react';

import { CameraModel } from './CameraModel';
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

    const classes = useStyles();

    return (
      <>
        <LoadingFab onClick={handleAutoDetect}>
          <CameraIcon />
        </LoadingFab>

        <div className={classes.root}>
          <List component="nav" aria-label="main mailbox folders">
            {cameras.map((camera, i) => (
              <ListItem key={i} button>
                <ListItemText primary={camera.model} secondary={camera.port} />
              </ListItem>
            ))}
          </List>
        </div>
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
