using Flux.src.Platform.OpenGL;

namespace Flux.src.Flux.Renderer
{
	public abstract class Texture 
	{
		public virtual void Bind(int unit = 0) { }
		public virtual int GetTextureID() { return -1; }
		public virtual int GetWidth() { return -1; }
		public virtual int GetHeight() { return -1; }	
	}

	public class Texture2D : Texture
	{
		public static Texture2D Create(string fileName)
		{
			return new OpenGLTexture(fileName);
		}
	}
}
