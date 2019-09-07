namespace FactorioNetParser.FactorioNet
{
    public enum MessageType
    {
        Ping,
        PingReply,
        ConnectionRequest,
        ConnectionRequestReply,
        ConnectionRequestReplyConfirm,
        ConnectionAcceptOrDeny,
        ClientToServerHeartbeat,
        ServerToClientHeartbeat,
        GetOwnAddress,
        GetOwnAddressReply,
        NatPunchRequest,
        NatPunch,
        TransferBlockRequest,
        TransferBlock,
        RequestForHeartbeatWhenDisconnecting,
        LanBroadcast,
        GameInformationRequest,
        GameInformationRequestReply,
        Empty
    }


    public static class MessageTypeExtensions
    {
        public static bool IsAlwaysReliable(this MessageType type)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (type)
            {
                case MessageType.ConnectionRequest:
                case MessageType.ConnectionRequestReply:
                case MessageType.ConnectionRequestReplyConfirm:
                case MessageType.ConnectionAcceptOrDeny:
                    return true;
                default:
                    return false;
            }
        }
    }
}