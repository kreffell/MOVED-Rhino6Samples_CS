using System.Linq;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;

namespace SampleCsCommands
{
  public class SampleCsTwistedCube : Command
  {
    public override string EnglishName
    {
      get { return "SampleCsTwistedCube"; }
    }

    protected override Result RunCommand(RhinoDoc doc, RunMode mode)
    {
      var brep = MakeTwistedCube();
      if (null == brep)
        return Result.Failure;

      doc.Objects.AddBrep(brep);
      doc.Views.Redraw();

      return Result.Success;
    }

    /// <summary>
    /// This example demonstrates how to construct a Brep
    /// with the topology shown below.
    ///
    ///             H-------e6-------G
    ///            /                /|
    ///           / |              / |
    ///          /  e7            /  e5
    ///         /   |            /   |
    ///        /                e10  |
    ///       /     |          /     |
    ///      e11    E- - e4- -/- - - F
    ///     /                /      /
    ///    /      /         /      /
    ///   D---------e2-----C      e9
    ///   |     /          |     /
    ///   |    e8          |    /
    ///   e3  /            e1  /
    ///   |                |  /
    ///   | /              | /
    ///   |                |/
    ///   A-------e0-------B
    ///
    /// </summary>
    /// <returns>A Brep if successful, null otherwise</returns>
    public static Brep MakeTwistedCube()
    {
      // Define the vertices of the twisted cube
      var points = new Point3d[8];
      points[A] = new Point3d(0.0, 0.0, 0.0);   // point A = geometry for vertex 0
      points[B] = new Point3d(10.0, 0.0, 0.0);  // point B = geometry for vertex 1
      points[C] = new Point3d(10.0, 8.0, -1.0); // point C = geometry for vertex 2
      points[D] = new Point3d(0.0, 6.0, 0.0);   // point D = geometry for vertex 3
      points[E] = new Point3d(1.0, 2.0, 11.0);  // point E = geometry for vertex 4
      points[F] = new Point3d(10.0, 0.0, 12.0); // point F = geometry for vertex 5
      points[G] = new Point3d(10.0, 7.0, 13.0); // point G = geometry for vertex 6
      points[H] = new Point3d(0.0, 6.0, 12.0);  // point H = geometry for vertex 7

      // Create the Brep
      var brep = new Brep();

      // Create eight Brep vertices located at the eight points
      for (var vi = 0; vi < points.Length; vi++)
      {
        // This simple example is exact - for models with
        // non-exact data, set tolerance as explained in
        // definition of BrepVertex.
        brep.Vertices.Add(points[vi], 0.0);
      }

      // Create 3d curve geometry - the orientations are arbitrarily chosen
      // so that the end vertices are in alphabetical order.
      brep.Curves3D.Add(TwistedCubeEdgeCurve(points[A], points[B])); // line AB
      brep.Curves3D.Add(TwistedCubeEdgeCurve(points[B], points[C])); // line BC
      brep.Curves3D.Add(TwistedCubeEdgeCurve(points[C], points[D])); // line CD
      brep.Curves3D.Add(TwistedCubeEdgeCurve(points[A], points[D])); // line AD
      brep.Curves3D.Add(TwistedCubeEdgeCurve(points[E], points[F])); // line EF
      brep.Curves3D.Add(TwistedCubeEdgeCurve(points[F], points[G])); // line FG
      brep.Curves3D.Add(TwistedCubeEdgeCurve(points[G], points[H])); // line GH
      brep.Curves3D.Add(TwistedCubeEdgeCurve(points[E], points[H])); // line EH
      brep.Curves3D.Add(TwistedCubeEdgeCurve(points[A], points[E])); // line AE
      brep.Curves3D.Add(TwistedCubeEdgeCurve(points[B], points[F])); // line BF
      brep.Curves3D.Add(TwistedCubeEdgeCurve(points[C], points[G])); // line CG
      brep.Curves3D.Add(TwistedCubeEdgeCurve(points[D], points[H])); // line DH

      // Create the 12 edges that connect the corners of the cube
      MakeTwistedCubeEdges(brep);

      // Create 3d surface geometry - the orientations are arbitrarily chosen so
      // that some normals point into the cube and others point out of the cube.
      brep.AddSurface(TwistedCubeSideSurface(points[A], points[B], points[C], points[D])); // ABCD
      brep.AddSurface(TwistedCubeSideSurface(points[B], points[C], points[G], points[F])); // BCGF
      brep.AddSurface(TwistedCubeSideSurface(points[C], points[D], points[H], points[G])); // CDHG
      brep.AddSurface(TwistedCubeSideSurface(points[A], points[D], points[H], points[E])); // ADHE
      brep.AddSurface(TwistedCubeSideSurface(points[A], points[B], points[F], points[E])); // ABFE
      brep.AddSurface(TwistedCubeSideSurface(points[E], points[F], points[G], points[H])); // EFGH

      // Create the Brep faces
      MakeTwistedCubeFaces(brep);

#if DEBUG
      for (var fi = 0; fi < brep.Faces.Count; fi++)
        TraverseBrepFace(brep, fi);

      string tlog;
      var rc = brep.IsValidTopology(out tlog);
      if (!rc)
      {
        RhinoApp.WriteLine(tlog);
        return null;
      }

      string glog;
      rc = brep.IsValidGeometry(out glog);
      if (!rc)
      {
        RhinoApp.WriteLine(glog);
        return null;
      }
#endif

      // Validate the results
      if (!brep.IsValid)
      {
        RhinoApp.Write("Twisted cube Brep is not valid.");
        return null;
      }

      return brep;
    }

