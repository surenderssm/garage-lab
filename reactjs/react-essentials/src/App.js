// import React, { useState, useEffect } from "react"
import React from "react"
import './App.css';
import { Routes, Route } from "react-router-dom"
import {
  Home, About, Events,
  Contact, Whoops404, Services,
  CompanyHistory, Location
} from "./pages"


function App() {
  return (
    <div>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/about" element={<About />} >
          <Route path="/services" element={<Services />} />
          <Route path="/history" element={<CompanyHistory />} />
          <Route path="/location" element={<Location />} />
        </Route>
        <Route path="/events" element={<Events />} />
        <Route path="/contact" element={<Contact />} />
        <Route path="*" element={<Whoops404 />} />
      </Routes>
    </div>);
}


// https://api.github.com/users/sumali
// testing platform

// function App({ login }) {
//   return (
//     <div>
//       <h1>Hello for test</h1>
//     </div>);
// }

// https://api.github.com/users/sumali
// testing platform

// function App({ login }) {

//   const [data, setData] = useState(null);
//   const [loading, setLoading] = useState(false);
//   const [error, setError] = useState(null);

//   useEffect
//     (() => {
//       // fetch(`https://api.github.com/users/${login}`)
//       if (!login) return;
//       setLoading(true);
//       fetch(`https://jsonplaceholder.typicode.com/todos/${login}`)
//         .then(response => response.json())
//         .then(setData)
//         .then(() => setLoading(false))
//         .catch(setError)
//     }, [login]);

//   if (loading)
//     return <h1>Loading...</h1>
//   if (error)
//     return <pre>{JSON.stringify(error, null, 2)}</pre>
//   if (!data)
//     return null;

//   if (data) {
//     // return <div>{JSON.stringify(data)}</div>
//     return (
//       <div>
//         <h1>{data.title}</h1>
//         <p>{data.id}</p>
//       </div>
//     )
//   }

//   return <div>No User</div>
// }

// function App() {
//   // useReducer

//   // const [checked, setChecked] = useState(false);
//   const [checked, toggle] = useReducer(
//     (checked) => !checked,
//     false
//   );

//   return (
//     <>
//       <input
//         type="checkbox"
//         value={checked}
//         onChange={toggle}
//       />
//       <p>
//         {checked ? "checked" : "not checked"}
//       </p>
//     </>
//   );
// }

// function App() {
//   // useState returns arrays
//   const [emotion, setEmotion] = useState("Happy");

//   const [secondary, setSecondary] = useState("tired");

//   useEffect(() => {
//     console.log(`CurrentState : ${emotion}`);
//   }, [emotion]);

//   useEffect(() => {
//     console.log(`Secondary State : ${secondary}`);
//   }, [secondary]);

//   return (
//     <>
//       <h1>Current emotion is {emotion} and {secondary}.</h1>
//       <button onClick={() => setEmotion("Happy")}>Happy</button>
//       <button onClick={() => setEmotion("Sad")}>Sad</button>
//       <button onClick={() => setEmotion("Enthusiastic")}>
//         Enthuse
//       </button>
//     </>
//   );
// }

// function SecretComponent() {
//   return <h1>secret for auth users</h1>
// }

// function RegularComponent() {
//   return <h1> Everyone can see this component</h1>
// }

// // destructuring objects
// function App({ authorized }) {
//   // conditional rendering
//   return (
//     <>
//       {authorized ? <SecretComponent /> : <RegularComponent />}
//     </>
//   )
//   // < SecretComponent />
//   // if (props.authorized) {
//   //   return <SecretComponent />;
//   // }
//   // else {
//   //   return <RegularComponent />;
//   // }
// }

export default App;
