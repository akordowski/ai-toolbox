using AIToolbox.Options;
using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class ServiceBuilderServiceTests
{
    private readonly AIToolboxOptions _options = new()
    {
        SemanticKernel = new SemanticKernelOptions
        {
            Kernel = new KernelOptions(),
            Memory = new MemoryOptions()
        }
    };
    private readonly ServiceCollection _services = [];
    private readonly ServiceBuilderService _builderService;

    public ServiceBuilderServiceTests()
    {
        _builderService = new ServiceBuilderService(_options, _services);
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Act
        var act = () => new ServiceBuilderService(_options, new ServiceCollection());

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Options()
    {
        // Act
        var act = () => new ServiceBuilderService(null!, _services);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("options");
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Services()
    {
        // Act
        var act = () => new ServiceBuilderService(_options, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("services");
    }

    [Fact]
    public void Should_Add_Connectors_With_Options()
    {
        // Arrange
        var connectorOptions = new GlobalConnectorOptions();

        // Act
        var result = _builderService.AddConnectors(connectorOptions);

        // Assert
        result.Should().NotBeNull();
        _options.GlobalConnectors.Should().Be(connectorOptions);
    }

    [Fact]
    public void Should_Add_Connectors_With_Null_Options()
    {
        // Act
        var result = _builderService.AddConnectors();

        // Assert
        result.Should().NotBeNull();
        _options.GlobalConnectors.Should().NotBeNull();
    }

    [Fact]
    public void Should_Add_Connectors_With_Options_Action()
    {
        // Act
        var result = _builderService.AddConnectors(options => options.AzureOpenAI = new GlobalAzureOpenAIOptions());

        // Assert
        result.Should().NotBeNull();
        _options.GlobalConnectors.Should().NotBeNull();
        _options.GlobalConnectors!.AzureOpenAI.Should().NotBeNull();
    }

    [Fact]
    public void Should_Throw_Exception_When_Add_Connectors_With_Null_Options_Action()
    {
        // Act
        var act = () => _builderService.AddConnectors((Action<GlobalConnectorOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("optionsAction");
    }

    [Fact]
    public void Should_Add_Kernel_With_Options()
    {
        // Arrange
        var options = new KernelOptions();

        // Act
        var result = _builderService.AddKernel(options);

        // Assert
        result.Should().NotBeNull();
        _options.SemanticKernel!.Kernel.Should().Be(options);
    }

    [Fact]
    public void Should_Add_Kernel_With_Null_Options()
    {
        // Act
        var result = _builderService.AddKernel();

        // Assert
        result.Should().NotBeNull();
        _options.SemanticKernel!.Kernel.Should().NotBeNull();
    }

    [Fact]
    public void Should_Add_Kernel_With_Options_Action()
    {
        // Act
        var result = _builderService.AddKernel(options => options.AddLogging = true);

        // Assert
        result.Should().NotBeNull();
        _options.SemanticKernel!.Kernel.Should().NotBeNull();
        _options.SemanticKernel!.Kernel!.AddLogging.Should().BeTrue();
    }

    [Fact]
    public void Should_Throw_Exception_When_Add_Kernel_With_Null_Options_Action()
    {
        // Act
        var act = () => _builderService.AddKernel((Action<KernelOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("optionsAction");
    }

    [Fact]
    public void Should_Add_Memory_With_Options()
    {
        // Arrange
        var options = new MemoryOptions();

        // Act
        var result = _builderService.AddMemory(options);

        // Assert
        result.Should().NotBeNull();
        _options.SemanticKernel!.Memory.Should().Be(options);
    }

    [Fact]
    public void Should_Add_Memory_With_Null_Options()
    {
        // Act
        var result = _builderService.AddMemory();

        // Assert
        result.Should().NotBeNull();
        _options.SemanticKernel!.Memory.Should().NotBeNull();
    }

    [Fact]
    public void Should_Add_Memory_With_Options_Action()
    {
        // Act
        var result = _builderService.AddMemory(options => options.Store = new MemoryStoreOptions());

        // Assert
        result.Should().NotBeNull();
        _options.SemanticKernel!.Memory.Should().NotBeNull();
        _options.SemanticKernel!.Memory!.Store.Should().NotBeNull();
    }

    [Fact]
    public void Should_Throw_Exception_When_Add_Memory_With_Null_Options_Action()
    {
        // Act
        var act = () => _builderService.AddMemory((Action<MemoryOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("optionsAction");
    }

    [Fact]
    public void Should_Add_Agents_With_Options()
    {
        // Arrange
        var options = new AgentOptions();

        // Act
        var result = _builderService.AddAgents(options);

        // Assert
        result.Should().NotBeNull();
        _options.SemanticKernel!.Agents.Should().Be(options);
    }

    [Fact]
    public void Should_Add_Agents_With_Null_Options()
    {
        // Act
        var result = _builderService.AddAgents();

        // Assert
        result.Should().NotBeNull();
        _options.SemanticKernel!.Agents.Should().NotBeNull();
    }

    [Fact]
    public void Should_Add_Agents_With_Options_Action()
    {
        // Act
        var result = _builderService.AddAgents(options => options.ChatCompletion = new ChatCompletionOptions());

        // Assert
        result.Should().NotBeNull();
        _options.SemanticKernel!.Agents.Should().NotBeNull();
        _options.SemanticKernel!.Agents!.ChatCompletion.Should().NotBeNull();
    }

    [Fact]
    public void Should_Throw_Exception_When_Add_Agents_With_Null_Options_Action()
    {
        // Act
        var act = () => _builderService.AddAgents((Action<AgentOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("optionsAction");
    }
}
