using System.Collections.Generic;
using System.Linq;

namespace BlogEngine.Common
{
    public class GenericResult<T> : GenericResult
    {
        public GenericResult()
        {
            Messages = new List<GenericMessage>();
        }
        public GenericResult(MessageType tipo, string mensaje, string codigo = null) : this()
        {
            Messages.Add(new GenericMessage(tipo, mensaje, codigo));
        }

        public T Data { get; set; }
    }

    public class GenericResult
    {
        public GenericResult()
        {
            Messages = new List<GenericMessage>();
        }
        public GenericResult(MessageType tipo, string mensaje, string codigo = null) : this()
        {
            Messages.Add(new GenericMessage(tipo, mensaje, codigo));
        }

        public bool Success => Messages.All(m => m.Type != MessageType.Error);

        public bool HasErrors => Messages.Any(m => m.Type == MessageType.Error);
        public bool HasWarnings => Messages.Any(m => m.Type == MessageType.Warning);


        public List<GenericMessage> Messages { get; set; }
         public override string ToString()
        {
            var _mensaje = Messages.Select(x => x.Message);
            return string.Join(",", _mensaje);
        }

        public void AddError(string mensajeError, string codigo = null)
        {
            Messages.Add(new GenericMessage(MessageType.Error, mensajeError, codigo));
        }
        public void AddWarning(string mensajeError, string codigo = null)
        {
            Messages.Add(new GenericMessage(MessageType.Warning, mensajeError, codigo));
        }
        public void AddInfo(string mensajeError, string codigo = null)
        {
            Messages.Add(new GenericMessage(MessageType.Info, mensajeError, codigo));
        }
    }


    public class GenericMessage
    {
        internal GenericMessage(MessageType type, string message, string code = null)
        {
            Type = type;
            Message = message;
            Code = code;
        }
        public MessageType Type { get; private set; }
        public string Message { get; private set; }
        public string Code { get; private set; }


    }

    public enum MessageType
    {
        Info = 1,
        Warning = 2,
        Error = 3,
    }
}
