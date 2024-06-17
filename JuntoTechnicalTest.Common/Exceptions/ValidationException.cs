namespace JuntoTechnicalTest.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Erros { get; private set; }
        public ValidationException(IDictionary<string, string[]> erros)
        => Erros = erros;

        public ValidationException(string message, IDictionary<string, string[]> erros) : base(message)
        => Erros = erros;

        public ValidationException(string message, IDictionary<string, string[]> erros, Exception innerException) : base(message, innerException)
        => Erros = erros;
    }
}
