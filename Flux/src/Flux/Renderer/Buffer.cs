namespace Flux.src.Flux.Renderer
{
	public class Buffer
	{
		public virtual void Bind() { }
		public virtual void Unbind() { }
		public virtual void SetLayout(BufferLayout layout) { }
		public virtual BufferLayout GetLayout() { return null; }
		public virtual int GetCount() { return 0; }
	}
}
