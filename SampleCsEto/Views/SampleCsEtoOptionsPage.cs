using System.Diagnostics;
using Eto.Drawing;
using Eto.Forms;
using Rhino.UI;

namespace SampleCsEto.Views
{
  class SampleCsEtoOptionsPage : OptionsDialogPage
  {
    private SampleCsEtoOptionsPageControl m_page_control;

    public SampleCsEtoOptionsPage()
      : base("Sample")
    {
    }

    public override bool OnActivate(bool active)
    {
      return (m_page_control == null || m_page_control.OnActivate(active));
    }

    public override bool OnApply()
    {
      return (m_page_control == null || m_page_control.OnApply());
    }

    public override void OnCancel()
    {
      if (m_page_control != null)
        m_page_control.OnCancel();
    }

    public override object PageControl
    {
      get
      {
        return (m_page_control ?? (m_page_control = new SampleCsEtoOptionsPageControl()));
      }
    }
  }

  class SampleCsEtoOptionsPageControl : Panel
  {
    public SampleCsEtoOptionsPageControl()
    {
      var hello_button = new Button { Text = "Hello" };
      hello_button.Click += (sender, e) => OnHelloButton();

      var layout = new DynamicLayout { DefaultSpacing = new Size(5, 5), Padding = new Padding(10) };
      layout.AddSeparateRow(hello_button, null);
      layout.Add(null);
      Content = layout;
    }

    public bool OnActivate(bool active)
    {
      Debug.WriteLine("SampleCsEtoOptionsDialogPage.OnActive(" + active + ")");
      return true;
    }

    public bool OnApply()
    {
      Debug.WriteLine("SampleCsEtoOptionsDialogPage.OnApply()");
      return true;
    }

    public void OnCancel()
    {
      Debug.WriteLine("SampleCsEtoOptionsDialogPage.OnCancel()");
    }

    protected void OnHelloButton()
    {
      MessageBox.Show(this, "Hello Rhino!", "Sample", MessageBoxButtons.OK);
    }
  }
}
