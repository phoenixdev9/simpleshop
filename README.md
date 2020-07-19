# Simple Shop

> Simple Online Shop build with C# ASP.NET CORE + Angular Typescript

## URL

[YouTube App Demo](https://www.youtube.com/watch?v=p58TuHMb13c)

## Setup & Run App

```
1. run dotnet restore

2. Install SSL

a). dotnet dev-certs https --clean

b). dotnet dev-certs https

c). dotnet dev-certs https --trust

OR Follow https://github.com/Eric-programming/simple-shop/blob/master/InstructionsSSL.txt

3. cd api && dotnet watch run

4. Go to https://localhost:5001

```

## Repository Design Pattern

```
Why?
- Reduce duplicate query logic
- Seperate our application from persistence frameworks (like Entity Framework)
```

## Data Model

![main](https://user-images.githubusercontent.com/54079742/85233227-90ed1680-b3b9-11ea-9ae9-5bbed09d0a3d.PNG)

- Version: 1.0.0
- License: MIT
- Author: Eric
