namespace RGM.Modes
{
    [Mode(ModeCategory.Public, ModeInfo.Set, ModeType.Original)]
    public class Original : Mode
    {
        public override string Name => "오리지널";
        public override string Description => "가끔은 도파민이 없는 기본을 해보시는 건 어떠신지..?";
        public override string Detail =>
"""
모드가 존재하지 않습니다.
""";
        public override string Color => "FFFFFF";

        public static Original Instance;

        public override void OnEnabled()
        {
            // 히히 날먹
        }

        public override void OnDisabled()
        {
        }
    }
}
