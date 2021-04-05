import React, { Component } from 'react';

type ClockState = {
  time: Date
}

export class Counter extends React.Component<{}, ClockState>{

  tick() {
    this.setState({
      time: new Date()
    });
  }

  componentWillMount() {
    this.tick();
  }

  componentDidMount() {
    setInterval(() => this.tick(), 1000);
  }

  render() {
    return <div>Hello world !! Time now is {this.state.time.toString()}</div>
  }
}