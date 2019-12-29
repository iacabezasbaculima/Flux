using Flux.src.Flux.Renderer;
using OpenTK.Graphics.OpenGL;

namespace Flux.src.Platform.OpenGL
{
	public class OpenGLVertexBuffer : VertexBuffer
	{
		public readonly int bufferId;
		private BufferLayout layout;

		internal OpenGLVertexBuffer(float[] vertices, BufferUsageHint hint)
		{
			bufferId = GL.GenBuffer();
			Create(vertices, hint);
		}
		public override void Bind()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, bufferId);
		}
		public override void Unbind()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		}
		public override void SetLayout(BufferLayout layout)
		{
			this.layout = layout;
		}
		public override BufferLayout GetLayout()
		{
			return layout;
		}
		private void Create(float[] vertices, BufferUsageHint hint)
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, bufferId);
			GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, hint);
		}
	}
}
