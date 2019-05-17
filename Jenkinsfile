pipeline {
	agent {
		label 'master'
	}
	environment {
		NETWORK_NAME = "my-network"
	}
    stages {
		stage('Prepare environment') {
			steps {
				sh 'docker network create ${NETWORK_NAME}'
			}
		}
		stage('Start Chrome') {
			steps {
				sh 'docker rm -f my-chrome || true'
                sh 'docker run --rm --name my-chrome -d --privileged --network ${NETWORK_NAME} -p 4444:4444 selenium/standalone-chrome:3.141.59'
            }
		}
        stage('Test') {
			agent {
				docker {
					image 'microsoft/dotnet:2.2-sdk'
					args '-p 3000:3000 --network ${NETWORK_NAME}' 
				}
			}
            steps {
                sh 'dotnet build && dotnet test --settings config/docker.runsettings' 
            }
        }
    }
	post {
		always {
		  sh 'docker rm -f my-chrome || true'
		  sh 'docker network rm ${NETWORK_NAME}'
		}    
  }
}