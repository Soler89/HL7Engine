using Hl7Cloud.Core.Domain.Interfaces;
using Hl7Engine.Module.MllpServer.Application.Pipelines;
using Hl7Engine.Module.MllpServer.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Hl7Engine.Module.MllpServer.Infrastructure.PipelineSteps;

public sealed class OpenSessionStep : IMllpPipelineStep
{
    private readonly ISessionFactory _factory;
    private readonly ISessionManager _manager;
    private readonly ILogger<OpenSessionStep> _logger;
        

    public OpenSessionStep(ISessionFactory factory, ISessionManager manager,ILogger<OpenSessionStep> logger)
    {
        _factory = factory;
        _manager = manager;
        _logger = logger;
    }

    public Task InvokeAsync(MllpConnectionContext ctx, Func<Task> next, CancellationToken ct)
    {
        ctx.Session = _factory.Create(ctx.Client);
        _logger.LogInformation("Session created: {SessionId}", ctx.Session.SessionId);
        _manager.Register(ctx.Session);
        return next();
    }
}