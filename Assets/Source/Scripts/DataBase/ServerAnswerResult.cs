public struct ServerAnswerResult
{
    public ServerAnswerResultType Type;
    public string Text;

    public ServerAnswerResult(ServerAnswerResultType type, string text)
    {
        Type = type;
        Text = text;
    }
}
