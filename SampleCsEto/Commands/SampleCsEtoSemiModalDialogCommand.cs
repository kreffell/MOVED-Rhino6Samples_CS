using Rhino.UI;

namespace SampleCsEto.Commands
{
  [System.Runtime.InteropServices.Guid("1fe1e204-dcdb-487d-8fe6-1d046fb6b597")]
  public class SampleCsEtoSemiModalDialogCommand : Rhino.Commands.Command
  {
    public override string EnglishName
    {
      get { return "SampleCsEtoSemiModalDialog"; }
    }

    protected override Rhino.Commands.Result RunCommand(Rhino.RhinoDoc doc, Rhino.Commands.RunMode mode)
    {
      var dialog = new Views.SampleCsEtoSemiModalDialog();
      dialog.RestorePosition();
      var rc = dialog.ShowSemiModal(doc, RhinoEtoApp.MainWindow);
      dialog.SavePosition();
      return (rc == Eto.Forms.DialogResult.Ok) ? Rhino.Commands.Result.Success : Rhino.Commands.Result.Cancel;
    }
  }
}
