var target = Argument("target", "Default");
var nugetPackagesDir = Directory("./artefacts");

Task("Restore-NuGet-Packages")
    .Does(() =>
{
    NuGetRestore("./Xamarin.RangeSlider.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
	  .Does (() =>
{
    NuGetRestore("./Xamarin.RangeSlider.sln");
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

Task("Default")
    .IsDependentOn("Pack-NugetPackages");

RunTarget (target);
