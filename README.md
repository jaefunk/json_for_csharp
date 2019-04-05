# Json For Csharp
 
basic use
```c#
json jSample = json.create();
jSample = "sample";
string s = jSample.get<string>();   // sample
jSample = 1234;
int i = jSample.get<int>();         // 1234
double d = jSample.get<double>();   // 1234.0
float f = jSample.get<float>();     // 1234.0
string s = jSample.get<string>();   // 1234(string)
```
use dictionary
```c#
json jSample = json.create();
jSample.add("integer", 1234);
jSample.add("string", "aaa");
int i = jSample["integer"].get<int>();      // 1234
string s = jSample["string"].get<string>(); // red
```
use array
```c#
json jSample = json.create();
jSample.add(1);
jSample.add(2);
jSample.add(3);
for (int i = 0; i < jSample.size(); ++i) {
    int integer = jSample[i].get<int>();
    Console.WriteLine(integer);
}
// 1 
// 2
// 3
```
use enumerator
```c#
foreach (var jObject in jSample) {
    var s = jObject.get<int>();
    Console.WriteLine(int);
}
// 1 
// 2
// 3
```
dump
```c#
json jSample = json.create();
jSample["integer"] = 1234;
jSample["string"] = "red";
string dump = jSample.dump();  
/*
{"integer":1234,"apple":"red"}
*/
dump = jSample.dump(true); // for pretty
/*
{
    "integer": 1234,
    "apple": "red"
}
*/
```
parse
```c#
json jSample = json.parse(dump);
string s = jSample["string"].get<string>();  // red
int i = jSample["integer"].get<int>();       // 1234
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