    // Symbolic vertex index constants to make code more readable
    private static int A = 0;
    private static int B = 1;
    private static int C = 2;
    private static int D = 3;
    private static int E = 4;
    private static int F = 5;
    private static int G = 6;
    private static int H = 7;

    // Symbolic edge index constants to make code more readable
    private static int AB = 0;
    private static int BC = 1;
    private static int CD = 2;
    private static int AD = 3;
    private static int EF = 4;
    private static int FG = 5;
    private static int GH = 6;
    private static int EH = 7;
    private static int AE = 8;
    private static int BF = 9;
    private static int CG = 10;
    private static int DH = 11;

    // Symbolic face index constants to make code more readable
    private static int ABCD = 0;
    private static int BCGF = 1;
    private static int CDHG = 2;
    private static int ADHE = 3;
    private static int ABFE = 4;
    private static int EFGH = 5;

    /// <summary>
    /// Create a 2d trimming curve.
    /// </summary>
    /// <param name="surface">The surface</param>
    /// <param name="side">The side</param>
    /// <returns>A 2d trimming curve is successful, null otherwise</returns>
    private static Curve TwistedCubeTrimmingCurve(Surface surface, int side)
    {
      // A trimming curve is a 2d curve whose image lies in the surface's domain.
      // The "active" portion of the surface is to the left of the trimming curve.
      // An outer trimming loop consists of a simple closed curve running 
      // counter-clockwise around the region it trims.

      var domain_u = surface.Domain(0);
      var domain_v = surface.Domain(1);

      var start = new Point2d();
      var end = new Point2d();

      switch (side)
      {
        case 0: // SW to SE
          start.X = domain_u.Min;
          start.Y = domain_v.Min;
          end.X = domain_u.Max;
          end.Y = domain_v.Min;
          break;
        case 1: // SE to NE
          start.X = domain_u.Max;
          start.Y = domain_v.Min;
          end.X = domain_u.Max;
          end.Y = domain_v.Max;
          break;
        case 2: // NE to NW
          start.X = domain_u.Max;
          start.Y = domain_v.Max;
          end.X = domain_u.Min;
          end.Y = domain_v.Max;
          break;
        case 3: // NW to SW
          start.X = domain_u.Min;
          start.Y = domain_v.Max;
          end.X = domain_u.Min;
          end.Y = domain_v.Min;
          break;
        default:
          return null;
      }

      var curve = new LineCurve(start, end) { Domain = new Interval(0.0, 1.0) };

      return curve;
    }

