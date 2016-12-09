using Rhino;
using Rhino.Render;
using Rhino.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SampleRdkMaterialAutoUI
{
  ///<summary>
  /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
  /// class. DO NOT create instances of this class yourself. It is the
  /// responsibility of Rhino to create an instance of this class.</para>
  /// <para>To complete plug-in information, please also see all PlugInDescription
  /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
  /// "Show All Files" to see it in the "Solution Explorer" window).</para>
  ///</summary>
  public class SampleRdkMaterialAutoUIPlugIn : Rhino.PlugIns.RenderPlugIn
  {

    //private static Guid m_render_plugin_id = new Guid("3285f192-ecac-40ee-a989-0930756d8f41");
    SampleRenderContentSerializer m_rcs;
    List<SampleRenderContentSerializer> m_rcs_list;

    public SampleRdkMaterialAutoUIPlugIn()
    {
      Instance = this;

      // Create our custom serializer for the material (.sample files)
      m_rcs_list = new List<SampleRenderContentSerializer>();
      m_rcs = new SampleRenderContentSerializer("sample", RenderContentKind.Material, true, true);
      m_rcs_list.Add(m_rcs);
    }

    ///<summary>Gets the only instance of the SampleRdkMaterialAutoUIPlugIn plug-in.</summary>
    public static SampleRdkMaterialAutoUIPlugIn Instance
    {
      get; private set;
    }


    // You can override methods here to change the plug-in behavior on
    // loading and shut down, add options pages to the Rhino _Option command
    // and mantain plug-in wide options in a document.

    protected override IEnumerable<RenderContentSerializer> RenderContentSerializers()
    {
      // Return our serializer to rdk
      IEnumerable<RenderContentSerializer> sel = from a in m_rcs_list select a;
      return sel;
    }

    protected override Result Render(RhinoDoc doc, RunMode mode, bool fastPreview)
    {
      return Result.Success;
    }
  }
}