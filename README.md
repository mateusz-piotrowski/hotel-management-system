# Hotel Management System

## Description

This project contains Hotel Management System written in C#.

## Motivation

Practice C# programming language skill.

## Requirements

1. [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
2. [.NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework)

## Installation

1. Clone repository

    ```
    git clone https://github.com/mateusz-piotrowski/hotel-management-system
    ```

1. Open in Visual Studio
2. Build
3. Run

## Build

In projects are included files:
1. `bookings.json`
2. `hotels.json`

Before build, open they preferences and mark `Build Action` = `Content` and `Copy to Output Directory` = `Copy if newer`.

## Commands

1. To check available day use command:

   ```
   Availability(H1, 20240901, SGL)
   ```
   where:
   * H1 - hotel
   * 20240901 - day to check
   * SGL - room type

2. To check available period use command:

   ```
   Availability(H1, 20240901-20240905, SGL)
   ```
    where:
   * H1 - hotel
   * 20240901-20240905 - period to check
   * SGL - room type

3. To check available booking use command:

   ```
   Search(H1, 365, SGL)
   ```
    where:
   * H1 - hotel
   * 365 - day ahead to check
   * SGL - room type

## Contributor

Mateusz Piotrowski

## License

MIT license
