using Hl7Cloud.Module.Hl7Validate.Application;
using Hl7Cloud.Module.Hl7Validate.Application.Handlers;
using Hl7Engine.Core.Application.Interfaces.EventBus;
using Hl7Engine.Core.Application.Interfaces.Module;
using HL7Engine.Module.Parser.IntegrationEvents.Event;
using Hl7Engine.Module.Validate.Infrastructure.EventBus;
using Hl7Engine.Module.Validate.Infrastructure.Rules.HL7v2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hl7Engine.Module.Validate.Infrastructure.Extensions;

public static class ValidateServiceCollectionExtensions
{
    public static IServiceCollection AddValidate(
        this IServiceCollection services,
        IConfiguration configuration)
    {
         
        services.AddSingleton<IValidateMessage , Hl7ValidationModule>();
        services.AddSingleton<IIntegrationEventHandler<ParserSuccessIntegrationEvent>, ParserSuccessIntegrationEventHandler>();
         
        services.AddHostedService<EventBusRegister>();
        return services;

    }
}

