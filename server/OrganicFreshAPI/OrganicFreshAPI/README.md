# OrganicFreshAPI
### Description
The API provides an interface to access a collection of endpoints from were the developer can interact with a SQL Server database. This readme file serves as a guide to undestand the functionalities and usage of the API.

## Getting Started
To get started with the API, follow the steps below:

Clone the project repository from Github:

```
git clone https://github.com/No-Country/c14-47-t-csharp-react.git
```
Navigate to the csproj file location directory:

```
cd server/OrganicFreshAPI/OrganicFreshAPI/
```
Create a appsettings.json file then set the database config and Cloudinary profile:

```
echo '{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "SQLServerConnection": ""
  },
  "CloudinarySettings": {
    "CloudName": "",
    "ApiKey": "",
    "ApiSecret": ""
  },
  "Jwt": {
    "Issuer": "",
    "Audience": "",
    "Key": ""
  }
}' > appsettings.json
```

Restore the project dependencies using the .NET CLI:

```
dotnet restore
```

#### Start the API server using the .NET CLI:

```
dotnet run
```

The API should now be running locally on http://localhost:5020

## Features
The API offers the following features

### Authorization and Authentication
All API request that intend to persist database changes or intend to retrieve personal information about a user, requires a admin bearer token in the Authorization header

```
Add the following header to the request
Authorization: Bearer 'Valid admin token'
```
In case of a invalid or unauthorized token, the response will be

```
401 Unauthorized || 403 Forbidden. Respectively
```

## Endpoints
#### POST /auth/register 
| Parameter | Type | Description |
| :--- | :--- | :--- |
| `name` | `string` | **Required**.|
| `email` | `string` | **Required**.|
| `password` | `string` | **Required**.|

#### POST /auth/login
| Parameter | Type | Description |
| :--- | :--- | :--- |
| `email` | `string` | **Required**.|
| `password` | `string` | **Required**.|

Response

```
{
  "jwt": "jwt",
  "isAdmin": false
}
```
#### GET /auth/me
**Requires valid token**

Response
```
{
  "name": "string",
  "createdAt": "DateTime",
  "modifiedAt": "DateTime",
  "deletedAt": "DateTime",
  "id": "Guid",
  "email": "string",
  "sales": [
    {
    "saleId": int,
    "userId": string,
    "userName": string,
    "userEmail": string,
    "createdAt": DateTime,
    "modifiedAt": DateTime,
    "deletedAt": DateTime,
    "checkoutDetails": [
      {
        "productId": int,
        "productName": string,
        "saleId": int,
        "quantity": decimal,
        "total": decimal
      }
    ]
  }
  ]
}
```
#### GET /auth/me/admin
**Requires a valid admin token**

Response
```
{
  "name": "string",
  "createdAt": "DateTime",
  "modifiedAt": "DateTime",
  "deletedAt": "DateTime",
  "id": "Guid",
  "email": "string"
}
```

### GET /admin/sales
**Requires a valid admin token**

Response
```
[
  {
    "saleId": int,
    "userId": string,
    "userName": string,
    "userEmail": string,
    "createdAt": DateTime,
    "modifiedAt": DateTime,
    "deletedAt": DateTime,
    "checkoutDetails": [
      {
        "productId": int,
        "productName": string,
        "saleId": int,
        "quantity": decimal,
        "total": decimal
      }
    ]
  }
]
```

### Categories
#### GET /categories

Response
```
[
  .,
  {
    "id": int,
    "name": "string",
    "imageUrl": "string",
    "publicId": "string",
    "active": boolean,
    "createdAt": "DateTime",
    "modifiedAt": "DateTime",
    "deletedAt": "DateTime",
    "products": []
  }
 ., 
]
```
#### GET /categories/{categoryId}
Response
```
  {
    "id": int,
    "name": "string",
    "imageUrl": "string",
    "publicId": "string",
    "active": boolean,
    "createdAt": "DateTime",
    "modifiedAt": "DateTime",
    "deletedAt": "DateTime",
    "products": []
  }
```

#### POST /categories
**Requires a valid admin token**
Content-Type: multipart/form-data
| Parameter | Type | Description |
| :--- | :--- | :--- |
| `name` | `string` | **Required**.|
| `image` | `IFormFile` | **Required**.|

