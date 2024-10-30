/*
 @licstart  The following is the entire license notice for the JavaScript code in this file.

 The MIT License (MIT)

 Copyright (C) 1997-2020 by Dimitri van Heesch

 Permission is hereby granted, free of charge, to any person obtaining a copy of this software
 and associated documentation files (the "Software"), to deal in the Software without restriction,
 including without limitation the rights to use, copy, modify, merge, publish, distribute,
 sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:

 The above copyright notice and this permission notice shall be included in all copies or
 substantial portions of the Software.

 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
 BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

 @licend  The above is the entire license notice for the JavaScript code in this file
*/
var NAVTREE =
[
  [ "Quiz Generator", "index.html", [
    [ "Documentation", "index.html#autotoc_md64", null ],
    [ "Basic Idea", "index.html#autotoc_md65", [
      [ "What is Quiz?", "index.html#autotoc_md66", null ],
      [ "What are Notes?", "index.html#autotoc_md67", null ],
      [ "Basic UI design", "index.html#autotoc_md68", [
        [ "Quiz Menu", "index.html#autotoc_md69", [
          [ "Quiz Creation", "index.html#autotoc_md70", null ],
          [ "Quiz Starting", "index.html#autotoc_md71", null ]
        ] ],
        [ "Quiz Window", "index.html#autotoc_md72", null ]
      ] ]
    ] ],
    [ "Developer Overview", "index.html#autotoc_md73", [
      [ "Technologies", "index.html#autotoc_md74", null ],
      [ "Advanced C# Tools", "index.html#autotoc_md75", null ],
      [ "Detailed Project Overview", "index.html#autotoc_md76", [
        [ "FileManager namespace", "index.html#autotoc_md77", null ],
        [ "QuizLogicalComponents namespace", "index.html#autotoc_md78", null ],
        [ "QGenerator namespace", "index.html#autotoc_md79", null ]
      ] ],
      [ "Future Expansions", "index.html#autotoc_md81", null ]
    ] ],
    [ "Developer Overview", "md__manual_2_developer_01_documentation.html", null ],
    [ "File Structure Documentation", "md__manual_2_file_01_structure_01_documentation.html", [
      [ "QuizPersistence", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md8", [
        [ ".sources", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md10", null ],
        [ "fileManagerClasses.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md11", null ],
        [ "QuizStates Module", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md12", [
          [ "QuizState.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md13", null ],
          [ "StateSerializer.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md14", null ],
          [ "RandomDagSequence.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md15", null ]
        ] ],
        [ "DataStructures", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md16", [
          [ "DAG.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md17", null ],
          [ "priorityQueue.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md18", null ]
        ] ]
      ] ],
      [ "NotesParsing", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md19", [
        [ "NodeAttributeCheckers.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md21", null ],
        [ "NotesParser.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md22", null ],
        [ "QuestionNodeParams.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md23", null ]
      ] ],
      [ "AbstractChainStructure", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md24", [
        [ "AbstractChain", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md26", [
          [ "ChainStep.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md27", null ],
          [ "ChainBuilder.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md28", null ]
        ] ]
      ] ],
      [ "TopicCreation", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md29", [
        [ "TopicCreationChain", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md31", [
          [ "ChainCreationBuilder.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md32", null ],
          [ "ChainStepsTypes.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md33", null ],
          [ "LeafSteps.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md34", null ],
          [ "SorceChooserStep.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md35", null ]
        ] ]
      ] ],
      [ "QuizStarting", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md36", [
        [ "RandomDagSequence.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md38", null ],
        [ "QuizStartingChain", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md39", [
          [ "ChainStartingBuilder.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md40", null ],
          [ "ContinueOrStartNewOption.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md41", null ],
          [ "leafStartStep.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md42", null ],
          [ "StartingStepsTypes.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md43", null ]
        ] ]
      ] ],
      [ "QuizGeneratorPresentation", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md44", [
        [ "MainPage", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md46", [
          [ "mainPage.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md47", null ],
          [ "topicEditBox.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md48", null ]
        ] ],
        [ "QuizStarting", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md49", [
          [ "questionsForm.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md50", null ],
          [ "ChooseQuizBeginningFrom.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md51", null ]
        ] ],
        [ "TopicCreation", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md52", [
          [ "ChooseQuestionsFormat.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md53", null ],
          [ "ChooseSourceStep.cs", "md__manual_2_file_01_structure_01_documentation.html#autotoc_md54", null ]
        ] ]
      ] ]
    ] ],
    [ "User Documentation", "md__manual_2_user_docs.html", null ],
    [ "Html Agility Pack", "md__c_1_2_users_2jarom_2_desktop_2_p_g___c_v_i_x_c4_x8_c_e_n_x_c3_x8_d_2quiz_generator_2packagesd746762943b27c8acb80de6b87a95c8e.html", null ],
    [ "Namespaces", "namespaces.html", [
      [ "Namespace List", "namespaces.html", "namespaces_dup" ],
      [ "Namespace Members", "namespacemembers.html", [
        [ "All", "namespacemembers.html", null ],
        [ "Functions", "namespacemembers_func.html", null ]
      ] ]
    ] ],
    [ "Classes", "annotated.html", [
      [ "Class List", "annotated.html", "annotated_dup" ],
      [ "Class Index", "classes.html", null ],
      [ "Class Hierarchy", "hierarchy.html", "hierarchy" ],
      [ "Class Members", "functions.html", [
        [ "All", "functions.html", "functions_dup" ],
        [ "Functions", "functions_func.html", "functions_func" ],
        [ "Variables", "functions_vars.html", null ],
        [ "Enumerations", "functions_enum.html", null ],
        [ "Properties", "functions_prop.html", null ]
      ] ]
    ] ],
    [ "Files", "files.html", [
      [ "File List", "files.html", "files_dup" ],
      [ "File Members", "globals.html", [
        [ "All", "globals.html", null ],
        [ "Typedefs", "globals_type.html", null ]
      ] ]
    ] ]
  ] ]
];

var NAVTREEINDEX =
[
"__chain__builder__8cs_8js.html",
"class_notes_parsing_1_1_or_question_node_params.html#a99b359ffa8160684b5509763a833315b",
"class_quiz_generator_presentation_1_1_quiz_starting_1_1_choose_quiz_beginning_from.html#a34ca893cde67ef4489b4c1d48a89b364",
"class_quiz_persistence_1_1_data_structures_1_1_priority_queue_1_1_item.html#a0cf5a7da23923048d8fdf73d0e492e19",
"class_topic_creation_1_1_topic_creation_chain_1_1_choose_notes_source.html#a7612fc45812dbed145c7a523892dec6b",
"functions_func_b.html"
];

var SYNCONMSG = 'click to disable panel synchronisation';
var SYNCOFFMSG = 'click to enable panel synchronisation';