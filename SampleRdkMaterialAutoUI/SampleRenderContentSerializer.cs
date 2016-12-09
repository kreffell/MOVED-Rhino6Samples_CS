using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Render;
using Rhino.Render.Fields;

namespace SampleRdkMaterialAutoUI
{
  // This class is our custom serializer for .sample material files.
  // The file extension .sample and it is able to load and save .sample
  // files. The sample project saves and loads only boolean fields.
  class SampleRenderContentSerializer : RenderContentSerializer
  {
    public SampleRenderContentSerializer(string fileExtension, RenderContentKind contentKind, bool canRead, bool canWrite) : base(fileExtension, contentKind, canRead, canWrite)
    {
    }

    public override string EnglishDescription
    {
      get
      {
        return "Sample Automatic UI Material";
      }
    }

    // 
    public override RenderContent Read(string pathToFile)
    {
      // Create new Sample Material
      SampleRdkMaterial material = new SampleRdkMaterial();
      string line;

      // Read values from file and set values
      System.IO.StreamReader file = new System.IO.StreamReader(pathToFile);
      while ((line = file.ReadLine()) != null)
      {
        string[] key_and_value = line.Split('=');
        if (key_and_value.Length == 2)
        {
          string key = key_and_value[0];
          string key_value = key_and_value[1];
          bool b_value = false;
          Boolean.TryParse(key_value, out b_value);

          material.Fields.Set(key, b_value);
        }
      }

      // Return material to rdk
      return material;
    }

    // Write sample material to .sample file
    public override bool Write(string pathToFile, RenderContent renderContent, CreatePreviewEventArgs previewArgs)
    {
      List<string> lines = new List<string>();

      foreach (Field field in renderContent.Fields)
      {
        Type type = field.GetType();

        // Bool fields
        if (type == typeof(BoolField))
        {
          string key = field.Key;
          bool bool_value = false;
          renderContent.Fields.TryGetValue(key, out bool_value);
          string value = bool_value.ToString();

          lines.Add(key + "=" + value);
        }
      }

      // Write all lines to .sample file
      using (System.IO.StreamWriter file =
      new System.IO.StreamWriter(pathToFile))
      {
        foreach (string line in lines)
        {
          file.WriteLine(line);
        }
      }
      return true;
    }
  }
}
