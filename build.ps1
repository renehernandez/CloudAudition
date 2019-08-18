[CmdletBinding()]
param (
    [Parameter()]
    [ValidateNotNullOrEmpty()]
    [string]
    $Task = 'Default',
    [Parameter()]
    [switch]
    $UseDocker
)


$isDockerPresent = (Get-Command -Name 'docker*' | Measure-Object).Count -ne 0

Write-Host "Start pipeline execution"

$PSDependenciesPath = Join-Path -Path $PSScriptRoot -ChildPath 'PSDependencies'
if (-not (Test-Path -Path $PSDependenciesPath)) {
    New-Item -Path $PSDependenciesPath -ItemType Directory -Force | Out-Null
}

if (-not (Test-Path -Path $PSDependenciesPath/InvokeBuild)) {
    Save-Module -Name InvokeBuild -Path $PSDependenciesPath -Force
}

Import-Module -Name $PSDependenciesPath/InvokeBuild -Force

$params = @{
    Task = $Task
    Result = 'Result'
}

$buildProjectFiles = Get-ChildItem -Path $PSScriptRoot -File -Filter '*.build.ps1' -Recurse -Depth 1

foreach ($buildFile in $buildProjectFiles) {
    Write-Host "Start pipeline tasks for $($buildFile.BaseName)"
    $params.File = $buildFile.FullName
    Invoke-Build @params

    if ($Result.Error) {
        foreach ($task in $Result.Tasks) {
            if ($task.Error) {
                Write-Error "Task '$($task.Name)' at $($task.InvocationInfo.ScriptName):$($task.InvocationInfo.ScriptLineNumber)"
            }
        }
    }
}
