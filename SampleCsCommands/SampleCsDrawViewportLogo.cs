using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input.Custom;

namespace SampleCsCommands
{
  /// <summary>
  /// SampleCsDrawViewportLogo command
  /// </summary>
  public class SampleCsDrawViewportLogo : Command
  {
    public override string EnglishName
    {
      get { return "SampleCsDrawViewportLogo"; }
    }

    protected override Result RunCommand(RhinoDoc doc, RunMode mode)
    {
      var conduit = new SampleCsDrawViewportLogoConduit {Enabled = true};
      doc.Views.Redraw();

      var gs = new GetString();
      gs.SetCommandPrompt("Press <Enter> to continue");
      gs.AcceptNothing(true);
      gs.Get();

      conduit.Enabled = false;
      doc.Views.Redraw();

      return Result.Success;
    }
  }

  /// <summary>
  /// SampleCsDrawViewportLogoConduit display conduit
  /// </summary>
  public class SampleCsDrawViewportLogoConduit : Rhino.Display.DisplayConduit
  {
    private float m_sprite_size = 64;
    private readonly Rhino.Display.DisplayBitmap m_bitmap;
    
    public SampleCsDrawViewportLogoConduit()
    {
      var logo = SampleCsCommands.Properties.Resources.Logo;
      m_bitmap = new Rhino.Display.DisplayBitmap(logo);
    }

    protected override void DrawForeground(Rhino.Display.DrawEventArgs e)
    {
      var rect = e.Viewport.Bounds;
      var point = new Point2d(rect.Right - (0.5 * m_sprite_size), rect.Bottom - (0.5 * m_sprite_size));
      e.Display.DrawSprite(m_bitmap, point, m_sprite_size);
    }
  }

}
