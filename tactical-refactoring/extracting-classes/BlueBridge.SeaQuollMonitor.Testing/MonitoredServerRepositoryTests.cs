using System;
using System.IO;
using System.Threading.Tasks;
using BlueBridge.SeaQuollMonitor.Domain;
using BlueBridge.SeaQuollMonitor.Monitoring;
using NSubstitute;
using NUnit.Framework;

namespace BlueBridge.SeaQuollMonitor.Testing
{
    [TestFixture]
    public class ErrorHandlingMonitoredServersRepositoryTests : MonitoredServersRepositoryTests
    {
        protected override IMonitoredServersRepository Create() =>
            new ErrorHandlingMonitoredServersRepository(base.Create());
    }

    [TestFixture]
    public class MonitoredServersRepositoryTests
    {
        protected virtual IMonitoredServersRepository Create()
        {
            const string monitoredServersJson = "monitoredServers.json";
            var baseDirectory = TestContext.CurrentContext.TestDirectory;

            var path = Path.Combine(baseDirectory, monitoredServersJson);
            Console.WriteLine(path);
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            var settingsRepository = Substitute.For<ISettingsRepository>();
            settingsRepository.BaseDirectory.Returns(baseDirectory);
            settingsRepository.GetValue("monitoredServersPath").Returns(monitoredServersJson);
            
            return new MonitoredServersRepository(settingsRepository);
        }

        [Test]
        public async Task GetAllServers_ForAnEmptyRepository_ShouldReturnEmpty()
        {
            var repository = Create();
            var servers = await repository.GetAllServers();
            Assert.That(servers, Is.Empty);
        }

        [Test]
        public async Task CreateServer_FollowedByGetServers_ShouldReturnTheCreatedServer()
        {
            var repository = Create();

            var server = new Server("localhost", DateTime.UtcNow, true, true);
            await repository.CreateServer(server);

            var servers = await repository.GetAllServers();

            Assert.That(servers, Is.EquivalentTo(new [] {server}));
        }

        [Test]
        public async Task CreateServer_FollowedByGetServerByName_ShouldReturnTheCorrectServer()
        {
            var repository = Create();

            var server1 = new Server("127.0.0.1", DateTime.UtcNow, true, true);
            var server2 = new Server("127.0.0.2", DateTime.UtcNow, true, true);
            await repository.CreateServer(server1);
            await repository.CreateServer(server2);

            var server = await repository.GetServer(server2.Name);

            Assert.That(server, Is.EqualTo(server2));
        }

        [Test]
        public async Task CreateThenUpdateThenGet_ShouldReturnTheModifiedServer()
        {
            var repository = Create();

            const string serverName = "localhost";
            var originalServer = new Server(serverName, DateTime.UtcNow, true, true);
            await repository.CreateServer(originalServer);

            var modifiedServer = originalServer with { IsLicensed = false };
            await repository.UpdateServer(modifiedServer);

            var retrievedServer = await repository.GetServer(serverName);
            Assert.That(retrievedServer, Is.EqualTo(modifiedServer));
            Assert.That(retrievedServer, Is.Not.EqualTo(originalServer));
        }

        [Test]
        public async Task RemoveServer_FollowedByGetAllServers_ShouldNotReturnTheRemovedServer()
        {
            var repository = Create();

            var server1 = new Server("127.0.0.1", DateTime.UtcNow, true, true);
            var server2 = new Server("127.0.0.2", DateTime.UtcNow, true, true);
            await repository.CreateServer(server1);
            await repository.CreateServer(server2);

            Assert.That(await repository.GetAllServers(), Is.EquivalentTo(new [] {server1, server2}));

            await repository.RemoveServer(server1.Name);
            Assert.That(await repository.GetAllServers(), Is.EquivalentTo(new [] {server2}));
        }
    }
}
