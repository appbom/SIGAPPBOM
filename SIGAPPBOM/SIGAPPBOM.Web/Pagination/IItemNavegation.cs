namespace SIGAPPBOM.Web.Pagination
{
    public class ItemNavegation
    {
        public string controller{get;set;}
        public string action { get; set; }
        public string title { get; set; }
        public bool isLink { get; set; }
        public bool active { get; set; }
    }
}
