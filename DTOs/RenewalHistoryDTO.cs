namespace LibraryManagement.DTOs
{
    public class RenewalHistoryDTO
    {
        public int id { get; set; }
        public string readerId { get; set; }
        public System.DateTime renewalDate { get; set; }
        public System.DateTime endDate { get; set; }
        public System.DateTime createdAt { get; set; }

        public  ReaderCardDTO ReaderCard { get; set; }

        //This is for UI binding only
        public int Index { get; set; }
    }
}
