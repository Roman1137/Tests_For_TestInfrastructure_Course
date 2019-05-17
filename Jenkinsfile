pipeline {
	agent {
		label 'master'
	}
    stages {
		stage('Verify docker') {
			steps {
                sh 'docker ps' 
            }
		}
        stage('Build') { 
			agent {
				docker {
					image 'microsoft/dotnet:2.2-sdk'
					args '-p 3000:3000' 
				}
			}
            steps {
                sh 'dotnet build' 
            }
        }
        stage('Test') {
			agent {
					docker {
						image 'microsoft/dotnet:2.2-sdk'
						args '-p 3000:3000' 
					}
			}
            steps {
                sh 'dotnet test --settings config/test.runsettings' 
            }
        }
    }
}