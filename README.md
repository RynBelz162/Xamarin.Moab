# ☢️ Xamarin.Moab
This repository is a Xamarin.iOS swift binding library for the popular [Nuke](https://kean.blog/nuke/home).

This repository is also inspired by the popular [Xamarin.Forms.Nuke](https://github.com/roubachof/Xamarin.Forms.Nuke).

This project currently binds for the `11.6.4` version of `Nuke`
This project also currently builds using iOS verion 16.2 as specified in `build-fat.sh`

### 📝 Steps to make changes to binding coverage
- Open the `NukeProxy` xcode project.
- Update the swift package for Nuke if upgrading to a newer version
- Make updates to the `NukeProxy.swift` file.
- Build in xCode.
- Run "Building Steps" from above.

### 📝 Building steps
- xCode, Visual Studio, and Objective Sharpie need to be installed.
- Run `sh build-fat.sh` to build the NukeProxy framework which will output to the `framework_output` folder.
- For objective sharpie to generate the `ApiDefinition.cs` make sure to uncomment out the last few lines of the `build-fat.sh` file. They are commented out because it throws an error but still kind of works.
- Navigate to Visual Studio `Xamarin.Moab` project and make sure the frameworks are linked in Native Frameworks.
- Build and run the `Xamarin.Moab.Sample` profect.

### ⚠️ Notes
- `build-fat.sh` will output some header file errors but they can be ignored.
- Objective sharpie outputs `NSURL` sometimes so you may need to manually update it to just `NSUrl`
- Objective sharipe likes to include the `using NukeProxy` which does not exist. Just remove it.
- Carthage is outdated and those files can be ignored, the XCode project now uses swift packages as references.