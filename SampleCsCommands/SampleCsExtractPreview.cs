using System.IO;
using System.Windows.Forms;
using Rhino;
using Rhino.Commands;

namespace SampleCsCommands
{
  public class SampleCsExtractPreview : Command
  {
    public override string EnglishName
    {
      get { return "SampleCsExtractPreview"; }
    }

    protected override Result RunCommand(RhinoDoc doc, RunMode mode)
    {
      var dialog = new OpenFileDialog
      {
        Filter = @"Rhino 3D Models (*.3dm)|*.3dm",
        DefaultExt = "3dm"
      };

      var rc = dialog.ShowDialog();
      if (rc != DialogResult.OK)
        return Result.Cancel;

      var filename = dialog.FileName;
      if (string.IsNullOrEmpty(filename) || !File.Exists(filename))
        return Result.Failure;

      var bitmap = RhinoDoc.ExtractPreviewImage(filename);
      if (null != bitmap)
      {
        filename = Path.ChangeExtension(filename, "png");
        bitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
      }

      return Result.Success;
    }
  }
}
