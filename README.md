# Asana

Code sample from a JavaScript runner built using ClearScript and JScript.
```
var settings = JSON.parse(slurp("c:\\web\\asanasettings.json"));
attach("Asana.DLL", "A");
var asana = new A.Asana.Asana();
asana.SetToken(settings.Authorisation.Token);

if (!CSSettings.ContainsKey("$ARG1")) {
    CSConsole.WriteLine("needs project");
    CSEnvironment.Exit(1);
}

var project = CSSettings("$ARG1");
if ("undefined" === typeof settings.Projects[project]) {
    CSConsole.WriteLine("needs real project");
    CSEnvironment.Exit(1);
}

asana.SetStringVar("{project_gid}", settings.Projects[project]);
var result = asana.Get("/projects/{project_gid}/sections?opt_pretty=true");
//CSConsole.WriteLine(result);
var json = JSON.parse(result);
for (var d = 0; d < json.data.length; d++) {
    CSConsole.Write('"{0}":"{1}"', project + json.data[d].name, json.data[d].gid);
    CSConsole.WriteLine(",");
}
```
MIT License
