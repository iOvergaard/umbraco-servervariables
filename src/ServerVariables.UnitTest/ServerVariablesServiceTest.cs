using Microsoft.Extensions.Configuration;
using ServerVariables.Services;

namespace ServerVariables.UnitTest;

public class ServerVariablesServiceTest
{
    private IServerVariablesService _serverVariablesService;

    [SetUp]
    public void Setup()
    {
        // Arrange
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("test_appsettings.json")
            .Build();
        _serverVariablesService = new ServerVariablesService(configuration);
    }

    [Test]
    public void Test_GetAppSettings()
    {

        // Act
        Dictionary<string, string?> appSettings = _serverVariablesService.GetAppSettings();

        // Assert
        Assert.That(appSettings, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(appSettings, Is.Not.Empty);
            Assert.That(appSettings.ContainsKey("FromAppSettings"), Is.True);
            Assert.That(appSettings["FromAppSettings"], Is.EqualTo("Hello from test_appsettings.json"));
        });
    }

    [Test]
    public void Test_SetVariable()
    {
        // Arrange
        _serverVariablesService.SetVariable("FromTest", "Hello from test");

        // Act
        Dictionary<string, string?> section = _serverVariablesService.GetSection("index");

        // Assert
        Assert.That(section, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(section, Is.Not.Empty);
            Assert.That(section.ContainsKey("FromTest"), Is.True);
            Assert.That(section["FromTest"], Is.EqualTo("Hello from test"));
        });
    }

    [Test]
    public void Test_SetVariableWithSection()
    {
        // Arrange
        _serverVariablesService.SetVariable("FromTest", "Hello from test", "test");

        // Act
        Dictionary<string, string?> section = _serverVariablesService.GetSection("test");

        // Assert
        Assert.That(section, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(section, Is.Not.Empty);
            Assert.That(section.ContainsKey("FromTest"), Is.True);
            Assert.That(section["FromTest"], Is.EqualTo("Hello from test"));
        });
    }

    [Test]
    public void Test_SetSection()
    {
        // Arrange
        Dictionary<string, string?> values = new Dictionary<string, string?>
        {
            {"FromTest", "Hello from test"},
            {"FromTest2", "Hello from test 2"}
        };

        // Act
        var result = _serverVariablesService.SetSection("test", values);

        // Assert
        Assert.That(result, Is.True);
        Dictionary<string, string?> section = _serverVariablesService.GetSection("test");
        Assert.That(section, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(section, Is.Not.Empty);
            Assert.That(section.ContainsKey("FromTest"), Is.True);
            Assert.That(section["FromTest"], Is.EqualTo("Hello from test"));
            Assert.That(section.ContainsKey("FromTest2"), Is.True);
            Assert.That(section["FromTest2"], Is.EqualTo("Hello from test 2"));
        });
    }
}
