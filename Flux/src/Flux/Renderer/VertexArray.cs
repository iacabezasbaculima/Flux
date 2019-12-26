using System.Collections.Generic;
using Flux.src.Platform.OpenGL;

namespace Flux.src.Flux.Renderer
{
	public abstract class VertexArray
	{
		public abstract void Bind();
		public abstract void Unbind();
		public abstract void AddVertexBuffer(VertexBuffer vertexBuffer);
		public abstract void SetIndexBuffer(IndexBuffer indexBuffer);

		public abstract List<VertexBuffer> GetVertexBuffers();
		public abstract IndexBuffer GetIndexBuffer();

		public static VertexArray Create()
		{
			return new OpenGLVertexArray();
		}
	}
}
