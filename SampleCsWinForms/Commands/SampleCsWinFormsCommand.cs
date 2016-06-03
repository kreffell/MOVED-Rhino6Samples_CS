using Rhino;
using Rhino.Commands;

namespace SampleCsWinForms.Commands
{
  [System.Runtime.InteropServices.Guid("3ad6e74a-1e9a-416d-a494-5724ebea6f03")]
  public class SampleCsWinFormsCommand : Command
  {
    public override string EnglishName
    {
      get { return "SampleCsWinFormsCommand"; }
    }

    protected override Result RunCommand(RhinoDoc doc, RunMode mode)
    {
      RhinoApp.WriteLine("{0} plug-in loaded.", SampleCsWinFormsPlugIn.Instance.Name);
      return Result.Success;
    }
  }
}
