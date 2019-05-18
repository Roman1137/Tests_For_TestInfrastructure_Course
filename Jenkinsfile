pipeline {
	agent {
		label 'master'
	}
    stages {
		stage('Reports') {
			steps {
				dir("/var/jenkins_home/workspace/UI_Tests_With_Allure/allure-report"){
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