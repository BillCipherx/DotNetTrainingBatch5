// See https://aka.ms/new-console-template for more information
using DotNetTrainingBatch5.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
//adoDotNetExample.Delete();

DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Create("asdfdfdsa","sdfasfa","asdffa");
//dapperExample.Update();
dapperExample.Delete();

Console.ReadKey();