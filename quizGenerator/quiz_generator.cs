using FileManager;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using FileManager;
using System.IO;
using HtmlAgilityPack;
using System.Reflection;
using System.Diagnostics;
using FileManager;
using System.Windows.Forms;
using quizGenerator;

namespace quizGenerator
{
    public class QuizGenerator
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Topics.OpenMainPage();
        }
    }
}