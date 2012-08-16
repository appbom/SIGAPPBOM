using System.Collections.Generic;
using System.Web.Mvc;
using System.Text;
using SIGAPPBOM.Web.Pagination;

namespace SIGAPPBOM.Web.HtmlHelpers
{
    public static class NavegationHelper
    {
        public static MvcHtmlString Navegation(this HtmlHelper helper, List<ItemNavegation> items)
        {
            StringBuilder liHtml = new StringBuilder();
            int contador = 0;
            foreach(ItemNavegation item in items)
            {
                contador++;
                TagBuilder litag = CreateLI(helper,item);
                liHtml.AppendLine(litag.ToString());
                
            }
            TagBuilder ultag = new TagBuilder("ul");
            ultag.InnerHtml = liHtml.ToString();
            ultag.AddCssClass("breadcrumb ventana-navegacion");
            return MvcHtmlString.Create(ultag.ToString());
        }
        private static TagBuilder CreateSeparator()
        {
             TagBuilder spanTag = new TagBuilder("span");
             spanTag.InnerHtml =">";
             spanTag.AddCssClass("divider");
             return spanTag;
            
        }
        private static TagBuilder CreateLI(HtmlHelper html,ItemNavegation item)
        {
            TagBuilder litag = new TagBuilder("li");
            UrlHelper urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var url = string.Empty;
            url = urlHelper.Action(item.action,item.controller);
            if (!item.active)
            {
                TagBuilder aTag = new TagBuilder("a");
                aTag.MergeAttribute("href", url);
                aTag.InnerHtml = item.title;
                litag.InnerHtml = aTag.ToString()+CreateSeparator().ToString();
            }
            else
                litag.InnerHtml = item.title;

            return litag;
        }
    }
}