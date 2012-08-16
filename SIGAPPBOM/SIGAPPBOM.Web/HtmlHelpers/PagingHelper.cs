using System.Web.Mvc;
using System.Text;
using SIGAPPBOM.Web.Pagination;

namespace SIGAPPBOM.Web.HtmlHelpers
{
    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper helper,
                                    IPagedList pagedList, string action)
        {
            StringBuilder liHtml = new StringBuilder();
            if (pagedList.HasPreviousPage)
            {
                TagBuilder litag = CreateLI(helper, pagedList.CurrentPage - 1, "&lt;&lt;", action);
                liHtml.AppendLine(litag.ToString());
            }
            for (int i = 1; i <= pagedList.TotalPages; i++)
            {
                TagBuilder litag = CreateLI(helper, i, i.ToString(), action);
                if (i == pagedList.CurrentPage)
                {
                    litag = new TagBuilder("li");
                    litag.InnerHtml = i.ToString();
                    litag.AddCssClass("active");
                }
                liHtml.AppendLine(litag.ToString());

            }
            if (pagedList.HasNextPage)
            {
                TagBuilder litag = CreateLI(helper, pagedList.CurrentPage + 1, "&gt;&gt;", action);
                liHtml.AppendLine(litag.ToString());
            }

            TagBuilder ultag = new TagBuilder("ul");
            ultag.InnerHtml = liHtml.ToString();

            return MvcHtmlString.Create(ultag.ToString());

        }

        private static TagBuilder CreateLI(HtmlHelper html,
                                            int pagenumber,
                                            string text,
                                            string action)
        {
            UrlHelper urlHelper = new UrlHelper(html.ViewContext.RequestContext);

            var url = string.Empty;
            url = urlHelper.Action(action,
                                   new { pagina = pagenumber });

            TagBuilder aTag = new TagBuilder("a");
            aTag.MergeAttribute("href", url);
            aTag.InnerHtml = text;

            TagBuilder litag = new TagBuilder("li");
            litag.InnerHtml = aTag.ToString();
            return litag;
        }


    }
}