    /// <summary>
    /// Creates a 3d edge curve
    /// </summary>
    /// <param name="start">The start point</param>
    /// <param name="end">The end point</param>
    /// <returns>The 3d edge curve if successful, null otherwise</returns>
    private static Curve TwistedCubeEdgeCurve(Point3d start, Point3d end)
    {
      // Creates a 3d line segment to be used as a 3d curve in a Brep
      var curve = new LineCurve(start, end) { Domain = new Interval(0.0, 1.0) };
      return curve;
    }

    /// <summary>
    /// Creates a Brep surface
    /// </summary>
    /// <param name="sw">The first corner point</param>
    /// <param name="se">The second corner point</param>
    /// <param name="ne">The third corner point</param>
    /// <param name="nw">The forth corner point</param>
    /// <returns>The surface if successful, null otherwise</returns>
    private static Surface TwistedCubeSideSurface(Point3d sw, Point3d se, Point3d ne, Point3d nw)
    {
      var nurb = NurbsSurface.Create(
        3,     // dimension
        false, // not rational
        2,     // "u" order
        2,     // "v" order
        2,     // number of control vertices in "u" dir
        2      // number of control vertices in "v" dir
        );

      // Corner CVs in counter-clockwise order starting in the south west
      nurb.Points.SetControlPoint(0, 0, sw);
      nurb.Points.SetControlPoint(1, 0, se);
      nurb.Points.SetControlPoint(1, 1, ne);
      nurb.Points.SetControlPoint(0, 1, nw);

      // "u" knots
      nurb.KnotsU[0] = 0.0;
      nurb.KnotsU[1] = 1.0;

      // "v" knots
      nurb.KnotsV[0] = 0.0;
      nurb.KnotsV[1] = 1.0;

      return nurb;
    }

    /// <summary>
    /// Makes an edge of the Brep
    /// </summary>
    /// <param name="brep">The Brep</param>
    /// <param name="vi0">Index of the start vertex</param>
    /// <param name="vi1">Index of the end vertex</param>
    /// <param name="c3i">Index of the 3d curve</param>
    public static void MakeTwistedCubeEdge(Brep brep, int vi0, int vi1, int c3i)
    {
      var start_vertex = brep.Vertices[vi0];
      var end_vertex = brep.Vertices[vi1];

      // This simple example is exact - for models with
      // non-exact data, set tolerance as explained in
      // definition of BrepEdge.
      brep.Edges.Add(start_vertex, end_vertex, c3i, 0.0);
    }

    /// <summary>
    /// Makes the edges of the Brep
    /// </summary>
    /// <param name="brep">The Brep</param>
    private static void MakeTwistedCubeEdges(Brep brep)
    {
      // In this simple example, the edge indices exactly match the 3d
      // curve indices. In general, the correspondence between edge and
      // curve indices can be arbitrary. It is permitted for multiple
      // edges to use different portions of the same 3d curve.  The 
      // orientation of the edge always agrees with the natural 
      // parametric orientation of the curve.

      // Edge that runs start A to B
      MakeTwistedCubeEdge(brep, A, B, AB);

      // Edge that runs start B to C
      MakeTwistedCubeEdge(brep, B, C, BC);

      // Edge that runs start C to D
      MakeTwistedCubeEdge(brep, C, D, CD);

      // Edge that runs start A to D
      MakeTwistedCubeEdge(brep, A, D, AD);

      // Edge that runs start E to F
      MakeTwistedCubeEdge(brep, E, F, EF);

      // Edge that runs start F to G
      MakeTwistedCubeEdge(brep, F, G, FG);

      // Edge that runs start G to H
      MakeTwistedCubeEdge(brep, G, H, GH);

      // Edge that runs start E to H
      MakeTwistedCubeEdge(brep, E, H, EH);

      // Edge that runs start A to E
      MakeTwistedCubeEdge(brep, A, E, AE);

      // Edge that runs start B to F
      MakeTwistedCubeEdge(brep, B, F, BF);

      // Edge that runs start C to G
      MakeTwistedCubeEdge(brep, C, G, CG);

      // Edge that runs start D to H
      MakeTwistedCubeEdge(brep, D, H, DH);
    }

