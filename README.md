# Receipt Processor API

## Overview
This is a simple C# Web API for processing receipts and calculating points based on receipt details. The API is built using ASP.NET Core and includes endpoints for processing receipts and retrieving the calculated points.

## Prerequisites
Ensure you have the following installed:
- [.NET SDK 6.0+](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Visual Studio](https://visualstudio.microsoft.com/) or any preferred IDE
- [Postman](https://www.postman.com/) (optional, for testing)

## Setup Instructions
1. **Clone the Repository**
   ```sh
   git clone <repository-url>
   cd ReceiptProcessor
   ```

2. **Restore Dependencies**
   ```sh
   dotnet restore
   ```

3. **Build the Project**
   ```sh
   dotnet build
   ```

4. **Run the Application**
   ```sh
   dotnet run
   ```
   By default, the API will be available at `http://localhost:5133`.

## Using Swagger UI
Swagger is enabled for easy API documentation and testing.
1. After running the application, open your browser and navigate to:
   ```
   http://localhost:5133/swagger
   ```

2. You will see the available endpoints with options to test them directly.

## API Endpoints

### 1. Process a Receipt
- **Endpoint:** `POST /receipts/process`
- **Description:** Submits a receipt and returns a unique receipt ID.
- **Request Body (JSON):**
  ```json
  {
    "retailer": "Target",
    "total": 35.50,
    "purchaseDate": "2023-10-15",
    "purchaseTime": "14:30",
    "items": [
      { "shortDescription": "Milk", "price": 4.50 },
      { "shortDescription": "Bread", "price": 3.00 }
    ]
  }
  ```
- **Response:**
  ```json
  { "id": "c71f1d10-b17c-4f1a-bc9d-912aa5b5f3cd" }
  ```

### 2. Get Points for a Receipt
- **Endpoint:** `GET /receipts/{id}/points`
- **Description:** Retrieves the total points for a processed receipt.
- **Response Example:**
  ```json
  { "points": 75 }
  ```

## Testing with Postman
1. Open Postman and create a new request.
2. **For Processing a Receipt:**
   - Select `POST`
   - Enter the URL: `http://localhost:5000/receipts/process`
   - Set Headers: `Content-Type: application/json`
   - Enter JSON body and send request.
3. **For Retrieving Points:**
   - Select `GET`
   - Enter URL: `http://localhost:5000/receipts/{id}/points`
   - Replace `{id}` with the actual receipt ID returned from the `POST` request.

## Notes
- The API runs on `http://localhost:5133` by default.
- The in-memory storage (`Dictionary<string, Receipt> receipts`) is used to store receipts temporarily; restarting the application will clear all data.
- The `CalculatePoints` method uses rules to assign points based on retailer name, total amount, item descriptions, and purchase date/time.

## License
This project is open-source and free to use. Modify as needed for your use case.

