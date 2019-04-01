# Json For Csharp
 
```c#
json jSample = json.create();
jSample = 1234; // integer
int i = jSample.get<int>(); // i is 1234
```

```c#
json jSample = json.create();
jSample = "sample"; // string
string s = jSample.get<string>(); // s is sample
```

```c#
json jSample = json.create();
jSample.add(1234);
jSample.add("kkk");
string s = jSample.dump();  // [1234, "asdf"]
```

```c#
json jSample = json.create();
jSample.add("jjj");
jSample.add("kkk");
foreach(var j in jSample)
{
    Console.WriteLine(j.get<string>());
}
// result is...
// jjj
// kkk
```

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
        "fruit": "cat",
        "color": "cute"
      }
    ]
  }
}
```
```
json jSample = json.parse("text_file_name");
```
