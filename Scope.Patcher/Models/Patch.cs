namespace Scope.Patcher.Models
{
    public struct Patch
    {
        public uint Address { get; private set; }
        public byte OldValue { get; private set; }
        public byte NewValue { get; private set; }

        public Patch(uint address, byte oldValue, byte newValue)
        {
            Address = address;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}