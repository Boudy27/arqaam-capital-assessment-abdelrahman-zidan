# Arqaam Capital – Watchlist API

A RESTful Web API built with **ASP.NET Core** for managing watchlist items and identifying the best pair of items based on their combined target prices.

## Features

- Create watchlist items with input validation.
- Retrieve all watchlist items.
- Find the pair of watchlist items with the highest combined target price that does not exceed a specified target total.
- Resolve ties by selecting the alphabetically earliest pair of stock symbols.

## Tech Stack

- C#
- .NET / ASP.NET Core Web API
- In-memory data storage
- Git and GitHub

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/Boudy27/arqaam-capital-assessment-abdelrahman-zidan.git
cd arqaam_capital
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Build the application

```bash
dotnet build
```

### 4. Run the API

```bash
dotnet run
```

No database setup is required.

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/watchlist-items` | Retrieve all items |
| POST | `/watchlist-items` | Create an item |
| GET | `/watchlist-items/best-pair?targetTotal={value}` | Find the best pair for a target total price |

## Example Requests

### Create a Watchlist Item

**POST** `/watchlist-items`

Content-Type: `application/json`

```json
{
  "symbol": "AAPL",
  "targetPrice": 150.00,
  "note": "Apple stock"
}
```

Expected response: `201 Created`.

### Retrieve All Items

**GET** `/watchlist-items`

Expected response: `200 OK`.

### Find the Best Pair

**GET** `/watchlist-items/best-pair?targetTotal=200`

The endpoint returns the pair with the highest combined target price that does not exceed the requested target total.

Example successful response:

```json
{
  "items": [
    {
      "symbol": "AAPL",
      "targetPrice": 80.00,
      "note": "Apple stock"
    },
    {
      "symbol": "MSFT",
      "targetPrice": 120.00,
      "note": "Microsoft stock"
    }
  ],
  "combinedTargetPrice": 290.00,
  "message": null
}
```

If no qualifying pair exists, the API returns:

```json
{
  "items": [],
  "combinedTargetPrice": null,
  "message": "No matching pair"
}
```

## Validation and Error Handling

The API enforces the following validation rules:

| Field | Validation |
|---|---|
| Symbol | Required |
| Symbol | Cannot be empty or whitespace only |
| Symbol | Must be between 1 to 10 characters |
| TargetPrice | Required |
| TargetPrice | Must be greater than zero |
| Note | Optional |
| Note | Maximum length of 250 characters |

Additional business rules:

- Duplicate stock symbols are rejected.
- Symbol comparison is case-insensitive.
- Symbols are trimmed and normalized to uppercase before storage.

### Expected HTTP Status Codes

| Status Code | Description |
|---|---|
| 200 | Request successful |
| 201 | Watchlist item created |
| 400 | Bad Request |
| 409 | Duplicate stock symbol |

## Best-Pair Algorithm

The best-pair functionality finds two distinct watchlist items whose combined target prices are as close as possible to the requested target total without exceeding it.

The implementation uses a two-pass approach:

**Pass 1 – Find the highest qualifying sum**

1. Sort the watchlist items by target price in ascending order.
2. Initialize two pointers: one at the beginning and one at the end.
3. Calculate the combined target price.
4. If the sum is within the target total, record it if it is better than the previous best and move the left pointer forward.
5. If the sum exceeds the target total, move the right pointer backward.
6. Continue until the pointers meet.

**Pass 2 – Resolve alphabetical ties**

Once the highest qualifying sum is known:

1. Group watchlist items by their target prices using a dictionary.
2. Sort the watchlist items alphabetically by symbol.
3. Iterate through the alphabetically sorted items.
4. Calculate the required complementary price for each item by subtracting its target price from the best sum.
5. Use the dictionary to check whether an item with the required price exists.
6. Select the first valid pair encountered, ensuring the alphabetically earliest pair is returned.

This approach avoids examining every possible pair and uses dictionary lookups to find matching items efficiently.If no qualifying pair exists, the API returns an empty items collection, a null combined target price, and the message `"No matching pair"`.

## Assumptions

- While it is good coding practice to implement full CRUD behaviour for a Model/Entity, I opted out of it here for the sake of keeping the project as simple as possible while fulfilling the requirements.

## Issues Encountered

While implementing the best pair algorithm, I initially implemented it to be purely functional and not taking time complexity into consideration. This resulted in the algorithm having an initial time complexity of O(n^2) due to me not taking the alphabetical sorting into consideration at the start so that meant the algorithm did a double pass on the list after finding the biggest sum. Using AI tools I found a better way to implement this by using the dictionary to find the earliest possible pair instead of having to loop on both pairs.

## Future Improvements

Given more development time and assuming I would add more features, I would consider:

- Attempting to find a better algorithm for the best-pair that doesn't have to find the earliest alphabetical pair seperately.
- Adding CRUD features and endpoints.
- Implementing databases such as MongoDB and Redis for scalability.


## Tools and Resources Used

The following tools and resources were used or should be documented as applicable to the actual development process:

- **.NET SDK:** Application development, building, and execution.
- **ASP.NET Core:** Web API framework.
- **Git:** Version control.
- **GitHub:** Source code hosting.
- **ChatGPT:** AI-assisted development guidance, code explanations, algorithm analysis, and documentation assistance.
- **IDE:** Visual Studio Code
- **Search engines:** Google
- **IDE extensions:** C# devkit, REST Client
- **Video Links:** [ASP.NET Core Full Course For Beginners (.NET 10)](https://youtu.be/YbRe4iIVYJk?si=BNMZFymhj3jMx-xW)

