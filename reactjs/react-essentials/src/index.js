import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter as Router } from "react-router-dom"

// destructuring arrays
// const [importantItem, second] = ["boots", "tent", "headLamp"]
// console.log(importantItem, second);

// managing the state : useState hook

ReactDOM.render(
  <Router>
    <App />
  </Router>
  , document.getElementById('root')
);




// function AppTwo() {
//   return (
//     <h1>This is the Second App</h1>
//   )
// }
// ReactDOM.render(
//   // wrapper but not change in dom element
//   // <React.Fragment>
//   // </React.Fragment>
//   <>
//     <App></App>
//     <AppTwo></AppTwo>
//   </>, document.getElementById('root')
// );
// ReactDOM.render(
//   // what to create
//   // used js to create html element
//   // React.createElement("h1", { style: { color: "blue" } }, "Hello World !"),

//   // too much of nesting > JSX (Javascript as XML) : tag in JS
//   // React.createElement("ul", null, React.createElement("li", null, "Monday"),
//   //   React.createElement("li", null, "Tuesday")),

//   // JSX : Babel behind the scene : https://babeljs.io/
//   // compiling the code 
//   // <ul>
//   //   <li>Monday</li>
//   //   <li>Tuesday</li>
//   // </ul>,

//   // using App component
//   <App></App>,
//   // where we want to put the above in our .html
//   document.getElementById('root')
// );

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();


