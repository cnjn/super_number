using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace 超奖_dotnet
{

    public record IpInfo(string ip, string country);
    public record Data(string open_at, string? superNumber, string issue);
    public record Resp(List<Data> data, bool status, int? code);
    public enum OrderType { Odd, Even};
    internal partial class Api(string sid)
    {
        /// <summary>
        /// 获取最新的开奖结果
        /// </summary>
        /// <returns>Resp(List<Data> data, bool status, int code)</returns>
        public async Task<Resp> GetLotteryResult()
        {
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
                .ReceiveJson<Resp>();
            return resp;
        }

        static string GetOrderType(OrderType orderType)
        {
            switch (orderType)
            {
                case OrderType.Odd: return "odd";
                case OrderType.Even: return "even";
                default: return "odd";
            }
        }

        /// <summary>
        /// 下注Api
        /// issue是下注的期数
        /// orderType决定下注单双
        /// </summary>
        /// <returns></returns>
        public async Task<Resp> Order(int issue, OrderType orderType, int money)
        {
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
                            issue,
                            type_id = 120,
                            bet = new[]
                            {
                                new
                                {
                                    content = GetOrderType(orderType),
                                    play_id = 426,
                                    money
                                }
                            },
                            smartchase = new
                            {
                                unitList = Array.Empty<string>(),
                                win_stop = false
                            }
                        }
                    }
                }
            };
            var resp = await "https://fa268.net/bet/?c=325"
                .WithHeader("User-Agent", "Mozilla/5.0 (Windows NT; Windows NT 10.0; zh-CN) WindowsPowerShell/5.1.22621.2506")
                .PostJsonAsync(data)
                .ReceiveJson<Resp>();
            return resp;

        }

        /// <summary>
        /// 返回最新的游戏期号，成功返回数字，失败返回0
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetLatestIssue()
        {
             
            var resp = await "https://fa268.net/bet/?c=231"
                .PostJsonAsync(new
                {
                    c = 231,
                    data = new
                    {
                        sid,
                        typeId = new[] {"120"}
                    }
                })
                .ReceiveString();
            try
            {
                var issue = SearchIssue().Match(resp).Groups[1].Value;
                return int.Parse(issue);
            }
            catch
            {
                return 0;
            }
        }

        [GeneratedRegex("""issue":"(\d+)""")]
        private static partial Regex SearchIssue();
    }
}
