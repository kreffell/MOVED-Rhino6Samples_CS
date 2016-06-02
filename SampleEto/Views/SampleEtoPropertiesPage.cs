using System.Diagnostics;
using Eto.Drawing;
using Eto.Forms;
using Rhino.DocObjects;
using Rhino.UI;

namespace SampleEto.Views
{
  class SampleEtoPropertiesPage : ObjectPropertiesPage
  {
    private SampleEtoPropertiesPageControl m_page_control;

    public override string EnglishPageTitle
    {
      get { return "Sample"; }
    }

    public override System.Drawing.Icon Icon
    {
      get { return Properties.Resources.SampleEtoPanel; }
    }

    public override object PageControl
    {
      get
      {
        return (m_page_control ?? (m_page_control = new SampleEtoPropertiesPageControl()));
      }
    }

    public override bool ShouldDisplay(RhinoObject rhObj)
    {
      Debug.WriteLine("SampleEtoPropertiesPage.ShouldDisplay(" + rhObj + ")");
      return true;
    }

    public override void InitializeControls(RhinoObject rhObj)
    {
      if (m_page_control != null)
        m_page_control.InitializeControls(rhObj);
    }
  }

  class SampleEtoPropertiesPageControl : Panel
  {
    public SampleEtoPropertiesPageControl()
    {
      var hello_button = new Button { Text = "Hello" };
      hello_button.Click += (sender, e) => OnHelloButton();

      var layout = new DynamicLayout { DefaultSpacing = new Size(5, 5), Padding = new Padding(10) };
      layout.AddSeparateRow(hello_button, null);
      layout.Add(null);
      Content = layout;
    }

    public void InitializeControls(RhinoObject rhObj)
    {
      Debug.WriteLine("SampleEtoPropertiesPage.InitializeControls(" + rhObj + ")");
    }

    protected void OnHelloButton()
    {
      MessageBox.Show(this, "Hello Rhino!", "Sample", MessageBoxButtons.OK);
    }
  }
}
