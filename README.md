# Flight Navigation Solution

## Introduction

Ensuring safe and efficient flight navigation is paramount in the aviation industry. This project aims to develop a comprehensive software solution for optimal flight route planning and risk mitigation, addressing challenges like GPS signal loss and adverse weather conditions. The solution leverages algorithms to identify optimal flight paths and provides real-time risk assessments and alternative routes.



## Project Overview

The system retrieves flight data from the AviationStackAPI and uses a routing algorithm to find the shortest path and alternate routes between waypoints. It includes a weather-based decision model to determine route safety. The backend is linked to a front-end application and stores user data in a database.

## Technologies

- **Frontend Technologies**: ReactJs, HTML, CSS
- **Backend Technologies**: C#, ASP.NET, .NET Core
- **Routing Algorithms**: Dijkstra's Algorithm, Yen's Algorithm
- **APIs**: AviationStackApi, OpenWeatherApi, GeoApify
- **Database**: PostgreSQL
- **ML Model**: Predictive Analytics, sci-kit-learn, Random Forest (Ensembled Decision Tree)
- **Authentication**: Pbkdf2

## Code Structure (Backend)

- **Controllers**: Contains API endpoint controllers.
- **Services**: Contains business logic and service classes.
- **Models**: Contains data models and entities.
- **Repositories**: Contains data access logic.
- **Utilities**: Contains helper functions and utilities.

## Development

### Data Collection and Management

- **AviationStackApi**: Flight Data
- **OpenWeatherApi**: Weather Data
- **Geoapify**: Geographical Coordinates

### Database

- **PostgreSQL**

### Scenario Identification

- **Risk Factors**: List and describe the various risk factors considered in the project, such as weather conditions and environmental variables.

### Authentication

- **Pbkdf2**

## API Documentation (Backend)

### Deployed Backend Link

[Swagger Documentation](https://skytracker.azurewebsites.net/swagger/index.html)

### Endpoints

- **POST /Users/Signup**: Sign up a new user.
- **POST /Authentication/Login**: Log in a user.
- **GET /flights/{flightId}**: Retrieve flight information.
- **GET /flights/{flightId}/track**: Retrieve the route waypoints of the whole track of the flight.
- **GET /paths**: Get alternate routes between two points as well as the shortest route.

## ML Model

The machine learning model implemented in this project is a Random Forest classifier trained to assess the safety of flight routes based on real-time data, including weather conditions.

### Data Source

- **Source**: Kaggle (Link for dataset)
- **Number of Rows**: ~90,000
- **Features**: Multiple features related to weather conditions

### Analysis and Considerations

- **Rain Presence**: All instances indicate rainy conditions.
- **Temperature and Apparent Temperature**: 8.3°C to 9.5°C
- **Humidity**: 83% to 89%
- **Wind Speed**: 3.9 km/h to 14.3 km/h
- **Wind Bearing**: 250° to 270°
- **Visibility**: 14.9 km to 15.8 km
- **Pressure**: 1015 to 1016.5 millibars

## Routing Algorithms

### Waypoint Class

- Represents a geographical point with latitude and longitude.
- Weather Condition for this data (boolean: [clear, not clear] returned by ML Model)

### Steps to Find the Shortest Path

1. **Graph Class**:
   - **AddEdge(Waypoint source, Waypoint destination, double weight)**: Adds an edge between two waypoints with a given weight.
   - **CreateGraph(Waypoint source, Waypoint destination, List<Waypoint> waypointsBetween)**: Creates a graph using the source, destination, and intermediate waypoints by adding edges between all waypoints.
   - **Dijkstra(Waypoint source, Waypoint destination)**: Implements Dijkstra's algorithm to find the shortest path between the source and destination waypoints.
   - **FindKShortestPaths(Waypoint source, Waypoint destination, int k)**: Finds the K shortest paths from source to destination using a variation of Yen's K-Shortest Paths algorithm.
   - **CalculatePathDistance(List<Waypoint> path)**: Helper method to calculate the total distance of a path.

### Dijkstra's Algorithm

- **Purpose**: Finds the shortest path between a starting node (source) and all other nodes in a weighted graph.
- **How it Works**:
  - Initializes distances from the source to all nodes as infinite, except the source node itself, which is set to zero.
  - Uses a priority queue to explore nodes with the smallest known distance.
  - Updates the distances to neighboring nodes if a shorter path is found through the current node.
  - Continues until all nodes have been visited and the shortest path to the destination node is determined.

### Yen's K-Shortest Paths Algorithm

- **Purpose**: Finds the K shortest paths between a pair of nodes in a weighted graph.
- **How it Works**:
  - Starts by finding the shortest path using Dijkstra's algorithm.
  - Iteratively finds the next shortest path by temporarily modifying the graph (removing edges and nodes) to explore alternative routes.
  - Identifies a "spur node" where the path deviates from previous paths, and generates and evaluates potential new paths.
  - Maintains a list of the K shortest paths found so far and stops when K paths are identified or no more alternative paths are available.

## Deployment

### Key Steps for Deployment

1. **Azure Environment Setup**:
   - Log in to Azure and create a resource group to organize resources.

2. **Web Application Deployment**:
   - Create an App Service Plan and a Web App to host the application.
   - Configure continuous deployment from a GitHub repository.

3. **API Management Configuration**:
   - Set up Azure API Management to handle API requests and responses.
   - Add API operations and configure CORS policies to ensure secure access.

4. **Network Security**:
   - Optionally, whitelist networks to restrict access to the API.

5. **Environment Variable Configuration**:
   - Set environment variables for API keys and other configurations necessary for the application.

6. **Accessing the Application**:
   - The web application and APIs can be accessed via the URLs provided by Azure.

7. **Deploying the Frontend Application**:
   - Deploy the frontend application on Netlify.

## Conclusion and Future Enhancements

The project successfully achieved its goal of enhancing flight navigation mechanisms through the development and implementation of a robust software solution.

### Key Achievements

- **Optimal Route Planning**: Implemented Dijkstra and Yen’s K-Shortest Paths algorithms to find the most efficient and safest routes.
- **Real-Time Risk Assessment**: Leveraged machine learning models for real-time risk assessment based on environmental factors.
- **User-Friendly Interface and Dashboard**: Developed an intuitive user interface that displays optimal flight routes, associated risks, and real-time updates.

### Future Enhancements

- **Enhanced Machine Learning Models**: Incorporate more sophisticated models and larger datasets to improve risk assessments.
- **Real-Time Collaboration Tools**: Implement tools for real-time collaboration between pilots, airlines, and airport authorities.
- **Integration with Air Traffic Control Systems**: Provide real-time updates on airspace restrictions and traffic congestion.

---

This README provides an overview of the flight navigation solution, its components, and deployment instructions. For detailed technical documentation, refer to the respective sections and external links provided.
