using Flux.src.Platform.OpenGL;

namespace Flux.src.Flux.Renderer
{
	public abstract class VertexBuffer
	{
		public abstract void Bind();
		public abstract void Unbind();

		public abstract BufferLayout GetLayout();
		public abstract void SetLayout(BufferLayout layout);

		public static VertexBuffer Create(float[] vertices)
		{
			return new OpenGLVertexBuffer(vertices, OpenTK.Graphics.OpenGL.BufferUsageHint.StaticDraw);
		}
	}
	public abstract class IndexBuffer
	{
		public abstract void Bind();
		public abstract void Unbind();

		public abstract int GetCount();

		public static IndexBuffer Create(uint[] indices) 
		{
			return new OpenGLIndexBuffer(indices, OpenTK.Graphics.OpenGL.BufferUsageHint.StaticDraw);
		}
	}
}
