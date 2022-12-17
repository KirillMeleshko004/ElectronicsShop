namespace ElectronicsShop.GlobalConst
{
    public static class OrderStatus
    {
        public const string AWAITING_CONFIRMATION = "Awaiting for confirmation";
        public const string ACCEPTED = "Accepted";
        public const string IN_TRANSIT = "In transit";
        public const string CANCELED = "Canceled";
        public const string CANCELLATION_REQUESTED = "Cancellation requested";
        public const string COMPLETED = "Completed";

        public static readonly List<string> STATUSES = new()
        {
            AWAITING_CONFIRMATION,
            ACCEPTED,
            IN_TRANSIT,
            CANCELED,
            CANCELLATION_REQUESTED,
            COMPLETED,
        };
    }
}
