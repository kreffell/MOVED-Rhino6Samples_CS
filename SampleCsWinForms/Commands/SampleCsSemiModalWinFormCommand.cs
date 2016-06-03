using System.Windows.Forms;
using Rhino;
using Rhino.Commands;
using RhinoWindows;
using RhinoWindows.Forms;

namespace SampleCsWinForms.Commands
{
  [System.Runtime.InteropServices.Guid("b1539151-08b6-4a44-8880-d7f4aeb119ca")]
  public class SampleCsSemiModalWinFormCommand : Command
  {
    public override string EnglishName
    {
      get { return "SampleCsSemiModalWinForm"; }
    }

    protected override Result RunCommand(RhinoDoc doc, RunMode mode)
    {
      var rc = Result.Cancel;

      if (mode == RunMode.Interactive)
      {
        var form = new Forms.SampleCsModalWinForm { StartPosition = FormStartPosition.CenterParent };
        var dialog_rc = form.ShowSemiModal(RhinoWinApp.MainWindow);
        if (dialog_rc == DialogResult.OK)
          rc = Result.Success;
      }
      else
      {
        var msg = string.Format("Scriptable version of {0} command not implemented.", EnglishName);
        RhinoApp.WriteLine(msg);
      }

      return rc;
    }
  }
}
