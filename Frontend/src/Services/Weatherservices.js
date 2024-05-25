const API_KEY = "ee34634b89efc3f18413b417b58c801c";
const BASE_URL = "https://api.openweathermap.org/data/2.5";

const getWeatherData = (infoType, searchParams) => {
  const url = new URL(BASE_URL + "/" + infoType);
  url.search = new URLSearchParams({ ...searchParams, appid: API_KEY });

  return fetch(url)
    .then((res) => res.json())
    .then((data) => data);
};

const formatCurrentWeather = (data) => {
  console.log(data);
  if (data.cod !== 200) {
    alert("Place not Found");
    return;
  }

  const {
    coord: { lat, lon },
    main: { temp, feels_like, temp_min, temp_max, humidity, pressure },
    name,
    visibility,
    dt,
    sys: { country },
    weather,
    wind: { speed, gust },
  } = data;

  const { main: details, icon } = weather[0];
  return {
    lat,
    lon,
    temp,
    feels_like,
    temp_min,
    temp_max,
    humidity,
    visibility,
    pressure,
    name,
    country,
    dt,
    details,
    icon,
    speed,
    gust,
  };
};

const getFormattedWeatherdata = async (searchParams) => {
  const formattedCurrentWeather = await getWeatherData(
    "weather",
    searchParams
  ).then(formatCurrentWeather);

  return { ...formattedCurrentWeather };
};

const iconUrlFromCode = (code) =>
  `http://openweathermap.org/img/wn/${code}@2x.png`;

export default getFormattedWeatherdata;
export { iconUrlFromCode };
