using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface ModelAPI
    {
        public void DrawBalls(int count);
        public void SetCanvasSize(int width, int height);
    }
}
