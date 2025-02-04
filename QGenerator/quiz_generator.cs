﻿using QuizPersistence;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using QuizGeneratorPresentation;

namespace QuizGeneratorPresentation
{
    public class QuizGenerator
    {
        [STAThread]
        public static void Main(string[] args)
        {
            const int IE11EmulationMode = 11001;
            var appName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName);
            Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", appName, IE11EmulationMode, Microsoft.Win32.RegistryValueKind.DWord);

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            Topics.OpenMainPage();
        }
    }
}