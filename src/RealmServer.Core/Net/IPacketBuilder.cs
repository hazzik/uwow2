namespace Hazzik.Net
{
    public interface IPacketBuilder
    {
        bool IsEmpty { get; }
        IPacket Build();
    }
}