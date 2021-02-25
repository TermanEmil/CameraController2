import './App.css';
import React, { Component, MouseEvent } from 'react';
import { container } from 'services/inversify.config';
import { AutoDetectCommandHandler } from 'services/core/camera-control/auto-detect/AutoDetectCommandHandler';
import { AutoDetectCommand } from 'services/core/camera-control/auto-detect/AutoDetectCommand';

class App extends Component {
  async handleAutoDetect(): Promise<void> {
    const handler = container.get<AutoDetectCommandHandler>(AutoDetectCommandHandler);
    const cameras = await handler.Handle(new AutoDetectCommand());
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
