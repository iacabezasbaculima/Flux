using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace Flux.src.Platform.OpenGL
{
	public class OpenGLVertexArray
	{
		private int rendererId;
		private List<OpenGLVertexBuffer> bufferList;
		private OpenGLIndexBuffer indexBuffer;
		public OpenGLVertexArray()
		{
			bufferList = new List<OpenGLVertexBuffer>();
			rendererId = GL.GenVertexArray();
		}
		public void Bind()	
		{
			GL.BindVertexArray(rendererId);
		}

		public void Unbind()
		{
			GL.BindVertexArray(0);
		}
		public void AddVertexBuffer(OpenGLVertexBuffer vertexBuffer)
		{
			if (vertexBuffer.GetLayout().GetElements().Count == 0) throw new NotBufferLayoutFound("Vertex buffer has no layout.");
			GL.BindVertexArray(rendererId);
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
			GL.BindVertexArray(rendererId);
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
