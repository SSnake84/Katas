using System.Collections.Generic;
using System.Linq;

namespace Wardrobe
{
    public class Wardrobe
    {
        public int TotalLength { get; set; }
        public List<Element> Elements { get; set; }

        public Wardrobe(int totalLength)
        {
            TotalLength = totalLength;
            Elements = new List<Element>();
        }
        public Wardrobe(int totalLength, List<Element> elements)
        {
            TotalLength = totalLength;
            Elements = elements;
        }

        public List<List<Element>> GetResults()
        {
            Elements.Sort((a,b) => a.Size.CompareTo(b.Size));
            var dic = new Dictionary<int, int>();
            for (var i = 0; i < Elements.Count; i++)
                dic.Add(Elements[i].Size, i);

            var stack = new Stack<Element>();

            var ret = new List<List<Element>>();

            var elIndex = 0;
            var sum = 0;
            while (elIndex < Elements.Count)
            {
                while (sum < TotalLength)
                    stack.Push(Element.Build(Elements[elIndex].Size), ref sum);

                if (sum == TotalLength)
                    ret.Add(stack.ToList());

                stack.Pop(ref sum);
                var el = stack.Pop(ref sum);
                elIndex = dic[el.Size] + 1;
                if (stack.Count >0 && elIndex == Elements.Count)
                {
                    el = stack.Pop(ref sum);
                    elIndex = dic[el.Size] + 1;
                }
            }
            return ret;
        }
    }
    public static class ExtensionMethods 
    {
        public static Element Pop(this Stack<Element> elements, ref int total)
        {
            var el = elements.Pop();
            total -= el.Size;
            return el;
        }
        public static void Push(this Stack<Element> elements, Element el, ref int total)
        {
            elements.Push(el);
            total += el.Size;
        }
    }
}
