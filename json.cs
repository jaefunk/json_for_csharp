/*
Json For CSharp v1.1.0
https://github.com/jaefunk/json_for_csharp
Copyright (c) 2019 jwkim <jaefunk@me.com>
Licensed under the MIT License <http://opensource.org/licenses/MIT>
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections;
using System.Collections.Generic;

public class json_enumerator : IEnumerator<json>
{
    private int index;
    private List<json> list = new List<json>();
    public json_enumerator(List<json> list) { this.list = list; index = -1; }
    public bool MoveNext() { return ++index < list.Count; }
    public void Reset() { index = -1; }
    void IDisposable.Dispose() { }
    public json Current { get { return list[index]; } }
    object IEnumerator.Current { get { return Current; } }
}

public class json : IEnumerable<json>
{
    public IEnumerator<json> GetEnumerator() { return new json_enumerator(_array); }
    IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }

    public enum type { none, boolean, number, str, arr, dic, }
    private type _type = type.none;
    private object _value = null;
    private List<json> _array = null;
    private Dictionary<string, json> _dictionary = null;

    public json this[int index]
    {
        get { if (size() > index) return _array[index]; return null; }
        set { if (size() > index) _array[index] = value; }
    }
    public json this[string key]
    {
        get
        {
            if (_type != type.dic)
                return null;
            if (_dictionary.ContainsKey(key) == false)
                add(key, create());
            return _dictionary[key];
        }
        set
        {
            if (_type != type.dic)
                return;
            if (_dictionary.ContainsKey(key) == false)
                add(key, create());
            _dictionary[key] = value;
        }
    }

    public static implicit operator json(bool value)
    {
        return create(type.boolean, value);
    }
    public static implicit operator json(int value)
    {
        return create(type.number, value);
    }
    public static implicit operator json(long value)
    {
        return create(type.number, value);
    }
    public static implicit operator json(float value)
    {
        return create(type.number, value);
    }
    public static implicit operator json(double value)
    {
        return create(type.number, value);
    }
    public static implicit operator json(string value)
    {
        return create(type.str, value);
    }
    public static json operator +(json left, json right)
    {
        left.add(right);
        return left;
    }
    public static json operator -(json left, json right)
    {
        left.remove(right);
        return left;
    }
    public Ty get<Ty>()
    {
        return (Ty)Convert.ChangeType(_value, typeof(Ty));
    }

    public bool is_contain(string key)
    {
        if (_type != type.dic)
            return false;
        return _dictionary.ContainsKey(key);
    }

    static public json create(type type = type.none, object value = null)
    {
        json _json = new global::json();
        _json._type = type;
        if (type == type.arr)
            _json._array = value != null ? (List<json>)value : new List<json>();
        else if (type == type.dic)
            _json._dictionary = value != null ? (Dictionary<string, json>)value : new Dictionary<string, json>();
        else
            _json._value = value;
        return _json;
    }
    public bool is_container()
    {
        return _type == type.arr || _type == type.dic;
    }
    public int size()
    {
        if (_type == type.arr)
            return _array.Count;
        else if (_type == type.dic)
            return _dictionary.Count;
        return 0;
    }
    public void remove(int index)
    {
        if (size() <= index)
            return;
        _array.RemoveAt(index);
    }
    public void remove(json value)
    {
        if (_type != type.arr)
            return;
        _array.Remove(value);
    }
    public void remove(string key)
    {
        if (_type != type.dic)
            return;
        _dictionary.Remove(key);
    }
    public void add(json value)
    {
        if (_type != type.arr)
        {
            if (_type != type.none)
                return;
            _type = type.arr;
        }
        if (_array == null)
            _array = new List<json>();
        _array.Add(value);
    }
    public void add(string key, json value)
    {
        if (_type != type.dic)
        {
            if (_type != type.none)
                return;
            _type = type.dic;
        }
        if (_dictionary == null)
            _dictionary = new Dictionary<string, json>();
        _dictionary.Add(key, value);
    }

    static readonly char[] WHITESPACE = { ' ', '\t', '\n', '\r' };
    static public json parse(string str)
    {
        json result = create();
        if (string.IsNullOrEmpty(str))
            return result;
        str = str.Trim(WHITESPACE);
        if (string.Compare(str, "true", true) == 0)
            result = true;
        else if (string.Compare(str, "false", true) == 0)
            result = false;
        else if (string.Compare(str, "null", true) == 0)
        {
        }
        else if (str[0] == '"')
            result = str.Substring(1, str.Length - 2);
        else
        {
            int token = 1;
            int offset = 0;
            switch (str[offset])
            {
                case '{':
                    result._type = type.dic;
                    result._dictionary = new Dictionary<string, json>();
                    break;
                case '[':
                    result._type = type.arr;
                    result._array = new List<json>();
                    break;
                default:
                    try
                    {
                        result = Convert.ToDouble(str);
                    }
                    catch (System.FormatException)
                    {
                        Console.WriteLine("please check formatting: " + str);
                    }
                    break;
            }

            string propName = "";
            bool openQuote = false;
            bool inProp = false;
            int depth = 0;
            while (++offset < str.Length)
            {
                if (Array.IndexOf(WHITESPACE, str[offset]) > -1)
                    continue;
                if (str[offset] == '\\')
                {
                    offset += 1;
                    continue;
                }
                if (str[offset] == '"')
                {
                    if (openQuote)
                    {
                        if (!inProp && depth == 0 && result._type == type.dic)
                            propName = str.Substring(token + 1, offset - token - 1);
                        openQuote = false;
                    }
                    else
                    {
                        if (depth == 0 && result._type == type.dic)
                            token = offset;
                        openQuote = true;
                    }
                }
                if (openQuote == true)
                    continue;
                if (result._type == type.dic && depth == 0)
                {
                    if (str[offset] == ':')
                    {
                        token = offset + 1;
                        inProp = true;
                    }
                }

                if (str[offset] == '[' || str[offset] == '{')
                    depth++;
                else if (str[offset] == ']' || str[offset] == '}')
                    depth--;

                if ((str[offset] == ',' && depth == 0) || depth < 0)
                {
                    inProp = false;
                    string inner = str.Substring(token, offset - token).Trim(WHITESPACE);
                    if (inner.Length > 0)
                    {
                        if (result._type == type.dic)
                            result._dictionary.Add(propName, parse(inner));
                        else if (result._type == type.arr)
                            result._array.Add(parse(inner));
                    }
                    token = offset + 1;
                }
            }
        }
        return result;
    }
    public string dump(bool pretty = false)
    {
        return stringfy(0, pretty);
    }
    private string stringfy(int depth, bool pretty)
    {
        string result = string.Empty;
        switch (_type)
        {
            case type.none:
                result = "{}";
                break;
            case type.boolean:
                result = get<bool>() ? "true" : "false";
                break;
            case type.number:
                result = _value.ToString();
                break;
            case type.str:
                result = string.Format("\"{0}\"", get<string>());
                break;
            case type.arr:
                result = "[";
                if (pretty) result += WHITESPACE[2];
                depth++;
                foreach (var v in _array)
                {
                    if (pretty) for (var i = 0; i < depth; ++i) result += WHITESPACE[1];
                    result += v.stringfy(depth, pretty);
                    result += ",";
                    if (pretty) result += WHITESPACE[2];
                }
                result = result.Remove(result.Length - 1, 1);
                if (pretty) result = result.Remove(result.Length - 1, 1);
                if (pretty) result += WHITESPACE[2];
                depth--;
                if (pretty) for (var i = 0; i < depth; ++i) result += WHITESPACE[1];
                result += "]";
                break;
            case type.dic:
                result = "{";
                if (pretty) result += WHITESPACE[2];
                depth++;
                foreach (var kv in _dictionary)
                {
                    if (pretty) for (var i = 0; i < depth; ++i) result += WHITESPACE[1];
                    result += string.Format("\"{0}\"", kv.Key);
                    result += ":";
                    if (pretty) result += WHITESPACE[0];
                    result += kv.Value.stringfy(depth, pretty);
                    result += ",";
                    if (pretty) result += WHITESPACE[2];
                }
                result = result.Remove(result.Length - 1, 1);
                if (pretty) result = result.Remove(result.Length - 1, 1);
                if (pretty) result += WHITESPACE[2];
                depth--;
                if (pretty) for (var i = 0; i < depth; ++i) result += WHITESPACE[1];
                result += "}";
                break;
        }
        return result;
    }
}
