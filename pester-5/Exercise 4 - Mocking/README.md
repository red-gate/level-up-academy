# Mocking

To avoid undesired side-effects while testing our code, we can mock out parts of our functions. Mocking can also be used to ensure that certain resources are being accessed.

- Copy the function and test files from the previous exercise, using the `cp` alias for Copy-Item. Pass the `-WhatIf` parameter to get more information on what is going to happen.
- Remove a lot of uncessesary output from the tests by adding `Mock Write-Host { }` to the `BeforeAll` block. Run your `Describe` block to verify that you don't see messages about "Listing planets".

## `-Verify` and `Should -Invoke`

- Add a `Mock` statement with `-Verifiable` as a parameter to the last unit test in the file.
- Add `Should -Invoke Write-Host 0` at the end of that same unit test.
- Run it.
- Make it pass.
- View the online documentation for Should (through PowerShell, not Google!)
- Rewrite the `Should` statement using its full parameter names.
