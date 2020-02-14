# msbuildgit
git extensions for msbuild

This is an .net assembly with custom tasks for msbuild to handle common git commands.

## Quick start

A typical msbuild project has the following format
```xml
<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Target Name="Build">
    <Message Text="This is my build" />
  </Target>
</Project>
```

To import an assembly, use the `<UsingTask>` element, specifying which class you want to import
```xml
<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Target Name="Build">
    
    <!--======================================================================-->
    <UsingTask AssemblyFile="msbuild.git.dll" TaskName="msbuild.git.GitGetSHA" />
    <!--======================================================================-->
    
    <Message Text="This is my build" />
  </Target>
</Project>
```

Then, just use the imported class
```xml
<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Target Name="Build">
    
    <UsingTask AssemblyFile="msbuild.git.dll" TaskName="msbuild.git.GitGetSHA" />
    
    <!--======================================================================-->
    <GitGetSHA Repository="c:\repos\myrepo" Branch="origin/master">
			<Output TaskParameter="SHA1" PropertyName="WhateverPropertyOfYourChoice" />
		</GitGetSHA>
    <!--======================================================================-->
    
    <Message Text="HEAD of origin/master: $(WhateverPropertyOfYourChoice)" />
  </Target>
</Project>
```
