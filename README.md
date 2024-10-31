# Quiz Generator

## Documentation

Docs can be accessed [here](https://jaromirprochazka.github.io/quizGenerator/).

## Basic Idea

This App is used for **building tests in form of quizzes from the input of digital notes** or other materials. These are intended as study aids, not a testing framework for teachers and the student decides, whether his answer is good enough. The user can **choose from multiple file formats for his notes or textbooks**. From these, the marked sections of the text are made quiz questions and mostly based on headings, keywords and so on, a logical ordering of the questions is created.

For each quiz is than stored users score, history of answers, last state to be returned to and other quality of life features.

### What is Quiz?

In our context, a quiz is a set of questions and answers. These are than iteratively gone throw by the student. Student tries to answer a question, that he is shown the answer. If he is satisfied, he can move on to the next, else the question is moved some number of questions ahead, so that the student can try again.

The ordering is based around the dependence between questions, some questions can be asked only if some other has been already answered. Besides these restraints, there is random ordering.

### What is Topic?

Topic is the thing, that is created from the notes after parsing. It is visible on the main page of the app in the Topics list and a quiz can be started from it. It is different from Quiz because from the Topic, multiple different Quizzes with different ordering and starting conditions can be started.

### What are Notes?

Notes are some texts in a supported format containing the questions and answers. The questions are in some way marked. A way, that can be configured in the app before inputting the notes file. For instance, user could choose that in his notes, each important topic is marked in italics, or in textbooks, all numbered statements and their proofs are selected as questions. 

From it, the app creates a quiz file, which will then be used to generate the quiz in UI, so the conversion expensive only occurs once and the quiz file can be reused.

---
### Future Expansions
---
Future expansions will mostly be more steps to the Creation and Open Chains. For the Creation one, there will be:
- a Step, where user sees all found questions and can some exclude or edit there answers
- adding options for more questions dependency (you can set one question to be dependent in the DAG on another question)

For the opening Chain:
- adding option for AI checker, that will have user summarize his answer and some king of Language Model will decide, if the answer was good enough

And besides the Chains:
- analytics in the Quiz List like number of wrongly answered questions to correctly answered ratio. Or weather the Quiz was already finished

---

More info in the [docs](https://jaromirprochazka.github.io/quizGenerator/).
