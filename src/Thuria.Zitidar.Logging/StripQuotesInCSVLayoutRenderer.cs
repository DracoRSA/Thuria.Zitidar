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
    /// <summary>
    /// Transform the text string
    /// </summary>
    /// <param name="text">Input String</param>
    /// <returns>Transformed string</returns>
    protected override string Transform(string text)
    {
      return text.Replace("\"", "&quot;").Replace("\n", " ").Replace("\r", " ");
    }
  }
}
