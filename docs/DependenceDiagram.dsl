workspace "Name" "Description"

    model {
        u = person "User" "A general User"
        notion = softwareSystem "Notion API" "An API for fetching notes from Notion app" "HTTP Server"
        qg = softwareSystem "Quiz Generator" {
            qgApp = container "Quiz Generator App" "" "Client side Windows App" {
                quizPersistence = component "QuizPersistence" "Handles Quiz state loading and storing" "Module" {
                }
                noteP = component "NotesParsing" "Defines tools for parsing notes into quiz questions" "Module"
                create = component "TopicCreation" "Business layer: Defines structures for Creating new Topics" "Module"              
                start = component "QuizStarting" "Business layer: Defines structures for Starting Quizzes from created Topics" "Module" 
                presentation = component "QuizGeneratorPresentation" "Presentation layer: displays quizzes and other services to user" "Module" {
                    # mainUi = component "MainPage" "The Dashboard from which the other services are accessed" "Submodule" {}
                    # startUi = component "QuizStarting" "A chain of pages used to configure the quiz start" "Submodule" {}
                    # createUi = component "TopicCreation" "A chain of pages used to configure the quiz creation" "Submodule" {}
                }
            }
            fs = container ".sources" "Stores Quiz data" "File System" {
                tags "Database"
            }
        }

        u -> presentation "Create Quiz"
        u -> presentation "Start Quiz"
        u -> presentation "Edit Quiz"
        
        # createUi -> mainUi "Is accessible from"
        # startUi -> mainUi "Is accessible from"
        # startUi -> start "generate"
        # createUi -> create "generate"
        
        create -> notion "fetch data" "HTTP GET"
        quizPersistence -> fs "read/write"
        quizPersistence -> noteP "create new topic"
        create -> quizPersistence "store" "Serialization"
        start -> quizPersistence "load" "Deserialization"
        presentation -> start "generate"
        presentation -> create "generate"
    }

    views {
        systemContext qg "Application_layer" {
            include *
            autolayout lr
        }

        container qg "Containers_layer" {
            include *
            autolayout lr
        }
        
        component qgApp "Component_layer" {
            include *
            autolayout lr
        }

        styles {
            element "Element" {
                background "#1168bd"
                color "#ffffff"
                shape RoundedBox
            }

            element "Component element" {
                background "#90d5ff"
                shape RoundedBox
            }

            element "External System" {
                background "#aaaaaa"
            }

            element "Person" {
                shape person
            }

            element "Web Front-End"  {
                shape WebBrowser
            }

            element "Database"  {
                shape Cylinder
            }
        }
    }
}
