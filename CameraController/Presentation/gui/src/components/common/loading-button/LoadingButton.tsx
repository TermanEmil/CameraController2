import { Button } from '@material-ui/core';
import { Theme } from '@material-ui/core';
import CircularProgress from '@material-ui/core/CircularProgress';
import { createStyles } from '@material-ui/core/styles';
import makeStyles from '@material-ui/core/styles/makeStyles';
import { ReactNode, useState } from 'react';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    wrapper: {
      margin: theme.spacing(1),
      position: 'relative',
    },
    buttonProgress: {
      color: 'secondary',
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
      <Button variant="contained" color="primary" disabled={loading} onClick={handleButtonClick}>
        {props.children}
        {loading && <CircularProgress size={24} className={classes.buttonProgress} />}
      </Button>
    </div>
  );
}
