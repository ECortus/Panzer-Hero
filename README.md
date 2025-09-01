# GIT-Resources-for-Unity

## Description

Custom .gitignore and .gitattributes files for Unity project repositories

I've been setting up git repos for some new Unity projects and thought it might be helpful to share my .gitignore and .gitattributes files with the community. 

## Reminder: 

* .gitignore is used to determine which files, directories and/or file types will be ignored in any given Git repos.
* .gitattributes is used to determine which large file types, typically binary files, are to be stored in Git LFS as opposed to the standard git repo. 

## Files

* .gitignore (for all Git repos)
* .gitattributes (only for Git LFS repos)
* gitignore_and_gitattributes.zip

## Notes

The .gitignore file was originally generated from Github for Unity and which I’m expanding upon.

The .gitattributes file is a hand assembled list, that's been tested in production and which I’m expanding upon.

gitignore_and_gitattributes.zip contains both the .gitignore & .gitattributes files within an archived directory. The files contained within the archived directory may be invisible when uncompressed.

## Warning

**It's always a good idea to start off with as complete as possible .gitignore and .gitattributes files at the the beginning of a project -> BEFORE anything else is added to the repo. You can end up in a nightmare scenario if you add Git LFS or .gitattributes items partway through a project.

## More info on .gitignore, .gitattributes and Git LFS

[More info on .gitignore and other .gitignore templates](https://github.com/github/gitignore)

[More info about Git LFS and .gitattribues](https://git-lfs.github.com/)
