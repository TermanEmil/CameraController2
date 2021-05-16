import { CircularProgress, createStyles, Fade, IconButton, IconButtonProps, makeStyles } from '@material-ui/core';
import { useState } from 'react';

const useStyles = makeStyles(() =>
  createStyles({
    wrapper: {
      position: 'relative',
    },
    buttonProgress: {
      position: 'absolute',
      left: 10,
      top: 10,
    },
  }),
);

export interface LoadingIconButtonProps extends IconButtonProps {
  onClick?(): Promise<void>;
}

export default function LoadingIconButton(props: LoadingIconButtonProps): JSX.Element {
  const classes = useStyles();
  const [loading, setLoading] = useState(false);

  const handleButtonClick = async () => {
    if (!loading) {
      setLoading(true);

      return props.onClick?.().finally(() => setLoading(false));
    }
  };

  return (
    <div className={classes.wrapper}>
      {loading && <CircularProgress size={26} color="primary" className={classes.buttonProgress} />}

      <Fade in={!loading}>
        <IconButton {...props} disabled={loading} onClick={handleButtonClick}>
          {props.children}
        </IconButton>
      </Fade>
    </div>
  );
}
