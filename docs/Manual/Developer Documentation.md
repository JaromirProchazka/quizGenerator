## Developer Overview

The UI is made in the Using the Windows Forms. The Quiz creation and Starting are done using a **Chain of responsibility** and a dedicated builder for them. For each Chain Step, a WF Window is prepared to set the steps parameters and on press of the `Continue` button finalize the Chain.

In the Quiz creation, the notes in html format parsed and there html nodes are traversed and analysed. When a node that satisfy the checks is determined to be a question, a question and answer are added to a resulting data file throw the **Quiz file manager Singleton**. On the Quizzes creation is this file opened in the WF Forms web view and it's components are shown or hidden using a *JS* script.   

### Technologies

The App logic is primarily written in **`C#`**. I would also like the app to by accessible as website. The C# libraries are mostly heavily dependent on supported note file formats, but for start, the C# library `HtmlAgilityPack` is necessary. This covers the back end side, the application that creates the quizzes themselves.

The Notion page fetching is done using a notion-to-html API.

The view of front end will than either be implemented in `windows forms` for native app, or the `JavaScript` in case of the web application.  

### Advanced C# Tools

This App also support using notes from app [Notion](https://www.notion.so/) using their API to fetch it. For that we will need **Asynchronous methods**.  Furthermore, we use **Extension methods** in the design of the utility parts. 

### Detailed Project Overview

#### FileManager namespace

Takes care of the **Quiz folders** with the **`QuestionsFile`** singleton. Also implements the input **notes parsing** with the **`NotesParser`**. 

The **`QuestionsFile`** singleton provides an API for accessing, creating new folders with the Quiz data and accessing the Quiz State. It also holds an instance of **`NotesParser`** which implements the `CreateQuestionsFileText` that parses the source html file and create the Quiz data. 

The questions are found using the **`QuestionNodeParams`** which holds the List of Attribute check classes. Each of these classes has a check for some possible html node attribute and on running the `QuestionNodeParams` function for analysing the `HtmlNode`, each of these checks are used on the node. This insures great extensibility, new Check can be created by making inheriting object from the `BaseAttributeCheck` and it's instance be added to the Checks List. **`QuestionNodeParams` is abstract and has two inheritors**, the **`AndQuestionNodeParams`** and **`OrQuestionNodeParams`**. The And one passes, if all Checks in the List pass and the Or one passes if at least one check in the List passes.  

#### QuizLogicalComponents namespace

`QuizLogicalComponents` implements a higher-level API for creating and opening the Quizzes. Also, there is an API for the **`QuizState`** loading and storing the Quizzes state. 

The abstract **`ChainStep<ProductT>`** class implements a Step of an abstract Chain of responsibility. There is also a Builder for this chain: **`ChainBuilder<StepT, ProductT>`**, which creates a Stating (initializes empty product) and Finishing (returns the finished product) step for the user and provides tools for adding Steps into the chain.

This abstract chain and its builder are implemented by the **`TopicCreationStep : ChainStep<TopicProduct>`** and it's builder. This `TopicCreationStep` is implemented by the `ChooseQuestionFormat` which lets user configure the **`NotesParser`** and define what is interpreted as a question. Also there is the **`ChooseNotesSource : TopicCreationStep`** which lets user choose the notes source. For that, there is the Notion Page source and local file source.

This implementation makes the creation and starting parts of the chain **highly extendible**. To make new options for the user, one just implements the given encapsulated Step either creating completely new one or extending already existing once like *adding new source options*.

Then there is a **`QuizState`** implemented with Json Serialization and Deserialization. The States Load or Store are than done with the `IStateSerializer<QT> where QT : QuizState` which implements the `QuizState LoadState()` and `StoreState(QT state)` methods.

#### QGenerator namespace

Contains mainly the UI components like the Main Page or Quiz Page. Also for Step components, there are special Forms implementing **`ChainStepForm<StepT, ProductT, BuilderT> : Form`**. This form holds the current Chain builder and the next form to be opened. When user presses Continue, the Chosen step is added to the Builder and the Builder is passed to the next form. Finaly, when the Building is finished, the Chain is built and executed.   
