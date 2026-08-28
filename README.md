# ContentParserAPI

The **ContentParserAPI** is a simple backend service built with .NET 10 (Minimal APIs).
It is designed to accept **Base64-encoded** payloads, safely decode them, and process the data
using a dynamically resolved **Strategy Pattern**.

All **POST endpoint** tests are located in the `ContentParserAPI.http` file.

## Technologies & Patterns
- **.NET 10 (C# 14) Minimal API**
- **CsvHelper**: used for CSV parsing within the `CsvStrategy`.
- **Keyed Dependency Injection**: utilizes `IServiceProvider.GetKeyedService` to dynamically route incoming requests to the correct parsing strategy based on the payload type.
- **Pattern Matching**: used for safe and efficient **string-to-enum** mapping for incoming payload types.

## How to Run & Test Locally
This project is designed to be pulled and tested without the need for external tools like Postman.

### Prerequisites
- .NET 10 SDK installed.
- IDE (Visual Studio, VS Code, etc.).

### Run the application and test the endpoint
1. Clone the repository and open the solution file.
2. Build the solution to restore necessary NuGet packages (including **CsvHelper**).
3. Run the application (via the Run/Debug button in your IDE). The API will start on a localhost port.
4. Locate and open the `ContentParserAPI.http` file (in the project's root directory).
5. The `.http` file contains pre-configured `POST` requests with valid and invalid **Base64** payloads (generated via an online encoder).
6. Click the `Send Request` or `Run` button near the test example to send a request and observe the routing process.

### Upcoming Architectural Refactoring
This project is currently undergoing an architectural review:

- Elimination of the Service Locator Anti-Pattern
- Implementation of the Result Pattern
- Global Exception Handling (RFC 7807)
- Single Responsibility Principle (SRP) Enforcement
- Record Types for DTOs