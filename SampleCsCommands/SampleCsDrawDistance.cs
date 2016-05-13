using System.Drawing;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

namespace SampleCsCommands
{
  public class SampleCsDrawDistance : Command
  {
    public override string EnglishName
    {
      get { return "SampleCsDrawDistance"; }
    }

    protected override Result RunCommand(RhinoDoc doc, RunMode mode)
    {
      Point3d base_pt;
      var rc = RhinoGet.GetPoint("Start of line", false, out base_pt);
      if (rc != Result.Success)
        return rc;

      var gp = new GetPoint();
      gp.SetCommandPrompt("End of line");
      gp.SetBasePoint(base_pt, true);
      gp.DrawLineFromPoint(base_pt, true);
      gp.DynamicDraw += gp_DynamicDraw;
      gp.Get();
      if (gp.CommandResult() != Result.Success)
        return gp.CommandResult();

      var end_pt = gp.Point();
      var vector = end_pt - base_pt;
      if (vector.Length > doc.ModelAbsoluteTolerance)
      {
        var line = new Line(base_pt, end_pt);
        doc.Objects.AddLine(line);
        doc.Views.Redraw();
      }

      return Result.Success;
    }

    void gp_DynamicDraw(object sender, GetPointDrawEventArgs e)
    {
      Point3d base_pt;
      if (e.Source.TryGetBasePoint(out base_pt))
      {
        // Format distance as string
        var distance = base_pt.DistanceTo(e.CurrentPoint);
        var text = string.Format("{0:0.000}", distance);

        // Get world-to-screen coordinate transformation
        var xform = e.Viewport.GetTransform(Rhino.DocObjects.CoordinateSystem.World, Rhino.DocObjects.CoordinateSystem.Screen);

        // Transform point from world to screen coordinates
        var screen_pt = xform * e.CurrentPoint;

        // Offset point so text does not overlap cursor
        screen_pt.X += 5.0;
        screen_pt.Y -= 5.0;

        // Draw the string 
        e.Display.Draw2dText(text, Color.Black, new Point2d(screen_pt.X, screen_pt.Y), false);
      }
    }
  }
}
