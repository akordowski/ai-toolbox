using AIToolbox.Options.Connectors;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

public class GlobalConnectorBuilderTests
{
    public static TheoryData<Action, string> ConstructWithInvalidParameters =>
        new()
        {
            { () => _ = new GlobalConnectorBuilder(null!, null!), "options" },
            { () => _ = new GlobalConnectorBuilder(new GlobalConnectorOptions(), null!), "services" }
        };

    public static TheoryData<Action> AddConnectorWithInvalidParameters
    {
        get
        {
            var builder = new GlobalConnectorBuilder(new GlobalConnectorOptions(), new ServiceCollection());

            return
            [
                () => builder.AddAzureOpenAIOptions((Action<GlobalAzureOpenAIOptions>)null!),
                () => builder.AddGoogleOptions((Action<GlobalGoogleOptions>)null!),
                () => builder.AddHuggingFaceOptions((Action<GlobalHuggingFaceOptions>)null!),
                () => builder.AddMistralAIOptions((Action<GlobalMistralAIOptions>)null !),
                () => builder.AddOllamaOptions((Action<GlobalOllamaOptions>)null !),
                () => builder.AddOpenAIOptions((Action<GlobalOpenAIOptions>)null !),
                () => builder.AddVertexAIOptions((Action<GlobalVertexAIOptions>)null !)
            ];
        }
    }

    public static TheoryData<Action, string> AddConnectorWithNoDefaultOptions
    {
        get
        {
            var builder = new GlobalConnectorBuilder(new GlobalConnectorOptions(), new ServiceCollection());

            return new TheoryData<Action, string>
            {
                { () => builder.AddAzureOpenAIOptions(), "No 'GlobalAzureOpenAIOptions' provided.*" },
                { () => builder.AddGoogleOptions(), "No 'GlobalGoogleOptions' provided.*" },
                { () => builder.AddHuggingFaceOptions(), "No 'GlobalHuggingFaceOptions' provided.*" },
                { () => builder.AddMistralAIOptions(), "No 'GlobalMistralAIOptions' provided.*" },
                { () => builder.AddOllamaOptions(), "No 'GlobalOllamaOptions' provided.*" },
                { () => builder.AddOpenAIOptions(), "No 'GlobalOpenAIOptions' provided.*" },
                { () => builder.AddVertexAIOptions(), "No 'GlobalVertexAIOptions' provided.*" }
            };
        }
    }

    public static TheoryData<Action, IServiceCollection, Type, object> AddConnectorWithOptions
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

            var azureOpenAIOptions = new GlobalAzureOpenAIOptions();
            var googleOptions = new GlobalGoogleOptions();
            var huggingFaceOptions = new GlobalHuggingFaceOptions();
            var mistralAIOptions = new GlobalMistralAIOptions();
            var ollamaOptions = new GlobalOllamaOptions();
            var openAIOptions = new GlobalOpenAIOptions();
            var vertexAIOptions = new GlobalVertexAIOptions();

            var services = new ServiceCollection();
            var builder1 = new GlobalConnectorBuilder(globalConnectorOptions, services);
            var builder2 = new GlobalConnectorBuilder(new GlobalConnectorOptions(), services);

            return new TheoryData<Action, IServiceCollection, Type, object>
            {
                // Default options
                { () => builder1.AddAzureOpenAIOptions(), services, typeof(GlobalAzureOpenAIOptions), globalConnectorOptions.AzureOpenAI },
                { () => builder1.AddGoogleOptions(), services, typeof(GlobalGoogleOptions), globalConnectorOptions.Google },
                { () => builder1.AddHuggingFaceOptions(), services, typeof(GlobalHuggingFaceOptions), globalConnectorOptions.HuggingFace },
                { () => builder1.AddMistralAIOptions(), services, typeof(GlobalMistralAIOptions), globalConnectorOptions.MistralAI },
                { () => builder1.AddOllamaOptions(), services, typeof(GlobalOllamaOptions), globalConnectorOptions.Ollama },
                { () => builder1.AddOpenAIOptions(), services, typeof(GlobalOpenAIOptions), globalConnectorOptions.OpenAI },
                { () => builder1.AddVertexAIOptions(), services, typeof(GlobalVertexAIOptions), globalConnectorOptions.VertexAI },

                // Custom options
                { () => builder2.AddAzureOpenAIOptions(azureOpenAIOptions), services, typeof(GlobalAzureOpenAIOptions), azureOpenAIOptions },
                { () => builder2.AddGoogleOptions(googleOptions), services, typeof(GlobalGoogleOptions), googleOptions },
                { () => builder2.AddHuggingFaceOptions(huggingFaceOptions), services, typeof(GlobalHuggingFaceOptions), huggingFaceOptions },
                { () => builder2.AddMistralAIOptions(mistralAIOptions), services, typeof(GlobalMistralAIOptions), mistralAIOptions },
                { () => builder2.AddOllamaOptions(ollamaOptions), services, typeof(GlobalOllamaOptions), ollamaOptions },
                { () => builder2.AddOpenAIOptions(openAIOptions), services, typeof(GlobalOpenAIOptions), openAIOptions },
                { () => builder2.AddVertexAIOptions(vertexAIOptions), services, typeof(GlobalVertexAIOptions), vertexAIOptions }
            };
        }
    }

    public static TheoryData<Action, IServiceCollection, Type> AddConnectorWithOptionsAction
    {
        get
        {
            var options = new GlobalConnectorOptions();
            var services = new ServiceCollection();
            var builder = new GlobalConnectorBuilder(options, services);

            return new TheoryData<Action, IServiceCollection, Type>
            {
                { () => builder.AddAzureOpenAIOptions(_ => { }), services, typeof(GlobalAzureOpenAIOptions) },
                { () => builder.AddGoogleOptions(_ => { }), services, typeof(GlobalGoogleOptions) },
                { () => builder.AddHuggingFaceOptions(_ => { }), services, typeof(GlobalHuggingFaceOptions) },
                { () => builder.AddMistralAIOptions(_ => { }), services, typeof(GlobalMistralAIOptions) },
                { () => builder.AddOllamaOptions(_ => { }), services, typeof(GlobalOllamaOptions) },
                { () => builder.AddOpenAIOptions(_ => { }), services, typeof(GlobalOpenAIOptions) },
                { () => builder.AddVertexAIOptions(_ => { }), services, typeof(GlobalVertexAIOptions) }
            };
        }
    }

    [Theory]
    [MemberData(nameof(ConstructWithInvalidParameters))]
    public void Should_Throw_On_Construct_With_Invalid_Parameters(Action act, string parameterName)
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
    [MemberData(nameof(AddConnectorWithInvalidParameters))]
    public void Should_Throw_On_Add_Connector_With_Invalid_Parameters(Action act)
    {
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("optionsAction");
    }

    [Theory]
    [MemberData(nameof(AddConnectorWithNoDefaultOptions))]
    public void Should_Throw_On_Add_Connector_With_No_Default_Options(Action act, string message)
    {
        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Theory]
    [MemberData(nameof(AddConnectorWithOptions))]
    public void Should_Add_Connector_With_Options(
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
    [MemberData(nameof(AddConnectorWithOptionsAction))]
    public void Should_Add_Connector_With_Options_Action(
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
