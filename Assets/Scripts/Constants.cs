using System;
using UnityEngine;

public static class Constants
{
    public const string Player = "Player";
    public const string WallTag = "Wall";
    public const int SequenceLength = 4;
    public const float CountDown = 3;
    public static readonly char[] KeyboardSequenceOptions = {'s','d','f','g','h','j','w','e','t','u','i','2','3','4','6','8','9'};

    public static string WinnerName = String.Empty;
    public static UnityEngine.Color WinnerColor;
    public static Sprite WinnerSprite;
}
