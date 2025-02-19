using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using AIToolbox.Tests;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit.DependencyInjection;

namespace AIToolbox.DependencyInjection.SemanticKernel;

public class SemanticKernelBuilderTests : BaseTestWithFixture<AIToolboxFixture>
{
    private readonly KernelOptions _kernelOptions = new()
    {
        Plugins = new PluginOptions()
    };

    public SemanticKernelBuilderTests(AIToolboxFixture fixture)
        : base(fixture)
    {
    }

    [Fact]
    public void Should_Get_Services_Configured_By_Default_Options()
    {
        var host = Fixture.GetHost((_, services) =>
        {
            services.AddAIToolbox(
                aiToolbox =>
                {
                    aiToolbox.AddSemanticKernel(semanticKernel =>
                    {
                        semanticKernel.AddKernel();
                    });
                },
                options =>
                {
                    options.SemanticKernel = new SemanticKernelOptions
                    {
                        Kernel = _kernelOptions
                    };
                });
        });

        // Assert
        AssertServices(host.Services);
    }

    [Fact]
    public void Should_Get_Services_Configured_By_Options_Action()
    {
        var host = Fixture.GetHost((_, services) =>
        {
            services.AddAIToolbox(aiToolbox =>
            {
                aiToolbox.AddSemanticKernel(
                    semanticKernel =>
                    {
                        semanticKernel.AddKernel();
                    },
                    options =>
                    {
                        options.Kernel = _kernelOptions;
                    });
            });
        });

        // Assert
        AssertServices(host.Services);
    }

    [Fact]
    public void Should_Get_Services_Configured_By_Config_File()
    {
        var host = Fixture.GetHost(
            (context, services) =>
            {
                services.AddAIToolbox(
                    aiToolbox =>
                    {
                        aiToolbox.AddSemanticKernel(semanticKernel =>
                        {
                            semanticKernel.AddKernel();
                        });
                    },
                    context.Configuration);
            },
            "ConfigSemanticKernelBuilderTests.json");

        // Assert
        AssertServices(host.Services, checkOptionsInstance: false);
    }

    private void AssertServices(IServiceProvider services, bool checkOptionsInstance = true)
    {
        if (checkOptionsInstance)
        {
            services.GetService<KernelOptions>().Should().Be(_kernelOptions);
        }
        else
        {
            services.GetService<KernelOptions>().Should().NotBeNull();
        }

        services.GetService<IKernelProvider>().Should().NotBeNull();
    }
}
