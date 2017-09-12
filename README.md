Ictus
=====

![Screenshot of Yiff.co](https://zyr.io/assets/img/posts/yiffco-004.png)

The code behind [Yiff.co](https://nsfw.ducky.ws?url=https://yiff.co), now open to all!

## Development

### Running

#### Pre-Requsites

 - .NET Core SDK 2.0+ (see: https://www.microsoft.com/net/download#core)
 - Git (see: https://git-scm.com/)

#### Setting Up

This assumes you have already pulled the repository and are in the root directory.

Restore needed packages...

```
$ cd src/Ictus
$ dotnet restore # Restore NuGet packages
$ cd ../..
```

Copy `src/Ictus/appsettings.json.example` to `src/Ictus/appsettings.json` and configure accordingly.

Set build envronment...

```
export ASPNETCORE_ENVIRONMENT="Development"
```

Run Webapp project to start the web server...

```
$ cd src/Ictus/
$ dotnet run
```

### Versioning

Ictus's versioning is split up into two parts (although, technically three):

`{1}{2}.{3}`

 - `{1}` Short-hand year (e.g. 2017 becomes 17)
 - `{2}` Release number; an incrementing number, with one leading zero, starting at 1 (e.g. 1 becomes 01). Passing 99 does not reset it back to 00, but goes to 100. This increments everytime there is a new/removed feature, and resets everytime `{1}` increments.
 - `{3}` Patch number; an incrementing number, not shown when 0, starting at 0. This increments everytime there is a fix, under-the-hood change, or visible minor change, and resets everytime `{2}` increments.

For example, **1709.4** translates into **Release 9 for 2017, Patch 4**.

The version also translates into three parts, which is needed for `<VersionPrefix>` in `src/Ictus/Ictus.csproj`. It's easy enough to not explain, but show in a few examples:

 - `17.7.0` is `1707`
 - `18.13.5` is `1813.5`
 - `19.1.1` is `1901.1`
 - `20.0.0` is `2000`, which is not valid.

Every release (`{2}`) also triggers a new codename. Currently, there is, has been, and will be:

 - 1701 'Frisky Fox'
 - 1702 'Kinky Kiwi'
 - 1703 'Dazzling Duck'
 - 1704 'Zesty Zebra'
 - 1705 'Hung Horse'
 - 1706 'Lewd Lion'
 - 1707 'Fabulous Flamingo' *(current)*
 - 1708 'Twinky Tiger' *(future)*

### Branches

#### master

The `master` branch is for production code, which only should ever be merged to when the version increments.

Before merging to `master`, make sure to update the version in `src/Ictus/Data/Constants/VersionConstant.cs` and `src/Ictus/Ictus.csproj`. See the "Versioning" paragraph for more details.

Commits must also be tagged for new versions, prefixed with a short version of the codename; for example, **1703 'Dazzling Duck'** becomes `duck/1703`

#### develop

The `develop` branch is for unstable code.

#### feature/*

If you are working on an issue, you may choose to work on a separate branch. This branch should be named {Tracker}-{Issue ID} (padded with three 0s), and prefixed with feature/ (e.g. feature/ZIT-038 for issue 38 on Zyrio Git).

 - Use `BB-XXX` for *BitBucket*.
 - Use `GH-XXX` for *GitHub*.
 - Use `VSO-XXX` for *Visual Studio Online*
 - Use `ZIT-XXX` for *Zyrio Git*.

Make sure your git commits also include the issue number in the message, like `[ZIT-000] Message goes here`. This is also true if you're choosing to work on an issue on any other branch.

After work is done, this branch should be merged into `develop`, and then closed when necessary.