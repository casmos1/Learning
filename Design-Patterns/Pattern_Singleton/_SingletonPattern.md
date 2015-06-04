# Singleton Pattern (Thread Safe)

Example came from here: http://www.codeproject.com/Tips/219559/Simple-Singleton-Pattern-in-Csharp

## Description

Often, a system only needs to create one instance of a class, and that instance will be accessed throughout the program. Examples would include objects needed for logging, communication, database access, etc.

So, if a system only needs one instance of a class, and that instance needs to be accessible in many different parts of a system, one control both instantiation and access by making that class a singleton.

A Singleton is the combination of two essential properties:

Ensure a class only has one instance.
Provide a global point of access to it.

This example is thread safe.