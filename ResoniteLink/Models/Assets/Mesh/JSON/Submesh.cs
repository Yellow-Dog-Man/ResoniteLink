using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    [JsonDerivedType(typeof(PointSubmesh), "points")]
    [JsonDerivedType(typeof(TriangleSubmesh), "triangles")]
    [JsonDerivedType(typeof(TriangleSubmeshFlat), "trianglesFlat")]
    public abstract class Submesh
    {

    }
}
