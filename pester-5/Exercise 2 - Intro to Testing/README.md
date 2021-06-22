# Exercise 2 - Intro to Testing

> Pester uses a file naming convention *.Tests.ps1 for test files. Those files are typically named after the tested function, and are placed next to a file that contains the function. The function file is imported via dot-sourcing in the BeforeAll on top of the file. And $PSScriptRoot is typically used to provide a relative path to the function file.

([source](https://pester.dev/docs/quick-start))

- Copy the `Get-Planets.ps1` file over to this folder, using `Copy-Item`.
- Create a test file, following the naming conventions.
- Add a `Describe` block with the name of the function we'll test to the file.
- Add a `BeforeAll` block inside the `Describe`. In the `BeforeAll`, use dot-sourcing to load the file hosting the functions we want to test.
- Add an `It` block after the `BeforeAll`:

```ps
    It 'Lists all planets by default' {
        $allPlanets = Get-Planets
        $allPlanets.Count | Should -Be 8
    }
```

- Run the test using Visual Studio Code's *Code Lens*.
- Place a breakpoint (`F9`) on the line that contains your `| Should` and then using Visual Studio Code's Code Lens to debug your test.
- Ensure that you can see the contents of `$allPlanets` by hovering over the variable.

If you're running into issues with Pester at this point, make sure you upgrade it to the latest version: `Install-Module -Name Pester -Force -SkipPublisherCheck`
