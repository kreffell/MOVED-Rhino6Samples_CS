using Eto.Drawing;
using Eto.Forms;
using Rhino.UI;

namespace SampleCsEto.Views
{
  /// <summary>
  /// Required class GUID, used as the panel Id
  /// </summary>
  [System.Runtime.InteropServices.Guid("0E7780CA-F004-4AE7-B918-19E68BF7C7C9")]
  public class SampleCsEtoPanel : Panel, IPanel
  {
    /// <summary>
    /// Provide easy access to the SampleCsEtoPanel.GUID
    /// </summary>
    public static System.Guid PanelId
    {
      get { return typeof(SampleCsEtoPanel).GUID; }
    }
    /// <summary>
    /// Provide easy access to the panel, this will be null until the panel has
    /// been opened at least once.
    /// </summary>
    public static SampleCsEtoPanel Panel
    {
      get { return (Panels.GetPanel(PanelId) as SampleCsEtoPanel); }
    }
    /// <summary>
    /// Required public constructor with NO parameters
    /// </summary>
    public SampleCsEtoPanel(uint documentSerialNumber)
    {
      m_document_sn = documentSerialNumber;

      Title = GetType().Name;

      var hello_button = new Button { Text = "Hello" };
      hello_button.Click += (sender, e) => OnHelloButton();

      m_document_sn_lable = new Label() {Text = $"Document serial number: {documentSerialNumber}"};

      var layout = new DynamicLayout { DefaultSpacing = new Size(5, 5), Padding = new Padding(10) };
      layout.AddSeparateRow(hello_button, null);
      layout.AddSeparateRow(m_document_sn_lable, null);
      layout.Add(null);
      Content = layout;
    }

    private readonly Label m_document_sn_lable;
    private readonly uint m_document_sn;

    public string Title { get; private set; }

    protected void OnHelloButton()
    {
      MessageBox.Show(this, "Hello Rhino!", Title, MessageBoxButtons.OK);
    }

    #region IPanel methods
    public void PanelShown(uint documentSerialNumber)
    {
      // Called when the panel tab is made visible, in Mac Rhino this will happen
      // for a document panel when a new document becomes active, the previous
      // documents panel will get hidden and the new current panel will get shown.
      Rhino.RhinoApp.WriteLine($"Panel shown for document {documentSerialNumber}, this serial number {m_document_sn} should be the same");
    }

    public void PanelHidden(uint documentSerialNumber)
    {
      // Called when the panel tab is hidden, in Mac Rhino this will happen
      // for a document panel when a new document becomes active, the previous
      // documents panel will get hidden and the new current panel will get shown.
      Rhino.RhinoApp.WriteLine($"Panel hidden for document {documentSerialNumber}, this serial number {m_document_sn} should be the same");
    }

    public void PanelClosing(uint documentSerialNumber)
    {
      // Called when the document or panel container is closed/destroyed
      Rhino.RhinoApp.WriteLine($"Panel closing for document {documentSerialNumber}, this serial number {m_document_sn} should be the same");
    }
    #endregion IPanel methods
  }
}
