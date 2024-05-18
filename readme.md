[![Build Status](https://dracorsa.visualstudio.com/Thuria%20-%20Zitidar/_apis/build/status/DracoRSA.Thuria.Zitidar?branchName=master)](https://dracorsa.visualstudio.com/Thuria%20-%20Zitidar/_build/latest?definitionId=2&branchName=master)

# Thuria - Zitidar

- [Thuria - Zitidar](#thuria---zitidar)
  - [Overview](#overview)
  - [Features](#features)
    - [Extensions](#extensions)
      - [DateTime](#datetime)
      - [Enumerable\<\>](#enumerable)
      - [String](#string)
      - [Type](#type)
      - [Data Reader (IDataReader)](#data-reader-idatareader)
    - [Cache](#cache)
    - [Web API](#web-api)

## Overview
Thuria Zitidar is a set of nuget packages that implements some of the basic plumbing that is required to create a Dotnet Core application that could be using a combination of Dependency Injection, the Akka Actor Framework, and Database Access or all of it.

## Features

* **Extensions** - Various extensions to just add functionality to types like DateTime, String, Type, etc
* **Caching** - A set of additional functionality to add structuremap functionality to any application (Bootstrapper, IOC Container wrapper, etc)
* **Web API Extensions** - A set of extensions and interfaces to make it easier to register and configure your Web API endpoints

### Extensions
The following extensions are part of the package:

#### DateTime
1. StartOfDay - Return the given date with start of day time set
2. EndOfDay - Return the given date with end of day time set

#### Enumerable\<>
1. ForEach\<T> - For each item in a collection, perform the given action
2. IsEmpty\<T> - Determine if the given collection is empty
3. GetAllAsString\<T> - Return a collection of string representations of all the items in a given collection
4. And\<T> - Concatenate the items in an array to a given collection

#### String
1. RemoveSpaceAndCharacters - Remove all spaces and characters from a string. Leave only numbers.
2. CamelCase - Convert a string to a camel case string
3. PascalCase - Convert a string to a pascal case string
4. EscapeQuotes - Escape the quotes in a string

#### Type
1. GetPropertyValue - Get the value of a specified property on an object
2. SetPropertyValue - Set the value of property on a given object
3. DoesPropertyExist - Determine if a property exists on a given object
4. GetDefaultData - Retrieve the default data for a given Type

#### Data Reader (IDataReader)
1. GetValue - Get a column value from the Data Reader
2. GetValue\<T> - - Get a column value from the Data Reader of Type \<T>
---

### Cache
The Cache package provides caching functionality with some basic cache expiry functionality.

Example:

``` 
var thuriaCache = new ThuriaCache<string>(54000);   // 15 Minutes Expiry
await thuriaCache.UpsertAsync("CacheKey", new ThuriaCacheData<string>("TestData"));

var cacheExists = await thuriaCache.ExistsAsync("CacheKey");
var returnedData = await thuriaCache.GetAsync("CacheKey");
```
---

### Web API
The Web API package contains the following features:

**TBD**
