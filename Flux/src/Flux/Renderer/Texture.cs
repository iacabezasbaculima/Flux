using Flux.src.Platform.OpenGL;

namespace Flux.src.Flux.Renderer
{
	public class Texture 
	{
		public virtual void Bind(OpenTK.Graphics.OpenGL4.TextureUnit unit = OpenTK.Graphics.OpenGL4.TextureUnit.Texture0) { }
		public virtual int GetTextureID() { return 0; }
		public virtual int GetWidth() { return 0; }
		public virtual int GetHeight() { return 0; }	
		public virtual void SetTest() { }
	}

	public class Texture2D : Texture
	{
		public static Texture2D Create(string fileName)
		{
			return new OpenGLTexture(fileName);
		}
	}
}
