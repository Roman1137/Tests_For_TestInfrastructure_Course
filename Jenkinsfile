pipeline {
	agent {
		label 'master'
	}
	environment {
		NETWORK_NAME = "my-network"
		
		BROWSER_NAME = "my-chrome"
		BROWSER_URL = "http://${BROWSER_NAME}:4444/wd/hub"
		
		FRONTEND_NAME = "todo-app"
		FRONTEND_URL = "http://${FRONTEND_NAME}:8080"
	}
    stages {
		stage('Prepare environment') {
			steps {
				sh 'docker network create ${NETWORK_NAME}'
			}
		}
		stage('Start Frontend'){
			agent {
				docker {
					image 'node:6-alpine' 
					args '-p 3000:3000' 
				}
			}
			steps {
				dir("frontend"){
					sh 'docker rm -f ${FRONTEND_NAME} || true'
					sh 'docker build --no-cache -t ${FRONTEND_NAME}:edge .'
					sh 'docker run -rm --name ${FRONTEND_NAME} -d --privileged --network {NETWORK_NAME} -p 8000:8080 ${FRONTEND_NAME}:edge'
				}
			}
		}
		stage('Start Chrome') {
			steps {
				sh 'docker rm -f ${BROWSER_NAME} || true'
                sh 'docker run --rm --name {BROWSER_NAME} -d --privileged --network ${NETWORK_NAME} -p 4444:4444 selenium/standalone-chrome:3.141.59'
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
                sh 'dotnet build && dotnet test --settings config/docker.runsettings -- SeleniumGridUrl=${BROWSER_URL} ToDoApplicationUrl=${}' 
            }
        }
    }
	post {
		always {
		  sh 'docker rm -f ${FRONTEND_NAME} || true'
		  sh 'docker rm -f ${BROWSER_NAME} || true'
		  sh 'docker network rm ${NETWORK_NAME}'
		}    
  }
}