# Json For Csharp
 
```c#
json jSample = json.create();
jSample = 1234; // integer
int i = jSample.get<int>();
double d = jSample.get<double>();
float f = jSample.get<float>();
string s = jSample.get<string>();
```

```c#
json jSample = json.create();
jSample = "sample"; // string
string s = jSample.get<string>(); // s is sample
```

```c#
json jSample = json.create();
jSample.add("jjj");
jSample.add("kkk");
string s = jSample.dump();  // ["jjj", "asdf"]
foreach (var jElm in jSample)
{
 var s = jElm.get<string>();
 Console.WriteLine(s);
}
// result is...
// jjj
// kkk
```

```c#
json jSample = json.create();
jSample["apple"] = "red";
jSample["banana"] = "yellow";
jSample["json"] = 1234;
string s = jSample.dump();
```
```json
// s is...
{
  "apple": "red",
  "banana": "yellow",
  "json": 1234
}
```

```json
// file parsing
{
  "boolean": true,
  "integer": 1234,
  "double": 1234.5678,
  "float": 1234.5678,
  "string": "hello",
  "array": [ "this", true, "is", 1234, "array" ],
  "dictionary": {
    "apple": 123,
    "is": "very",
    "delicous": {
      "email": "jaefunk@me.com",
      "json": "json is good"
    },
    "second_array": [
      {
        "fruit": "banana",
        "color": "yellow"
      },
      {
        "fruit": "cat",
        "color": "cute"
      }
    ]
  }
}
```
```
json jSample = json.parse(...json_string...);
string s = jSample.dump();
```
