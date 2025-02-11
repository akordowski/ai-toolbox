using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class SemanticKernelBuilder : ISemanticKernelBuilder
{
    private readonly SemanticKernelOptions _options;
    private readonly IServiceCollection _services;

    /// <inheritdoc />
    public SemanticKernelBuilder(
        SemanticKernelOptions options,
        IServiceCollection services)
    {
        Verify.ThrowIfNull(options, nameof(options), $"No '{nameof(SemanticKernelOptions)}' provided.");
        Verify.ThrowIfNull(services, nameof(services));

        _options = options;
        _services = services;
    }

    /// <inheritdoc />
    public IAddKernel AddKernel(KernelOptions? options = null)
    {
        if (options is not null)
        {
            _options.Kernel = options;
        }

        _ = CreateKernelBuilder();

        return this;
    }

    /// <inheritdoc />
    public IAddKernel AddKernel(Action<KernelOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.Kernel ??= new KernelOptions();
        _ = CreateKernelBuilder();

        optionsAction(_options.Kernel);

        return this;
    }

    /// <inheritdoc />
    public IAddKernel AddKernel(Action<IKernelBuilder> builderAction, KernelOptions? options = null)
    {
        Verify.ThrowIfNull(builderAction, nameof(builderAction));

        if (options is not null)
        {
            _options.Kernel = options;
        }

        builderAction(CreateKernelBuilder());

        return this;
    }

    /// <inheritdoc />
    public IAddKernel AddKernel(Action<IKernelBuilder, KernelOptions> action)
    {
        Verify.ThrowIfNull(action, nameof(action));

        _options.Kernel ??= new KernelOptions();

        action(CreateKernelBuilder(), _options.Kernel);

        return this;
    }

    /// <inheritdoc />
    public IAddMemory AddMemory(MemoryOptions? options = null)
    {
        if (options is not null)
        {
            _options.Memory = options;
        }

        _ = CreateMemoryBuilder();

        return this;
    }

    /// <inheritdoc />
    public IAddMemory AddMemory(Action<MemoryOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.Memory ??= new MemoryOptions();
        _ = CreateMemoryBuilder();

        optionsAction(_options.Memory);

        return this;
    }

    /// <inheritdoc />
    public IAddMemory AddMemory(Action<IMemoryBuilder> builderAction, MemoryOptions? options = null)
    {
        Verify.ThrowIfNull(builderAction, nameof(builderAction));

        if (options is not null)
        {
            _options.Memory = options;
        }

        builderAction(CreateMemoryBuilder());

        return this;
    }

    /// <inheritdoc />
    public IAddMemory AddMemory(Action<IMemoryBuilder, MemoryOptions> action)
    {
        Verify.ThrowIfNull(action, nameof(action));

        _options.Memory ??= new MemoryOptions();

        action(CreateMemoryBuilder(), _options.Memory);

        return this;
    }

    /// <inheritdoc />
    public IAddChatCompletion AddChatCompletion(ChatCompletionOptions? options = null)
    {
        if (options is not null)
        {
            _options.Agents ??= new AgentOptions();
            _options.Agents.ChatCompletion = options;
        }

        _ = CreateChatCompletionBuilder();

        return this;
    }

    /// <inheritdoc />
    public IAddChatCompletion AddChatCompletion(Action<ChatCompletionOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.Agents ??= new AgentOptions();
        _options.Agents.ChatCompletion ??= new ChatCompletionOptions();
        _ = CreateChatCompletionBuilder();

        optionsAction(_options.Agents.ChatCompletion);

        return this;
    }

    /// <inheritdoc />
    public IAddChatCompletion AddChatCompletion(Action<IChatCompletionBuilder> builderAction, ChatCompletionOptions? options = null)
    {
        Verify.ThrowIfNull(builderAction, nameof(builderAction));

        if (options is not null)
        {
            _options.Agents ??= new AgentOptions();
            _options.Agents.ChatCompletion = options;
        }

        builderAction(CreateChatCompletionBuilder());

        return this;
    }

    /// <inheritdoc />
    public IAddChatCompletion AddChatCompletion(Action<IChatCompletionBuilder, ChatCompletionOptions> action)
    {
        Verify.ThrowIfNull(action, nameof(action));

        _options.Agents ??= new AgentOptions();
        _options.Agents.ChatCompletion ??= new ChatCompletionOptions();

        action(CreateChatCompletionBuilder(), _options.Agents.ChatCompletion);

        return this;
    }

    private KernelBuilder CreateKernelBuilder() =>
        new(_options.Kernel!, _services);

    private MemoryBuilder CreateMemoryBuilder() =>
        new(_options.Memory!, _services);

    private ChatCompletionBuilder CreateChatCompletionBuilder() =>
        new(_options.Agents?.ChatCompletion!, _services);
}
