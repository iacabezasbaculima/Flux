using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using Flux.src.Flux.Renderer;

namespace Flux.src.Platform.OpenGL
{
	public class OpenGLVertexArray : VertexArray
	{
		private int _vaoID;
		private int _vertexBufferIndex = 0;
		private List<VertexBuffer> bufferList;
		private IndexBuffer indexBuffer;
		public OpenGLVertexArray()
		{
			bufferList = new List<VertexBuffer>();
			_vaoID = GL.GenVertexArray();
		}
		public override void Bind()	
		{
			GL.BindVertexArray(_vaoID);
		}

		public override void Unbind()
		{
			GL.BindVertexArray(0);
		}
		public override void AddVertexBuffer(VertexBuffer vertexBuffer)
		{
			if (vertexBuffer.GetLayout().GetElements().Count == 0) throw new NotBufferLayoutFound("Vertex buffer has no layout.");
			GL.BindVertexArray(_vaoID);
			vertexBuffer.Bind();
			//int index = 0;
			foreach (var item in vertexBuffer.GetLayout())
			{
				GL.EnableVertexAttribArray(_vertexBufferIndex);
				GL.VertexAttribPointer(_vertexBufferIndex,
					item.GetComponentCount(),
					BufferElement.ShaderDataTypeToGLBaseType(item.type),
					item.normalized,
					vertexBuffer.GetLayout().GetStride(),
					item.offset);

				_vertexBufferIndex++;
			}
			bufferList.Add(vertexBuffer);
		}
		public override void SetIndexBuffer(IndexBuffer indexBuffer)
		{
			GL.BindVertexArray(_vaoID);
			indexBuffer.Bind();
			this.indexBuffer = indexBuffer;
		}
		public override List<VertexBuffer> GetVertexBuffers() { return bufferList; }
		public override IndexBuffer GetIndexBuffer() { return indexBuffer; }
	}
	class NotBufferLayoutFound : Exception
	{
		public NotBufferLayoutFound(string msg) : base(msg) { }
	}
}
