namespace Shared.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value > maxPageSize)
                {
                    _pageSize = maxPageSize;
                } 
                else
                {
                    _pageSize = value;
                }
            }
        }

        public string? OrderBy { get; set; }

        public string? Fields { get; set; }
    }
}
