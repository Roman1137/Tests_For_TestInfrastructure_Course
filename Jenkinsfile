pipeline {
	agent {
		label 'master'
	}
    stages {
		stage('Start Chrome') {
			steps {
				sh 'docker rm -f temporary-chrome || true'
                sh 'docker run --rm --name my-chrome -d --privileged --network my-network selenium/standalone-chrome:3.141.59'
            }
		}
        stage('Build') { 
			agent {
				docker {
					image 'microsoft/dotnet:2.2-sdk'
					args '-p 3000:3000 --network my-network' 
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
						args '-p 3000:3000 --network my-network' 
					}
			}
            steps {
                sh 'dotnet test --settings config/test.runsettings' 
            }
        }
    }
	post {
		always {
		  sh 'docker rm -f my-chrome || true'
		}    
  }
}