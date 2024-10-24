# File Structure Documentation

This page describes the individual modules and their files for development purposes. 



## FileManager
Module that provides an interface for interactiong with the Apps persistance. For exampel creating new Topics folder, renaming Topics...

### .sources
Defines an empty file for the Topics folders. This folder acts as sort of databese of created Topics (Quizzes).

### fileManagerClasses.cs
Defines an API for interacting with **Quiz folders** with the **`QuestionsFile`** singleton. Quiz folders is the Persitence structure used by this app.
The **`QuestionsFile`** singleton provides an API for accessing, creating new folders with the Quiz data and accessing the Quiz State. 
