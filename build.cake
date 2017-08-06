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
		Information("MajorMinorPatch: {0}", versionInfo.MajorMinorPatch);
		Information("FullSemVer: {0}", versionInfo.FullSemVer);
		Information("InformationalVersion: {0}", versionInfo.InformationalVersion);
		Information("LegacySemVer: {0}", versionInfo.LegacySemVer);
		Information("Nuget v1 version: {0}", versionInfo.NuGetVersion);
		Information("Nuget v2 version: {0}", versionInfo.NuGetVersionV2);
    });

Task("Default")
	.IsDependentOn("Pack-NugetPackages");

RunTarget (target);
