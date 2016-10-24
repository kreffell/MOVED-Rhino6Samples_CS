using System.Drawing;
using Rhino;
using Rhino.ApplicationSettings;
using Rhino.Commands;

namespace SampleCsCommands
{
  public class SampleCsAppearanceColors : Command
  {
    public override string EnglishName
    {
      get { return "SampleCsAppearanceColors"; }
    }

    protected override Result RunCommand(RhinoDoc doc, RunMode mode)
    {
      //
      // Colors from Appearance page
      //

      Print("Command prompt");
      PrintColor("Background", AppearanceSettings.CommandPromptBackgroundColor);
      PrintColor("Text color", AppearanceSettings.CommandPromptTextColor);
      PrintColor("Hover color", AppearanceSettings.CommandPromptBackgroundColor);

      //
      // Colors from Appearance -> Colors page
      //

      Print("Viewport colors");
      PrintColor("Background", AppearanceSettings.ViewportBackgroundColor);
      PrintColor("Major grid line", AppearanceSettings.GridThickLineColor);
      PrintColor("Minor grid line", AppearanceSettings.GridThinLineColor);
      PrintColor("X-axis line", AppearanceSettings.GridXAxisLineColor);
      PrintColor("Y-axis line", AppearanceSettings.GridYAxisLineColor);
      PrintColor("Z-axis line", AppearanceSettings.GridZAxisLineColor);
      PrintColor("World axis icon X", AppearanceSettings.WorldCoordIconXAxisColor);
      PrintColor("World axis icon Y", AppearanceSettings.WorldCoordIconYAxisColor);
      PrintColor("World axis icon Z", AppearanceSettings.WorldCoordIconZAxisColor);
      PrintColor("Layout", AppearanceSettings.PageviewPaperColor);

      Print("Object display");
      PrintColor("Selected objects", AppearanceSettings.SelectedObjectColor);
      PrintColor("Locked objects", AppearanceSettings.LockedObjectColor);
      PrintColor("New layer", AppearanceSettings.DefaultLayerColor);

      Print("Interface objects");
      PrintColor("Feedback", AppearanceSettings.CrosshairColor);
      PrintColor("Tracking lines", AppearanceSettings.CrosshairColor);
      PrintColor("Crosshair", AppearanceSettings.CrosshairColor);

      Print("Layer dialog box");
      PrintColor("Layout settings background", AppearanceSettings.CurrentLayerBackgroundColor);

      Print("General");
      PrintColor("Window color 1", AppearanceSettings.GetPaintColor(PaintColor.NormalStart));
      PrintColor("Window color 2", AppearanceSettings.GetPaintColor(PaintColor.NormalEnd));
      PrintColor("Window color 3", AppearanceSettings.GetPaintColor(PaintColor.HotStart));
      PrintColor("Window border", AppearanceSettings.GetPaintColor(PaintColor.NormalBorder));
      PrintColor("Window text", AppearanceSettings.GetPaintColor(PaintColor.TextEnabled));
      PrintColor("Active viewport title", AppearanceSettings.GetPaintColor(PaintColor.ActiveViewportTitle));
      PrintColor("Inactive viewport title", AppearanceSettings.GetPaintColor(PaintColor.InactiveViewportTitle));

      Print("Widget colors");
      PrintColor("U-axis", AppearanceSettings.GetWidgetColor(WidgetColor.UAxisColor));
      PrintColor("V-axis", AppearanceSettings.GetWidgetColor(WidgetColor.VAxisColor));
      PrintColor("W-axis", AppearanceSettings.GetWidgetColor(WidgetColor.WAxisColor));

      return Result.Success;
    }

    private static void Print(string label)
    {
      RhinoApp.WriteLine(label);
    }

    private static void PrintColor(string label, Color color)
    {
      RhinoApp.WriteLine("\t{0} - {1}", label, color.Name);
    }
  }
}
