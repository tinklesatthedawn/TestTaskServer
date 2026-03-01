using System;
using System.Collections.Generic;
using System.Text;
using DependencyInjection;
using Application.UseCases.LogMessage.Create;
using Domain.Entities;
using System.Text.Json;

namespace Logger
{
    public class ServerLogger : ILogger
    {
        public async Task<LogMessageEntity?> WriteErrorMessage(string source, string title, string? message, CancellationToken cancellationToken)
        {
            return await WriteMessage(source, title, message, LogMessageEntity.MessageType.Error, cancellationToken);
        }

        public async Task<LogMessageEntity?> WriteWarningMessage(string source, string title, string? message, CancellationToken cancellationToken)
        {
            return await WriteMessage(source, title, message, LogMessageEntity.MessageType.Warning, cancellationToken);
        }

        public async Task<LogMessageEntity?> WriteInformationMessage(string source, string title, string? message, CancellationToken cancellationToken)
        {
            return await WriteMessage(source, title, message, LogMessageEntity.MessageType.Information, cancellationToken);
        }

        private async Task<LogMessageEntity?> WriteMessage(string source, string title, string? message, LogMessageEntity.MessageType type, CancellationToken cancellationToken)
        {
            LogMessageEntity logMessage = new LogMessageEntity();
            logMessage.Source = source;
            logMessage.Title = title;
            logMessage.Type = type;
            logMessage.Body = message;

            Console.WriteLine(Environment.NewLine + logMessage.ToString());

            var usecase = DI.Get<CreateLogMessage>();
            var result = await usecase.Handle(logMessage, cancellationToken);
            return result.LogMessage;
        }
    }
}
