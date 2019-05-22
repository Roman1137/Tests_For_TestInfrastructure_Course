pipeline {
	agent {
		label 'master'
	}
	environment {
		// HOME helps to avoid permission error
		HOME = '/tmp'
		NETWORK_NAME = "my-network"
		
		SELENIUM_CLUSTER_NAME = "my-chrome"
		SELENIUM_CLUSTER_URL = "http://${SELENIUM_CLUSTER_NAME}:4444/wd/hub"
		
		TODO_APP_NAME = "todo-app"
		TODO_APP_URL = "http://${TODO_APP_NAME}:8080"	
	}
    stages {
		stage('Prepare environment') {
			steps {
				sh "docker network create ${NETWORK_NAME}"
			}
		}
		stage('Start Frontend') {
			steps {
				dir("frontend") {
					sh "docker rm -f ${TODO_APP_NAME} || true"
					sh "docker build --no-cache -t ${TODO_APP_NAME}:edge ."
					sh "docker run --rm --name ${TODO_APP_NAME} -d --privileged --network ${NETWORK_NAME} -p 8000:8080 ${TODO_APP_NAME}:edge"
				}
			}
		}
		stage('Start Browser') {
			steps {
				sh "docker rm -f ${SELENIUM_CLUSTER_NAME} || true"
                sh "docker run --rm --name ${SELENIUM_CLUSTER_NAME} -d --privileged --network ${NETWORK_NAME} selenium/standalone-chrome:3.141.59"
            }
		}
        stage('Run tests') {
			agent {
				docker {
					image 'microsoft/dotnet:2.2-sdk'
					args "-p 3000:3000 --network ${NETWORK_NAME} -e ToDoApplicationUrl=${TODO_APP_URL} -e SeleniumClusterUrl=${SELENIUM_CLUSTER_URL}" 
				}
			}
            steps {
				saveDotnetWorkspaceName();
				
				sh "dotnet build && dotnet test --settings config/jenkins.runsettings --logger 'trx' --results-directory ../TestResults"	
            }
			post {
				always {
					packTestResults();
				}
			}
        }
    }
	post {
		always {
			cleanUpDockerItems();
		
			publishAllureResults();
			publishTrxResults();
			
			cleanDotnetWorkspace();
			cleanJenkinsWorkspace();
		}    
    }
}

def DOTNET_WORKSPACE;
def saveDotnetWorkspaceName() {
	script {
		DOTNET_WORKSPACE = "${env.WORKSPACE}"
	}
}

def cleanDotnetWorkspace() {	
	sh "rm -r ${DOTNET_WORKSPACE}/*"
}

def cleanJenkinsWorkspace() {	
	sh "rm -r ${env.WORKSPACE}/*"
}

def packTestResults() {
	stash includes: 'allure-results/*', name: 'allure-results'
	
	stash includes: 'TestResults/*', name: 'TestResults'
}

def publishAllureResults() {
	unstash 'allure-results';

	script {
		allure([
			includeProperties: false,
			jdk: '',
			properties: [],
			reportBuildPolicy: 'ALWAYS',
			results: [[path: 'allure-results']]			
		])
	}
}

def publishTrxResults() {
	unstash 'TestResults'

	step([$class: 'MSTestPublisher', testResultsFile:"TestResults/*.trx", failOnError: true, keepLongStdio: true])
}

def cleanUpDockerItems() {
	sh "docker rm -f ${TODO_APP_NAME} || true"
	sh "docker rm -f ${SELENIUM_CLUSTER_NAME} || true"
	sh "docker network rm ${NETWORK_NAME}"
}
