import { Theme } from '@material-ui/core';
import CircularProgress from '@material-ui/core/CircularProgress';
import Fab from '@material-ui/core/Fab';
import { createStyles } from '@material-ui/core/styles';
import makeStyles from '@material-ui/core/styles/makeStyles';
import { ReactNode, useState } from 'react';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    wrapper: {
      margin: theme.spacing(1),
      position: 'relative',
    },
    fabProgress: {
      position: 'absolute',
      top: -6,
      left: -6,
      zIndex: 1,
    },
    buttonProgress: {
      position: 'absolute',
      top: '50%',
      left: '50%',
      marginTop: -12,
      marginLeft: -12,
    },
  }),
);

interface Props {
  onClick?(): Promise<void>;
  children?: ReactNode;
}

export default function LoadingFab(props: Props): JSX.Element {
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
      <Fab color="primary" onClick={handleButtonClick}>
        <>{props.children}</>
      </Fab>
      {loading && <CircularProgress size={68} className={classes.fabProgress} />}
    </div>
  );
}
