import './App.css';
import { Component } from 'react';
import { getCameras } from './GetCameras';

class App extends Component {
  async handleAutoDetect(): Promise<void> {
    const cameras = await getCameras();
    console.log(cameras);
  }

  render(): JSX.Element {
    return (
      <div>
        <button onClick={this.handleAutoDetect}>Auto Detect</button>
      </div>
    );
  }
}

export default App;
