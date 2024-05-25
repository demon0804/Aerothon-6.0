import "./App.css";

import { BrowserRouter, Route, Routes } from "react-router-dom";

import Flightdetails from "./Components/Flightdetails.js";
import LandingPage from "./Components/Landing.js";
import LoginPage from "./Components/Login.js";
import SignupPage from "./Components/Signup.js";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" Component={LandingPage}></Route>
        <Route path="/login" Component={LoginPage}></Route>
        <Route path="/signup" Component={SignupPage}></Route>
        <Route path="/flightdetails" Component={Flightdetails}></Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
