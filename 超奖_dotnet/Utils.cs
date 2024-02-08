using Flurl.Http;
using System.Security.Cryptography;
using System.Text.Json.Nodes;

partial class Utils
{
    
    public record IpInfo(string ip, string country);
    public record Data(string? open_at, string? superNumber, string? issue);
    public record Resp(List<Data> data, bool status, int? code);

  

    public static async Task<Resp> GetLotteryResult(string sid)
    {
        // 获取最新的开奖信息

        var data = new
        {
            c = 225,
            data = new
            {
                sid,
                type = 120,
                rows = 84,
                date = true,
                open_at = new DateTimeOffset(DateTime.Today.Date).ToUnixTimeSeconds(),
                page = 0
            }

        };

        var resp = await "https://fa268.net/bet/?c=225"
            .PostJsonAsync(data)
            //.ReceiveString();
            .ReceiveJson<Resp>();
        return resp;
        /*
        var obj = JsonObject.Parse(resp);
        var ret = new Resp(new List<Data>(), obj["status"].GetValue<bool>());
        if (obj["data"] == null)
        {
            return ret;
        }
        foreach (var item in obj["data"]!.AsArray())
        { 
            ret.data.Add(new Data(item["open_at"].GetValue<string>(), item["superNumber"]==null ? null : item["superNumber"].GetValue<string>(), item["issue"].GetValue<string>()));
        }
        return ret;
        */
    }

}