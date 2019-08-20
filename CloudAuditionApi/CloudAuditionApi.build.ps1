function Format-Version
{
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)]
        [ValidateNotNullOrEmpty()]
        [string]
        $Version,
        [Parameter(Mandatory)]
        [AllowNull()]
        [AllowEmptyString()]
        [string]
        $Branch,
        [Parameter(Mandatory)]
        [int]
        $Build
    )

    try {
        if ($Branch -ne 'master') {
            "$Version-$($Branch)b$Build"
        }
        else {
            $Version
        }
    }
    catch {
        $PSCmdlet.ThrowTerminatingError($_)
    }
}

# Variables
$CiDockerComposePath = Join-Path -Path $PSScriptRoot -ChildPath 'docker-compose.ci.yml'
$ReleaseDockerfilePath = Join-Path -Path $PSScriptRoot -ChildPath 'Dockerfile'
$VersionPath = Join-Path -Path $PSScriptRoot -ChildPath '../version.txt'
$Version = (Get-Content -Path  $VersionPath -Raw).Trim()

$formattedVersion = Format-Version -Version $version -Branch $env:BUILD_SOURCEBRANCHNAME -Build $env:BUILD_BUILDID
$VersionedImageName = "cloud_audition_api:$formattedVersion"


Task Default Test, BuildReleaseDockerImage

Task Test UnitTest, IntegrationTest

Task Publish PushToDockerHub


Task Build SetCurrentLocation, {
    Invoke-BuildExec {
        docker-compose -f $CiDockerComposePath build
    }
}


Task UnitTest Build, {
    Invoke-BuildExec {
        docker-compose -f $CiDockerComposePath run `
            --rm `
            --no-deps `
            api dotnet test ./CloudAuditionApi.UnitTests
    }
}


Task IntegrationTest Build, {
    Invoke-BuildExec {
        docker-compose -f $CiDockerComposePath run `
            --rm `
            -e POSTGRES_DB="$($env:POSTGRES_DB)" `
            -e POSTGRES_USER="$($env:POSTGRES_USER)" `
            -e POSTGRES_PASSWORD="$($env:POSTGRES_PASSWORD)" `
            api dotnet test ./CloudAuditionApi.IntegrationTests
    }
}


Task PushToDockerHub BuildReleaseDockerImage, {
    Invoke-BuildExec {
        docker login -u $env:DOCKER_ID -p $env:DOCKER_PASSWORD

        docker push "$($env:DOCKER_ID)/$VersionedImageName"

        if ($env:BUILD_SOURCEBRANCHNAME -eq 'master') {
            docker tag "$($env:DOCKER_ID)/$VersionedImageName" "$($env:DOCKER_ID)/cloud_audition_api:latest"
        }
    }
}


Task BuildReleaseDockerImage {
    Invoke-BuildExec {
        docker build -f $ReleaseDockerfilePath `
            -t "$($env:DOCKER_ID)/$VersionedImageName" `
            $PSScriptRoot
    }
}


Task SetCurrentLocation {
    Write-Host "Setting current location to: $PSScriptRoot"
    Set-Location -Path $PSScriptRoot
}
