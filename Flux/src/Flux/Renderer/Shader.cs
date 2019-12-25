using OpenTK;
using OpenTK.Graphics;
using Flux.src.Platform.OpenGL;

namespace Flux.src.Flux.Renderer
{ 
	public class Shader
	{
		public static Shader Create(string name, string vertexFilename, string fragmentFilename)
		{
			return new OpenGLShader(name, vertexFilename, fragmentFilename);
		}
		public virtual void Bind() { }
		public virtual void Unbind() { }
		public virtual void SetInt(string name, int val) { }
		public virtual void SetFloat(string name, float val) { }
		public virtual void SetMatrix4(string name, Matrix4 m, bool transpose = true) { }
		public virtual void SetVector3(string name, Vector3 v) { }
		public virtual void SetVector4(string name, Vector4 v) { }
		public virtual void SetVector4(string name, Color4 c) { }
	}
}