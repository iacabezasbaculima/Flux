using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using OpenTK.Graphics.OpenGL;

namespace Flux.src.Flux.Renderer
{
	public class BufferLayout : IEnumerable<BufferElement>
	{
		private List<BufferElement> elements = new List<BufferElement>();
		private int stride;
		public BufferLayout ()
		{
			elements = new List<BufferElement>();
			//CalculateOffsetsAndStride();

		}
		public int GetStride() { return stride; }
		public void CalculateOffsetsAndStride()

		{
			int offset = 0;
			stride = 0;
			BufferElement temp;
			for (int i = 0; i < elements.Count; i++)
			{
				temp = elements[i];
				temp.offset = offset;
				offset += temp.size;
				stride += temp.size;
				elements.RemoveAt(i);
				elements.Insert(i, temp);
			}
			
		}
		public void Add(ShaderDataType type, string name)
		{
			elements.Add(new BufferElement(type, name));
		}

		public IEnumerator<BufferElement> GetEnumerator()
		{
			return elements.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		public List<BufferElement> GetElements() { return elements;}
	}
	/// <summary>
	/// A struct that represents the type of data in a vertex buffer object
	/// Eg: VBO can have: position, color, texture coords. These are buffer elements
	/// </summary>
	public struct BufferElement
	{
		public ShaderDataType type;
		public string name; // layout name declared in shader
		public int offset;  // offset between position and color data for example
		public int size;
		public bool normalized;
		public BufferElement(ShaderDataType type, string name, bool normalized = false)
		{
			size = ShaderDataTypeSize(type);
			this.type = type;
			offset = 0;
			this.name = name;
			this.normalized = normalized;

		}
		public static VertexAttribPointerType ShaderDataTypeToGLBaseType(ShaderDataType type)
		{
			switch (type)
			{
				case ShaderDataType.Float1: return VertexAttribPointerType.Float;
				case ShaderDataType.Float2: return VertexAttribPointerType.Float;
				case ShaderDataType.Float3: return VertexAttribPointerType.Float;
				case ShaderDataType.Float4: return VertexAttribPointerType.Float;
				case ShaderDataType.Mat3:	return VertexAttribPointerType.Int;
				case ShaderDataType.Mat4:	return VertexAttribPointerType.Int;
				case ShaderDataType.Int1:	return VertexAttribPointerType.Int;
				case ShaderDataType.Int2:	return VertexAttribPointerType.Int;
				case ShaderDataType.Int3:	return VertexAttribPointerType.Int;
				case ShaderDataType.Int4:	return VertexAttribPointerType.Int;
				case ShaderDataType.Bool:	return VertexAttribPointerType.Int;
			}
			return 0;
			throw new UnknownShaderDataType("An unknown type has been given.");
		}
		static int ShaderDataTypeSize(ShaderDataType type)
		{
			switch (type)
			{
				case ShaderDataType.Float1: return 4;		// 4 bytes
				case ShaderDataType.Float2: return 4 * 2;	// 8 bytes
				case ShaderDataType.Float3: return 4 * 3;   // 12 bytes
				case ShaderDataType.Float4: return 4 * 4;   // 16 bytes
				case ShaderDataType.Mat3:	return 4 * 3 * 3;	// ...
				case ShaderDataType.Mat4:	return 4 * 4 * 4;
				case ShaderDataType.Int1:	return 4;
				case ShaderDataType.Int2:	return 4 * 2;
				case ShaderDataType.Int3:	return 4 * 3;
				case ShaderDataType.Int4:	return 4 * 4;
				case ShaderDataType.Bool:	return 4;
			}
			return 0;
			throw new UnknownShaderDataType("An unknown type has been given.");
		}
		public int GetComponentCount()
		{
			switch (this.type)
			{
				case ShaderDataType.Float1: return 1;
				case ShaderDataType.Float2: return 2;
				case ShaderDataType.Float3: return 3;
				case ShaderDataType.Float4: return 4;
				case ShaderDataType.Mat3:	return 3 * 3;
				case ShaderDataType.Mat4:	return 4 * 4;
				case ShaderDataType.Int1:	return 1;
				case ShaderDataType.Int2:	return 2;
				case ShaderDataType.Int3:	return 3;
				case ShaderDataType.Int4:	return 4;
				case ShaderDataType.Bool:	return 1;
			}
			return 0;
			throw new UnknownShaderDataType("An unknown type has been given.");
		}
	}
	public enum ShaderDataType
	{
		None = 0, Float1, Float2, Float3, Float4, Mat3, Mat4, Int1, Int2, Int3, Int4, Bool
	}
	public class UnknownShaderDataType : Exception
	{
		public UnknownShaderDataType(string msg) : base(msg) { }
	}
}
