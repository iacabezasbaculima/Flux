using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Flux.src.Platform.OpenGL
{
	public class OpenGLIndexBuffer : Flux.Renderer.Buffer
	{
		private readonly int bufferId ;
		private readonly int length;	// number of indices - NOT size in bytes
		internal OpenGLIndexBuffer(uint[] indices, BufferUsageHint hint)
		{
			this.length = indices.Length;
			bufferId = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, bufferId);
			GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, hint);
		}
		public override void Bind()
		{
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, bufferId);
		}
		public override void Unbind()
		{
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
		}
		public override int GetCount() { return length; }

	}
}
