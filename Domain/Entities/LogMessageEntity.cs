using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class LogMessageEntity : ICopyable<LogMessageEntity>
    {   
        private string? _source;
        private string? _title;
        private string? _messageBody;

        public enum MessageType
        {
            Error,
            Warning,
            Information
        }

        public int Id { get; set; }
        public string? Source { get => _source; set => SetSource(value); }
        public MessageType Type { get; set; }
        public string? Title { get => _title; set => SetTitle(value); }
        public string? Body { get => _messageBody; set => SetMessageBody(value); }
        
        

        public DateTime TimeStamp { get; set; }

        public LogMessageEntity Copy()
        {
            LogMessageEntity copy = new LogMessageEntity();
            copy.Id = Id;
            copy.Type = Type;
            copy.Source = Source;
            copy.Title = Title;
            copy.Body = Body;
            copy.TimeStamp = TimeStamp;
            return copy;
        }

        public LogMessageEntity CopyPropertiesFrom(LogMessageEntity toCopy)
        {
            Id = toCopy.Id;
            Type = toCopy.Type;
            Source = toCopy.Source;
            Title = toCopy.Title;
            Body = toCopy.Body;
            TimeStamp = toCopy.TimeStamp;
            return this;
        }

        private void SetMessageBody(string? message)
        {
            if (message == null || message.IsWhiteSpace())
            {
                _messageBody = null;
            } 
            else
            {
                _messageBody = message;
            }  
        }
        private void SetSource(string? source)
        {
            if (source == null || source.IsWhiteSpace())
            {
                _source = null;
            }
            else
            {
                _source = source;
            }
        }
        private void SetTitle(string? title)
        {
            if (title == null || title.IsWhiteSpace())
            {
                _title = null;
            }
            else
            {
                _title = title;
            }
        }

        public override string ToString()
        {
            return $"{Type.ToString()}: || {Source} || {Title} || {Body}";
        }
    }
}
