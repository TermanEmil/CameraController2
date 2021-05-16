import { Button, ButtonProps } from '@material-ui/core';
import CircularProgress from '@material-ui/core/CircularProgress';
import { createStyles } from '@material-ui/core/styles';
import makeStyles from '@material-ui/core/styles/makeStyles';
import { useState } from 'react';

const useStyles = makeStyles(() =>
  createStyles({
    buttonProgress: {
      position: 'absolute',
      top: '50%',
      left: '50%',
      marginTop: -12,
      marginLeft: -12,
    },
  }),
);

export interface LoadingButtonProps extends ButtonProps {
  onClick?(): Promise<void>;
}

export default function LoadingButton(props: LoadingButtonProps): JSX.Element {
  const classes = useStyles();
  const [loading, setLoading] = useState(false);

  const handleButtonClick = async () => {
    if (!loading) {
      setLoading(true);

      return props.onClick?.().finally(() => setLoading(false));
    }
  };

  return (
    <Button {...props} disabled={loading} onClick={handleButtonClick}>
      {loading && <CircularProgress size={24} color="primary" className={classes.buttonProgress} />}
      {props.children}
    </Button>
  );
}
