// See https://aka.ms/new-console-template for more information
using KSTDotNetCore.ConsoleAppRestClientExample;
using RestSharp;

Console.WriteLine("Hello, World!");

RestClientExample restClientExample = new RestClientExample();
await restClientExample.runAsync();

Console.ReadLine();