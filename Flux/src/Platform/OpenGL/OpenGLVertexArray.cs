using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace Flux.src.Platform.OpenGL
{
	public class OpenGLVertexArray
	{
		private int _vaoID;
		private List<OpenGLVertexBuffer> bufferList;
		private OpenGLIndexBuffer indexBuffer;
		public OpenGLVertexArray()
		{
			bufferList = new List<OpenGLVertexBuffer>();
			_vaoID = GL.GenVertexArray();
		}
		public void Bind()	
		{
			GL.BindVertexArray(_vaoID);
		}

		public void Unbind()
		{
			GL.BindVertexArray(0);
		}
		public void AddVertexBuffer(OpenGLVertexBuffer vertexBuffer)
		{
			if (vertexBuffer.GetLayout().GetElements().Count == 0) throw new NotBufferLayoutFound("Vertex buffer has no layout.");
			GL.BindVertexArray(_vaoID);
			vertexBuffer.Bind();
			int index = 0;
			foreach (var item in vertexBuffer.GetLayout())
			{
				GL.EnableVertexAttribArray(index);
				GL.VertexAttribPointer(index,
					item.GetComponentCount(),
					Flux.Renderer.BufferElement.ShaderDataTypeToGLBaseType(item.type),
					item.normalized,
					vertexBuffer.GetLayout().GetStride(),
					item.offset);
				index++;
			}
			bufferList.Add(vertexBuffer);
		}
		public void SetIndexBuffer(OpenGLIndexBuffer indexBuffer)
		{
			GL.BindVertexArray(_vaoID);
			indexBuffer.Bind();
			this.indexBuffer = indexBuffer;
		}
		public List<OpenGLVertexBuffer> GetVertexBuffers() { return bufferList; }
		public OpenGLIndexBuffer GetIndexBuffer() { return indexBuffer; }
	}
	class NotBufferLayoutFound : Exception
	{
		public NotBufferLayoutFound(string msg) : base(msg) { }
	}
}
