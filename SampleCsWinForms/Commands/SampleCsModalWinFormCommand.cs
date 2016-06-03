using System.Windows.Forms;
using Rhino;
using Rhino.Commands;
using RhinoWindows;

namespace SampleCsWinForms.Commands
{
  [System.Runtime.InteropServices.Guid("79a8c6e4-b0c7-44fe-9faf-e9f041004f74")]
  public class SampleCsModalWinFormCommand : Command
  {
    public override string EnglishName
    {
      get { return "SampleCsModalWinFormCommand"; }
    }

    protected override Result RunCommand(RhinoDoc doc, RunMode mode)
    {
      var rc = Result.Cancel;

      if (mode == RunMode.Interactive)
      {
        var form = new Forms.SampleCsModalWinForm { StartPosition = FormStartPosition.CenterParent };
        var dialog_rc = form.ShowDialog(RhinoWinApp.MainWindow);
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
