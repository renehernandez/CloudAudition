

Task Default Test

Task Test UnitTest, IntegrationTest


Task Build {
    Invoke-BuildExec {
        dotnet build $PSScriptRoot/CloudAuditionApi.sln
    }
}


Task UnitTest Build, {
    Invoke-BuildExec {
        dotnet test $PSScriptRoot/CloudAuditionApi.UnitTests
    }
}


Task IntegrationTest Build, {
    Invoke-BuildExec {
        dotnet test $PSScriptRoot/CloudAuditionApi.IntegrationTests
    }
}
