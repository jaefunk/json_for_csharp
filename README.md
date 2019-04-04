# Json For Csharp
 
basic use
```c#
json jSample = json.create();

jSample = "sample";
string s = jSample.get<string>();   // "sample"

jSample = 1234;
int i = jSample.get<int>();         // 1234
double d = jSample.get<double>();   // 1234.0
float f = jSample.get<float>();     // 1234.0
string s = jSample.get<string>();   // 1234(string)
```

dump and parse
```c#
json jSample1 = json.create();
jSample1["apple"] = "red";
jSample1["integer"] = 1234;

string s = jSample1["apple"].get<string>();  // "red"
int i = jSample1["integer"].get<int>();      // 1234

string dump1 = jSample1.dump();  
/*
{"apple":"red","integer":1234}
*/
string dump2 = jSample1.dump(true);
/*
{
    "apple": "red",
    "integer": 1234
}
*/
```
```c#
json jSample2 = json.parse(dump1);
string s = jSample2["apple"].get<string>();  // "red"
int i = jSample2["integer"].get<int>();      // 1234
```

enumerator
```c#
foreach (var jObject in jSample)
{
    var s = jObject.get<string>();
    Console.WriteLine(s);
}
// red
// 1234(string)
```

my test json data
```json
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
        "fruit": "apple",
        "color": "red"
      }
    ]
  }
}
```
