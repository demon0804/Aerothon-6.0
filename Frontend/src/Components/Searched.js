import React from "react";
import ConnectingAirportsOutlinedIcon from "@mui/icons-material/ConnectingAirportsOutlined";
import { useNavigate } from "react-router-dom";
import "./Searched.css";

function Searched({
  name,
  fid,
  departure,
  source,
  destination,
  lattitude,
  longitude,
}) {
  const navigate = useNavigate();

  function Flightdetails() {
    navigate("/flightdetails", {
      state: {
        id: fid,
        destination: destination,
        source: source,
        name: name,
        lattitude: lattitude,
        longitude: longitude,
      },
    });
  }

  return (
    <div className="searched" onClick={Flightdetails}>
      <h3>Flight id: {fid}</h3>
      <div className="details">
        <div className="sc">
          <p>{departure}</p>
          <p>From: {source}</p>
        </div>
        <ConnectingAirportsOutlinedIcon />
        <div className="dest">
          <p>19:20</p>
          <p>To: {destination}</p>
        </div>
      </div>
    </div>
  );
}

export default Searched;
