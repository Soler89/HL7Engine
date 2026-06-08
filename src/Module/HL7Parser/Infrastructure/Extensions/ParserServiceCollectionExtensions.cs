using System.Net.Http.Headers;
using Hl7Engine.Module.Parser.Infrastructure;
using Hl7Engine.Core.Application.Interfaces.EventBus;
using Hl7Engine.Core.Application.Interfaces.Module;
using Hl7Engine.Core.Application.Message.Dto;
using Hl7Engine.Core.Infrastructure.EventBus;
using Hl7Engine.Module.MllpServer.IntegrationEvents.Event;
using HL7Engine.Module.Parser.Application.Handlers;
using HL7Engine.Module.Parser.Application.Interfaces;
using Hl7Engine.Module.Parser.Infrastructure.Configuration;
using Hl7Engine.Module.Parser.Infrastructure.Extractors;
using Hl7Engine.Module.Parser.Infrastructure.Interfaces;
using Hl7Engine.Module.Parser.Infrastructure.Parsers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NHapi.Base.Model;
using NHapi.Base.Parser;

namespace Hl7Engine.Module.Parser.Infrastructure.Extensions;

public static class ParserServiceCollectionExtensions
{
    public static IServiceCollection AddParser(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<ParserConfig>()
            .Bind(configuration.GetSection(ParserConfig.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddSingleton<PipeParser>();

        services.AddSingleton<IParserServices, ParserServices>();
       
        services.AddSingleton<ISegmentExtractor, MshExtractor>();
        services.AddSingleton<ISegmentExtractor, PidExtractor>();

       
        services.AddSingleton<IMessageParser<MessageDto>, Hl7MessageParser>();
        services.AddSingleton<IIntegrationEventHandler<MllpMessageIncomingIntegrationEvent>, MllpMessageIncomingIntegrationEventHandler>();
        services.AddHostedService<EventBus.EventBusRegister>();
        return services;

    }
}

