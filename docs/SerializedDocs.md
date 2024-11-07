# Quiz Generator Documentation

## Table of Contents

---

1. [Introduction](#introduction)
2. [Architecture](#architecture)
3. [Installation and Setup](#installation-and-setup)
4. [Technical Details](#technical-Details)
5. [Development Guidelines](#development-guidelines)
6. [Testing Strategy](#testing-strategy)
7. [Database Schema](#database-schema)
8. [Troubleshooting](#troubleshooting)
9. [Deployment](#deployment)
10. [Glossary](#glossary)
11. [Future Enhancements](#future-enhancements)

---

## Introduction

The Quiz Generator is a Windows application designed to generate quizzes from notes files and support self-testing. It allows users to create topics from HTML-formatted notes and start quizzes based on those topics.

### Purpose

The Quiz Generator application is designed to make the learning process more efficient by transforming raw notes into interactive, personalized quizzes. Its primary purpose is to empower students and educators alike to create engaging, adaptive assessments that reinforce knowledge retention and comprehension.

**Core Objectives:**

1. _Create a efficient learning workflow_:
   - Listen or Read
   - Take notes
   - Highlight important concepts
   - Iteratively test your self from highlighted concepts until all learned
2. _Efficient Learning Tool_: Convert static notes into dynamic, interactive quizzes that stimulate active recall and reinforce learning.
3. _Customizable Assessments_: Allow users to tailor quizzes to their specific needs, adapting difficulty levels and content to suit individual learning styles.
4. _Time-Saving Solution_: Automate the quiz creation process, significantly reducing the time spent on manually crafting assessment materials.
5. _Adaptive Learning Experience_: Implement algorithms that adjust question difficulty and frequency based on user performance, optimizing the learning process.

### Scope

The Quiz Generator application is specifically designed to serve the needs of students across various educational levels and disciplines. Its primary focus is on enhancing the learning experience and improving knowledge retention among students.

It can also be used by anybody who wants to self study from books on other sources and maintain his knowledge.

### Key Features

- Supports parsing notes in HTML format
- Generates quizzes with questions and answers from parsed notes
- Allows starting quizzes from created topics
- Implements a Chain of Responsibility pattern for quiz creation and starting
- Utilizes Windows Forms for UI development
- Supports fetching notes from Notion API

## Architecture

The Quiz Generator is a Windows application. It follows a modular architecture with clear separation of concerns:

### External Systems

1. **Notion API**: Used for fetching notes from Notion app

![High level user-app-outside_dependencies diagram](https://github.com/JaromirProchazka/quizGenerator/blob/main/docs/ArchitectureDiagrams/L1.png?raw=true)

### Containers

1. **Quiz Generator App**: Client-side Windows application
2. **.sources**: File system container for storing quiz data (acts as a database)

![Application containers diagram](https://github.com/JaromirProchazka/quizGenerator/blob/main/docs/ArchitectureDiagrams/L2.png?raw=true)

### Components

1. **QuizPersistence**: Handles loading and storing quiz state
2. **NotesParsing**: Defines tools for parsing notes into quiz questions
3. **TopicCreation**: Business layer for creating new topics
4. **QuizStarting**: Business layer for starting quizzes from created topics
5. **QuizGeneratorPresentation**: Presentation layer displaying quizzes and other services to the user
   5.a. **MainPage**: The Dashboard from which the other services are accessed
   5.b. **QuizStarting**: A chain of pages used to configure the quiz start
   5.c. **TopicCreation**: It is a chain of pages used to configure the quiz creation

![Quiz components diagram](https://github.com/JaromirProchazka/quizGenerator/blob/main/docs/ArchitectureDiagrams/L3.png?raw=true)

### C4 Model

![High level user-app-outside_dependencies diagram](https://github.com/JaromirProchazka/quizGenerator/blob/main/docs/ArchitectureDiagrams/L1.png?raw=true)
![Application containers diagram](https://github.com/JaromirProchazka/quizGenerator/blob/main/docs/ArchitectureDiagrams/L2.png?raw=true)
![Quiz components diagram](https://github.com/JaromirProchazka/quizGenerator/blob/main/docs/ArchitectureDiagrams/L3.png?raw=true)

## Installation and Setup

### Prerequisites

- .NET Framework 4.8 or later
- Visual Studio 2019 or later
- HtmlAgilityPack NuGet package
- Notion API credentials (for Notion integration)

### Step-by-Step Installation Guide

1. Clone the repository from GitHub.
2. Open the solution in Visual Studio.
3. Restore NuGet packages.
4. Build the solution.

## Technical Details

### Technologies

- Primary backend language: .NET with C#
- Frontend: Windows Forms for native app, JavaScript for web application
- HTML parsing library: HtmlAgilityPack

### Data Flow

1. User interacts with the presentation layer
2. Requests are processed through business logic layers (TopicCreation, QuizStarting)
3. Notes are fetched from external sources (local files or Notion API)
4. Parsed data is stored/retrieved via QuizPersistence component
5. Stored data is accessed from the .sources container

## Development Guidelines

### Coding Standards

1. Follow C# coding conventions.
2. Use meaningful variable names.
3. Implement proper exception handling.
4. Write unit tests for critical components.
5. **Opend-closed principal**: for new functionalities use inheritance to create new classes with needed features. Do not touch the old once

### Version Control Practices

1. Use Git for version control.
2. Commit frequently with descriptive commit messages.
3. Use feature branches for new features or significant changes.
4. Perform code reviews before merging pull requests.

## Testing Strategy

Unit tests for individual components and methods

## Database Schema

The Quiz Generator uses a file-based storage system for quiz data. The schema consists of:

The topics folder contains the styling and quiz visualization scripts. Further for each topic, there is a folder. Each folder containers an original notes file, the generated quiz file with the questions and answers and a serialized state file.

```
.sources\
        |-------— styles.css
        |-------— script.js
        |-------— *general topic folder\
                                    |-------— notes.html
                                    |-------— questions.html
                                    |-------— state.json
        |-------— ... more topic folders
```

## Modules Documentation

![QuizPersistence components diagram](https://github.com/JaromirProchazka/quizGenerator/blob/main/docs/ArchitectureDiagrams/L3.png?raw=true)

- **QuizPersistence:** Module that provides an interface for interactiong with the Apps persistance. For exampel creating new Topics folder, renaming Topics...
  - **DataStructures:** Defines dataStructures useful for Questions Sequencing.
- **NotesParsing:** Defines tools for parsing notes into Quizzes. The questions are found using the **`QuestionNodeParams`** which holds the List of Attribute check classes. Each of these classes has a check for some possible html node attribute and on running the `QuestionNodeParams` function for analysing the `HtmlNode`, each of these checks are used on the node. This insures great extensibility, new Check can be created by making inheriting object from the `BaseAttributeCheck` and it's instance be added to the Checks List. **`QuestionNodeParams` is abstract and has two inheritors**, the **`AndQuestionNodeParams`** and **`OrQuestionNodeParams`**. The And one passes, if all Checks in the List pass and the Or one passes if at least one check in the List passes.
- **AbstractChainStructure:**
  - **AbstractChain:** Defiens an abstract Chain of responsibility Pattern and its Builder.
- **TopicCreation:** Defines structures for Creating new Topics.
  - **TopicCreationChain:** This abstract chain and its builder are implemented by the **`TopicCreationStep : ChainStep<TopicProduct>`** and it's builder. This `TopicCreationStep` is implemented by the `ChooseQuestionFormat` which lets user configure the **`NotesParser`** and define what is interpreted as a question. Also there is the **`ChooseNotesSource : TopicCreationStep`** which lets user choose the notes source. For that, there is the Notion Page source and local file source.
- **QuizStarting:** Defines structures for Starting Quizzes from created Topics.
  - **QuizStartingChain:** Defiens step and its inheritors for choosing, from which possition to start the quiz and a builder for the quiz starting chain.
- **QuizGeneratorPresentation:** Contains mainly the UI components like the Main Page or Quiz Page. Also for Step components, there are special Forms implementing ChainStepForm<StepT, ProductT, BuilderT> : Form. This form holds the current Chain builder and the next form to be opened. When user presses Continue, the Chosen step is added to the Builder and the Builder is passed to the next form. Finaly, when the Building is finished, the Chain is built and executed.
  - **MainPage:** Defines the programs dashboard.
  - **QuizStarting:** Defiens UI elemets for satarting an existing Quiz from a Topic.
  - **TopicCreation:** Defiens UI elemets for creating new Topics.

## Troubleshooting

### Common Errors and Solutions

1. **Error: Unable to parse notes file**
   Solution: Check if the notes file is in the correct format. Verify the question selector configuration.

2. **Error: Failed to connect to Notion API**
   Solution: Ensure the API key is valid and network connectivity is stable.

## Deployment

1. Package the Windows Forms application for desktop deployment
2. Creating a web version using JavaScript for broader accessibility is simple because of the design, only the frontend must be replaced and server setup

## Glossary

1. **Topic**: A collection of notes parsed into a structured format.
2. **Quiz**: A set of questions and answers derived from a topic.
3. **Question Marker**: CSS selector used to identify questions in HTML notes.
4. **Chain of Responsibility**: Design pattern used for quiz creation and starting processes.
5. **Notes**: Notes are some texts in a supported format containing the questions and answers. The questions are in some way marked. A way, that can be configured in the app before inputting the notes file. For instance, user could choose that in his notes, each important topic is marked in italics, or in textbooks, all numbered statements and their proofs are selected as questions.

### Change Log

[Include detailed change logs]

## Future Enhancements

---

Future expansions will mostly be more steps to the Creation and Open Chains. For the Creation one, there will be:

- a Step, where user sees all found questions and can some exclude or edit there answers
- adding options for more questions dependency (you can set one question to be dependent in the DAG on another question)

For the opening Chain:

- adding option for AI checker, that will have user summarize his answer and some king of Language Model will decide, if the answer was good enough

And besides the Chains:

- analytics in the Quiz List like number of wrongly answered questions to correctly answered ratio. Or weather the Quiz was already finished
