# Encapsulate external dependencies

This solution contains a command-line to do app. It's a bit over-engineered so there's something
to work against when practicing refactoring :)

Currently, the ToDoApp.Engine project references Serilog directly, and uses Serilog types
directly. This makes it hard to change what logging library is uses. The aim of this session is
to isolate concrete references to Serilog to just one project, so that the logging library can
be changed in just one place.

I'd recommend following these steps:

- Create an ILogger interface, and an implementation that delegates to Serilog.
- Extract the ILogger interface to a new ToDoApp.Logging project.
- Extract the implementation that delegates to Serilog to a new ToDoApp.Logging.Serilog project.
- Change the classes that use logging to take a constructor parameter of an ILogger, rather than
  using a static reference. You'll need to pass in a stub instance in the tests.
- You should now be able to remove references to both Serilog and ToDoApp.Logging.Serilog from
  ToDoApp.Engine. The only project with a reference to Serilog should be ToDoApp.Logging.Serilog,
  and the only project with a reference to ToDoApp.Logging.Serilog should be ToDoApp (the entry
  point).
