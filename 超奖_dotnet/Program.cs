using System.Net;
using Flurl.Http;
using Flurl.Http.Configuration;


namespace 超奖_dotnet
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 设置代理
            FlurlHttp.Clients.WithDefaults(builder => builder
            .ConfigureHttpClient(hc => { })
            .ConfigureInnerHandler(hch =>
            {
                hch.Proxy = new WebProxy("http://172.28.90.45:808");
                hch.UseProxy = true;
            }));

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}