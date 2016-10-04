using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SampleCsWinForms.Forms;

namespace SampleCsWinForms.Forms
{
  internal class SampleCsOptionsPageDialog : Rhino.UI.OptionsDialogPage
  {
    private SampleCsOptionsUserControl m_control;

    public SampleCsOptionsPageDialog()
      : base("Sample")
    {
    }

    public override object PageControl
    {
      get { return m_control ?? (m_control = new SampleCsOptionsUserControl()); }
    }

    public override bool OnApply()
    {
      Rhino.RhinoApp.WriteLine("SampleCsOptionsPageDialog.OnApply");
      return true;
    }

    public override void OnCancel()
    {
      Rhino.RhinoApp.WriteLine("SampleCsOptionsPageDialog.OnCancel");
    }
  }

}
