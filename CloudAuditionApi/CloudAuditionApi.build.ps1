
# Variables
$DockerComposePath = Join-Path -Path $PSScriptRoot -ChildPath 'docker-compose.ci.yml'


Task Default Test

Task Test UnitTest, IntegrationTest


Task Build {
    Invoke-BuildExec {
        docker-compose -f $DockerComposePath build    
    }
}


Task UnitTest Build, {
    Invoke-BuildExec {
        docker-compose -f $DockerComposePath run --service-ports cloud_audition_api dotnet test ./CloudAuditionApi.UnitTests
    }
}


Task IntegrationTest Build, {
    Invoke-BuildExec {
        docker-compose -f $DockerComposePath run --service-ports cloud_audition_api dotnet test ./CloudAuditionApi.IntegrationTests
    }
}
