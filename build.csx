///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target          = Argument<string>("target", "Default");
var configuration   = Argument<string>("configuration", "Debug");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////

var projectName = "Swasey";

// Directories
// WorkingDirectory is relative to this file. Make it relative to the Solution file.
var baseDir = GetContext().Environment.WorkingDirectory;
var packagingRoot = baseDir.Combine("Packaging");
var testResultsDir = baseDir.Combine("TestResults");
var nugetPackagingDir = packagingRoot.Combine(projectName);

// Files
var solutionInfoCs = baseDir.Combine("build").GetFilePath("SolutionInfo.cs");
var nuspecFile = baseDir.Combine("build").GetFilePath(projectName + ".nuspec");
var solution = baseDir.GetFilePath(projectName + ".sln");
var solutionDir = solution.GetDirectory();

var appVeyorEnv = AppVeyor().Environment;

// Get whether or not this is a local build.
var local = !BuildSystem().IsRunningOnAppVeyor;
var isReleaseBuild = !local && appVeyorEnv.Repository.Tag.IsTag;

// Release notes
var releaseNotes = ParseReleaseNotes(baseDir.GetFilePath("ReleaseNotes.md"));

// Version
var buildNumber = !isRelaseBuild ? 0 : appVeyorEnv.Build.Number;
var version = releaseNotes.Version.ToString();
var semVersion = isReleaseBuild ? version : (version + string.Concat("-build-", buildNumber));

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(() =>
{
    // Executed BEFORE the first task.
    Information("Running tasks...");
});

Teardown(() =>
{
    // Executed AFTER the last task.
    Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
	.Does(() =>
{
	// Clean Solution directories
	Information("Cleaning {0}", solutionDir);
	CleanDirectories(solutionDir + "/packages");
	CleanDirectories(solutionDir + "/**/bin/" + configuration);
	CleanDirectories(solutionDir + "/**/obj/" + configuration);

    foreach (var dir in new [] { packagingRoot, testResultsDir })
    {
         Information("Cleaning {0}", dir);
         CleanDirectories(dir.FullPath);
    }
});

Task("Restore")
    .IsDependentOn("Clean")
	.Does(() =>
{
	Information("Restoring {0}", solution);
	NuGetRestore(solution);
});

Task("AssemblyInfo")
    .IsDependentOn("Restore")
    .WithCriteria(() => !isReleaseBuild)
    .Does(() =>
{
    Information("Creating {0} - Version: {1}", solutionInfoCs, version);
    CreateAssemblyInfo(solutionInfoCs, new AssemblyInfoSettings {
        Product = projectName,
        Version = version,
        FileVersion = version,
        InformationalVersion = semVersion
    });
});

Task("Build")
	.IsDependentOn("AssemblyInfo")
	.Does(() =>
{
	Information("Building {0}", solution);
	MSBuild(solution, settings =>
        settings.SetConfiguration(configuration)
	);
});

Task("UnitTests")
    .IsDependentOn("Build")
    .Does(() =>
{
    Information("Running Tests in {0}", solution);
    XUnit2(
        solutionDir + "/**/bin/" + configuration + "/**/*.Tests*.dll",
        new XUnit2Settings {
            OutputDirectory = testResultsDir,
            HtmlReport = true
        }
    );
});

Task("CopyNugetPackageFiles")
    .IsDependentOn("UnitTests")
    .WithCriteria(() => isReleaseBuild)
    .Does(() =>
{
    var baseBuildDir = solutionDir.Combine(projectName).Combine("bin").Combine(configuration);

    var net45BuildDir = baseBuildDir.Combine("Net45");
    var net45PackageDir = nugetPackagingDir.Combine("lib/net45/");

    var dirMap = new Dictionary<DirectoryPath, DirectoryPath> {
        { net45BuildDir, net45PackageDir },
        { netcore45BuildDir, netcore45PackageDir },
        { portableBuildDir, portablePackageDir }
    };

    CleanDirectories(dirMap.Values);

    foreach (var dirPair in dirMap)
    {
        var files = new DirectoryInfo(dirPair.Key.FullPath)
            .EnumerateFiles()
            .Select(x => new FilePath(x.FullName));
        CopyFiles(files, dirPair.Value);
    }

    var packageFiles = new FilePath[] {
        solutionDir.CombineWithFilePath("LICENSE.txt"),
        solutionDir.CombineWithFilePath("README.md"),
        solutionDir.CombineWithFilePath("ReleaseNotes.md")
    };

    CopyFiles(packageFiles, nugetPackagingDir);
});

Task("CreateNugetPackage")
    .IsDependentOn("CopyNugetPackageFiles")
    .WithCriteria(() => isReleaseBuild)
    .Does(() =>
{
    NuGetPack(
        nuspecFile,
        new NuGetPackSettings {
            Version = semVersion,
            ReleaseNotes = releaseNotes.Notes.ToArray(),
            BasePath = nugetPackagingDir,
            OutputDirectory = packagingRoot,
            Symbols = false,
            NoPackageAnalysis = false
        }
    );
});

///////////////////////////////////////////////////////////////////////////////
// TASK TARGETS
///////////////////////////////////////////////////////////////////////////////

Task("Package")
    .IsDependentOn("CreateNugetPackage");

Task("Default")
    .IsDependentOn("Package");

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

Information("Building {0} [{1}] ({2} - {3}).", solution.GetFilename(), configuration, version, semVersion);

RunTarget(target);
