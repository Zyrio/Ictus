Branches & Release Process
==========================

This repo has various branches in it, **which you must abide by** (otherwise releases will be screwed up).

It follows the standard used in most large Git projects, abeit a few tweaks.

## Development Branches ##

### master ###

This is the production branch, which should only be merged to from the `develop` branch. Unstable and work-in-progress should not go here.

Before merging from the `develop` branch, bump the version in `src/Yio/Data/Constants/AppDataConstant.cs` file to an appropriate number, and also change `<VersionPrefix>` in `src/Yio/Yio.csproj` (e.g. `1701.5` maps to `17.1.5`). On commit/merge, make sure you tag it, prefixed with the codename (e.g. `mojo/1701.5`).

### develop ###

This is the main development branch where work-in-progress and unstable code goes.

### feature/* ###

If you are working on an [issue](https://git.zyr.io/zyrio/yio/issues), you may choose to work on a separate branch. This branch should be named `ZIT-{Issue ID}` (padded with three 0s), and prefixed with `feature/` (e.g. `feature/ZIT-038` for [this issue](https://git.zyr.io/Zyrio/yio/issues/38)).

Make sure your git commits also include the issue number in the message, like `[ZIT-000] Message goes here`. This is also true if you're choosing to work on an issue on any othre branch.

After work is done, this branch should be merged into `develop`, and then closed when necessary.

## Release Branches ##

Branches used for release cannot be directly written or merged too, and code must be merged [via a Pull Request](https://bitbucket.org/Zyrio/yio/pull-requests/) from the `master` branch.

Please make sure the final commit message is "Release {version} to {environment}" (e.g. v1701.5 on Live should be "Release 1701.5 to Live").

### release/live ###

This branch is for the live environment ([yiff.co](http://yiff.co)).

## Other Branches ##

### hotfix ###

Currently, a `hotfix` branch does not exist, and is not used.