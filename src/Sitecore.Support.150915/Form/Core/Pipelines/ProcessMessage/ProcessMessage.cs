namespace Sitecore.Support.Form.Core.Pipelines.ProcessMessage
{
  using Sitecore.Form.Core.Pipelines.ProcessMessage;
  using Sitecore.Forms.Core.Data;
  using Sitecore.Form.Core.Configuration;
  using Sitecore.Form.Core.Controls.Data;
  using System.Text.RegularExpressions;
  using Sitecore.Form.Core.Utility;

  public class ProcessMessage
  {
    private static readonly string shortHrefReplacer = string.Join("", "href=\"", Sitecore.Web.WebUtil.GetServerUrl(), "/" );
    private static readonly string hrefReplacer = (shortHrefReplacer + "~");
    private static readonly string srcReplacer = string.Join("", "src=\"", Sitecore.Web.WebUtil.GetServerUrl(), "/~");

    public void ExpandTokens(ProcessMessageArgs args)
    {
      var mail = args.Mail.ToString();
      foreach (AdaptedControlResult field in args.Fields)
      {
        var item = new FieldItem(StaticSettings.ContextDatabase.GetItem(field.FieldID));
        var id = field.FieldID.ToString();
        var value = field.Value;

        value = Regex.Replace(FieldReflectionUtil.GetAdaptedValue(item, value), "src=\"/sitecore/shell/themes/standard/~", srcReplacer);
        value = Regex.Replace(value, "href=\"/sitecore/shell/themes/standard/~", hrefReplacer);
        value = Regex.Replace(value, "on\\w*=\".*?\"", string.Empty);
        mail = ProcessMessageHelper.ReplaceFieldValue(mail, id, value);
      }

      args.Mail.Clear();
      args.Mail.Append(mail);
    }
  }

  public static class ProcessMessageHelper
  {
    public static string ReplaceFieldValue(string mail, string id, string value)
    {
      return Regex.Replace(mail, $@"\[\<label id=""{id}""\>[^<]*</label>]", value);
    }
  }
}