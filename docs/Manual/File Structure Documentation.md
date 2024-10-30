# File Structure Documentation

This page describes the individual modules and their files for development purposes.

Here you can see a high level c4 architectural diagram:

![High level user-app-outside_dependencies diagram](https://github.com/JaromirProchazka/quizGenerator/blob/main/docs/ArchitectureDiagrams/L1.png)

Here is a bit more detailed diagram with containers:

![Application containers diagram](https://github.com/JaromirProchazka/quizGenerator/blob/main/docs/ArchitectureDiagrams/L1.png)

## QuizPersistence

---

![QuizPersistence components diagram](https://github.com/JaromirProchazka/quizGenerator/blob/main/docs/ArchitectureDiagrams/L3-persist.png)

Module that provides an interface for interactiong with the Apps persistance. For exampel creating new Topics folder, renaming Topics...

### .sources

Defines an empty file for the Topics folders. This folder acts as sort of databese of created Topics (Quizzes).

### fileManagerClasses.cs

Defines an API for interacting with **Quiz folders** with the **`QuestionsFile`** singleton. Quiz folders is the Persitence structure used by this app.
The **`QuestionsFile`** singleton provides an API for accessing, creating new folders with the Quiz data and accessing the Quiz State.

### QuizStates Module

Provides structures for Quiz state saving and loading from the Persistence layer.

#### QuizState.cs

Defines an abstract `QuizState` class. There is also interface for updating the state, for later storing.

#### StateSerializer.cs

Defines Serializer called `IStateSerializer<QT> where QT : QuizState` with `QuizState LoadState()` and `bool StoreState(QST state)`. It loads and stores `QuizState` to the persistance layer.

#### RandomDagSequence.cs

Also defines a `ISequenceOfQuestions` class that takes care of the questions ordering with function `List<string> getSequence()`. It is part of this module becose the question sequence class is part of the QuizState. The way in which the questions can be reshuffeled is encoded here and some must be saved.

### DataStructures

Defines dataStructures useful for Questions Sequencing.

#### DAG.cs

Defins a Directed acyclic graph datastructure with implementation for finding a random topological order. This random topological order is than used for finding a random questions suffeling.

#### priorityQueue.cs

Defines a simple priority queue used by the DAG class.

## NotesParsing

---

Defines tools for parsing notes into Quizzes.

The questions are found using the **`QuestionNodeParams`** which holds the List of Attribute check classes. Each of these classes has a check for some possible html node attribute and on running the `QuestionNodeParams` function for analysing the `HtmlNode`, each of these checks are used on the node. This insures great extensibility, new Check can be created by making inheriting object from the `BaseAttributeCheck` and it's instance be added to the Checks List. **`QuestionNodeParams` is abstract and has two inheritors**, the **`AndQuestionNodeParams`** and **`OrQuestionNodeParams`**. The And one passes, if all Checks in the List pass and the Or one passes if at least one check in the List passes.

### NodeAttributeCheckers.cs

Defines `BaseAttributeCheck` class with the `bool Check(HtmlNode node, StyleAttributes? inlineNodeStyle = null)` method. It is used to determine if given node passes the given check. It is then implemented by `AttributeCheck<T> : BaseAttributeCheck` which takes the type and value of the Expected attribute in the Node. Its inheritors are then the concreat checks like `NameCheck : AttributeCheck<string>` that checks the Nodes name.

### NotesParser.cs

Defines a `NotesParser` class with `string CreateQuestionsFileText(string notesMarkdown)`. This is an interfae for creating the Quizz config file contents from the Notes file contents.

### QuestionNodeParams.cs

Defines a Complete node params checker that creates a `IEnumerable<BaseAttributeCheck> Checks`. On the node checking, each of the checks is applied to the node.

## AbstractChainStructure

---

Defiens an abstract Chain of responsibility Pattern and its Builder.

### AbstractChain

#### ChainStep.cs

For the Chain, there is defined `ChainStep<ProductT>` and the product of the chain `ChainProduct`.

#### ChainBuilder.cs

Since the Chain building can be cumbersome, there is also a `ChainBuilder<StepT, ProductT>`, which is used to easily build `ChainBuilder<StepT, ProductT>`.

## TopicCreation

---

Defines structures for Creating new Topics.

### TopicCreationChain

This abstract chain and its builder are implemented by the **`TopicCreationStep : ChainStep<TopicProduct>`** and it's builder. This `TopicCreationStep` is implemented by the `ChooseQuestionFormat` which lets user configure the **`NotesParser`** and define what is interpreted as a question. Also there is the **`ChooseNotesSource : TopicCreationStep`** which lets user choose the notes source. For that, there is the Notion Page source and local file source.

#### ChainCreationBuilder.cs

Builder for the quiz creation chain.

#### ChainStepsTypes.cs

Defines the chain of responsibility for creationg new Topics/Quizzes.

#### LeafSteps.cs

Defines the edge chain steps used by builder. Mainly the first (initializing) and last (returning) steps.

#### SorceChooserStep.cs

Defiens step and its inheritors for choosing the source of the notes.

## QuizStarting

---

Defines structures for Starting Quizzes from created Topics.

### RandomDagSequence.cs

Definef data-structures for correctaly suffeling quizzes.

### QuizStartingChain

#### ChainStartingBuilder.cs

Builder for the quiz starting chain.

#### ContinueOrStartNewOption.cs

Defiens step and its inheritors for choosing, from which possition to start the quiz.

#### leafStartStep.cs

Defines the edge chain steps used by builder. Mainly the first (initializing) and last (returning) steps.

#### StartingStepsTypes.cs

Defines the chain of responsibility for starting Quizzes from Topics.

## QuizGeneratorPresentation

---

Contains mainly the UI components like the Main Page or Quiz Page. Also for Step components, there are special Forms implementing ChainStepForm<StepT, ProductT, BuilderT> : Form. This form holds the current Chain builder and the next form to be opened. When user presses Continue, the Chosen step is added to the Builder and the Builder is passed to the next form. Finaly, when the Building is finished, the Chain is built and executed.

![QuizPersistence components diagram](https://github.com/JaromirProchazka/quizGenerator/blob/main/docs/ArchitectureDiagrams/L3-presentation.png)

### MainPage

Defines the programs dashboard.

#### mainPage.cs

Defines Findows form UI of a dashboard, from which all other features are reached.

#### topicEditBox.cs

Define Windows form for Editing box of a Topic.

### QuizStarting

Defiens UI elemets for satarting an existing Quiz from a Topic.

#### questionsForm.cs

Defines the form, in which the Quiz is taken.

#### ChooseQuizBeginningFrom.cs

A WF used for choosing, from where to start the quiz. If from start or from the saved last possition.

### TopicCreation

Defiens UI elemets for creating new Topics.

#### ChooseQuestionsFormat.cs

A WF used for choosing how the questions in the notes are formated.

#### ChooseSourceStep.cs

A WF used for choosing the source of the notes that will be converted to quiz.
