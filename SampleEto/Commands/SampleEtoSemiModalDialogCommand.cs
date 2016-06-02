using Rhino.UI;

namespace SampleEto.Commands
{
  [System.Runtime.InteropServices.Guid("1fe1e204-dcdb-487d-8fe6-1d046fb6b597")]
  public class SampleEtoSemiModalDialogCommand : Rhino.Commands.Command
  {
    public override string EnglishName
    {
      get { return "SampleEtoSemiModalDialog"; }
    }

    protected override Rhino.Commands.Result RunCommand(Rhino.RhinoDoc doc, Rhino.Commands.RunMode mode)
    {
      var dialog = new Views.SampleEtoSemiModalDialog();
      dialog.RestorePosition();
      var rc = dialog.ShowSemiModal(doc, RhinoEtoApp.MainWindow);
      dialog.SavePosition();
      return (rc == Eto.Forms.DialogResult.Ok) ? Rhino.Commands.Result.Success : Rhino.Commands.Result.Cancel;
    }
  }
}
