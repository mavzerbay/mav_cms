namespace MAV.Cms.Common.BaseModels
{
    public class BaseSpecParams
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Sort { get; set; }
        public string MatchMode { get; set; }
        public bool? isAll { get; set; }

        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}
