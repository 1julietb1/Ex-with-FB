using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EX
{
    public class MapWithMaxProxy<T, V> : Dictionary<T, V>
        where T : class
        where V : class
    {
        public Comparison<KeyValuePair<T, V>> MaxComperator { get; set; }

        public KeyValuePair<T, V> GetMaxFromMap()
        {
            List<KeyValuePair<T, V>> myList = this.ToList();
            myList.Sort(MaxComperator);
            KeyValuePair<T, V> result = new KeyValuePair<T, V>();
            if (myList.Count != 0)
            {
                result = myList[0];
            }

            return result;
        }
    }
}
