
namespace Flux.src.Flux.Renderer
{
	public class TriangleShapeFactory : IShapeFactory
	{
		public IShape Create()
		{
			return new Platform.OpenGL.OpenGLTriangle();
		}
	}
}
