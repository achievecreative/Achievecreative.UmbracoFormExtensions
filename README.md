# Achievecreative.UmbracoFormExtensions
Custom workflow type, able to execute some code after form submitted


### Example
```C#
@using Achievecreative.UmbracoFormExtensions
@using Umbraco.Core.Logging
@model Umbraco.Forms.Core.Record
@{
    LogHelper.Log(this.GetType(), "Form submitted");
}
