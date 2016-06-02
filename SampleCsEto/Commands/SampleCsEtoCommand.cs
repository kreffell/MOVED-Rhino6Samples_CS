namespace SampleCsEto.Commands
{
  [System.Runtime.InteropServices.Guid("56d9bddf-d9b3-4f5c-8363-6c3843f09322")]
  public class SampleCsEtoCommand : Rhino.Commands.Command
  {
    public override string EnglishName
    {
      get { return "SampleCsEto"; }
    }

    protected override Rhino.Commands.Result RunCommand(Rhino.RhinoDoc doc, Rhino.Commands.RunMode mode)
    {
      Rhino.RhinoApp.WriteLine("{0} plug-in loaded.", SampleCsEtoPlugIn.Instance.Name);
      return Rhino.Commands.Result.Success;
    }
  }
}
