# REPO_NAME
This project was generated using the [CQRS Microservice Template](https://github.com/thirschel/dotnet-cqrs-microservice-template)!

## Setting up development environment 🛠

### 1. Add a config file

### 2. Build the solution

> You can also use Visual Studio or VS Code to build and run the solution.

|||||
|-|-|-|-|
|Windows| .Net Core | > ```dotnet build PROJECT_NAME.sln``` |
|| Docker | > ```docker build -t REPO_NAME .``` |
|Linux| .Net Core | $ ```make build``` |
|| Docker | $ ``` make build-docker``` |

## 3. Run the solution


|||||
|-|-|-|-|
|Windows| .Net Core | > ```dotnet run --project src\PROJECT_NAME.Api\``` |
|| Docker | > ```docker run -d -e [envar=value] -p 5000:80 REPO_NAME```|
|Linux| .Net Core | $ ```make run``` |
|| Docker | $ ```make run-docker``` |

## 4. Try it out!

[http://localhost:5000/swagger](http://localhost:5000/swagger)


## Contributing 🤝

This repository follows [conventional commits](https://www.conventionalcommits.org/en/v1.0.0/#summary). 

This leads to **more readable messages** that are easy to follow when looking through the **project history**. But also,
we use the git commit messages to **generate the change log**. 

Below is an explanation, along with examples, of how these commit messages are written.

### Commit Message Format

```code
<type>(<scope>): <subject>
<BLANK LINE>
<body>
<BLANK LINE>
<footer>
```

```text
docs(changelog): update changelog to beta.5
```

```text
fix(release): need to depend on latest rxjs and zone.js
The version in our package.json gets copied to the one we publish, and users need the latest of these.
```

### Revert

If the commit reverts a previous commit, it should begin with `revert:`, followed by the header of the reverted commit. In the body it should say: `This reverts commit <hash>.`, where the hash is the SHA of the commit being reverted.

### Type

Must be one of the following:

- **build**: Changes that affect the build system or external dependencies
- **ci**: Changes to our CI configuration files and scripts
- **docs**: Documentation only changes
- **feat**: A new feature
- **fix**: A bug fix
- **perf**: A code change that improves performance
- **refactor**: A code change that neither fixes a bug nor adds a feature
- **style**: Changes that do not affect the meaning of the code (white-space, formatting, missing semi-colons, etc)
- **test**: Adding missing tests or correcting existing tests

### Scope

The scope should be the name of the area of work affected (as perceived by the person reading the changelog generated from commit messages.

There are currently a few exceptions to the "use area of work" rule:

- **changelog**: used for updating the release notes in CHANGELOG.md

- none/empty string: useful for `style`, `test` and `refactor` changes that are done across all
  packages (e.g. `style: add missing semicolons`) and for docs changes that are not related to a
  specific package (e.g. `docs: fix typo in tutorial`).

### Subject

The subject contains a succinct description of the change:

- use the imperative, present tense: "change" not "changed" nor "changes"
- don't capitalize the first letter
- no dot (.) at the end

### Body

Just as in the **subject**, use the imperative, present tense: "change" not "changed" nor "changes".
The body should include the motivation for the change and contrast this with previous behavior.

### Footer

The footer should contain any information about **Breaking Changes** and is also the place to
reference GitHub issues that this commit **Closes**.

**Breaking Changes** should start with the word `BREAKING CHANGE:` with a space or two newlines. The rest of the commit message is then used for this.