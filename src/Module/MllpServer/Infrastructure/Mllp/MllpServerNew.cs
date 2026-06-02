using Hl7Engine.Core.Application.Interfaces.EventBus;
using Hl7Engine.Core.Application.Interfaces.SocketStore;
using Hl7Engine.Module.MllpServer.Infrastructure.Configuration;
using Hl7Engine.Module.MllpServer.IntegrationEvents.Event;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Hl7Engine.Module.MllpServer.Application.Pipelines;
using Hl7Engine.Module.MllpServer.Domain.Entities;


namespace Hl7Engine.Module.MllpServer.Infrastructure.Mllp
{
    public sealed class MllpServerNew : BackgroundService
    {
        private readonly TcpListener _listener;
        private readonly IMllpPipeline _connectionPipeline;
        private readonly SemaphoreSlim _semaphore;
        private readonly ConcurrentDictionary<Task, byte> _activeTasks = new();
        private readonly ILogger<MllpServerNew> _logger;
        private readonly MllpServerConfig _config;

        public MllpServerNew(
            IOptions<MllpServerConfig> config,
            IMllpPipeline connectionPipeline,
            ILogger<MllpServerNew> logger)
        {
            _config = config.Value;
            _listener = new TcpListener(IPAddress.Any, _config.Port);
            _connectionPipeline = connectionPipeline;
            _semaphore = new SemaphoreSlim(5);
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _listener.Start();
            _logger.LogInformation("MLLP server started on port {Port}", _config.Port);

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    TcpClient client = await _listener.AcceptTcpClientAsync(stoppingToken);

                    if (!await _semaphore.WaitAsync(TimeSpan.FromSeconds(5), stoppingToken))
                    {
                        client.Close();
                        continue;
                    }

                    var task = HandleClientAsync(client, stoppingToken);
                    _activeTasks.TryAdd(task, 0);
                    await task.ContinueWith(t =>
                    {
                        _activeTasks.TryRemove(task, out _);   // ← descomentar y mover aquí
                        _semaphore.Release();
                        if (t.IsFaulted)
                            _logger.LogError(t.Exception, "Error en conexión MLLP");
                    }, TaskContinuationOptions.ExecuteSynchronously);
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            { }
            finally
            {
                _listener.Stop();
                var allTasks = Task.WhenAll(_activeTasks.Keys);
                await Task.WhenAny(allTasks, Task.Delay(TimeSpan.FromSeconds(15)));
                _logger.LogInformation("MLLP server stopped");
            }
        }

        private async Task HandleClientAsync(TcpClient client, CancellationToken ct)
        {
            var ctx = new MllpConnectionContext { Client = new TcpClientConnection(client) };
            try
            {
                await _connectionPipeline.ExecuteAsync(ctx, ct);
            }
            finally
            {
                // Limpieza de sesión (opcional, si no se hace en otro paso)
                client.Dispose();
            }
        }
    }

 
  
   

   

   

   

     

    

    

    

  

   
  

}
