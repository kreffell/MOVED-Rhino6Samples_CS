using System.ComponentModel;
using System.Runtime.CompilerServices;
using Rhino.DocObjects.Tables;


namespace SampleCustomRenderSettingsSections
{
  public class CustomRenderSettingsViewModel : INotifyPropertyChanged
  {

    public event PropertyChangedEventHandler PropertyChanged;
    private DataSource m_data_source;
    private bool m_model_to_viewmodel_update;
    private bool m_check_box_value;

    public bool CheckBoxValue
    {
      get
      {
        return m_check_box_value;
      }

      set
      {
        if (value != m_check_box_value)
        {
          m_check_box_value = value;
          OnPropertyChanged();
        }
      }
    }

    public CustomRenderSettingsViewModel()
    {
      m_data_source = new DataSource();
      RegisterControlEvents();
    }

    private void RegisterControlEvents()
    {
      m_data_source.DataChanged += new PropertyChangedEventHandler(ViewModelChanged);
      m_model_to_viewmodel_update = false;
    }

    private void UnRegisterControlEvents()
    {
      m_data_source.DataChanged -= new PropertyChangedEventHandler(ViewModelChanged);
      m_model_to_viewmodel_update = true;
    }

    void ViewModelChanged(object sender, PropertyChangedEventArgs e)
    {
      DisplayData();
    }

    public void DisplayData()
    {
      UnRegisterControlEvents();

      try
      {
        CheckBoxValue = m_data_source.CheckBoxValue;
      }
      catch
      {
        // Some problem with datareading.
      }
      finally
      {
        RegisterControlEvents();
      }
    }

    void OnPropertyChanged([CallerMemberName] string memberName = null)
    {
      // ViewModel -> UI
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(memberName));
      }

      //No updated to datasource if datasource -> viewmodel update
      if (m_model_to_viewmodel_update) return;

      // ViewModel -> DataSource
      if (memberName.Equals("CheckBoxValue"))
      {
        m_data_source.CheckBoxValue = CheckBoxValue;
      }
    }
  }
}
