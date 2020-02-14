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

[git add](https://git-scm.com/docs/git-add)

```<GitAdd Repository="c:\repos\myrepo" Path="path-to/file.txt" Force="true"/>```

```> git add --force -- "path-to/file.txt"```

__Path__ (optional): path to files to be added. Defaults to __'.'__

__Force__ (optional): allows adding otherwise ignored files. Defaults to __false__

### <a name="GitCheckout"></a>GitCheckout

[git checkout](https://git-scm.com/docs/git-checkout)

```<GitCheckout Repository="c:\repos\myrepo" Branch="master" Track="true" Quiet="true" Force="true"/>```

```> git checkout --force --quiet --track "master"```

__Branch__ (required): what to checkout. Can be a commit or a branch name.

__Quiet__ (optional): quiet, suppress feedback messages. Defaults to __false__

__Force__ (optional): when switching branches, proceed even if the index or the working tree differs from HEAD. This is used to throw away local changes. When checking out paths from the index, do not fail upon unmerged entries; instead, unmerged entries are ignored. Defaults to __false__

__Track__ (optional): When creating a new branch, set up "upstream" configuration. See "--track" in git-branch(1) for details. If no -b option is given, the name of the new branch will be derived from the remote-tracking branch, by looking at the local part of the refspec configured for the corresponding remote, and then stripping the initial part up to the "\*". This would tell us to use "hack" as the local branch when branching off of "origin/hack" (or "remotes/origin/hack", or even "refs/remotes/origin/hack"). If the given name has no slash, or the above guessing results in an empty name, the guessing is aborted.You can explicitly give a name with -b in such a case. Defaults to __false__

### <a name="GitClean"></a>GitClean
### <a name="GitCommit"></a>GitCommit
### <a name="GitGetSHA"></a>GitGetSHA
### <a name="GitPull"></a>GitPull
### <a name="GitPush"></a>GitPush
### <a name="GitReset"></a>GitReset
### <a name="GitTag"></a>GitTag
