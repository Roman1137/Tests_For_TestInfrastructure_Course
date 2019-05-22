pipeline {
	agent {
		label 'master'
	}
	environment {
		// HOME helps to avoid permission error
		HOME = '/tmp'
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
			steps {
				dir("frontend"){
					sh 'docker rm -f ${FRONTEND_NAME} || true'
					sh 'docker build --no-cache -t ${FRONTEND_NAME}:edge .'
					sh 'docker run --rm --name ${FRONTEND_NAME} -d --privileged --network ${NETWORK_NAME} -p 8000:8080 ${FRONTEND_NAME}:edge'
				}
			}
		}
		stage('Start Browser') {
			steps {
				sh 'docker rm -f ${BROWSER_NAME} || true'
                sh 'docker run --rm --name ${BROWSER_NAME} -d --privileged --network ${NETWORK_NAME} selenium/standalone-chrome:3.141.59'
            }
		}
        stage('Run tests') {
			agent {
				docker {
					image 'microsoft/dotnet:2.2-sdk'
					args '-p 3000:3000 --network ${NETWORK_NAME}' 
				}
			}
            steps {
				saveDotnetWorkspaceName();
				updateTestConfigFile();
				
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

def updateTestConfigFile() {
	def configsFolderPath = "Tests_For_TestInfrastructure_Course/config";
	sh "sed -i 's|ToDoApplicationUrl_Value|${FRONTEND_URL}|g' ${configsFolderPath}/docker.runsettings"
	sh "sed -i 's|SeleniumGridUrl_Value|${BROWSER_URL}|g' ${configsFolderPath}/docker.runsettings"
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
	sh 'docker rm -f ${FRONTEND_NAME} || true'
	sh 'docker rm -f ${BROWSER_NAME} || true'
	sh 'docker network rm ${NETWORK_NAME}'
}
