# Extracting classes workshop

This workshop is all about breaking up complex pieces of code into simpler parts, and taking advantage of dependency injection as a class level to help make code more testable.

## The code

The `ExtractingClasses.sln` contains a number of simplified projects from a product called Sea Quoll Monitor, by a company called Blue Bridge. The product and it's architecture is suspiciously like the licensing code in SQL Monitor, and this exercise is based on some rearchitecture that took place within SQL Monitor early in 2021.

### Getting the code to run

- The `Blue Bridge` sub-folder contains a bunch of configuration files for a number of base monitors. You should copy this folder as-is to `C:\ProgramData`, resulting in the following folder structure:
  - C:
    - ProgramData
      - Blue Bridge
        - Sea Quoll Monitor
          - BaseMonitorA
          - BaseMonitorB
          - BaseMonitorC
- The code hijacks the SQL Monitoring permits-based licensing in our UAT environment. For this to function correctly, you must either run from within the Redgate internal network or be connected to the VPN, as access to `https://permits.coredev-uat-1.testnet.red-gate.com` and `https://portal.coredev-uat-1.testnet.red-gate.com` is required.
- You should ideally have a Redgate ID account set up in our UAT environment. If you've been involved in licensing testing for a major version release of a product, this may already be in plcae. If not, some usable credentials will be made avaiable during the workshop.

With all of this in place, you should be able to run the `BlueBridge.SeaQuollMonitor.Console` project and see something like the following written to the console:

```
License management url: https://permits.coredev-uat-1.testnet.red-gate.com/activate?machineHash=V59AA9ACD414B81D8EE636CE0C81EE5ABABEC20B28137D510EBD01831AF1B3EB68&machineName=PANTALAIMON&productCode=18&majorVersion=12&returnurl=http
s%3A%2F%2Flocalhost%2Fseaquollmonitor%2F
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

You can poke the system in a couple of ways:
1. Visit the "license management url" that was written to the console. You'll need to login with a UAT Redgate ID, and then you'll be able to assign licences to your running ~SQL Monitor~ Sea Quoll Monitor software.
   - If you change the licenses assigned in the UAT portal, it can take a very long time for those changes to be pushed back to Sea Quoll Monitor, so use the `R` key to trigger a manual refresh.
   - If you don't have any SQL Monitor licenses available, you can create some at https://accountadmin.coredev-uat-1.testnet.red-gate.com/licensing/create. Please note, rather than logging in with a UAT Redgate ID, you must first log in with your Redgate domain credentials (full email address and password), and then change the `anne.employee@red-gate.com` to that of your UAT Redgate ID.
2. You can edit the `monitoredServers.json` files found in the `BaseMonitorA`, `BaseMonitorB` and `BaseMonitorC` sub-folders within `C:\ProgramData\Blue Bridge\Sea Quoll Monitor`. Try adding more servers or flipping the `IsSuspended` property between `true` and `false`. A file watcher will quickly respond to these changes and reallocate licenses, though you'll still need to press `R` to refresh the full view of the monitored servers.

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