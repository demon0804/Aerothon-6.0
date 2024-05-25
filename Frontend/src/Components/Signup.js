import React, { useState } from "react";
import "./Signup.css";
import { useNavigate } from "react-router-dom";
import ArrowBackSharpIcon from "@mui/icons-material/ArrowBackSharp";
import { createUser } from "../Services/AuthServices";

function Signup() {
  const navigate = useNavigate();
  let [first, setFirst] = useState("");
  let [last, setLast] = useState("");
  let [email, setEmail] = useState("");

  let [user, setUser] = useState("");
  let [password, setPassword] = useState("");
  let [repassword, setRepassword] = useState("");

  function LoginPage() {
    navigate("/login");
  }

  const Register = async () => {
    if (!first || !last || !email || !user || !password || !repassword)
      alert("enter valid details");
    else if (password.length < 8)
      alert("password must be of size more than or equal to 8");
    else if (password !== repassword) alert("password didnt matched");
    else {
      const newUser = {
        firstName: first,
        lastName: last,
        userName: user,
        password: password,
        email: email,
      };

      console.log(newUser);

      try {
        const createdUser = await createUser(newUser);
        console.log(createdUser);
        alert("User Signed Up Successfully");
        navigate("/login");
        setFirst("");
        setLast("");
        setEmail("");
        setUser("");
        setPassword("");
        setRepassword("");
      } catch (error) {
        console.error("Error creating user", error);
      }
    }
  };

  return (
    <div className="sign-box">
      <ArrowBackSharpIcon className="home" onClick={LoginPage} />
      <h2>Register Here</h2>
      <div className="log-text">
        <input
          type="text"
          value={first}
          onChange={(e) => setFirst(e.target.value)}
          name="first name"
          placeholder="First Name"
        />
        <input
          type="text"
          value={last}
          onChange={(e) => setLast(e.target.value)}
          name="last name"
          placeholder="Last Name"
        />
        <input
          type="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          name="email"
          placeholder="Email"
        />
        <input
          type="text"
          value={user}
          onChange={(e) => setUser(e.target.value)}
          name="username"
          placeholder="Username"
        />
        <input
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          name="password"
          placeholder="Password"
        />
        <input
          type="password"
          value={repassword}
          onChange={(e) => setRepassword(e.target.value)}
          name="Re-password"
          placeholder="Confirm Password"
        />
      </div>
      <div className="login-btns">
        <button className="btn" onClick={Register}>
          Sign up
        </button>
        <p onClick={LoginPage} className="link">
          Login
        </p>
      </div>
    </div>
  );
}

export default Signup;
