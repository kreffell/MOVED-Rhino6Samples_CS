using System;
using Rhino;
using Rhino.Commands;
using Rhino.UI;

namespace SampleEto.Commands
{
  [System.Runtime.InteropServices.Guid("5ae77c74-9bb9-4bea-93eb-8ee7c7838167")]
  public class SampleEtoModelessFormCommand : Command
  {
    /// <summary>
    /// Form accessor
    /// </summary>
    private Views.SampleEtoModelessForm Form
    {
      get;
      set;
    }

    public override string EnglishName
    {
      get { return "SampleEtoModelessForm"; }
    }

    protected override Result RunCommand(RhinoDoc doc, RunMode mode)
    {
      if (null == Form)
      {
        Form = new Views.SampleEtoModelessForm { Owner = RhinoEtoApp.MainWindow };
        Form.RestorePosition();
        Form.Closed += OnFormClosed;
        Form.Show();
      }
      return Result.Success;
    }

    /// <summary>
    /// FormClosed EventHandler
    /// </summary>
    private void OnFormClosed(object sender, EventArgs e)
    {
      Form.SavePosition();
      Form.Dispose();
      Form = null;
    }
  }
}
