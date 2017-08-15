using System.Linq;
using Umbraco.Forms.Core;

namespace Achievecreative.UmbracoFormExtensions
{
    public static class RecordExtensions
    {
        public static string Field(this Record source, string fieldAlias)
        {
            if (source == null || string.IsNullOrEmpty(fieldAlias))
            {
                return string.Empty;
            }

            var field = source.GetForm()?.AllFields?.FirstOrDefault(x => x.Alias == fieldAlias);
            if (field == null)
            {
                return string.Empty;
            }

            return source.GetRecordField(field.Id)?.ValuesAsString();
        }
    }
}
