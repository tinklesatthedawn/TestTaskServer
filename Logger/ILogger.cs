using Domain.Entities;

namespace Logger
{
    public interface ILogger
    {
        Task<LogMessageEntity?> WriteErrorMessage(string source, string title, string? message, CancellationToken cancellationToken);

        Task<LogMessageEntity?> WriteWarningMessage(string source, string title, string? message, CancellationToken cancellationToken);

        Task<LogMessageEntity?> WriteInformationMessage(string source, string title, string? message, CancellationToken cancellationToken);
    }
}
