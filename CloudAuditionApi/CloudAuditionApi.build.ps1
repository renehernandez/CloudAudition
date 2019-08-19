
# Variables
$DockerComposePath = Join-Path -Path $PSScriptRoot -ChildPath 'docker-compose.ci.yml'

Task Default Test

Task Test UnitTest, IntegrationTest


Task Build SetCurrentLocation, {
    Invoke-BuildExec {
        docker-compose -f $DockerComposePath build
    }
}


Task UnitTest Build, {
    Invoke-BuildExec {
        docker-compose -f $DockerComposePath run `
            --rm `
            --no-deps `
            api dotnet test ./CloudAuditionApi.UnitTests
    }
}


Task IntegrationTest Build, {
    Invoke-BuildExec {
        docker-compose -f $DockerComposePath run `
            --rm `
            -e POSTGRES_DB="$($env:POSTGRES_DB)" `
            -e POSTGRES_USER="$($env:POSTGRES_USER)" `
            -e POSTGRES_PASSWORD="$($env:POSTGRES_PASSWORD)" `
            api dotnet test ./CloudAuditionApi.IntegrationTests
            
            
    }
}

Task SetCurrentLocation {
    Write-Host "Setting current location to: $PSScriptRoot"
    Set-Location -Path $PSScriptRoot
}