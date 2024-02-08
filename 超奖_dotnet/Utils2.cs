using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


partial class Utils
{
    public static async Task<Resp> order(string sid, string issue, string content, string money)
    {
        // 下注
        var data = new
        {
            c = 325,
            data = new
            {
                sid,
                smart = false,
                info = new[]
                {
                    new
                    {
                        issue = int.Parse(issue),
                        type_id = 120,
                        bet = new[]
                        {
                            new
                            {
                                content,
                                play_id = 426,
                                money = int.Parse(money)
                            }
                        },
                        smartchase = new
                        {
                            unitList = new string[]{},
                            win_stop = false
                        }
                    }
                }

            }

        };

        var resp = await "https://fa268.net/bet/?c=325"
            .WithHeader("User-Agent", "Mozilla/5.0 (Windows NT; Windows NT 10.0; zh-CN) WindowsPowerShell/5.1.22621.2506")
            .PostJsonAsync(data)
            .ReceiveJson<Resp> ();
        return resp;
    }

}
