namespace LibraryManagement.Services
{
    public class ReceiptService
    {
        private ReceiptService() { }
        private static ReceiptService _ins;
        public static ReceiptService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ReceiptService();
                }
                return _ins;
            }
            private set => _ins = value;
        }

    }
}
