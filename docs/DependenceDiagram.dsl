workspace "Name" "Description"

    !identifiers hierarchical

    model {
        u = person "User" "A general User"
        notion = softwareSystem "Notion API" "An API for fetching notes from Notion app"
        qg = softwareSystem "Software System" {
            qPersist = container "Persistence" {
                quizPersistence = component "QuizPersistence" "Handles Quiz state loading and storing" "Module" {
                    
                }
                fs = component ".sources" "Stores Quiz data" "File System" {
                    tags "Database"
                }
            }
            tCreateBusiness = container "Creating Quizzes" "Business layer: Defines structures for Creating new Topics" {
                create = component "TopicCreation" "" "Module" {
                    
                }
            }
            qStartBusiness = container "Quiz Taking" "Business layer: Defines structures for Starting Quizzes from created Topics" "Module" {
                start = component "QuizStarting" "" "Module" {
                    
                }
            }
            presentation = container "QuizGeneratorPresentation" "Presentation layer: displays quizzes and other services to user" "Module" {
                mainUi = component "MainPage" "The Dashboard from which the other services are accessed" "Submodule" {}
                startUi = component "QuizStarting" "A chain of pages used to configure the quiz start" "Submodule" {}
                createUi = component "TopicCreation" "A chain of pages used to configure the quiz creation" "Submodule" {}
            }
        }

        u -> qg.presentation.createUi "Create Quiz"
        u -> qg.presentation.startUi "Start Quiz"
        u -> qg.presentation.mainUi "Edit Quiz"
        
        qg.presentation.createUi -> qg.presentation.mainUi "Is accessible from"
        qg.presentation.startUi -> qg.presentation.mainUi "Is accessible from"
        
        qg.tCreateBusiness.create -> notion "fetch data" "HTTP GET"
        qg.qPersist.quizPersistence -> qg.qPersist.fs "read/write"
        qg.tCreateBusiness.create -> qg.qPersist.quizPersistence "store"
        qg.qStartBusiness.start -> qg.qPersist.quizPersistence "load"
        qg.presentation.createUi -> qg.tCreateBusiness.create "generate"
        qg.presentation.startUi -> qg.qStartBusiness.start "generate"
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
        
        component qg.presentation "Presentation_module_layer" {
            include *
            autolayout lr
        }
        
        component qg.qPersist "Persist_module_layer" {
            include *
            autolayout lr
        }

        styles {
            element "Element" {
                color white
            }
            element "Person" {
                background #116611
                shape person
            }
            element "Software System" {
                background #2D882D
            }
            element "Container" {
                background #55aa55
            }
            element "Database" {
                shape cylinder
            }
        }
    }

}
