# Exercise 1 - Planets

In the accompanying [Get-Planets.ps1](Get-Planets.ps1) file, we see a function definition with the same name, that accepts a `string` input (defaulted to `*`) that returns a number of planet names.

The function follows PowerShell's begin-process-end pattern. The `process` block can be seen as the main function body, with `begin` and `end` happening before and after respectively. This structure is set up so that you can pass multiple vaules to a function in a pipeline and have the `process` execute once for each object.

In our example, we'll use the `begin` and `end` to output some debugging information.

- Open [Get-Planets.ps1](Get-Planets.ps1) and familiarize yourself with the contents
- Load the script into your PowerShell session by using dot-sourcing: `. ./Get-Planets.ps1`
- Run the script in your PowerShell session: `Get-Planets m*`
