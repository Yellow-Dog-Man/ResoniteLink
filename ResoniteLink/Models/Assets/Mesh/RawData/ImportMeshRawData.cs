using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Imports a mesh asset from raw mesh data.
    /// This is recommended method to import meshes, as it's a lot more efficient, but can be more difficult to work with.
    /// To use this, first configure all the structure metadata (vertex counts, submeshes, blendshapes and so on)
    /// and then call method to allocate the raw data.
    /// Aftewards you'll be able to access the raw data buffers and fill them with actual mesh data.
    /// </summary>
    public class ImportMeshRawData : BinaryPayloadMessage
    {
        /// <summary>
        /// Number of vertices in this mesh.
        /// </summary>
        [JsonPropertyName("vertexCount")]
        public int VertexCount { get; set; }

        /// <summary>
        /// Do vertices have normals?
        /// </summary>
        [JsonPropertyName("hasNormals")]
        public bool HasNormals { get; set; }

        /// <summary>
        /// Do vertices have tangents?
        /// </summary>
        [JsonPropertyName("hasTangents")]
        public bool HasTangents { get; set; }

        /// <summary>
        /// Do vertices have colors?
        /// </summary>
        [JsonPropertyName("hasColors")]
        public bool HasColors { get; set; }

        /// <summary>
        /// How many bone weights does each vertex have.
        /// If some vertices have fewer bone weights, use weight of 0 for remainder bindings
        /// </summary>
        [JsonPropertyName("boneWeightCount")]
        public int BoneWeightCount { get; set; }

        /// <summary>
        /// Configuration of UV channels for this mesh.
        /// Each entry represents one UV channel of the mesh.
        /// Number indicates number of UV dimensions. This must be between 2 and 4 (inclusive)
        /// </summary>
        [JsonPropertyName("uvChannelDimensions")]
        public List<int> UV_Channel_Dimensions { get; set; }

        /// <summary>
        /// Submeshes that form this mesh. Meshes will typically have at least one submesh.
        /// </summary>
        [JsonPropertyName("submeshes")]
        public List<SubmeshRawData> Submeshes { get; set; }

        /// <summary>
        /// Blendshapes of this mesh.
        /// These allow modifying the vertex positions, normals & tangents for animations such as facial expressions.
        /// </summary>
        [JsonPropertyName("blendshapes")]
        public List<BlendShapeRawData> BlendShapes { get; set; }

        /// <summary>
        /// Bones of the mesh when data represents a skinned mesh.
        /// These will be referred to by their index from vertex data.
        /// </summary>
        [JsonPropertyName("bones")]
        public List<Bone> Bones { get; set; }

        [JsonIgnore]
        public Span<float3> Positions => _positions.Access(RawBinaryPayload);
        [JsonIgnore]
        public Span<float3> Normals => _normals.Access(RawBinaryPayload);
        [JsonIgnore]
        public Span<float4> Tangents => _tangents.Access(RawBinaryPayload);
        [JsonIgnore]
        public Span<color> Colors => _colors.Access(RawBinaryPayload);
        [JsonIgnore]
        public Span<BoneWeight> BoneWeights => _boneWeights.Access(RawBinaryPayload);

        public Span<float2> AccessUV_2D(int index)
        {
            if (UV_Channel_Dimensions[index] != 2)
                throw new InvalidOperationException($"UV channel {index} is not 2D");

            return MemoryMarshal.Cast<float, float2>(_uvs[index].Access(RawBinaryPayload));
        }

        public Span<float3> AccessUV_3D(int index)
        {
            if (UV_Channel_Dimensions[index] != 3)
                throw new InvalidOperationException($"UV channel {index} is not 3D");

            return MemoryMarshal.Cast<float, float3>(_uvs[index].Access(RawBinaryPayload));
        }

        public Span<float4> AccessUV_4D(int index)
        {
            if (UV_Channel_Dimensions[index] != 4)
                throw new InvalidOperationException($"UV channel {index} is not 4D");

            return MemoryMarshal.Cast<float, float4>(_uvs[index].Access(RawBinaryPayload));
        }

        BufferSegment<float3> _positions;
        BufferSegment<float3> _normals;
        BufferSegment<float4> _tangents;
        BufferSegment<color> _colors;

        BufferSegment<BoneWeight> _boneWeights;

        List<BufferSegment<float>> _uvs;

        public void AllocateBuffer()
        {
            if (RawBinaryPayload != null)
                throw new InvalidOperationException("Buffer has already been allocated");

            var size = ComputeBufferOffsets();

            RawBinaryPayload = new byte[size];

            AssignBuffers();
        }

        public void MapBuffer()
        {
            var size = ComputeBufferOffsets();

            if (size != RawBinaryPayload.Length)
                throw new DataException("Buffer size mismatch");

            AssignBuffers();
        }

        int ComputeBufferOffsets()
        {
            int offset = 0;

            _positions = BufferSegment<float3>.AllocateBuffer(VertexCount, ref offset);
            _normals = BufferSegment<float3>.AllocateBuffer(HasNormals ? VertexCount : 0, ref offset);
            _tangents = BufferSegment<float4>.AllocateBuffer(HasTangents ? VertexCount : 0, ref offset);
            _colors = BufferSegment<color>.AllocateBuffer(HasColors ? VertexCount : 0, ref offset);

            _uvs = new List<BufferSegment<float>>();

            if (UV_Channel_Dimensions != null)
                foreach (var uvDimensions in UV_Channel_Dimensions)
                    _uvs.Add(BufferSegment<float>.AllocateBuffer(VertexCount * uvDimensions, ref offset));

            _boneWeights = BufferSegment<BoneWeight>.AllocateBuffer(VertexCount * BoneWeightCount, ref offset);

            if(Submeshes != null)
                foreach (var submesh in Submeshes)
                    submesh.ComputeBufferOffsets(ref offset);

            if(BlendShapes != null)
                foreach(var blendshape in BlendShapes)
                    blendshape.ComputeBufferOffsets(this, ref offset);

            return offset;
        }

        void AssignBuffers()
        {
            if(Submeshes != null)
                foreach (var submesh in Submeshes)
                    submesh.AssignBuffer(RawBinaryPayload);

            if(BlendShapes != null)
                foreach(var blendshape in BlendShapes)
                    blendshape.AssignBuffer(RawBinaryPayload);
        }
    }
}