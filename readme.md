[![Build Status](https://dracorsa.visualstudio.com/Thuria%20-%20Zitidar/_apis/build/status/DracoRSA.Thuria.Zitidar?branchName=master)](https://dracorsa.visualstudio.com/Thuria%20-%20Zitidar/_build/latest?definitionId=2&branchName=master)

Thuria - Zitidar
===

Overview
---

Thuria Zitidar is a set of nuget packages that implements some of the basic plumbing that is required to create a Dotnet Core application that could be using a combination of Dependency Injection, the Akka Actor Framework, and Database Access or all of it.

Features
---

* **Core** - A set of interfaces to allow for development against just an API using dependency injection
* **Extensions** - Various extensions to just add functionality to types like DateTime, String, Type, etc
* **Structuremap** - A set of additional functionality to add structuremap functionality to any application (Bootstrapper, IOC Container wrapper, etc)
* **Settings** - A set of classes to implement the management of settings via Json, Environment Variables or Command Line options.
* **Akka** - A set of functions and features to assist with creating an Actor System using [Akka.Net](http://getakka.net)
* **Logging** - A NLog implementation of the IThuriaLogger that can be used by an application to add logging.
* **Nancy** - A base implementation of hosting a Nancy Service in an application using StructureMap and Thuria

Core
---

Core containes all the Thuria Zitidar Framework contracts and the relevant Enumerations required by these contracts.
No concrete implementations are done in this package.

---
Extensions
---

The following extensions are part of the package:

* **DateTime** -
1. StartOfDay - Return the given date with start of day time set
2. EndOfDay - Return the given date with end of day time set

* **Enumerable\<>** -
1. ForEach\<T> - For each item in a collection, perform the given action
2. IsEmpty\<T> - Determine if the given collection is empty
3. GetAllAsString\<T> - Return a collection of string representations of all the items in a given collection
4. And\<T> - Concatenate the items in an array to a given collection

* **String**
1. RemoveSpaceAndCharacters - Remove all spaces and characters from a string. Leave only numbers.
2. CamelCase - Convert a string to a camel case string
3. PascalCase - Convert a string to a pascal case string
4. EscapeQuotes - Escape the quotes in a string

* **Type** 
1. GetPropertyValue - Get the value of a specified property on an object
2. SetPropertyValue - Set the value of property on a given object
3. DoesPropertyExist - Determine if a property exists on a given object
4. GetDefaultData - Retrieve the default data for a given Type

---
Structuremap
---

The structuremap package provides the following functionality:

* StructuremapThuriaIocContainer - A concrete implementation opf the IThuriaIocContainer interface specifically aimed at Structuremap.
* StructuremapThuriaBootstrapper - The structuremap bootstrapper allows you to bootstrap the Structuremap IOC Container of an application using a builder interface. The following functionality is provided:  

        * WithScannningOfFiles - Scan files
        * WithRegistry - Add a specified structuremap registry
        * WithObjectMapping - Add a object mapping
        * WithConcreteObjectMapping - Add a concrete object mapping

Example:

> ThuriaStructureMapBootstrapper.Create()  
                                .WithScanningOfFiles(true)  
                                .WithRegistry(new PluginRegistry())  
                                .WithObjectMapping(typeof(ISomeInterface), typeof(SomeObject))  
                                .WithConcreteObjectMapping(typeof(ISomeInterface), new SomeObject())  
                                .Build();

* StructuremapThuriaIocContainer

---
Settings
---

The Settings package contains the following settings implementations:

* **Base Settings**
1. **ApplicationName** - Application Name
2. **StartDebugMode** - On startup immediately launch the debugger

* **Database Settings**
1. **GetConnectionString(string dbContextName)** - Get the database connetion string matching the provided Database Context Name.

---
Nancy
---

A base implementation of hosting a Nancy Service in an application using StructureMap and Thuria. The following
classes have been provided:

* NancyBootstrapper - A StructureMap based bootstrapper for the Nancy Framework
* NancyServiceHost - A Startable, Stoppable service to easily Start, Stop and Self Host a Nancy Service
* NancyStartup - Basic Nancy Startup functionality used to start a self hosted nancy Service

---

---
Akka
---

In development

---
