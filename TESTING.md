# Testing ResoniteLink

ResoniteLink is tested using `xunit` v2 (for compatability reasons with ResonitLinks target framework being `netstandard2.0`). See [Getting Started with xUnit.net v2](https://xunit.net/docs/getting-started/v2/getting-started) for more information on this framework.

Tests for the whole project are stored within the `ResoniteLink.Tests` project, and the folder structure is as follows:
- `ResoniteLink.Tests/ResoniteLink`: contains tests for the `ResoniteLink` project
- `ResoniteLink.Tests/REPL`: contains tests for the `ResoniteLink.REPL` project

## Running Tests

To run the tests for the project, ensure you're in the root folder of the repository and run:

```bash
dotnet test
```

This will run the tests and show a summary at the end similar to the below:

```
Test summary: total: 22, failed: 0, succeeded: 22, skipped: 0, duration: 0.6s
Build succeeded with 3 warning(s) in 3.4s
```