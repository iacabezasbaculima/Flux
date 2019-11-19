using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flux.src.Flux.Renderer
{
	public abstract class AbstractShapeFactory
	{
		public abstract IShape Create();
	}
}
