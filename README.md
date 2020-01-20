# Xamarin DotNet Core Issue

This is a repository in order to test (Narrow down) a DotNet Core Extension library issue I'm having with a Xamarin Forms application. The Android app is building and running fine (Tested with Emulator: pixel_2_xl_oreo_8_0_538dpi Android 8.0 - API 26). However the build server is not able to build the solution.

A, seperate, full application has been released and is available on the Google Play Store.

## Setup
The solution is created with Visual Studio 2019 (Version 16.4.2) using a new Xamarin Forms template and then followed the blog post: https://montemagno.com/add-asp-net-cores-dependency-injection-into-xamarin-apps-with-hostbuilder/

## Issue
The build server is showing the following issues:

```
##[error]C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Xamarin\Android\Xamarin.Android.Common.targets(1808,2): Error XA2002: Can not resolve reference: `Microsoft.Bcl.AsyncInterfaces`, referenced by `Microsoft.Extensions.Hosting.Abstractions`. Please add a NuGet package or assembly reference for `Microsoft.Bcl.AsyncInterfaces`, or remove the reference to `Microsoft.Extensions.Hosting.Abstractions`.

##[error]C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Xamarin\Android\Xamarin.Android.Common.targets(1808,2): Error XA2002: Can not resolve reference: `System.Diagnostics.EventLog`, referenced by `Microsoft.Extensions.Logging.EventLog`. Please add a NuGet package or assembly reference for `System.Diagnostics.EventLog`, or remove the reference to `Microsoft.Extensions.Logging.EventLog`.

##[error]C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Xamarin\Android\Xamarin.Android.Common.targets(1808,2): Error XA2002: Can not resolve reference: `Microsoft.Bcl.AsyncInterfaces`, referenced by `System.Text.Json`. Please add a NuGet package or assembly reference for `Microsoft.Bcl.AsyncInterfaces`, or remove the reference to `System.Text.Json`.
```

I've created a public Team project on my Azure DevOps which can be viewed the this location: https://dev.azure.com/Chroomsoft/XamarinDotNetCoreIssue/_build?definitionId=44&_a=summary

The code is included in this GitHub repository.

The (YAML) build is trigged to run with every push to master. 

## Issue tracking
https://github.com/dotnet/extensions/issues/1631

Please feel free to Fork the repro to poke around. 
