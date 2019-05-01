using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Converter
    {
        static public List<T> Convert_Array<T>(object converted)
        {
            List<object> temp = (List<object>)converted;
            List<T> returned = new List<T>();
            foreach(object iter in temp)
            {
                returned.Add((T)iter);
            }
            return returned;
        }
        static public Dictionary<TKey,TValue> Convert_Dictionary<TKey,TValue>(object converted)
        {
            Dictionary<object,object> temp = (Dictionary<object,object>)converted;
            Dictionary<TKey,TValue> returned=new Dictionary<TKey, TValue>();
            object[] keys = temp.Keys.ToArray();
            object[] values = temp.Values.ToArray();
            for(int i = 0; i < keys.Length; i++)
            {
                returned.Add((TKey)keys[i], (TValue)values[i]);
            }
            return returned;
        }
        static public List<List<T>> Convert_Grid<T>(object converted)
        {
            List<object> temp = (List<object>)converted;
            List<List<T>> returned = new List<List<T>>();
            for(int i = 0; i < temp.Count; i++)
            {
                returned.Add(new List<T>());
                List<object> rowtemp = (List<object>)temp[i];
                for(int j = 0; j < rowtemp.Count; j++)
                {
                    returned[i].Add((T)rowtemp[j]);
                }
            }
            return returned;
        }
    }
}
