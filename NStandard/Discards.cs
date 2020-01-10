namespace NStandard
{
    public struct Discards
    {
        public static Discards _ = new Discards();

        public override bool Equals(object obj) => obj is Discards;
        public override int GetHashCode() => 0;
        public static bool operator ==(Discards left, Discards right) => true;
        public static bool operator !=(Discards left, Discards right) => false;
    }

}
