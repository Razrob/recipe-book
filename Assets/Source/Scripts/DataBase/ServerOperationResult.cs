public struct ServerOperationResult
{
    public ServerOperationType Type;
    public ServerAnswerResult Result;

    public ServerOperationResult(ServerOperationType type, ServerAnswerResult result)
    {
        Type = type;
        Result = result;
    }
}
