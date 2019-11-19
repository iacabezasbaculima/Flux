
namespace Flux.src.Flux.Renderer
{
	public class QuadShapeFactory : IShapeFactory
	{
		public IShape Create()
		{
			return new Platform.OpenGL.OpenGLQuad();
		}
	}
}
