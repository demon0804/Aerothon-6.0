import { useNavigate } from "react-router-dom";

const API_URL = "https://skytracker.azurewebsites.net";

export const AuthUser = async (payload) => {
  try {
    const response = await fetch(`${API_URL}/Authentication/Login`, {
      mode: "cors",
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    });
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

export const createUser = async (userData) => {
  const response = await fetch(`${API_URL}/Users/Signup`, {
    mode: "cors",
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(userData),
  })
    .then((response) => {
      const data = response.data;
      return data;
    })
    .catch((error) => {
      throw error;
    });
};
