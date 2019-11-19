
namespace Flux.src.Flux.Renderer
{
	public class PyramidShapeFactory : IShapeFactory
	{
		public IShape Create()
		{
			return new Platform.OpenGL.OpenGLPyramid();
		}
	}
}
