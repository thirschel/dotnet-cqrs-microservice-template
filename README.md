# CQRS Microservice Template

## What is this?
This project acts as a dotnet cli custom template when using `dotnet new`. This will scaffold a new microservice with the following features:

| Feature |
|---|
| .NET 7.0  |
| [Clean Architecture](http://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)  |
| [CQRS](https://martinfowler.com/bliki/CQRS.html)  |
|  Docker File   |

## Requirements

| Requirement |
|---|
| .NET 7.0 SDK  |

## Installation 
1. Clone this repo
2. Install the template to your sdk `dotnet new -i <PATH TO REPO>`. This will create a new template for you to use: `cqrs-ms`.

#### Uninstalling
1. `dotnet new -u`
    * This will produce output that looks like 
    ```
    ...
    Templates:
      CQRS Microservice (cqrs-ms) C#
    Uninstall Command:
      dotnet new -u C:\dev\cqrs-ms
    ...
2. Run the uninstall command for the `cqrs-ms` template

#### Upgrading
When upgrading, it is best practice to uninstall the template and then re-install after you have pulled the latest

## Usage

| Parameter | Description |
|---|---|
| projectName `-P`  | Standard project name for the template. Used as prefix for all files and namespaces eg (`ProjectName.Api`) |
| repoName `-r`  | The name of the repository. In most cases will be the same value as `Output` |
| output `-o`  | The output directory. Defaults to the current directory. In most cases will be the same value as `repoName` |

**__If `-o` is not supplied the project will be created in the current working directory__**

**__In most cases `repoName (-r)` and `output (-o)` will be the same. Due to the cli they needed to be different parameters__**

`dotnet new cqrs-ms -p <PROJECT NAME> -r <REPO NAME> -o <NEW FOLDER NAME>`
