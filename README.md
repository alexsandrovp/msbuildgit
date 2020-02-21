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
  
  <!--======================================================================-->
  <UsingTask AssemblyFile="msbuild.git.dll" TaskName="msbuild.git.GitGetSHA" />
  <!--======================================================================-->
  
  <Target Name="Build">
    <Message Text="This is my build" />
  </Target>
</Project>
```

Then, just use the imported class/task
```xml
<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  
  <UsingTask AssemblyFile="msbuild.git.dll" TaskName="msbuild.git.GitGetSHA" />
  
  <Target Name="Build">
    
    <!--======================================================================-->
    <GitGetSHA Repository="c:\repos\myrepo" Branch="origin/master">
        <Output TaskParameter="SHA" PropertyName="WhateverPropertyOfYourChoice" />
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

```xml
<GitAdd Repository="c:\repos\myrepo" Path="path-to/file.txt" Force="true"/>
```

```bash
> git add --force -- "path-to/file.txt"
```

__Path__ (optional): path to files to be added. See [pathspec](https://git-scm.com/docs/git-add#Documentation/git-add.txt-ltpathspecgt82308203). Default: __'.'__

__Force__ (optional): allows adding otherwise ignored files. See [--force](https://git-scm.com/docs/git-add#Documentation/git-add.txt---force). Default: __false__


### <a name="GitCheckout"></a>GitCheckout

[git checkout](https://git-scm.com/docs/git-checkout)

```xml
<GitCheckout Repository="c:\repos\myrepo" Branch="master" Track="true" Quiet="true" Force="true"/>
```

```bash
> git checkout --force --quiet --track "master"
```

__Branch__ (required): what to checkout. Can be a commit or a branch name. See [branch](https://git-scm.com/docs/git-checkout#Documentation/git-checkout.txt-ltbranchgt)

__Quiet__ (optional): quiet, suppress feedback messages. See [--quiet](https://git-scm.com/docs/git-checkout#Documentation/git-checkout.txt--q). Default: __false__

__Force__ (optional): when switching branches, proceed even if the index or the working tree differs from HEAD. This is used to throw away local changes. See [--force](https://git-scm.com/docs/git-checkout#Documentation/git-checkout.txt---force). Default: __false__

__Track__ (optional): When creating a new branch, set up "upstream" configuration. See [--track](https://git-scm.com/docs/git-checkout#Documentation/git-checkout.txt---track). Default: __false__


### <a name="GitClean"></a>GitClean

[git clean](https://git-scm.com/docs/git-clean)

```xml
<GitClean Repository="c:\repos\myrepo" Quiet="true" Force="true"
          UntrackedDirectories="true" EvenIgnoredEntries="true" Exclude="*.txt"/>
```

```bash
> git clean --quiet --force -d -x --exclude="*.txt*"
```

__Quiet__ (optional): be quiet, only report errors, but not the files that are successfully removed. See [--quiet](https://git-scm.com/docs/git-clean#Documentation/git-clean.txt---quiet). Default: __false__

__Force__ (optional): See [--force](https://git-scm.com/docs/git-clean#Documentation/git-clean.txt---force). Default: __true__

__UntrackedDirectories__ (optional): remove untracked directories. See [-d](https://git-scm.com/docs/git-clean#Documentation/git-clean.txt--d). Default: __true__

__EvenIgnoredEntries__ (optional): remove ignored files too. See [-x](https://git-scm.com/docs/git-clean#Documentation/git-clean.txt--x). Default: __true__

__OnlyIgnoredEntries__ (optional): remove only ignored files. See [-X](https://git-scm.com/docs/git-clean#Documentation/git-clean.txt--X). Default: __false__

__Exclude__ (optional): do not clean these files. See [--exclude](https://git-scm.com/docs/git-clean#Documentation/git-clean.txt---excludeltpatterngt)


### <a name="GitCommit"></a>GitCommit

[git commit](https://git-scm.com/docs/git-commit)

```xml
<GitCommit Repository="c:\repos\myrepo" Message="your commit message"/>
```

```bash
> git commit --message "your commit messsage"
```

__Message__ (optional): commit message. See [--message](https://git-scm.com/docs/git-commit#Documentation/git-commit.txt---messageltmsggt). Default: __"Auto-commit from msbuild"__


### <a name="GitGetSHA"></a>GitGetSHA

[git rev-parse](https://git-scm.com/docs/git-rev-parse)

```xml
<GitGetSHA Repository="c:\repos\myrepo" Branch="master">
  <Output TaskParameter="SHA" PropertyName="MyProperty" />
</GitGetSHA>
```

```bash
> git rev-parse "master"
```

__Branch__ (optional): name of branch to retrieve the SHA hash. Default: __"HEAD"__

__SHA__ (output): receives the sha1 hash of the selected branch


### <a name="GitPull"></a>GitPull

[git pull]()

```xml
<GitPull Repository="c:\repos\myrepo" Quiet="true" Force="true"
         Verbose="true" Rebase="true" Origin="myserver"/>
```

```bash
> git pull --quiet --force --verbose --rebase "myserver"
```

__Quiet__ (optional): See [--quiet](https://git-scm.com/docs/git-pull#Documentation/git-pull.txt---quiet). Default: __false__

__Force__ (optional): See [--force](https://git-scm.com/docs/git-pull#Documentation/git-pull.txt---force). Default: __false__

__Verbose__ (optional): See [--verbose](https://git-scm.com/docs/git-pull#Documentation/git-pull.txt---verbose). Default: __false__

__Rebase__ (optional): rebase the current branch on top of the upstream branch after fetching. See [--rebase](https://git-scm.com/docs/git-pull#Documentation/git-pull.txt---rebasefalsetruemergespreserveinteractive). Default: __false__

__Origin__ (optional): server to fetch/pull from. See [repository](https://git-scm.com/docs/git-pull#Documentation/git-pull.txt-ltrepositorygt). Default: __"origin"__


### <a name="GitPush"></a>GitPush

[git push](https://git-scm.com/docs/git-push)

```xml
<GitPush Repository="c:\repos\myrepo" Tags="true" Target="myserver" Branch="mybranch"/>
```

```bash
> git push --tags "myserver" "mybranch"
```

__Tags__ (optional): push tags. See [--tags](https://git-scm.com/docs/git-push#Documentation/git-push.txt---tags). Default: __false__

__Target__ (optional): where to push to. See [repository](https://git-scm.com/docs/git-push#Documentation/git-push.txt-ltrepositorygt). Default: __"origin"__

__Branch__ (optional): branch to push. Default: __"HEAD"__


### <a name="GitReset"></a>GitReset

[git reset](https://git-scm.com/docs/git-reset)

```xml
<GitReset Repository="c:\repos\myrepo" Quiet="true" Mode="hard" Commit="origin/master"/>
```

```bash
> git reset --hard --quiet "origin/master"
```

__Quiet__ (optional): be quiet, only report errors. See [--quiet](https://git-scm.com/docs/git-reset#Documentation/git-reset.txt---quiet). Default: __true__

__Mode__ (optional): reset mode. See [mode](https://git-scm.com/docs/git-reset#Documentation/git-reset.txt-emgitresetemltmodegtltcommitgt). Default: __hard__

__Commit__ (optional): the commit to reset to. Not used by default.


### <a name="GitTag"></a>GitTag

[git tag](https://git-scm.com/docs/git-tag)

```xml
<GitTag Repository="c:\repos\myrepo" Force="true" Delete="false" Annotate="true"
        Message="my commit message" Commit="c922c83" Tag="my_tag_name"/>
```

```bash
> git tag --force --annotate --message "my commit message" "my_tag_name" "c922c83"
```

__Force__ (optional): replace an existing tag with the given name (instead of failing). See [--force](https://git-scm.com/docs/git-tag#Documentation/git-tag.txt---force). Default: __false__

__Delete__ (optional): delete existing tags with the given names. If used, Annotate, Message and Commit are ignored. See [--delete](https://git-scm.com/docs/git-tag#Documentation/git-tag.txt---delete). Default: __false__

__Annotate__ (optional): make an annotated tag (meant for release). See [--annotate](https://git-scm.com/docs/git-tag#Documentation/git-tag.txt---annotate). Default: __true__

__Message__ (optional): optional commit message. See [--message](https://git-scm.com/docs/git-tag#Documentation/git-tag.txt---messageltmsggt). Default: __"Tag added automatically by msbuild"__

__Commit__ (optional): what to tag. See [commit](https://git-scm.com/docs/git-tag#Documentation/git-tag.txt-ltcommitgt). Not used by default

__Tag__ (required): name of tag to set. See [tagname](https://git-scm.com/docs/git-tag#Documentation/git-tag.txt-lttagnamegt).

