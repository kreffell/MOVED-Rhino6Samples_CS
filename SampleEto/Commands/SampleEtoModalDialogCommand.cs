using Rhino.UI;

namespace SampleEto.Commands
{
  [System.Runtime.InteropServices.Guid("3effa6c1-ae59-463e-8c86-ee727044308e")]
  public class SampleEtoModalDialogCommand : Rhino.Commands.Command
  {
    public override string EnglishName
    {
      get { return "SampleEtoModalDialog"; }
    }

    protected override Rhino.Commands.Result RunCommand(Rhino.RhinoDoc doc, Rhino.Commands.RunMode mode)
    {
      var dialog = new Views.SampleEtoModalDialog();
      dialog.RestorePosition();
      var rc = dialog.ShowModal(RhinoEtoApp.MainWindow);
      dialog.SavePosition();
      return (rc == Eto.Forms.DialogResult.Ok) ? Rhino.Commands.Result.Success : Rhino.Commands.Result.Cancel;
    }
  }
}
