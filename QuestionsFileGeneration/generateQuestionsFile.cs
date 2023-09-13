using FileManager;
using System;
using System.Collections.Generic;
using FileManager;
using System.IO;
using quizGenerator;

class createQuestions
{
    static void Main(string[] args)
    {
        if (args.Length == 0) {
            throw new ArgumentException("No notes HTML file was given!");
        }
        if (Path.GetFileName(args[0]) != QuestionsFile.notesFileName) {
            throw new ArgumentException($"Incorrect path, the file must be 'notes.html', but is {args[0]}");
        }

        string notesMarkdown = File.ReadAllText(args[0]);
        string QuestionsFileMarkdown = QuestionsFile.CreateQuestionsFileText(notesMarkdown);
        string questionsFilePath = @".\" + QuestionsFile.questionsFileName;

        File.WriteAllText(questionsFilePath, QuestionsFileMarkdown);
    }
}