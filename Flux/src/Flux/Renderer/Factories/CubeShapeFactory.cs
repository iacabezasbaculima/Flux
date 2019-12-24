
namespace Flux.src.Flux.Renderer
{
	public class CubeShapeFactory : IShapeFactory
	{
		public IShape Create()
		{
			return new Platform.OpenGL.OpenGLCube(Platform.OpenGL.OpenGLCube.VAODataType.VERTS_INDICES);
		}
	}
}
