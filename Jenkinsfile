pipeline {
	agent {
		label 'master'
	}
    stages {
		stage('Reports') {
			steps {
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