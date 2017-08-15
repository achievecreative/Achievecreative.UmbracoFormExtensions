using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Attributes;
using Umbraco.Forms.Core.Enums;

namespace Achievecreative.UmbracoFormExtensions.WorkflowTypes
{
    public class ExecuteCode : WorkflowType
    {
        public ExecuteCode()
        {
            base.Id = Guid.Parse("5A4FBF1C-749F-4ED4-B1B1-6508CDCA727C");
            base.Name = "Execute Code";
            base.Description = "Execute some code.";
            base.Icon = "icon-rocket";
            base.Group = "Code";
        }

        [Setting("CodeFile", description = "The view file that has the code you want to execute. \n The file need to be placed at '~/Views/Partials/CodeFiles' folder.")]
        public string CodeFile { get; set; }

        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs e)
        {
            var executeResult = CodeFilesUtil.RenderPartial(CodeFile, record);

            switch (executeResult.Trim())
            {
                case "Success":
                    return WorkflowExecutionStatus.Completed;
                case "Failed":
                    return WorkflowExecutionStatus.Failed;
                case "Cancelled":
                default:
                    return WorkflowExecutionStatus.Cancelled;
            }
        }

        public override List<Exception> ValidateSettings()
        {
            var exceptions = new List<Exception>();

            if (string.IsNullOrEmpty(CodeFile))
            {
                exceptions.Add(new Exception($"'CodeFile' setting has not been set."));
            }
            else
            {
                var path = HttpContext.Current.Server.MapPath($"~/Views/Partials/CodeFiles/{CodeFile}.cshtml");
                if (!File.Exists(path))
                {
                    exceptions.Add(new Exception("'CodeFile' not exists. The file need to be placed at '~/Views/Partials/CodeFiles' folder, without '.cshtml'"));
                }
            }

            return exceptions;
        }
    }
}
