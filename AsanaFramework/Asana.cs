using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Asana
{
    public class Asana
    {
        private Dictionary<string, object> Vars = new Dictionary<string, object>();
        internal const string apiUrl = "https://app.asana.com/api/1.0";
        internal string AccessToken { get; set; }
        internal string JsonBody { get; set; }
        public Asana(bool debug = false)
        {
            if (debug)
                Debugger.Launch();
        }

        public Asana SetToken(string token)
        {
            AccessToken = token;
            return this;
        }

        public Asana SetJsonBody(string body)
        {
            JsonBody = body;
            return this;
        }

        public Asana SetVar(string name, object value)
        {
            Vars[name] = value;
            return this;
        }

        public Asana SetStringVar(string name, string value) => SetVar(name, value);
        public Asana SetIntVar(string name, int value) => SetVar(name, value);
        public Asana SetDoubleVar(string name, double value) => SetVar(name, value);
        public Asana SetBooleanVar(string name, bool value) => SetVar(name, value);
        public Asana SetListOfStringVar(string name, List<string> value) => SetVar(name, value);

        public object GetVar(string name) => Vars.ContainsKey(name) ? Vars[name] : null;
        public string GetStringVar(string name) => GetVar(name)?.ToString();
        public int GetIntVar(string name) => (int)GetVar(name);
        public double GetDoubleVar(string name) => (double)GetVar(name);
        public bool GetBooleanVar(string name) => (bool)GetVar(name);
        public List<string> GetListOfStringVar(string name) => (List<string>)GetVar(name);


        public string Get(string url, bool debug = false) => Do("GET", url, debug);
        public string Put(string url, bool debug = false) => Do("PUT", url, debug);
        public string Post(string url, bool debug = false) => Do("POST", url, debug);
        public string Delete(string url, bool debug = false) => Do("DELETE", url, debug);
        public string Do(string method, string url, bool debug = false)
        {
            if (debug) Debugger.Launch();

            foreach (var braced in from var in Vars where var.Key.StartsWith("{") where var.Key.EndsWith("}") select var)
            {
                url = url.Replace(braced.Key, braced.Value.ToString());
            }

            var client = new RestClient(apiUrl);
            var request = new RestRequest(url);

            request.Method = (Method)Enum.Parse(typeof(Method), method);
            request.AddHeader("Authorization", "Bearer " + AccessToken);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            foreach (var unbraced in from var in Vars where !var.Key.StartsWith("{") select var)
            {
                request.AddParameter(unbraced.Key, unbraced.Value, ParameterType.GetOrPost);
            }

            if (JsonBody != null)
            {
                request.AddJsonBody(JsonBody);
            }

            var response = client.Execute(request);
            return response.Content;
        }
    }
}
