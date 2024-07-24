# Quiz Generator

## Basic Idea

This App is used for **building tests in form of quizzes from the input of digital notes** or other materials. These are intended as study aids, not a testing framework for teachers and the student decides, whether his answer is good enough. The user can **choose from multiple file formats for his notes or textbooks**. From these, the marked sections of the text are made quiz questions and mostly based on headings, keywords and so on, a logical ordering of the questions is created.

For each quiz is than stored users score, history of answers, last state to be returned to and other quality of life features.

### What is Quiz?

In our context, a quiz is a set of questions and answers. These are than iteratively gone throw by the student. Student tries to answer a question, that he is shown the answer. If he is satisfied, he can move on to the next, else the question is moved some number of questions ahead, so that the student can try again.

The ordering is based around the dependence between questions, some questions can be asked only if some other has been already answered. Besides these restraints, there is random ordering.

### What are Notes?

Notes are some text in a supported format containing the questions and answers. The questions are in some way marked. A way, that can be configured in the app before inputting the notes file. For instance, user could choose, that in his notes, each important topic is marked in italics, or in textbooks, all numbered statements and their proofs are selected as questions. 

From it, the app creates a quiz file, which will than be used to generate the quiz in UI, so the conversion expensive only occurs once and the quiz file can be reused.

## Technologies

The App logic is primarily written in **`C#`**. I would also like the app to by accessible as website. The C# libraries are mostly heavily dependent on supported note file formats, but for start, the C# library `HtmlAgilityPack` is necessary. This covers the back end side, the application that creates the quizzes themselves.

The view of front end will than either be implemented in `windows forms` for native app, or the `javascript` in case of the web application.  

### Advanced C# Tools

This App also support using notes from app [Notion](https://www.notion.so/) using there API to fetch it. For that we will need **Asynchronous methods**.  Furthermore, we use **Extension methods** in the design of the utility parts. 

### Basic UI design

#### Quiz Menu

At the Start of the App, you will be greeted by a list of your Quiz on top, and a `Create New Topic` button at the bottom. If you want to add a new topic, press the button and select the notes file on your device, with your notes.

After that a new topic is added to the topics list at the top, or a topic with the same name is updated. Than select topic in list, you can then either click  

- `Open` button and start the quiz,
- or click `Edit` button and a edit window will appear. You can **Rename** the topic or **Delete** it

#### Quiz Window

When the quiz is started, user is greeted on bottom with the question and answers window and on top with 4 buttons.

- press `Answer` button, and read your notes, which are shown at the bottom.
    - If you answer matches, press `Next 👍` which simply shows new question and increment counter.
    - If you failed to answer correctly, press the `Next 👎` button. This one also shows new question, but it moves the incorrectly answered question ahead, so that it will be asked again and hopefully answered correctly.
- when you are done and want to start a different quiz, simply press the `Back` button, which closes this quiz.

There is also a counter, which says “`number of answered questions / number of question`”. If you answer incorrectly and press `Next 👎`, the counter doesn’t increment.