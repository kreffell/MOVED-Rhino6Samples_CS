using System;
using Rhino.UI.Controls;
using Eto.Forms;
using Rhino.UI;
using SampleCustomRenderSettingsSections;
using System.ComponentModel;

namespace CustomRenderSections
{
  ///<summary>
  /// The UI implementation of of Section one
  ///</summary>
  public class CustomRenderSection2 : CustomSection
  {
    private CustomRenderSettingsViewModel m_view_model;
    private Label m_section_label;
    private Label m_checkbox_value_lb;
    private LocalizeStringPair m_caption;
    private CheckBox m_checkbox;

    ///<summary>
    /// The Heigth of the section
    ///</summary>
    public override int SectionHeight
    {
      get
      {
        return 70;
      }
    }

    public override LocalizeStringPair Caption
    {
      get { return m_caption; }
    }

    new public CustomRenderSettingsViewModel ViewModel
    {
      get
      {
        return m_view_model;
      }

      set
      {
        m_view_model = value;
      }
    }

    ///<summary>
    /// Constructor for SectionOne
    ///</summary>
    public CustomRenderSection2()
    {
      m_caption = new LocalizeStringPair("Custom Sun Section2", "Custom Sun Section2");
      InitializeComponents();
      InitializeLayout();
      RegisterControlEvents();
    }

    private void InitializeComponents()
    {

      m_section_label = new Label()
      {
        Text = "This is the render section 2",
        VerticalAlignment = VerticalAlignment.Center,
      };


      m_checkbox_value_lb = new Label()
      {
        Text = "Value of checkbox",
        VerticalAlignment = VerticalAlignment.Center,
      };

      m_checkbox = new CheckBox();
    }


    private void InitializeLayout()
    {
      TableLayout layout = new TableLayout()
      {
        // Padding around the table
        Padding = 10,
        // Spacing between table cells
        Spacing = new Eto.Drawing.Size(15, 15),
        Rows =
                {
                    new TableRow(m_section_label),
                    new TableRow(m_checkbox_value_lb, m_checkbox)
                }
      };
      Content = layout;
    }

    public void SetViewModel(CustomRenderSettingsViewModel view_model)
    {
      ViewModel = view_model;

      // Databinding
      ViewModel.PropertyChanged += new PropertyChangedEventHandler(ViewModelChanged);
      m_checkbox.Bind(m_checkOn => m_checkOn.Checked, ViewModel, (CustomRenderSettingsViewModel m) => m.CheckBoxValue);

      // Get initial values for display
      ViewModel.DisplayData();
    }

    void ViewModelChanged(object sender, PropertyChangedEventArgs e)
    {
      /* Data in Model has changed... */

      // Example code  to read CheckBoxValue when the value 
      // has changed. However this is not needed as we use ETO's
      // direct binding in the SetViewModel function

      //if (e.PropertyName.CompareTo("CheckBoxValue") == 0)
      //{
      //  bool data_value = ViewModel.CheckBoxValue;
      //}
    }

    private void RegisterControlEvents()
    {
      m_checkbox.CheckedChanged += OnCheckedChanged;
    }

    private void UnRegisterControlEvents()
    {
      m_checkbox.MouseDoubleClick -= OnCheckedChanged;
    }

    private void OnCheckedChanged(object sender, EventArgs e)
    {
      // We could change the CheckBoxValue (UI -> ViewModel -> DataSrouce)
      // here, however as we have used two way binding in the SetViewModel function
      // we do not need to update the value here. An example code is still shown
      // below

      //if (m_checkbox.Checked != null)
      //{
      //  bool checked_state = (bool)m_checkbox.Checked;
      //  ViewModel.CheckBoxValue = checked_state;
      //}
    }
  }
}
