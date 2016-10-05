using Rhino.UI.Controls;
using System;

namespace CustomRenderSections
{
  ///<summary>
  /// Base class for all the custom sections
  ///</summary>
  public abstract class CustomSection : EtoCollapsibleSection
  {
    private bool m_bHidden;

    public override bool Hidden
    {
      get
      {
        m_bHidden = false;

        // Plugin uuid
        Guid uuidPlugIn = new Guid("54cc4233-7407-4c76-9422-0b6f01ca802a");

        // Hide the section if it does not belong to the current renderer
        if (uuidPlugIn != Rhino.Render.Utilities.DefaultRenderPlugInId)
        { 
          m_bHidden = true; // Wrong renderer.
        }
        return m_bHidden;
      }
    }
  };
}