    /// <summary>
    /// Creates a trimming loop
    /// </summary>
    /// <param name="brep">The Brep</param>
    /// <param name="face">The face the loop is on</param>
    /// <param name="eSi">Index of edge on south side of surface</param>
    /// <param name="eSDir">Orientation of edge with respect to surface trim</param>
    /// <param name="eEi">Index of edge on east side of surface</param>
    /// <param name="eEDir">Orientation of edge with respect to surface trim</param>
    /// <param name="eNi">Index of edge on north side of surface</param>
    /// <param name="eNDir">Orientation of edge with respect to surface trim</param>
    /// <param name="eWi">Index of edge on west side of surface</param>
    /// <param name="eWDir">Orientation of edge with respect to surface trim</param>
    /// <returns>The index of the added loop</returns>
    private static int MakeTwistedCubeTrimmingLoop(
        Brep brep,
        BrepFace face,
        int eSi,
        int eSDir,
        int eEi,
        int eEDir,
        int eNi,
        int eNDir,
        int eWi,
        int eWDir
        )
    {
      var surface = brep.Surfaces[face.SurfaceIndex];
      var loop = brep.Loops.Add(BrepLoopType.Outer, face);

      // Create trimming curves running counter-clockwise around the surface's domain.
      // Start at the south side.
      for (var side = 0; side < 4; side++)
      {
        // side: 0=south, 1=east, 2=north, 3=west
        var curve = TwistedCubeTrimmingCurve(surface, side);

        // Add trimming curve to brep trim curves array
        var c2i = brep.Curves2D.Add(curve);

        var ei = 0;
        var reverse = false;
        var iso = IsoStatus.None;

        switch (side)
        {
          case 0: // South
            ei = eSi;
            reverse = (eSDir == -1);
            iso = IsoStatus.South;
            break;
          case 1: // East
            ei = eEi;
            reverse = (eEDir == -1);
            iso = IsoStatus.East;
            break;
          case 2: // North
            ei = eNi;
            reverse = (eNDir == -1);
            iso = IsoStatus.North;
            break;
          case 3: // West
            ei = eWi;
            reverse = (eWDir == -1);
            iso = IsoStatus.West;
            break;
        }

        var trim = brep.Trims.Add(brep.Edges[ei], reverse, loop, c2i);
        trim.IsoStatus = iso;

        // This brep is closed, so all trims have mates
        trim.TrimType = BrepTrimType.Mated; 

        // This simple example is exact - for models with
        //non-exact data, set tolerance as explained in
        // definition of BrepTrim.
        trim.SetTolerances(0.0, 0.0);
      }

      return loop.LoopIndex;
    }

