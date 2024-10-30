### Basic UI design

#### Quiz Menu

At the Start of the App, you will be greeted by a list of your Quiz on top, and a `Create New Topic` button at the bottom. If you want to add a new topic, press the button and choose the method of providing the notes.

After that a new topic is added to the topics list at the top, or a topic with the same name is updated. Then select topic in list, you can then either click

- `Open` button and start the quiz,
- or click `Edit` button and a edit window will appear. You can **Rename** the topic or **Delete** it

##### Quiz Creation

You can either provide a local html file, or a link to a Notion page. Note that the page must be at the time of Creation PUBLIC. Press `Continue` to move on to the next step.

Then you can choose the _Question format_, which are the attributes that if the given node in the source nodes will have, it will be interpreted as the question and will be added to the final Quiz.

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
