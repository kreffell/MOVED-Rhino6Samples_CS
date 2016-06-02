using System.Collections.Generic;
using Rhino;
using Rhino.UI;

namespace SampleEto
{
  public class SampleEtoPlugIn : Rhino.PlugIns.PlugIn
  {
    public SampleEtoPlugIn()
    {
      Instance = this;
    }

    public static SampleEtoPlugIn Instance
    {
      get;
      private set;
    }

    protected override void DocumentPropertiesDialogPages(RhinoDoc doc, List<OptionsDialogPage> pages)
    {
      var page = new Views.SampleEtoOptionsPage();
      pages.Add(page);
    }

    protected override void OptionsDialogPages(List<OptionsDialogPage> pages)
    {
      var page = new Views.SampleEtoOptionsPage();
      pages.Add(page);
    }

    protected override void ObjectPropertiesPages(List<ObjectPropertiesPage> pages)
    {
      var page = new Views.SampleEtoPropertiesPage();
      pages.Add(page);
    }
  }
}