    /// <summary>
    /// Makes a Brep face
    /// </summary>
    /// <param name="brep">The Brep</param>
    /// <param name="si">Index of 3d surface</param>
    /// <param name="sDir">Orientation of surface with respect to Brep</param>
    /// <param name="eSi">Index of edge on south side of surface</param>
    /// <param name="eSDir">Orientation of edge with respect to surface trim</param>
    /// <param name="eEi">Index of edge on east side of surface</param>
    /// <param name="eEDir">Orientation of edge with respect to surface trim</param>
    /// <param name="eNi">Index of edge on north side of surface</param>
    /// <param name="eNDir">Orientation of edge with respect to surface trim</param>
    /// <param name="eWi">Index of edge on west side of surface</param>
    /// <param name="eWDir">Orientation of edge with respect to surface trim</param>
    private static void MakeTwistedCubeFace(
        Brep brep,
        int si,
        int sDir,
        int eSi,
        int eSDir,
        int eEi,
        int eEDir,
        int eNi,
        int eNDir,
        int eWi,
        int eWDir
        )
    {
      // Add new face to brep
      var face = brep.Faces.Add(si);

      // Create loop and trims for the face
      MakeTwistedCubeTrimmingLoop(
        brep, face,
        eSi, eSDir,
        eEi, eEDir,
        eNi, eNDir,
        eWi, eWDir
        );

      // Set face direction relative to surface direction
      face.OrientationIsReversed = (sDir == -1);
    }

    /// <summary>
    /// Adds faces to the Brep
    /// </summary>
    /// <param name="brep">The Brep</param>
    private static void MakeTwistedCubeFaces(Brep brep)
    {
      MakeTwistedCubeFace(
        brep,
        ABCD,       // Index of surface ABCD
        +1,         // Orientation of surface with respect to brep
        AB, +1,     // South side edge and its orientation with respect to the trimming curve (AB)
        BC, +1,     // East side edge and its orientation with respect to the trimming curve (BC)
        CD, +1,     // North side edge and its orientation with respect to the trimming curve (CD)
        AD, -1      // West side edge and its orientation with respect to the trimming curve (AD)
        );

      MakeTwistedCubeFace(
        brep,
        BCGF,       // Index of surface BCGF
        -1,         // Orientation of surface with respect to brep
        BC, +1,     // South side edge and its orientation with respect to the trimming curve (BC)
        CG, +1,     // East side edge and its orientation with respect to the trimming curve (CG)
        FG, -1,     // North side edge and its orientation with respect to the trimming curve (FG)
        BF, -1      // West side edge and its orientation with respect to the trimming curve (BF)
        );

      MakeTwistedCubeFace(
        brep,
        CDHG,       // Index of surface CDHG
        -1,         // Orientation of surface with respect to brep
        CD, +1,     // South side edge and its orientation with respect to the trimming curve (CD)
        DH, +1,     // East side edge and its orientation with respect to the trimming curve (DH)
        GH, -1,     // North side edge and its orientation with respect to the trimming curve (GH)
        CG, -1      // West side edge and its orientation with respect to the trimming curve (CG)
        );

      MakeTwistedCubeFace(
        brep,
        ADHE,       // Index of surface ADHE
        +1,         // Orientation of surface with respect to brep
        AD, +1,     // South side edge and its orientation with respect to the trimming curve (AD)
        DH, +1,     // East side edge and its orientation with respect to the trimming curve (DH)
        EH, -1,     // North side edge and its orientation with respect to the trimming curve (EH)
        AE, -1      // West side edge and its orientation with respect to the trimming curve (AE)
        );

      MakeTwistedCubeFace(
        brep,
        ABFE,       // Index of surface ABFE
        -1,         // Orientation of surface with respect to brep
        AB, +1,     // South side edge and its orientation with respect to the trimming curve (AB)
        BF, +1,     // South side edge and its orientation with respect to the trimming curve (BF)
        EF, -1,     // South side edge and its orientation with respect to the trimming curve (EF)
        AE, -1      // South side edge and its orientation with respect to the trimming curve (AE)
        );

      MakeTwistedCubeFace(
        brep,
        EFGH,       // Index of surface EFGH
        -1,         // Orientation of surface with respect to brep
        EF, +1,     // South side edge and its orientation with respect to the trimming curve (EF)
        FG, +1,     // South side edge and its orientation with respect to the trimming curve (FG)
        GH, +1,     // South side edge and its orientation with respect to the trimming curve (GH)
        EH, -1      // South side edge and its orientation with respect to the trimming curve (EH)
        );
    }

