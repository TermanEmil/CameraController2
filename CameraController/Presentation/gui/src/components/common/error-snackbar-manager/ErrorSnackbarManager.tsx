import { Grow, Snackbar } from '@material-ui/core';
import { TransitionProps } from '@material-ui/core/transitions';
import { Alert } from '@material-ui/lab';
import React, { SyntheticEvent, useEffect, useState } from 'react';

interface ErrorSnackbarManagerProps {
  errorMessage: string;
  errorId: number;
}

export default function ErrorSnackbarManager(props: ErrorSnackbarManagerProps): JSX.Element {
  const [open, setOpen] = useState(false);

  const handleClose = (event?: SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  };

  useEffect(() => {
    if (props.errorMessage) {
      setOpen(true);
    }
  }, [props.errorMessage, props.errorId]);

  function Transition(props: TransitionProps) {
    return <Grow {...props}></Grow>;
  }

  return (
    <>
      <Snackbar
        open={open}
        TransitionComponent={Transition}
        autoHideDuration={5000}
        onClose={handleClose}
        anchorOrigin={{
          vertical: 'top',
          horizontal: 'right',
        }}
      >
        <Alert onClose={handleClose} severity="error">
          {props.errorMessage}
        </Alert>
      </Snackbar>
    </>
  );
}
