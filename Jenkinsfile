pipeline {
	agent any
	stages {
		stage('PreBuild') {
			steps {
				sh 'docker pull mcr.microsoft.com/dotnet/aspnet:6.0'
				sh 'docker pull mcr.microsoft.com/dotnet/sdk:6.0'
			}
		}

		stage('Build') {
			steps {
				script {
					sh "docker build -t \"127.0.0.1:42266/acfun-card\" --label \"com.microsoft.created-by=visual-studio\" --label \"com.microsoft.visual-studio.project-name=acfuncard\" -f ./AcFunCard/Dockerfile ."
				}
			}
		}

		stage('Publish') {
			steps {
				script {
					sh "docker push 127.0.0.1:42266/acfun-card"
				}
			}
		}
	}
}