    /// <summary>
    /// Traverse a Brep face
    /// </summary>
    /// <param name="brep">The Brep</param>
    /// <param name="fi">The index of the face</param>
    private static void TraverseBrepFace(Brep brep, int fi)
    {
      if (null == brep)
      {
        RhinoApp.WriteLine("ERROR: brep is null");
        return;
      }

      if (fi < 0 || fi >= brep.Faces.Count)
      {
        RhinoApp.WriteLine("Invalid face index");
        return;
      }

      var face = brep.Faces[fi];

      // Each face has an underlying untrimmed surface
      var si = face.SurfaceIndex;
      if (si < 0 || si >= brep.Surfaces.Count)
        RhinoApp.WriteLine("ERROR: invalid brep.Faces[{0}].SurfaceIndex", fi);
      else
      {
        var surface = brep.Surfaces[si];
        if (null == surface)
          RhinoApp.WriteLine("ERROR: invalid brep.Surfaces[{0}] is null", si);
      }

      // The face is trimmed with one or more trimming loops.
      //
      // All the 2d trimming curves are oriented so that the
      // active region of the trimmed surface lies to the left
      // of the 2d trimming curve.  
      //
      // If face.m_bRev is TRUE, the orientations of the face in
      // the b-rep is opposited the natural parameteric orientation
      // of the surface.

      // Number of trimming loops on this face (>=1)
      var loop_count = face.Loops.Count;

      for (var li = 0; li < loop_count; li++)
      {
        var loop = face.Loops[li];

        // Number of trimming edges in this loop
        var loop_trim_count = loop.Trims.Count();

        for (var lti = 0; lti < loop_trim_count; lti++)
        {
          var trim = loop.Trims[lti];

          // 2d trimming information

          // Each trim has a 2d parameter space curve
          var c2i = trim.TrimCurveIndex;
          if (c2i < 0 || c2i >= brep.Curves2D.Count)
            RhinoApp.WriteLine("ERROR: invalid brep.Trims[{0}].TrimCurveIndex", c2i);
          else
          {
            var crv2d = brep.Curves2D[c2i];
            if (null == crv2d)
              RhinoApp.WriteLine("ERROR: invalid brep.Curves2D[{0}] is null", c2i);
          }

          // Topology and 3d geometry information

          // Trim starts at vertex0 and ends at vertex1. When the trim
          // is a loop or on a singular surface side, vertex0 and vertex1
          // will be equal.
          //
          // var vertex0 = trim.StartVertex;
          // var vertex1 = trim.EndVertex;
          //
          // The vertexX.EdgeIndices array contains the brep.Edges indices of
          // the edges that begin or end at vertexX.

          var edge = trim.Edge;
          if (null == edge)
          {
            // This trim lies on a portion of a singular surface side.
            // The vertex indices are still valid and will be equal.
          }
          else
          {
            // If trim.IsReversed() is false, the orientations of the 3d edge
            // and the 3d curve obtained by composing the surface and 2d
            // curve agree.
            //
            // If trim.IsReversed() is true, the orientations of the 3d edge
            // and the 3d curve obtained by composing the surface and 2d
            // curve are opposite.

            var c3i = edge.EdgeCurveIndex;
            if (c3i < 0 || c3i >= brep.Curves3D.Count)
              RhinoApp.WriteLine("ERROR: invalid brep.Edges[{0}].EdgeCurveIndex", c3i);
            else
            {
              var crv3d = brep.Edges[c3i];
              if (null == crv3d)
                RhinoApp.WriteLine("ERROR: invalid brep.Curves3D[{0}] is null", c3i);
            }

            // The edge.TrimIndices array contains the brep.Trims indices
            // for the other trims that are joined to this edge.
          }
        }
      }
    }
  }
}
