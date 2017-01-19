using System.ComponentModel;
using System.Runtime.CompilerServices;
using Rhino.UI.Controls;
using Rhino.Render;
using Rhino.Collections;


namespace SampleCustomRenderSettingsSections
{
  public class CustomRenderSettingsViewModel : CollapsibleSectionViewModel, INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    private RenderSettings RenderSettingsForRead()
    {
      return GetData(Rhino.UI.Controls.DataSource.ProviderIds.RenderSettings, false, true) as RenderSettings;
    }

    private RenderSettings RenderSettingsForWrite()
    {
      return GetData(Rhino.UI.Controls.DataSource.ProviderIds.RenderSettings, true, true) as RenderSettings;
    }

    private void CommitRenderSettings()
    {
      Commit(Rhino.UI.Controls.DataSource.ProviderIds.RenderSettings);
    }

    // The CheckBoxValue is a custom boolean User Data 
    // value for Render Settings
    public bool? CheckBoxValue
    {
      get
      {
        var rs = RenderSettingsForRead();

        bool value = false;
        ArchivableDictionary userdata = rs.UserDictionary;
        if (!userdata.TryGetBool("BoolValue", out value))
          return false;

        return value; 
      }

      set
      {
        if (value != CheckBoxValue)
        {
          if (value != null)
          {
            using (var u = UndoHelper("Custom Render Section 1 BoolValue changed"))
            {
              var rs = RenderSettingsForWrite();

              ArchivableDictionary userdata = rs.UserDictionary;
              userdata.Set("BoolValue", (bool)value);

              CommitRenderSettings();
              OnPropertyChanged();
            }
          }
        }
      }
    }

    public CustomRenderSettingsViewModel(ICollapsibleSection section)
      : base(section)
    {
      
    }

    void OnPropertyChanged([CallerMemberName] string memberName = null)
    {
      // ViewModel -> UI
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(memberName));
      }
    }
  }
}
