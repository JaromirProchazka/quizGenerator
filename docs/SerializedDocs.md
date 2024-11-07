## Developer Overview

The UI is made in the Using the Windows Forms. The Quiz creation and Starting are done using a **Chain of responsibility** and a dedicated builder for them. For each Chain Step, a WF Window is prepared to set the steps parameters and on press of the `Continue` button finalize the Chain.

In the Quiz creation, the notes in html format parsed and there html nodes are traversed and analysed. When a node that satisfy the checks is determined to be a question, a question and answer are added to a resulting data file throw the **Quiz file manager Singleton**. On the Quizzes creation is this file opened in the WF Forms web view and it's components are shown or hidden using a _JS_ script.

### Technologies

The App logic is primarily written in **`C#`**. I would also like the app to by accessible as website. The C# libraries are mostly heavily dependent on supported note file formats, but for start, the C# library `HtmlAgilityPack` is necessary. This covers the back end side, the application that creates the quizzes themselves.

The Notion page fetching is done using a notion-to-html API.

The view of front end will than either be implemented in `windows forms` for native app, or the `JavaScript` in case of the web application.

This App also support using notes from app [Notion](https://www.notion.so/) using their API to fetch it. For that we will need **Asynchronous methods**. Furthermore, we use **Extension methods** in the design of the utility parts.

