import './App.css';

import { ErrorHandler } from 'components/common/error-handler/ErrorHandler';
import { createBrowserHistory } from 'history';
import { Route, Router } from 'react-router-dom';

import CameraList from './CameraList';
import CameraView from './CameraView';

export default function App(): JSX.Element {
  const history = createBrowserHistory();

  return (
    <div>
      <ErrorHandler>
        <Router history={history}>
          <Route exact path="/" component={CameraList} />
          <Route path="/cameras/:port" component={CameraView} />
        </Router>
      </ErrorHandler>
    </div>
  );
}
