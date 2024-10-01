# Quiz Generator

## Documentation

Docs can be accessed [here](https://jaromirprochazka.github.io/quizGenerator/).

## Basic Idea

This App is used for **building tests in form of quizzes from the input of digital notes** or other materials. These are intended as study aids, not a testing framework for teachers and the student decides, whether his answer is good enough. The user can **choose from multiple file formats for his notes or textbooks**. From these, the marked sections of the text are made quiz questions and mostly based on headings, keywords and so on, a logical ordering of the questions is created.

For each quiz is than stored users score, history of answers, last state to be returned to and other quality of life features.


### What is Quiz?

In our context, a quiz is a set of questions and answers. These are than iteratively gone throw by the student. Student tries to answer a question, that he is shown the answer. If he is satisfied, he can move on to the next, else the question is moved some number of questions ahead, so that the student can try again.

The ordering is based around the dependence between questions, some questions can be asked only if some other has been already answered. Besides these restraints, there is random ordering.

### What are Notes?

Notes are some texts in a supported format containing the questions and answers. The questions are in some way marked. A way, that can be configured in the app before inputting the notes file. For instance, user could choose that in his notes, each important topic is marked in italics, or in textbooks, all numbered statements and their proofs are selected as questions. 

From it, the app creates a quiz file, which will then be used to generate the quiz in UI, so the conversion expensive only occurs once and the quiz file can be reused.

### Basic UI design

#### Quiz Menu

At the Start of the App, you will be greeted by a list of your Quiz on top, and a `Create New Topic` button at the bottom. If you want to add a new topic, press the button and choose the method of providing the notes. 

After that a new topic is added to the topics list at the top, or a topic with the same name is updated. Then select topic in list, you can then either click  

- `Open` button and start the quiz,
- or click `Edit` button and a edit window will appear. You can **Rename** the topic or **Delete** it

##### Quiz Creation

You can either provide a local html file, or a link to a Notion page. Note that the page must be at the time of Creation PUBLIC. Press `Continue` to move on to the next step. 

Then you can choose the *Question format*, which are the attributes that if the given node in the source nodes will have, it will be interpreted as the question and will be added to the final Quiz. 

After pressing `Continue`, new Topic is added to the Topics list where you can edit its name.

##### Quiz Starting

On press of the `Open` button, the Topic selected in the Topics list will be opened. You will be presented with options of either continuing where you ended in your last test, or the option of starting from the beginning with 0 in the current question counter. After that your quiz is opened.  

#### Quiz Window

When the quiz is started, user is greeted on bottom with the question and answers window and on top with 4 buttons.

- press `Answer` button, and read your notes, which are shown at the bottom.
    - If you answer matches, press `Next 👍` which simply shows new question and increment counter.
    - If you failed to answer correctly, press the `Next 👎` button. This one also shows new question, but it moves the incorrectly answered question ahead, so that it will be asked again and hopefully answered correctly.
- when you are done and want to start a different quiz, simply press the `Back` button, which closes this quiz.

There is also a counter, which says “`number of answered questions / number of questions`”. If you answer incorrectly and press `Next 👎`, the counter doesn’t increment.

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

---

### Future Expansions

Future expansions will mostly be more steps to the Creation and Open Chains. For the Creation one, there will be:
- a Step, where user sees all found questions and can some exclude or edit there answers
- adding options for more questions dependency (you can set one question to be dependent in the DAG on another question)

For the opening Chain:
- adding option for AI checker, that will have user summarize his answer and some king of Language Model will decide, if the answer was good enough

And besides the Chains:
- analytics in the Quiz List like number of wrongly answered questions to correctly answered ratio. Or weather the Quiz was already finished

---

More info in the [docs](https://jaromirprochazka.github.io/quizGenerator/).
