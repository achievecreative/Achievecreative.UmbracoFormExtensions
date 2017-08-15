using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core.Logging;

namespace Achievecreative.UmbracoFormExtensions.WorkflowTypes
{
    /// <summary>
    /// Summary description for ViewHelper
    /// </summary>
    public static class CodeFilesUtil
    {
        public class CodeFilesController : Controller { }

        public static string RenderPartial(string partialName, object model)
        {
            try
            {
                //get a wrapper for the legacy WebForm context
                var httpCtx = new HttpContextWrapper(System.Web.HttpContext.Current);

                //create a mock route that points to the empty controller
                var rt = new RouteData();
                rt.Values.Add("controller", "CodeFiles");

                //create a controller context for the route and http context
                var ctx = new ControllerContext(new RequestContext(httpCtx, rt), new CodeFilesController());

                //find the partial view using the viewengine
                var view = ViewEngines.Engines.FindPartialView(ctx, $"CodeFiles/{partialName}").View;

                using (var sw = new StringWriter())
                {
                    //create a view context and assign the model
                    var vctx = new ViewContext(ctx, view, new ViewDataDictionary { Model = model },
                        new TempDataDictionary(), sw);

                    //render the partial view
                    view.Render(vctx, sw);

                    return sw.GetStringBuilder().ToString();
                }
            }
            catch (Exception e)
            {
                LogHelper.Error<CodeFilesController>(e.Message, e);
            }

            return string.Empty;
        }
    }
}