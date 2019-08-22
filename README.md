# CloudAudition [![Build Status](https://dev.azure.com/renehr9102/renehr9102/_apis/build/status/renehernandez.CloudAudition?branchName=master)](https://dev.azure.com/renehr9102/renehr9102/_build/latest?definitionId=2&branchName=master)

CloudAudition is an API-focused project used to experiment and showcase different cloud practices

For documentation for the API project check [CloudAuditionApi](CloudAuditionApi/README.md)

## Installation

Each component of CloudAudition may specify its own installation procedure, but all of them share a common pattern of being shipped as docker images ready to use.

## Build process

The build process for CloudAudition is designed in a way that can be almost completely run locally to allow for easier verification before running in the CI system and publishing the artifacts.

To better accomplish this goal, the build process relies heavily in the use of **Docker**, in particular *Dockerfile* and *docker-compose*.

### Build requirements

- PowerShell core: Tested with version 6.2.1, but it could possibly function with older versions
- Docker: Tested with version 19.03.1, but it could possibly function with older versions
- Docker-Compose: Tested with version 1.24.1, but it could possibly function with older versions as long as they support version 3.4 of the YAML schema.

### Build structure

The main elements for the build process are the following:

* build.ps1: Act as the entry point for the build process, both locally and in the CI system. It provides options to select which particular task to run and which project to filter on.
* azure-pipelines.yml: Specification for the CI process on Azure DevOps
* version.txt: Contains the version number to be used for tagging the artifacts during the publishing stage.
* ProjectName.build.ps1: A per-project specification of the set tasks to run. It specifies as a minimum the *Build*, *Test* and *Publish* tasks

Below is a representation of the build structure

```powershell
│   azure-pipelines.yml
│   build.ps1
│   version.txt
│
├───CloudAuditionApi
│   │   .dockerignore
│   │   CloudAuditionApi.build.ps1
│   │   docker-compose.ci.yml
│   │   Dockerfile.ci
│   │
│   ├─── Rest of the subFolders and projects
│
└───PSDependencies
    └─── PowerShell dependencies
```

## License

This projects uses a MIT License which you can check [here](LICENSE)
