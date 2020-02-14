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

To import an assembly, use the `<UsingTask>` element, specifying which class/taks you want to import
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

Then, just use the imported class/task
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
All taks require the parameter `Repository="<path to repo>"`

## Available tasks

[GitAdd](#GitAdd)
[GitCheckout](#GitCheckout)
[GitClean](#GitClean)
[GitCommit](#GitCommit)
[GitGetSHA](#GitGetSHA)
[GitPull](#GitPull)
[GitPush](#GitPush)
[GitReset](#GitReset)
[GitTag](#GitTag)

### <a name="GitAdd"></a>GitAdd
```<GitAdd Repository="c:\repos\myrepo" Path="version/version.txt" Force="true"/>```
```> git add --force -- "version/version.txt"```
__Path__ (optional): path to files to be added. Defaults to __'.'__
__Force__ (optional): allows adding otherwise ignored files. Defaults to __false__

### <a name="GitCheckout"></a>GitCheckout
### <a name="GitClean"></a>GitClean
### <a name="GitCommit"></a>GitCommit
### <a name="GitGetSHA"></a>GitGetSHA
### <a name="GitPull"></a>GitPull
### <a name="GitPush"></a>GitPush
### <a name="GitReset"></a>GitReset
### <a name="GitTag"></a>GitTag
