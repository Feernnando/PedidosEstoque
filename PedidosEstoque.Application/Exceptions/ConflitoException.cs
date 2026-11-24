namespace PedidosEstoque.Application.Exceptions;
public class ConflitoException : Exception
{
    public ConflitoException(string message) : base(message)
    {
    }
}