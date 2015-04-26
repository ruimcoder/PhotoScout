using System.Web.Mvc;

namespace ScoutterSite.Helpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString Translate(this HtmlHelper htmlHelper, string key)
        {
            var viewPath = ((System.Web.Mvc.RazorView)htmlHelper.ViewContext.View).ViewPath;
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;

            var httpContext = htmlHelper.ViewContext.HttpContext;
            var val = (string)httpContext.GetLocalResourceObject(viewPath, key, culture);

            return MvcHtmlString.Create(val);
        }
    }
}