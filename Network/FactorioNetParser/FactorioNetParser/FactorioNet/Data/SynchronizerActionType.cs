namespace FactorioNetParser.FactorioNet.Data
{
    public enum SynchronizerActionType : byte
    {
        GameEnd,
        PeerDisconnect,
        NewPeerInfo,
        ClientChangedState,
        ClientShouldStartSendingTickClosures,
        MapReadyForDownload,
        MapLoadingProgressUpdate,
        MapSavingProgressUpdate,
        SavingForUpdate,
        MapDownloadingProgressUpdate,
        CatchingUpProgressUpdate,
        PeerDroppingProgressUpdate,
        PlayerDesynced,
        BeginPause,
        EndPause,
        SkippedTickClosure,
        SkippedTickClosureConfirm,
        ChangeLatency,
        IncreasedLatencyConfirm,
        SavingCountDown,
        InputActionSegmentsInFlight,
        InputActionSegmentsInFlightFinished
    }
}