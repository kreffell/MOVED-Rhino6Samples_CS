namespace SampleEto.Commands
{
  [System.Runtime.InteropServices.Guid("9cac6db3-33a3-4a76-82ca-003d7e90b2db")]
  public class SampleEtoPanelCommand : Rhino.Commands.Command
  {
    public SampleEtoPanelCommand()
    {
      Rhino.UI.Panels.RegisterPanel(PlugIn, typeof(Views.SampleEtoPanel), "Sample", Properties.Resources.SampleEtoPanel);
      Instance = this;
    }

    public static SampleEtoPanelCommand Instance
    {
      get;
      private set;
    }

    public override string EnglishName
    {
      get { return "SampleEtoPanel"; }
    }

    protected override Rhino.Commands.Result RunCommand(Rhino.RhinoDoc doc, Rhino.Commands.RunMode mode)
    {
      var panel_id = Views.SampleEtoPanel.PanelId;
      var visible = Rhino.UI.Panels.IsPanelVisible(panel_id);

      var prompt = (visible)
        ? "Sample panel is visible. New value"
        : "Sample panel is hidden. New value";

      var go = new Rhino.Input.Custom.GetOption();
      go.SetCommandPrompt(prompt);
      var hide_index = go.AddOption("Hide");
      var show_index = go.AddOption("Show");
      var toggle_index = go.AddOption("Toggle");
      go.Get();
      if (go.CommandResult() != Rhino.Commands.Result.Success)
        return go.CommandResult();

      var option = go.Option();
      if (null == option)
        return Rhino.Commands.Result.Failure;

      var index = option.Index;
      if (index == hide_index)
      {
        if (visible)
          Rhino.UI.Panels.ClosePanel(panel_id);
      }
      else if (index == show_index)
      {
        if (!visible)
          Rhino.UI.Panels.OpenPanel(panel_id);
      }
      else if (index == toggle_index)
      {
        if (visible)
          Rhino.UI.Panels.ClosePanel(panel_id);
        else
          Rhino.UI.Panels.OpenPanel(panel_id);
      }

      return Rhino.Commands.Result.Success;
    }
  }
}
