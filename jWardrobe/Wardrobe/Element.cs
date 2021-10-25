using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wardrobe
{
    public class Element
    {
        public int Size { get; set; }
        private Element(int size)
        {
            Size = size;
        }

        public static Element Build(int size) {
            return new Element(size);
        }

        public override string ToString()
        {
            return Size + " cm";
        }
    }
}
