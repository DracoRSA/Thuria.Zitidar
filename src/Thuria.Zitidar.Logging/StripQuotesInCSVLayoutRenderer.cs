using NLog.Config;
using NLog.LayoutRenderers;
using NLog.LayoutRenderers.Wrappers;

namespace Thuria.Zitidar.Logging
{
  /// <summary>
  /// Strip Quotes in CSV Layout Renderer
  /// </summary>
  [LayoutRenderer("strip-quotes")]
  [AmbientProperty("StripQuotes")]
  [ThreadAgnostic]
  public class StripQuotesInCSVLayoutRenderer : WrapperLayoutRendererBase
  {
    protected override string Transform(string text)
    {
      return text.Replace("\"", "&quot;").Replace("\n", " ").Replace("\r", " ");
    }
  }
}
