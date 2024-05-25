import React from "react";
import "./Weatherdetails.css";
import { UilTemperature, UilTear, UilWind } from "@iconscout/react-unicons";
import CompressOutlinedIcon from "@mui/icons-material/CompressOutlined";
import VisibilityOutlinedIcon from "@mui/icons-material/VisibilityOutlined";
import StormSharpIcon from "@mui/icons-material/StormSharp";
import ThermostatSharpIcon from "@mui/icons-material/ThermostatSharp";
import { iconUrlFromCode } from "../Services/Weatherservices";

function Weatherdetails({ weather, status, topic }) {

  return (
    <div className="weatherdetails">
      <h2>{topic}</h2>
      <div className="coords">
        <p>lat: {weather?.lat}</p>
        <p>lon: {weather?.lon}</p>
        <p>
          {weather?.name}, {weather?.country}
        </p>
      </div>

      <div className="weatherdata">
        <div className="deatil">
          <img src={iconUrlFromCode(weather?.icon)} alt="" />
          <p>{weather?.details}</p>
        </div>
        <div className="temps">
          <div className="icon-data">
            <UilTemperature style={{ color: "black" }} />
            <p>temp: {weather?.temp?.toFixed() - 273}°C</p>
          </div>
          <div className="icon-data">
            <ThermostatSharpIcon style={{ color: "black" }} />
            <p>feels like: {weather?.feels_like?.toFixed() - 273}°C</p>
          </div>
        </div>
        <div className="other-details">
          <div className="icon-data">
            <UilTear style={{ color: "black" }} />
            <p>humidity: {weather?.humidity}%</p>
          </div>
          <div className="icon-data">
            <UilWind style={{ color: "black" }} />
            <p>Wind: {weather?.speed} km/h</p>
          </div>
          <div className="icon-data">
            <StormSharpIcon style={{ color: "black" }} />
            <p>Gust: {weather?.gust} kts</p>
          </div>
          <div className="icon-data">
            <CompressOutlinedIcon style={{ color: "black" }} />
            <p>Pressure: {weather?.pressure} P</p>
          </div>
          <div className="icon-data">
            <VisibilityOutlinedIcon style={{ color: "black" }} />
            <p>Visibility: {weather?.visibility}</p>
          </div>
        </div>
      </div>
      <div className="status">
        <p>status: {status}</p>
      </div>
    </div>
  );
}

export default Weatherdetails;
