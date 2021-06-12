import './App.css';

import { ErrorHandler } from 'components/common/error-handler/ErrorHandler';
import { BrowserRouter, Route, Switch } from 'react-router-dom';

import CameraList from './CameraList';
import CameraView from './CameraView';

export default function App(): JSX.Element {
  return (
    <div>
      <ErrorHandler>
        <BrowserRouter>
          <Switch>
            <Route exact path="/" component={CameraList} />
            <Route path="/cameras/:port" component={CameraView} />
          </Switch>
        </BrowserRouter>
      </ErrorHandler>
    </div>
  );
}
