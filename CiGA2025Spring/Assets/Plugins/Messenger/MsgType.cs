public static class MsgType
{
    public static string SimpleEvent = nameof(SimpleEvent);
    public static string IntEvent = nameof(IntEvent);

    public static string GameRestart = nameof(GameRestart);
    public static string GameOver = nameof(GameOver);
    public static string GameStart = nameof(GameStart);
    public static string Player1Hurt = nameof(Player1Hurt);
    public static string Player1Dash = nameof(Player1Dash);
    public static string Player1IsDying = nameof(Player1IsDying);
    public static string Player2Hurt = nameof(Player2Hurt);
    public static string Player2Dash = nameof(Player2Dash);
    public static string Player2IsDying = nameof(Player2IsDying);

    public static string ChangeBubbleBar = nameof(ChangeBubbleBar);
    public static string ResetPlayer = nameof(ResetPlayer);
    public static string ResetMap = nameof(ResetMap);
    public static string InfBubble = nameof(InfBubble);

    public static string Player1Selected = nameof(Player1Selected);
    public static string Player2Selected = nameof(Player2Selected);

    public static string TipsFlipPage = nameof(TipsFlipPage);
}