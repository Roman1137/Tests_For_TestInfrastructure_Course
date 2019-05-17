pipeline {
	agent none
    stages {
		agent {
			docker {
				image 'microsoft/dotnet:2.2-sdk'
				args '-p 3000:3000' 
			}
		}
		stage('Verify docker') {
			steps {
                sh 'docker ps' 
            }
		}
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