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
  public class CustomRenderSection1 : CustomSection
  {
    private CustomRenderSettingsViewModel m_view_model;
    private Button m_button;
    private CheckBox m_checkbox;
    private Label m_button_lb;
    private Label m_checkbox_lb;
    private Label m_button_clicks_lb;
    private Label m_checkbox_value_lb;
    private int m_clicks;
    private LocalizeStringPair m_caption;

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
    /// The Heigth of the section
    ///</summary>
    public override int SectionHeight
    {
      get
      {
        return 80;
      }
    }

    public override LocalizeStringPair Caption
    {
      get { return m_caption; }
    }

    ///<summary>
    /// Constructor for SectionOne
    ///</summary>
    public CustomRenderSection1()
    {
      m_caption = new LocalizeStringPair("Custom Render Section1", "Custom Render Section1");
      InitializeComponents();
      InitializeLayout();
      RegisterControlEvents();
    }

    private void InitializeComponents()
    {
      m_clicks = 0;

      m_button = new Button()
      {
        Text = "Button"
      };

      m_checkbox = new CheckBox();

      m_button_lb = new Label()
      {
        Text = "",
        VerticalAlignment = VerticalAlignment.Center,
        Width = 20
      };

      m_checkbox_lb = new Label()
      {
        Text = "",
        VerticalAlignment = VerticalAlignment.Center,
        Width = 20
      };

      m_button_clicks_lb = new Label()
      {
        Text = "Number of clicks: ",
        VerticalAlignment = VerticalAlignment.Center,
      };

      m_checkbox_value_lb = new Label()
      {
        Text = "Value of checkbox",
        VerticalAlignment = VerticalAlignment.Center,
      };

      m_checkbox_lb.Text = m_checkbox.Checked.ToString();
      m_button_lb.Text = m_clicks.ToString();
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
                    new TableRow(m_button_clicks_lb, m_button_lb, m_button),
                    new TableRow(m_checkbox_value_lb, m_checkbox, m_checkbox_lb),
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
      m_button.Click += OnButtonClick;
    }

    private void UnRegisterControlEvents()
    {
      m_checkbox.MouseDoubleClick -= OnCheckedChanged;
      m_button.Click -= OnButtonClick;
    }

    private void OnCheckedChanged(object sender, EventArgs e)
    {
      m_checkbox_lb.Text = m_checkbox.Checked.ToString();

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

    private void OnButtonClick(object sender, EventArgs e)
    {
      m_clicks++;
      m_button_lb.Text = m_clicks.ToString();
    }

  }
}
