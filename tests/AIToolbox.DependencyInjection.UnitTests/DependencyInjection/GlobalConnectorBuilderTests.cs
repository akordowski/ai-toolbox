using AIToolbox.Options.Connectors;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

public class GlobalConnectorBuilderTests
{
    public static TheoryData<Action, string> ConstructWithNullParameters =>
        new()
        {
            { () => _ = new GlobalConnectorBuilder(null!, null!), "options" },
            { () => _ = new GlobalConnectorBuilder(new GlobalConnectorOptions(), null!), "services" }
        };

    public static TheoryData<Action> AddGlobalOptionsWithNullParameters
    {
        get
        {
            var builder = new GlobalConnectorBuilder(new GlobalConnectorOptions(), new ServiceCollection());

            return
            [
                () => builder.AddGlobalAzureOpenAIOptions(null!),
                () => builder.AddGlobalGoogleOptions(null!),
                () => builder.AddGlobalHuggingFaceOptions(null!),
                () => builder.AddGlobalMistralAIOptions(null!),
                () => builder.AddGlobalOllamaOptions(null!),
                () => builder.AddGlobalOpenAIOptions(null!),
                () => builder.AddGlobalVertexAIOptions(null!)
            ];
        }
    }

    public static TheoryData<Action, string> AddGlobalOptionsWithNoDefaultOptions
    {
        get
        {
            var builder = new GlobalConnectorBuilder(new GlobalConnectorOptions(), new ServiceCollection());

            return new TheoryData<Action, string>
            {
                { () => builder.AddGlobalAzureOpenAIOptions(), "No 'GlobalAzureOpenAIOptions' provided.*" },
                { () => builder.AddGlobalGoogleOptions(), "No 'GlobalGoogleOptions' provided.*" },
                { () => builder.AddGlobalHuggingFaceOptions(), "No 'GlobalHuggingFaceOptions' provided.*" },
                { () => builder.AddGlobalMistralAIOptions(), "No 'GlobalMistralAIOptions' provided.*" },
                { () => builder.AddGlobalOllamaOptions(), "No 'GlobalOllamaOptions' provided.*" },
                { () => builder.AddGlobalOpenAIOptions(), "No 'GlobalOpenAIOptions' provided.*" },
                { () => builder.AddGlobalVertexAIOptions(), "No 'GlobalVertexAIOptions' provided.*" }
            };
        }
    }

    public static TheoryData<Action, IServiceCollection, Type, object> AddGlobalOptionsWithDefaultOptions
    {
        get
        {
            var globalConnectorOptions = new GlobalConnectorOptions
            {
                AzureOpenAI = new GlobalAzureOpenAIOptions(),
                Google = new GlobalGoogleOptions(),
                HuggingFace = new GlobalHuggingFaceOptions(),
                MistralAI = new GlobalMistralAIOptions(),
                Ollama = new GlobalOllamaOptions(),
                OpenAI = new GlobalOpenAIOptions(),
                VertexAI = new GlobalVertexAIOptions()
            };

            var services = new ServiceCollection();
            var builder = new GlobalConnectorBuilder(globalConnectorOptions, services);

            return new TheoryData<Action, IServiceCollection, Type, object>
            {
                { () => builder.AddGlobalAzureOpenAIOptions(), services, typeof(GlobalAzureOpenAIOptions), globalConnectorOptions.AzureOpenAI },
                { () => builder.AddGlobalGoogleOptions(), services, typeof(GlobalGoogleOptions), globalConnectorOptions.Google },
                { () => builder.AddGlobalHuggingFaceOptions(), services, typeof(GlobalHuggingFaceOptions), globalConnectorOptions.HuggingFace },
                { () => builder.AddGlobalMistralAIOptions(), services, typeof(GlobalMistralAIOptions), globalConnectorOptions.MistralAI },
                { () => builder.AddGlobalOllamaOptions(), services, typeof(GlobalOllamaOptions), globalConnectorOptions.Ollama },
                { () => builder.AddGlobalOpenAIOptions(), services, typeof(GlobalOpenAIOptions), globalConnectorOptions.OpenAI },
                { () => builder.AddGlobalVertexAIOptions(), services, typeof(GlobalVertexAIOptions), globalConnectorOptions.VertexAI }
            };
        }
    }

    public static TheoryData<Action, IServiceCollection, Type> AddGlobalOptionsWithOptionsAction
    {
        get
        {
            var services = new ServiceCollection();
            var builder = new GlobalConnectorBuilder(new GlobalConnectorOptions(), services);

            return new TheoryData<Action, IServiceCollection, Type>
            {
                { () => builder.AddGlobalAzureOpenAIOptions(_ => { }), services, typeof(GlobalAzureOpenAIOptions) },
                { () => builder.AddGlobalGoogleOptions(_ => { }), services, typeof(GlobalGoogleOptions) },
                { () => builder.AddGlobalHuggingFaceOptions(_ => { }), services, typeof(GlobalHuggingFaceOptions) },
                { () => builder.AddGlobalMistralAIOptions(_ => { }), services, typeof(GlobalMistralAIOptions) },
                { () => builder.AddGlobalOllamaOptions(_ => { }), services, typeof(GlobalOllamaOptions) },
                { () => builder.AddGlobalOpenAIOptions(_ => { }), services, typeof(GlobalOpenAIOptions) },
                { () => builder.AddGlobalVertexAIOptions(_ => { }), services, typeof(GlobalVertexAIOptions) }
            };
        }
    }

    [Theory]
    [MemberData(nameof(ConstructWithNullParameters))]
    public void Should_Throw_On_Construct_With_Null_Parameters(Action act, string parameterName)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(parameterName);
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Act
        _ = new GlobalConnectorBuilder(new GlobalConnectorOptions(), new ServiceCollection());
    }

    [Theory]
    [MemberData(nameof(AddGlobalOptionsWithNullParameters))]
    public void Should_Throw_On_Add_Global_Options_With_Null_Parameters(Action act)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("optionsAction");
    }

    [Theory]
    [MemberData(nameof(AddGlobalOptionsWithNoDefaultOptions))]
    public void Should_Throw_On_Add_Global_Options_With_No_Default_Options(Action act, string message)
    {
        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Theory]
    [MemberData(nameof(AddGlobalOptionsWithDefaultOptions))]
    public void Should_Add_Global_Options_With_Default_Options(
        Action act,
        IServiceCollection services,
        Type optionsType,
        object options)
    {
        // Act
        act();

        // Assert
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == optionsType &&
                                                      descriptor.ImplementationInstance == options);
    }

    [Theory]
    [MemberData(nameof(AddGlobalOptionsWithOptionsAction))]
    public void Should_Add_Global_Options_With_Options_Action(
        Action act,
        IServiceCollection services,
        Type optionsType)
    {
        // Act
        act();

        // Assert
        services.Should().ContainSingle(descriptor => descriptor.Lifetime == ServiceLifetime.Singleton &&
                                                      descriptor.ServiceType == optionsType &&
                                                      descriptor.ImplementationInstance != null);
    }
}
