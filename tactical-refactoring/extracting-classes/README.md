# Extracting classes workshop

This workshop is all about breaking up complex pieces of code into simpler parts, and taking advantage of dependency injection as a class level to help make code more testable.

## The code

The `ExtractingClasses.sln` contains a number of simplified projects from a product called Sea Quoll Monitor, by a company called Blue Bridge. The product and it's architecture is suspiciously like the licensing code in SQL Monitor, and this exercise is based on some rearchitecture that took place within SQL Monitor early in 2021.

### Getting the code to run

You should be able to run the `BlueBridge.SeaQuollMonitor.Console` project and see something like the following written to the console:

```
Press R to refresh the licences or any other key to exit
```

If you press the `R` key, this will trigger a refresh of the licensing code within Sea Quoll Monitor, and it will emit further output that looks something like this:

```
Available license count: 70
Used license count: 4
Available license count: 70
Used license count: 4
Base Monitor: Central office
    development.internal.megacorp.com   Added 2021-10-29 07:44:07Z, Unlicensed, Suspended
    staging.internal.megacorp.com       Added 2021-10-29 07:44:09Z, Licensed, Active
    testing.internal.megacorp.com       Added 2021-10-29 05:44:07Z, Licensed, Active
Base Monitor: DMZ
    dr.secure.megacorp.com      Added 2021-10-27 07:44:07Z, Unlicensed, Suspended
    production.secure.megacorp.com      Added 2021-10-27 07:44:07Z, Licensed, Active
Base Monitor: Satellite office
    analytics.internal.megacorp.com     Added 2021-10-29 07:44:07Z, Unlicensed, Suspended
    research.internal.megacorp.com      Added 2021-10-27 07:44:07Z, Licensed, Active
```

You can poke the system in a number of ways, since all the persistence is stored in json files within the `Data` sub-folder.

1. Try changing the number of available licenses in the `licenses.json` file.
2. Try changing the requirements for any of the three base monitors by editing its corresponding `monitoredServers.json` file. Maybe add or remove some servers, or suspend monitoring of an existing server.

Making changes to these files will trigger a reallocation of licences, which will then update some of the files.

## The exercise

All of the license allocation and coordination happens inside `LicenseAllocator`, in the `private async Task DoRefresh()` method. It's responsible for the following:

1. Get the number of available licenses from the Regate licensing system.
2. Query all of the registered base monitors to see what servers they're monitoring, and thus what licenses are actually required.
3. Execute out an allocation algorithm to decide which servers will be licenses, given the potentially limited number of licenses actually available. Older servers get priority over more recently registered servers, and no licence is required for suspended servers.
4. Update the license status of the relevant servers from each base monitor.
5. Report back to the Regate licensing system how many licenses were actually used.

All of the above is carried out by `LicenseAllocator` using only two external dependencies, `ILicenseService` for communicating with the Redgate licensing system, and `IBaseMonitorRegistry` for communicating with all of the base monitors. This makes the above process and its five separate responsibilities difficult to test.

The task is to see much you can separate out the above responsibilities, so that they can be implemented by external services that can be injected into `LicenseAllocator`.

## Hints

The responsibility of items 1 and 5 above are already reasonably well isolated. They're single method calls on the `ILicenseService` interface. So focus more on these resposnsibilities:

- Retrieve all of the servers from all of the base monitors.
- The raw algorithm for deciding which servers should or should not be licenced.
- The algorithm to work out which servers have actually changed.
- Update the servers whose licensed state has been modified.

FWIW, the related code from SQL Monitor can be found in [EntitlementsCoordinator.RefreshAsync](https://github.com/red-gate/sqlmonitor/blob/main/Source/UI/Website/Infrastructure/Entitlements/EntitlementsCoordinator.cs#L62-L94). We extracted into separate services the act of retrieving licensing requirements from all of the base monitors, and then the act of pushing the licensing changes back to each base monitor.

## Bonus challenge

There's an awful lot of plumbing that takes place to wire together events triggered by changes that affect licensing. For example, The `LicenseAllocator` constructor registers event handlers on both `ILicenseService` and `IBaseMonitorRegistry`, and the latter in turn has complex chaining of event handlers through to each base monitor's `IMonitoredServersRepository`. Try stripping all of it our and replacing it with the following:

- An interface that provides the facility for any component interested changes to the licensing requirements to register that interest without being coupled to the source (or sources) of the event. i.e. the `LicenseAllocator` would want to register an event handler for this so that a refresh of the licensing allocation takes place when required.
- An interface that provides the facility for any component to raise an event to indicate that licensing requirements have changed. i.e. `LicenseService` would want to raise this event if the number of licenses available from the Redgate licensing system changes, and `MonitoredServersRepository` would raise this event if severs are added or removed, or their suspended state changes.
- A concrete class that implements both interfaces that basically joins the event emitters and event receivers togethers.

There's an example of this in SQL Monitor:
- [IEntitlementsChangedEventSink.cs](https://github.com/red-gate/sqlmonitor/blob/main/Source/UI/Website/Infrastructure/Entitlements/Events/IEntitlementsChangedEventSink.cs)
- [IEntitlementsChangedEventSource.cs](https://github.com/red-gate/sqlmonitor/blob/main/Source/UI/Website/Infrastructure/Entitlements/Events/IEntitlementsChangedEventSource.cs)
- [EntitlementsChangedEventCoordinator.cs](https://github.com/red-gate/sqlmonitor/blob/main/Source/UI/Website/Infrastructure/Entitlements/Events/EntitlementsChangedEventCoordinator.cs)
- [Dependency injection](https://github.com/red-gate/sqlmonitor/blob/2d71f4b66641431e779d4f77f8fa9a276c007300/Source/UI/Website/Infrastructure/DependencyInjection/WebsiteModule.cs#L103-L106)