Response

```
  {
    "id": int,
    "name": "string",
    "imageUrl": "string",
    "publicId": "string",
    "active": boolean,
    "createdAt": "DateTime",
    "modifiedAt": "DateTime",
    "deletedAt": "DateTime",
    "products": []
  }
```
#### PUT /categories/{categoryId}
**Requires a valid admin token**
Content-Type: multipart/form-data
| Parameter | Type | Description |
| :--- | :--- | :--- |
| `name` | `string` | **Optional**.|
| `image` | `IFormFile` | **Optional**.|

Response

```
  {
    "id": int,
    "name": "string",
    "imageUrl": "string",
    "publicId": "string",
    "active": boolean,
    "createdAt": "DateTime",
    "modifiedAt": "DateTime",
    "deletedAt": "DateTime",
    "products": []
  }
```
#### DELETE /categories/{categoryId}
**Requires a valid admin token**
Response
```
204 No Content
```

### Products
#### GET /products
Response
```
{
  "id": int,
  "name": "string",
  "price": decimal,
  "categoryId": int,
  "categoryName": "string",
  "active": bool,
  "weightUnit": "string",
  "stock": decimal,
  "imageUrl": "string",
  "publicId": "string",
  "createdAt": "DateTime",
  "modifiedAt": "DateTime",
  "deletedAt": "DateTime"
}
```

#### POST /products
**Requires a valid admin token**
Content-Type: multipart/form-data
| Parameter | Type | Description |
| :--- | :--- | :--- |
| `name` | `string` | **Required**.|
| `image` | `IFormFile` | **Required**.|
| `price` | `decimal` | **Optional**.|
| `categoryId` | `int` | **Required**.|
| `weightUnit` | `string` | **Optional**.|
| `stock` | `decimal` | **Optional**.|

Response
```
{
  "id": int,
  "name": "string",
  "price": decimal,
  "categoryId": int,
  "categoryName": "string",
  "active": bool,
  "weightUnit": "string",
  "stock": decimal,
  "imageUrl": "string",
  "publicId": "string",
  "createdAt": "DateTime",
  "modifiedAt": "DateTime",
  "deletedAt": "DateTime"
}
```

#### PUT /products/{productId}
**Requires a valid admin token**
Content-Type: multipart/form-data
| Parameter | Type | Description |
| :--- | :--- | :--- |
| `name` | `string` | **Optional**.|
| `image` | `IFormFile` | **Optional**.|
| `price` | `decimal` | **Optional**.|
| `categoryId` | `int` | **Optional**.|
| `weightUnit` | `string` | **Optional**.|
| `stock` | `decimal` | **Optional**.|

Response
```
{
  "id": int,
  "name": "string",
  "price": decimal,
  "categoryId": int,
  "categoryName": "string",
  "active": bool,
  "weightUnit": "string",
  "stock": decimal,
  "imageUrl": "string",
  "publicId": "string",
  "createdAt": "DateTime",
  "modifiedAt": "DateTime",
  "deletedAt": "DateTime"
}
```

#### DELETE /products/{productId}
**Requires a valid admin token**
```
204 No Content
```

### POST /checkout
| Parameter | Type | Description |
| :--- | :--- | :--- |
| `productId` | `int` | **Required**.|
| `quantity` | `decimal` | **Required**.|

### Errors
In case of validation error, the response will be
```
400 Bad Request
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "traceId": "00-a60b1f8fc584a464a56b10a3082debfa-cc23209ddc54150a-00",
  "errors": {
    "prop1": [
      "error1",
      .
      .
      .,
      "error n"
    ]
  }
}
```
In case of api error, the response will be
```
40x
{
  "type": "https://tools.ietf.org/html/rfc7235#section-3.1",
  "title": "Description of the error",
  "status": 40x,
  "traceId": "00-72f70847aaf67bf9062ed62f87aecc54-f2340cc1dab4de2b-00"
}
```
In case of an unexpected error, the response will be
```
500 Internal Server Error
```
### Contact
If you have any questions, suggestions, or feedback regarding the API
, please contact the developer at sebasperichon@gmail.com