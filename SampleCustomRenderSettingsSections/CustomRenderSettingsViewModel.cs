using System.ComponentModel;
using System.Runtime.CompilerServices;
using Rhino.DocObjects.Tables;

using Rhino.UI.Controls;
using Rhino.Render;


namespace SampleCustomRenderSettingsSections
{
  public class CustomRenderSettingsViewModel : CollapsibleSectionViewModel, INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    private RenderSettings RenderSettingsForRead()
    {
      return base.GetData(Rhino.UI.Controls.DataSource.ProviderIds.Settings, false) as RenderSettings;
    }

    private RenderSettings RenderSettingsForWrite()
    {
      return base.GetData(Rhino.UI.Controls.DataSource.ProviderIds.Settings, true) as RenderSettings;
    }

    private void CommitRenderSettings()
    {
      base.Commit(Rhino.UI.Controls.DataSource.ProviderIds.Settings);
    }

    public bool CheckBoxValue
    {
      get
      {
        var rs = RenderSettingsForRead();
        return null!=rs ? rs.FlatShade : false;
      }

      set
      {
        if (value != CheckBoxValue)
        {
          var rs = RenderSettingsForWrite();
          if (rs != null)
          {
            rs.FlatShade = value;
            CommitRenderSettings();
            OnPropertyChanged();
          }
        }
      }
    }

    public CustomRenderSettingsViewModel(ICollapsibleSection section)
      : base(section)
    {
      RegisterControlEvents();
    }

    private void RegisterControlEvents()
    {
      //m_data_source.DataChanged += new PropertyChangedEventHandler(ViewModelChanged);
    }

    private void UnRegisterControlEvents()
    {
      //m_data_source.DataChanged -= new PropertyChangedEventHandler(ViewModelChanged);
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
        //CheckBoxValue = m_data_source.CheckBoxValue;
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
//if (m_model_to_viewmodel_update) return;

      // ViewModel -> DataSource
     // if (memberName.Equals("CheckBoxValue"))
      //{
      //  m_data_source.CheckBoxValue = CheckBoxValue;
      //}
    }
  }
}
