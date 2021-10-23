using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyBall
{
    class StageElement
    {
        public Block[] block;
        public Obstacle[] obstacle;
        public Special[] special;
        public DashItem[] dashItems;
        public int x;
        public int y;
    }
}
