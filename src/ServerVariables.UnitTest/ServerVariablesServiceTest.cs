using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ServerVariables.Models;
using ServerVariables.Services;
using Umbraco.Cms.Core;

#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.

namespace ServerVariables.UnitTest;

public class ServerVariablesServiceTest
{
    private ServerVariablesService _serverVariablesService;

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
            { "FromTest", "Hello from test" }, { "FromTest2", "Hello from test 2" }
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

    [Test]
    public void Test_GetAll()
    {
        // Arrange
        _serverVariablesService.SetVariable("FromTest", "Hello from test");
        _serverVariablesService.SetVariable("FromBoolean", true);
        _serverVariablesService.SetVariable("FromTest", "Hello from test", "test");

        // Act
        Dictionary<string, Dictionary<string, dynamic>> all = _serverVariablesService.GetAll();

        // Assert
        Assert.That(all, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(all, Is.Not.Empty);
            Assert.That(all.ContainsKey("index"), Is.True);
            Assert.That(all["index"], Is.Not.Empty);
            Assert.That(all["index"].ContainsKey("FromTest"), Is.True);
            Assert.That(all["index"]["FromTest"], Is.EqualTo("Hello from test"));
            Assert.That(all["index"].ContainsKey("FromBoolean"), Is.True);
            Assert.That(all["index"]["FromBoolean"], Is.EqualTo(true));
            Assert.That(all.ContainsKey("test"), Is.True);
            Assert.That(all["test"], Is.Not.Empty);
            Assert.That(all["test"].ContainsKey("FromTest"), Is.True);
            Assert.That(all["test"]["FromTest"], Is.EqualTo("Hello from test"));
        });
    }

    [Test]
    public void Test_GetPagedItems()
    {
        // Arrange
        _serverVariablesService.SetVariable("FromTest", "Hello from test");
        _serverVariablesService.SetVariable("FromBoolean", true);
        _serverVariablesService.SetVariable("FromTest", "Hello from test", "test");

        // Act
        Attempt<IEnumerable<ServerVariablesCollectionResponseModel>?, CollectionOperationStatus> result =
            _serverVariablesService.GetPagedItems("section", Direction.Ascending, null, 0, 2, CancellationToken.None,
                out var totalNumberOfItems);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.Result, Is.Not.Empty);
            Assert.That(totalNumberOfItems, Is.EqualTo(3));
        });
    }

    [Test]
    public void Test_GetPagedItemsPaging()
    {
        // Arrange
        _serverVariablesService.SetVariable("FromTest", "Hello from test");
        _serverVariablesService.SetVariable("FromBoolean", true);
        _serverVariablesService.SetVariable("FromTest", "Hello from test", "test");

        // Act: Paging
        Attempt<IEnumerable<ServerVariablesCollectionResponseModel>?, CollectionOperationStatus> result =
            _serverVariablesService.GetPagedItems("section", Direction.Ascending, null, 2, 1, CancellationToken.None,
                out var totalNumberOfItems);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Success, Is.True);
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.Result, Is.Not.Empty);
            Assert.That(result.Result!.Count, Is.EqualTo(1));
            Assert.That(totalNumberOfItems, Is.EqualTo(3));
        });
    }

    [Test]
    public void Test_GetPagedItemsSkipTakePagingProblem()
    {
        // Arrange
        _serverVariablesService.SetVariable("FromTest", "Hello from test");
        _serverVariablesService.SetVariable("FromBoolean", true);
        _serverVariablesService.SetVariable("FromTest", "Hello from test", "test");

        // Act: SkipTakeToPagingProblem
        Attempt<IEnumerable<ServerVariablesCollectionResponseModel>?, CollectionOperationStatus> result =
            _serverVariablesService.GetPagedItems("section", Direction.Ascending, null, 1, 2, CancellationToken.None,
                out var totalNumberOfItems);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Success, Is.False);
            Assert.That(result.Status, Is.EqualTo(CollectionOperationStatus.InvalidSkipTake));
        });
    }
}
