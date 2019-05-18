pipeline {
	agent {
		label 'master'
	}
    stages {
		stage('Reports') {
			steps {
				dir("/var/jenkins_home/workspace/Allure_Only/allure-report"){
					sh "ls"
				}
				script {
						allure([
								includeProperties: false,
								jdk: '',
								properties: [],
								reportBuildPolicy: 'ALWAYS',
								results: [[path: 'target/allure-results']]
						])
				}
			}
		}
    }
}