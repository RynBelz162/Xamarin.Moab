# ☢️ Xamarin.Moab
This repository is a Xamarin.iOS swift binding library for the popular [Nuke](https://kean.blog/nuke/home).

This repository is also inspired by the popular [Xamarin.Forms.Nuke](https://github.com/roubachof/Xamarin.Forms.Nuke).

This project currently binds for the `10.9.0` version of `Nuke`

### Get Nuke from Carthage
- Not using swift package manager because it does not seem to work with Xamarin.iOS binding projects.
- `brew install carthage`
- `carthage update`
- Add Nuke framework from `/Carthage` directory

### 📝 Building steps
- xCode, Visual Studio, and Objective Sharpie need to be installed.
- Run `sh build-fat.sh` to build the frameworks which will output to the `framework_output` folder.
- For objective sharpie to generate the `ApiDefinition.cs` make sure to uncomment out the last few lines of the `build-fat.sh` file. They are commented out because it throws an error but still kind of works.
- Navigate to Visual Studio `Xamarin.Moab` project and make sure the frameworks are linked in Native Frameworks.
- Build and run the `Xamarin.Moab.Sample` profect.

### 📝 Steps to make changes to binding coverage
- Open the `NukeProxy` xcode project.
- Make updates to the `NukeProxy.swift` file.
- Build in xCode.
- Run "Building Steps" from above.

### ⚠️ Notes
- Objective sharpie outputs `NSURL` sometimes so you may need to manually update it to just `NSUrl`
- Objective sharipe like to include the `using NukeProxy` which does not exist. Just remove it.