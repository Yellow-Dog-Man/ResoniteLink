---
title: GitHub Actions
---

The repository for ResoniteLink has several GitHub Actions workflows that are divided in two types.

## Composite Actions

For the Composite Actions used within this repository, please go to [Yellow-Dog-Man/composite-actions-templates](https://github.com/Yellow-Dog-Man/composite-actions-templates).

## Workflows

Workflows are what's executing and building all the steps, and most importantly, using the Actions.

### build-publish

In charge of building and publishing ResoniteLink when needed.

It also builds a release version of REPL and attaches to the latest tag.

#### Triggers

- When a push is done on any branch
- When a pull request is opened, re-opened or synchronized
- When a release is published

#### Actions used

- `actions/checkout@v6`
- `Yellow-Dog-Man/composite-actions-templates/.github/actions/dotnet-build@main`
- `Yellow-Dog-Man/composite-actions-templates/.github/actions/dotnet-build@main`
- `softprops/action-gh-release@v2`

### docs-generation

In charge of generating, then publishing the documentation you're reading right now.

#### Triggers

- When a release is published
- Manually via `workflow_dispatch`

#### Actions used

- `actions/checkout@v6`
- `actions/setup-dotnet@v5`
- `actions/upload-pages-artifact@v4`
- `actions/deploy-pages@v4`

#### Inputs

If manual:

| Name  | Description                                 | Default  | Required |
|-------|---------------------------------------------|----------|----------|
| `ref` | The branch or tag the workflow will run on. | `master` | false    |

#### Deployment environment

- `github-pages` to https://yellow-dog-man.github.io/ResoniteLink/
