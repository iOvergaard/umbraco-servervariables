using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ServerVariables.Services;
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.

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

        IOptions<ServerVariablesOptions> options =
            Options.Create(
                configuration
                    .GetSection(Constants.ServerVariablesSection)
                    .Get<ServerVariablesOptions>()
            )!;

        _serverVariablesService = new ServerVariablesService(options);
    }

    [Test]
    public void Test_FromAppSettings()
    {
        // Act
        Dictionary<string, dynamic> appSettings = _serverVariablesService.GetSection("index");

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
        _serverVariablesService.SetVariable("FromBoolean", true);

        // Act
        Dictionary<string, dynamic> section = _serverVariablesService.GetSection("index");

        // Assert
        Assert.That(section, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(section, Is.Not.Empty);
            Assert.That(section.ContainsKey("FromTest"), Is.True);
            Assert.That(section["FromTest"], Is.EqualTo("Hello from test"));
            Assert.That(section.ContainsKey("FromBoolean"), Is.True);
        });
    }

    [Test]
    public void Test_SetVariableWithSection()
    {
        // Arrange
        _serverVariablesService.SetVariable("FromTest", "Hello from test", "test");

        // Act
        Dictionary<string, dynamic> section = _serverVariablesService.GetSection("test");

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
        Dictionary<string, dynamic> variables = new()
        {
            {"FromTest", "Hello from test"},
            {"FromTest2", "Hello from test 2"}
        };

        // Act
        var result = _serverVariablesService.SetSection("test", variables);

        // Assert
        Assert.That(result, Is.True);
        Dictionary<string, dynamic> section = _serverVariablesService.GetSection("test");
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
