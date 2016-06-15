using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

namespace SampleCsEventWatcher
{
  public class SampleCsEventWatcherCommand : Command
  {
    public override string EnglishName
    {
      get { return "SampleCsEventWatcher"; }
    }

    protected override Result RunCommand(RhinoDoc doc, RunMode mode)
    {
      var enabled = SampleCsEventHandlers.Instance.IsEnabled;
      var prompt = enabled ? "Event watcher is enabled. New value" : "Event watcher is disabled. New value";

      var go = new GetOption();
      go.SetCommandPrompt(prompt);
      go.AcceptNothing(true);

      var d_option = go.AddOption("Disable");
      var e_option = go.AddOption("Enable");
      var t_option = go.AddOption("Toggle");

      var res = go.Get();
      if (res == GetResult.Nothing)
        return Result.Success;
      if (res != GetResult.Option)
        return Result.Cancel;

      var option = go.Option();
      if (null == option)
        return Result.Failure;

      if (d_option == option.Index)
      {
        if (enabled)
          SampleCsEventHandlers.Instance.Enable(false);
      }
      else if (e_option == option.Index)
      {
        if (!enabled)
          SampleCsEventHandlers.Instance.Enable(true);
      }
      else if (t_option == option.Index)
      {
        SampleCsEventHandlers.Instance.Enable(!enabled);
      }

      return Result.Success;
    }
  }
}
