pipeline {
	agent {
		label 'master'
	}
	environment {
		// HOME helps to avoid permission error
		HOME = '/tmp'
		NETWORK_NAME = "my-network"	
	}
    stages {
		stage('Prepare environment') {
			steps {
				sh "docker network create ${NETWORK_NAME}"
				readContainerNamesFromConfig();
			}
		}
		stage('Start Frontend'){
			steps {
				dir("frontend"){
					sh "docker rm -f ${TODO_APPLICATION_CONTAINER_NAME} || true"
					sh "docker build --no-cache -t ${TODO_APPLICATION_CONTAINER_NAME}:edge ."
					sh "docker run --rm --name ${TODO_APPLICATION_CONTAINER_NAME} -d --privileged --network ${NETWORK_NAME} -p 8000:8080 ${TODO_APPLICATION_CONTAINER_NAME}:edge"
				}
			}
		}
		stage('Start Browser') {
			steps {
				sh "docker rm -f ${SELENIUM_CLUSTER_CONTAINER_NAME} || true"
                sh "docker run --rm --name ${SELENIUM_CLUSTER_CONTAINER_NAME} -d --privileged --network ${NETWORK_NAME} selenium/standalone-chrome:3.141.59"
            }
		}
        stage('Run tests') {
			agent {
				docker {
					image 'microsoft/dotnet:2.2-sdk'
					args "-p 3000:3000 --network ${NETWORK_NAME}" 
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


def TODO_APPLICATION_CONTAINER_NAME;
def SELENIUM_CLUSTER_CONTAINER_NAME;
def readContainerNamesFromConfig() {
	script {
		def configsFolderPath = "Tests_For_TestInfrastructure_Course/config";
		TODO_APPLICATION_CONTAINER_NAME =  sh "cat ${configsFolderPath}/jenkins.runsettings | grep "ToDoAppContainerName" | grep -oP 'value=\K(\S+)(\")'"
		SELENIUM_CLUSTER_CONTAINER_NAME =  sh "cat ${configsFolderPath}/jenkins.runsettings | grep "SeleniumClusterContainerName" | grep -oP 'value=\K(\S+)(\")'"
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
	sh "docker rm -f ${TODO_APPLICATION_CONTAINER_NAME} || true"
	sh "docker rm -f ${SELENIUM_CLUSTER_CONTAINER_NAME} || true"
	sh "docker network rm ${NETWORK_NAME}"
}
