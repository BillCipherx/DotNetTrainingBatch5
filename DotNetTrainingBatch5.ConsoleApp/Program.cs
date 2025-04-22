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
//dapperExample.Edit(1);
//dapperExample.Edit(2);
//dapperExample.Edit(3);
//dapperExample.Update();
//dapperExample.Delete();

EFCoreExample eFCoreExample = new EFCoreExample();
//eFCoreExample.Read();
//eFCoreExample.Create("asdfdfdsa", "sdfasfa", "asdffa");
//eFCoreExample.Edit(5);
//eFCoreExample.Update(3, "OkNarSar", "SawTeePal", "BarLar");
//eFCoreExample.Delete(3011);

DapperExample2 dapperExample2 = new DapperExample2();
dapperExample2.Read();

Console.ReadKey();