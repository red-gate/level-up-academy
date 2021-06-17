# Calculator

Before starting this exercise, ensure that you can build and run the calculator.

## Task 1 - Extract the dependency on the console

The calculator as it stands prints the answer out to the console. Extract the dependency on the console so that the calculator can print to other locations.

## Task 2 - Use a DI framework

The calculator instance can now be constructed using a DI framework. Add a NuGet reference to Ninject, configure it to inject the printer into the calculator, and then use the kernel to get an instance of the calculator.

## Task 3 - More complex DI configuration

The printer isn't guaranteed to be error free, so we want to add some error handling to it. Using a [decorator pattern](https://www.dofactory.com/net/decorator-design-pattern), add error handling to the printer to print an message instead of the answer if the original printer throws an exception. Don't forget to configure it to be used.

To test that the error handler is working, change the original printer to throw an exception. What happens?

## Task 4 - Extracting configuration

You should now have some configuration in your `Main` method. As the code grows, this can be cumbersome to maintain. Extract this configuration into a Ninject module.

