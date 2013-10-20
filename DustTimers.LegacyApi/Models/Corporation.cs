namespace DustTimers.LegacyApi.Models
{
    public class Corporation
    {
        public int Id { get; set; }
        public string CorporationName { get; set; }
        public string Ticker { get; set; }
        public int CeoId { get; set; }
        public string CeoName { get; set; }
        public int AllianceId { get; set; }
        public string AllianceName { get; set; }
        public int MemberCount { get; set; }
    }
}
