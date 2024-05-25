const API_URL = "https://skytracker.azurewebsites.net";

export const getFlight = async (payload) => {
  try {
    const response = await fetch(`${API_URL}/Flights/${payload}`, {
      mode: "cors",
      method: "GET",
      headers: { "Content-Type": "application/json" },
    });

    console.log(response);
    if (!response.ok) {
      throw new Error("Network response was not ok");
    }

    const data = await response.json();
    console.log(data);
    return data;
  } catch (error) {
    console.error("Error Fetching user:", error);
    throw error;
  }
};

export const getFlightPath = async (payload) => {
  try {
    const response = await fetch(`${API_URL}/Flights/${payload}/track`, {
      mode: "cors",
      method: "GET",
      headers: { "Content-Type": "application/json" },
    });
    if (!response.ok) {
      throw new Error("Network response was not ok");
    }
    const data = await response.json();
    console.log(data);
    return data;
  } catch (error) {
    console.error("Error Fetching flight data:", error);
    throw error;
  }
};

export const getNewFlightPath = async (
  sourcelat,
  sourcelong,
  destinationLat,
  destinationLong
) => {
  try {
    const response = await fetch(
      `${API_URL}/flights/paths?sourcelat=${sourcelat}&sourcelong=${sourcelong}&destinationLat=${destinationLat}&destinationLong=${destinationLong}`,
      {
        mode: "cors",
        method: "GET",
        headers: { "Content-Type": "application/json" },
      }
    );
    if (!response.ok) {
      throw new Error("Network response was not ok");
    }
    const data = await response.json();
    console.log(data);
    return data;
  } catch (error) {
    console.error("Error Fetching flight data:", error);
    throw error;
  }
};
