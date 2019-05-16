pipeline {
    agent {
        docker {
            image 'microsoft/dotnet:2.1-aspnetcore-runtime' 
            args '-p 3000:3000' 
        }
    }
    stages {
        stage('Build') { 
            steps {
                sh 'dotnet build' 
            }
        }
        stage('Test') {
            steps {
                sh 'dotnet test --settings config/test.runsettings' 
            }
        }
    }
}