#tool "nuget:?package=vswhere"
#tool "nuget:?package=GitVersion.CommandLine"
#tool nuget:?package=MSBuild.SonarQube.Runner.Tool
#addin nuget:?package=Cake.Sonar

var target = Argument("target", "Default");
var nugetPackagesDir = Directory("./artifacts");
var msBuildPath = VSWhereLatest().CombineWithFilePath("./MSBuild/15.0/Bin/MSBuild.exe");
var solution = "./Xamarin.RangeSlider.sln";

Task("Restore-NuGet-Packages")
    .Does(() =>
{
    NuGetRestore(solution);
});

Task("SonarBegin")
  .Does(() => {
     SonarBegin(new SonarBeginSettings{
        Url = "https://sonarqube.com",
        Login = "halkar@github",
        Verbose = true
     });
  });

Task("SonarEnd")
  .Does(() => {
     SonarEnd(new SonarEndSettings{
        Login = "halkar@github"
     });
  });

Task("Build")
	.IsDependentOn("Update-Version")
	.IsDependentOn("Restore-NuGet-Packages")
	.Does (() =>
{
    MSBuild(solution, new MSBuildSettings() {
            ToolPath = msBuildPath
        }
        .SetConfiguration("Release")
        .SetVerbosity(Verbosity.Minimal)
        .SetNodeReuse(false));
});

var nuGetPackSettings = new NuGetPackSettings
{
    BasePath = "./",
    OutputDirectory = nugetPackagesDir
};
Task("Pack-NugetPackages")
    .IsDependentOn("SonarBegin")
	.IsDependentOn("Build")
    .IsDependentOn("SonarEnd")
	.Does (() =>
{
    CreateDirectory(nugetPackagesDir);
    NuGetPack("./Xamarin.RangeSlider.nuspec", nuGetPackSettings);
	NuGetPack("./Xamarin.Forms.RangeSlider.nuspec", nuGetPackSettings);
});

Task("Update-Version")
	.Does(() => {
        GitVersion(new GitVersionSettings{
            UpdateAssemblyInfo = true,
            OutputType = GitVersionOutput.BuildServer
        });
        var versionInfo = GitVersion(new GitVersionSettings{ OutputType = GitVersionOutput.Json });
		var files = GetFiles("./*.nuspec");
		foreach(var file in files)
		{
			Information("Updating {0} with MajorMinorPatch: {1}", file, versionInfo.MajorMinorPatch);
			TransformTextFile(file)
				.WithToken("version", versionInfo.MajorMinorPatch)
				.Save(file);
		}
    });

Task("Default")
	.IsDependentOn("Pack-NugetPackages");

RunTarget (target);
