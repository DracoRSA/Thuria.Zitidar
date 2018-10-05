Thuria - Zitidar
===

Overview
---

The Thuria itidar set of packages give core based functionality to any Dotnet Core project.

Features
---

* **Core** - A set of interfaces to allow for development against just an API using dependency injection
* **Extensions** - Various extensions to just add functionality to types like DateTime, String, Type, etc
* **Structuremap** - A set of additional functionality to add structuremap functionality to any application (Bootstrapper, IOC Container wrapper, etc)

Core
---

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

---
Structuremap
---

The structuremap package provides the following functionality:

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

