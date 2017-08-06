#tool "nuget:?package=GitVersion.CommandLine"

var target = Argument("target", "Default");
var nugetPackagesDir = Directory("./artifacts");

Task("Restore-NuGet-Packages")
    .Does(() =>
{
    NuGetRestore("./Xamarin.RangeSlider.sln");
});

Task("Build")
	.IsDependentOn("Update-Version")
	.IsDependentOn("Restore-NuGet-Packages")
	.Does (() =>
{
    DotNetBuild("./Xamarin.RangeSlider.sln", c => c.Configuration = "Release");
});

var nuGetPackSettings = new NuGetPackSettings
{
    BasePath = "./",
    OutputDirectory = nugetPackagesDir
};
Task("Pack-NugetPackages")
	.IsDependentOn("Build")
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
