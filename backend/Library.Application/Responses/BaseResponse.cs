namespace Library.Application.Responses;

public class BaseResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public BaseResponse() { }

    public BaseResponse(T data, string message = "Operação realizada com sucesso.")
    {
        Success = true;
        Message = message;
        Data = data;
    }

    public BaseResponse(string message)
    {
        Success = false;
        Message = message;
    }
}
