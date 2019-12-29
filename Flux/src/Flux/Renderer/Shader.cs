using OpenTK;
using OpenTK.Graphics;
using Flux.src.Platform.OpenGL;

namespace Flux.src.Flux.Renderer
{ 
	public abstract class Shader
	{
		public static Shader Create(string name, string vertexFilename, string fragmentFilename)
		{
			return new OpenGLShader(name, vertexFilename, fragmentFilename);
		}
		public abstract void Bind();
		public abstract void Unbind();
		public abstract void SetMVPMatrix(Matrix4 model, Matrix4 view, Matrix4 projection);
		public abstract void SetInt(string name, int val);
		public abstract void SetFloat(string name, float val);
		public abstract void SetMatrix4(string name, Matrix4 m, bool transpose = true);
		public abstract void SetVector3(string name, Vector3 v);
		public abstract void SetVector4(string name, Vector4 v);
		public abstract void SetVector4(string name, Color4 c);
	}
}