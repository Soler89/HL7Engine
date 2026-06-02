using Hl7Cloud.Core.Domain.Interfaces;
using Hl7Engine.Core.Application.Interfaces.EventBus;
using Hl7Engine.Core.Application.Interfaces.SocketStore;
using Hl7Engine.Core.Infrastructure.EventBus;
using Hl7Engine.Core.Infrastructure.SocketStore;
using Hl7Engine.Module.MllpServer.Application.Interfaces;
using Hl7Engine.Module.MllpServer.Application.Pipelines;
using Hl7Engine.Module.MllpServer.Infrastructure.Configuration;
using Hl7Engine.Module.MllpServer.Infrastructure.Factory;
using Hl7Engine.Module.MllpServer.Infrastructure.Mllp;
using Hl7Engine.Module.MllpServer.Infrastructure.PipelineSteps;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Hl7Engine.Module.MllpServer.Infrastructure.Extensions.MllpServerServiceCollectionExtensions.MllpPipelineBuilder;
 
 
 
 

namespace Hl7Engine.Module.MllpServer.Infrastructure.Extensions;

public static class MllpServerServiceCollectionExtensions
{
    public static IServiceCollection AddMllpServer(
        this IServiceCollection services,
        IConfiguration configuration)
    {

       
        services.AddOptions<MllpServerConfig>()
            .Bind(configuration.GetSection(MllpServerConfig.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        // 2. Registrar dependencias externas (debes implementarlas tú)
       
        services.AddSingleton<ISessionManager, InMemorySessionManager>(); // gestión de sesiones
        services.AddTransient<ISessionFactory, DefaultSessionFactory>();  // creación de sesiones

        // 3. Registrar los pasos del pipeline de mensaje
        //services.AddTransient<ErrorBoundaryStep>();
        services.AddTransient<ExtractMllpFrameStep>();
        services.AddTransient<OpenSessionStep>();
        services.AddTransient<PublishIntegrationEventStep>();

        // 4. Pipeline de mensaje (procesa una trama)
        
        // 5. Pipeline de conexión (por cada cliente)
        services.AddTransient(sp =>
        {
            var builder = new MllpPipelineBuilder();
            builder.Use(sp.GetRequiredService<OpenSessionStep>());
            builder.Use(sp.GetRequiredService<ExtractMllpFrameStep>());
            builder.Use(sp.GetRequiredService<PublishIntegrationEventStep>());
            return (IMllpPipeline)builder.Build();
        });

        // 6. Servidor en segundo plano


        services.AddHostedService<Mllp.MllpServerNew>();

        return services;
    }
    public sealed class MllpPipelineBuilder
    {
        private readonly List<IMllpPipelineStep> _steps = new();

        /// <summary>Añade un paso al pipeline. El orden de uso determina el orden de ejecución.</summary>
        public MllpPipelineBuilder Use(IMllpPipelineStep step)
        {
            _steps.Add(step);
            return this;
        }

        /// <summary>Construye y devuelve el pipeline con los pasos registrados.</summary>
        public IMllpPipeline Build()
        {
            return new MllpPipeline(_steps.ToArray());
        }
        public interface IMessagePipeline : IMllpPipeline { }
    }
}