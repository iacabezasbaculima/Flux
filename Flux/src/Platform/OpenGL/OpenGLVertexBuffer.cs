using Flux.src.Flux.Renderer;
using OpenTK.Graphics.OpenGL;

namespace Flux.src.Platform.OpenGL
{
	public class OpenGLVertexBuffer : Flux.Renderer.Buffer
	{
		public readonly int bufferId;
		private BufferLayout layout;

		internal OpenGLVertexBuffer(float[] vertices, int length, BufferUsageHint hint)
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
			return this.layout;
		}
		private void Create(float[] vertices, BufferUsageHint hint)
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, bufferId);
			GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, hint);
		}
		public void UpdateBuffer(float[] data)
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, bufferId);
			GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.DynamicDraw);
		}
		
	}
}
