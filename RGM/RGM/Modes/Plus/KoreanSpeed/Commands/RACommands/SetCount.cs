using System;
using System.Text.RegularExpressions;
using CommandSystem;
using Exiled.API.Features;

namespace RGM.Modes;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class SetCount : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        try
        {
            if (!Round.IsStarted)
            {
                response = "라운드 시작 전 사용할 수 없습니다.";
                return false;
            }

            if (!SpeedStore.isEnabled)
            {
                response = "해당 모드가 활성화되지 않았습니다. 활성화된 상태에서 다시 시도하세요.";
                return false;
            }

            if (arguments.Count <= 0)
            {
                response = SpeedStore.Count <= 0 ? "그 누구도 죽지 않았습니다." :
                    SpeedStore.Count >= 125 ? "이 시체더미의 제단 속에서 당신의 속도는 한계까지 빨라집니다." :
                    $"{SpeedStore.Count}번의 죽음만큼 속도가 빨라집니다.";
                return true;
            }

            if (byte.TryParse(arguments.At(1), out var data) &&
                Regex.IsMatch(arguments.At(0), "[A-Za-z]"))
                switch (arguments.At(0))
                {
                    case "add":
                        // 다른 용도(제거)로 인해 가능성 버그를 방지
                        if (data <= 0)
                        {
                            response = "최소한, 0 이상의 값을 입력하세요.";
                            return false;
                        }

                        SpeedStore.Count += data + SpeedStore.Count > 125 ? (byte)125 : data;
                        break;
                    case "set":
                        // Stack overflow 방지
                        if (data <= 0)
                        {
                            response = "최소한, 0 이상의 값을 입력하세요.";
                            return false;
                        }

                        SpeedStore.Count = data + SpeedStore.Count > 125 ? (byte)125 : data;
                        break;
                    case "remove":
                    case "rm":
                        if (data <= 0)
                        {
                            response = "최소한, 0 이상의 값을 입력하세요.";
                            return false;
                        }

                        SpeedStore.Count = SpeedStore.Count - data < 0 ? (byte)0 : data;
                        break;
                    case "clear":
                        SpeedStore.Clear();
                        break;
                    default:
                        response = "알 수 없는 조건입니다.";
                        return false;
                }
            else
                response = """
                           수가 알맞지 않거나, 너무 큽니다.
                           0 ~ 255 사이의 수이면서 문자가 들어가지 않은 수를 입력해주세요.
                           """;

            KoreanSpeed.AddEffects();

            response = "해당 명령이 성공적으로 처리되었습니다.";
            return true;
        }
        catch (Exception ex)
        {
            response = "해당 명령을 처리하는 도중 오류가 발생했습니다. 해당 오류는 로그에 기록됩니다.";
            Log.Error(ex);
            return false;
        }
    }

    public string Command => "setcount";

    public string[] Aliases { get; } =
    [
        "count",
        "setc"
    ];

    public string Description =>
        """
        모드 "한국인이 좋아하는 속도"의 카운트를 설정합니다.
        추가: /setcount add <값>
        설정: /setcount set <값>
        (0 ~ 255 사이로 설정해주세요.)
        """;
}