using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Forms.VisualStyles;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace SPS01CalibrateAndTestNewModeApp.Mode
{
    public class FullBrigeDataMode
    {
        
    }

    public class SpsCalibration
    {
        public string ID { get; set; }

        public string CalibraMode { get; set; }

        public bool Status = true;
        public string StatusMsg { get; set; }

        public Dictionary<string, double> FullBridgeTarget = new Dictionary<string, double>
            { { "O1", 0 }, { "O2", 0 }, { "O3", 0 }, { "O4", 0 } };

        public Dictionary<string, double> HalfBridgeTarget = new Dictionary<string, double>
            { { "O1", 0 }, { "O2", 0 }, { "O3", 0 }, { "O4", 0 } };

        public Dictionary<string, double> FullBridgeRawData = new Dictionary<string, double>
        {
            { "T0P1", 0 }, { "T0P2", 0 }, { "T0P3", 0 }, { "T0P4", 0 }, { "T1P1", 0 }, { "T1P2", 0 }, { "T2P1", 0 },
            { "T2P2", 0 }, { "T3P1", 0 }, { "T3P2", 0 }
        };

        public Dictionary<string, double> HalfBridgeRawData = new Dictionary<string, double>
        {
            { "T0P1", 0 }, { "T0P2", 0 }, { "T0P3", 0 }, { "T0P4", 0 }, { "T1P1", 0 }, { "T1P2", 0 }, { "T2P1", 0 },
            { "T2P2", 0 }, { "T3P1", 0 }, { "T3P2", 0 }
        };

        public Dictionary<string, double> TsiTempTarget = new Dictionary<string, double>
            { { "T0", 0 }, { "T1", 0 }, { "T2", 0 }, { "T3", 0 } };

        public Dictionary<string, double> TsiTempRaw = new Dictionary<string, double>
            { { "T0", 0 }, { "T1", 0 }, { "T2", 0 }, { "T3", 0 } };

        public Dictionary<string, double> TseTempTarget = new Dictionary<string, double>
            { { "T0", 0 }, { "T1", 0 }, { "T2", 0 }, { "T3", 0 } };

        public Dictionary<string, double> TseTempRaw = new Dictionary<string, double>
            { { "T0", 0 }, { "T1", 0 }, { "T2", 0 }, { "T3", 0 } };

        public Dictionary<string, string> FullBridgeFactor = new Dictionary<string, string>
        {
            { "s0", "0" },{"tsc1", "0"}, {"tsc2", "0"}, {"tsc3", "0"}, {"tco1", "0"}, {"tco2", "0"}, {"tco3", "0"},
            {"f0", "0"}, {"k2", "0"}, {"k3", "0"}, {"baseT", "0"}
        };

        public Dictionary<string, string> FullBridgeFactorHex = new Dictionary<string, string>
        {
            { "s0", "0x0000" }, { "tsc1", "0x0000" }, { "tsc2", "0x0000" }, { "tsc3", "0x0000" }, { "tco1", "0x0000" }, 
            { "tco2", "0x0000" }, { "tco3","0x0000" }, { "f0", "0x0000" }, { "k2", "0x0000" }, { "k3", "0x0000" },
            { "baseT", "0x0000" }
        };

        public Dictionary<string, string> HalfBridgeFactor = new Dictionary<string, string>
        {
            { "s0", "0" },{"tsc1", "0"}, {"tsc2", "0"}, {"tsc3", "0"}, {"tco1", "0"}, {"tco2", "0"}, {"tco3", "0"},
            {"f0", "0"}, {"k2", "0"}, {"k3", "0"}, {"baseT", "0"}
        };

        public Dictionary<string, string> HalfBridgeFactorHex = new Dictionary<string, string>
        {
            { "s0", "0x0000" }, { "tsc1", "0x0000" }, { "tsc2", "0x0000" }, { "tsc3", "0x0000" }, { "tco1", "0x0000" },
            { "tco2", "0x0000" }, { "tco3", "0x0000" }, { "f0", "0x0000" }, { "k2", "0x0000" }, { "k3", "0x0000" },
            { "baseT", "0x0000" }
        };

        public Dictionary<string, double> TsiFactor = new Dictionary<string, double>
            { { "k", 0 }, { "m", 0 }, { "Toff", 0 } };

        public Dictionary<string, double> TseFactor = new Dictionary<string, double>
            { { "k", 0 }, { "m", 0 }, { "Toff", 0 } };

        public byte[] NvmData { get; set; } = new byte[256];

        public byte[] RegData { get; set; } = new byte[256];

        // 将所有属性值拼接为JSON字符串
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this,Formatting.Indented);
        }

        public string FullBridgeDataToJson()
        {
            var fullBridgeDataObj = new { FullBridgeRawData, FullBridgeTarget, TsiTempRaw, CalibraMode };
            return JsonConvert.SerializeObject(fullBridgeDataObj);
        }

        public string HalfBridgeDataToJson()
        {
            var halfBridgeDataObj = new { HalfBridgeRawData, HalfBridgeTarget, TsiTempRaw, CalibraMode };
            return JsonConvert.SerializeObject(halfBridgeDataObj);
        }

        public string TsiDataToJson()
        {
            var tsiDataObj = new { TsiTempRaw, TsiTempTarget };
            return JsonConvert.SerializeObject(tsiDataObj);
        }

        public string TseDataToJson()
        {
            var tseDataObj = new { TseTempRaw, TseTempTarget };
            return JsonConvert.SerializeObject(tseDataObj);
        }

        public void JsonToFullBridgeFactor(string json)
        {
           var factorObj = JsonConvert.DeserializeObject<dynamic>(json);
           FullBridgeFactor = factorObj.factor;
           FullBridgeFactorHex = factorObj.factor_hex;
        }
        public void JsonToHalfBridgeFactor(string json)
        {
            var factorObj = JsonConvert.DeserializeObject<dynamic>(json);
            for (int i = 0; i < HalfBridgeFactor.Count; i++)
            {
                var key = HalfBridgeFactor.Keys.ToList()[i];
                HalfBridgeFactor[key] = factorObj.factor[key];
            }

            for (int i = 0; i < HalfBridgeFactorHex.Count; i++)
            {
                var key = HalfBridgeFactorHex.Keys.ToList()[i];
                HalfBridgeFactorHex[key] = factorObj.factor_hex[key];
            }
            
        }
        public void JsonToTsiFactor(string json)
        {
            TsiFactor = JsonConvert.DeserializeObject<Dictionary<string, double>>(json);
            
        }
        public void JsonToTseFactor(string json)
        {
            TseFactor = JsonConvert.DeserializeObject<Dictionary<string, double>>(json);
        }

        public void JsonToFullBridgeFactorHex(string json)
        {
            FullBridgeFactorHex = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public void JsonToHalfBridgeFactorHex(string json)
        {
            FullBridgeFactorHex = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public async Task<string> Calibration(string jsoStr)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // 定义 API 的 URL，这里假设 API 运行在本地的 5000 端口，路径为 /api
                    var apiUrl = "http://172.20.100.40:5000/api";
                    
                    // 创建一个 StringContent 对象，用于将 JSON 数据作为请求体发送
                    var content = new StringContent(jsoStr, Encoding.UTF8, "application/json");

                    // 发送 POST 请求到 API，并等待响应
                    var response = await client.PostAsync(apiUrl, content);
                    
                    // 检查响应状态码是否表示成功（状态码在 200 - 299 之间）
                    if (response.IsSuccessStatusCode)
                    {
                        // 读取响应内容并转换为字符串
                        var responseBody = await response.Content.ReadAsStringAsync();

                        // 将响应的 JSON 字符串反序列化为动态对象，方便后续处理
                        // dynamic result = JsonConvert.DeserializeObject(responseBody);

                        // 输出 API 的响应结果
                        Console.WriteLine("API 响应结果:");
                        Console.WriteLine(responseBody);
                        
                        return responseBody;
                    }
                    else
                    {
                        // 如果响应状态码表示失败，输出错误信息
                        Console.WriteLine($"请求失败，状态码: {response.StatusCode}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                // 捕获并输出请求过程中可能出现的异常信息
                Console.WriteLine($"发生异常: {ex.Message}");
                return null;
            }
        }
        
        // 表名 spsdata
        // 列名 Id, Data, FullBridgeRawData, HalfBridgeRawData, TsiTempRaw, TseTempRaw, Press, Temp, FullBridgeTarget, HalfBridgeTaget, TSEMode, FullBridgeFactor, HalfBridgeFactor, CreatTime, UpdateTime, TsiFactor, TseFactor, Status, CalibraMode
        
        public void InsertDataBase()
        {
            var connectionStr = "Server=172.20.100.20;Database=product_base_sps01;Uid=Link_pb;Pwd=link*1234;";
            using (var connection = new MySqlConnection(connectionStr))
            {
                connection.Open();
                // 查询Id 对应的数据是否存在
                var cmd = new MySqlCommand("SELECT count(*) FROM spsdata WHERE Id = @Id", connection);
                cmd.Parameters.AddWithValue("@Id", ID);
                var count = cmd.ExecuteScalar();
                var countInt = Convert.ToInt32(count);
                if (countInt > 0)
                {
                    // 如果存在，则更新数据
                    SelectDataBase();
                    UpdateDataBase();
                }
                else
                {
                    // 如果不存在，仅插入Id 数据
                    cmd = new MySqlCommand("INSERT INTO spsdata (Id) VALUES (@Id)", connection);
                    cmd.Parameters.AddWithValue("@Id", ID);
                    cmd.ExecuteNonQuery();
                    UpdateDataBase();
                }
                
            }
        }

        public void UpdateDataBase()
        {
            var connectionStr = "Server=172.20.100.20;Database=product_base_sps01;Uid=Link_pb;Pwd=link*1234;";
            using (var connection = new MySqlConnection(connectionStr))
            {
                connection.Open();
                // 查询Id 对应的数据是否存在
                var cmd = new MySqlCommand("SELECT count(*) FROM spsdata WHERE Id = @Id", connection);
                cmd.Parameters.AddWithValue("@Id", ID);
                var count = cmd.ExecuteScalar();
                var countInt = Convert.ToInt32(count);
                
                if (countInt > 0)
                {
                    // 如果存在，则更新数据
                    var fullBridgeRawDataJson = JsonConvert.SerializeObject(FullBridgeRawData);
                    var halfBridgeRawDataJson = JsonConvert.SerializeObject(HalfBridgeRawData);
                    var tsiTempRawJson = JsonConvert.SerializeObject(TsiTempRaw);
                    var tseTempRawJson = JsonConvert.SerializeObject(TseTempRaw);
                    var fullBridgeTargetJson = JsonConvert.SerializeObject(FullBridgeTarget);
                    var halfBridgeTargetJson = JsonConvert.SerializeObject(HalfBridgeTarget);
                    var fullBridgeFactorJson = JsonConvert.SerializeObject(FullBridgeFactor);
                    var halfBridgeFactorJson = JsonConvert.SerializeObject(HalfBridgeFactor);
                    var tsiFactorJson = JsonConvert.SerializeObject(TsiFactor);
                    var tseFactorJson = JsonConvert.SerializeObject(TseFactor);
                    var nvmDataJson = JsonConvert.SerializeObject(NvmData);
                    var regDataJson = JsonConvert.SerializeObject(RegData);
                    var statusObj = new { Status, StatusMsg };
                    var statusJson = JsonConvert.SerializeObject(statusObj);
                    cmd = new MySqlCommand("UPDATE spsdata SET FullBridgeRawData = @FullBridgeRawData, " +
                                           "HalfBridgeRawData = @HalfBridgeRawData, TsiTempRaw = @TsiTempRaw, " +
                                           "TseTempRaw = @TseTempRaw, FullBridgeTarget = @FullBridgeTarget," +
                                           " HalfBridgeTarget = @HalfBridgeTarget, FullBridgeFactor = @FullBridgeFactor," +
                                           " HalfBridgeFactor = @HalfBridgeFactor, TsiFactor = @TsiFactor, TseFactor = @TseFactor," +
                                           " NvmData = @NvmData, RegData = @RegData, Status = @Status, CalibraMode = @CalibraMode " +
                                           "WHERE Id = @Id", connection);
                    cmd.Parameters.AddWithValue("@FullBridgeRawData", fullBridgeRawDataJson);
                    cmd.Parameters.AddWithValue("@HalfBridgeRawData", halfBridgeRawDataJson);
                    cmd.Parameters.AddWithValue("@TsiTempRaw", tsiTempRawJson);
                    cmd.Parameters.AddWithValue("@TseTempRaw", tseTempRawJson);
                    cmd.Parameters.AddWithValue("@FullBridgeTarget", fullBridgeTargetJson);
                    cmd.Parameters.AddWithValue("@HalfBridgeTarget", halfBridgeTargetJson);
                    cmd.Parameters.AddWithValue("@FullBridgeFactor", fullBridgeFactorJson);
                    cmd.Parameters.AddWithValue("@HalfBridgeFactor", halfBridgeFactorJson);
                    cmd.Parameters.AddWithValue("@TsiFactor", tsiFactorJson);
                    cmd.Parameters.AddWithValue("@TseFactor", tseFactorJson);
                    cmd.Parameters.AddWithValue("@NvmData", nvmDataJson);
                    cmd.Parameters.AddWithValue("@RegData", regDataJson);
                    cmd.Parameters.AddWithValue("@Status", statusJson);
                    cmd.Parameters.AddWithValue("@CalibraMode", CalibraMode);
                    cmd.Parameters.AddWithValue("@Id", ID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SelectDataBase()
        {
            var connectionStr = "Server=172.20.100.20;Database=product_base_sps01;Uid=Link_pb;Pwd=link*1234;";
            using (var connection = new MySqlConnection(connectionStr))
            {
                connection.Open();
                // 查询Id 对应的数据是否存在
                var cmd = new MySqlCommand("SELECT * FROM spsdata WHERE Id = @Id", connection);
                cmd.Parameters.AddWithValue("@Id", ID);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // 查询到数据，将数据反序列化为对象
                    var fullBridgeRawDataJson = reader["FullBridgeRawData"] as string;
                    var halfBridgeRawDataJson = reader["HalfBridgeRawData"] as string;
                    var tsiTempRawJson = reader["TsiTempRaw"] as string;
                    var tseTempRawJson = reader["TseTempRaw"] as string;
                    var fullBridgeTarget = reader["FullBridgeTarget"] as string;
                    var halfBridgeTarget = reader["HalfBridgeTarget"] as string;
                    var fullBridgeFactorJson = reader["FullBridgeFactor"] as string;
                    var halfBridgeFactorJson = reader["HalfBridgeFactor"] as string;
                    var tsiFactorJson = reader["TsiFactor"] as string;
                    var tseFactorJson = reader["TseFactor"] as string;
                    var nvmDataJson = reader["NvmData"] as string;
                    var regDataJson = reader["RegData"] as string;
                    var statusJson = reader["Status"] as string;
                    var statusObj = JsonConvert.DeserializeObject<dynamic>(statusJson);
                    Status = statusObj.Status;
                    StatusMsg = statusObj.StatusMsg;
                    CalibraMode = reader["CalibraMode"] as string;
                    FullBridgeRawData = JsonConvert.DeserializeObject<Dictionary<string, double>>(fullBridgeRawDataJson);
                    HalfBridgeRawData = JsonConvert.DeserializeObject<Dictionary<string, double>>(halfBridgeRawDataJson);
                    TsiTempRaw = JsonConvert.DeserializeObject<Dictionary<string, double>>(tsiTempRawJson);
                    TseTempRaw = JsonConvert.DeserializeObject<Dictionary<string, double>>(tseTempRawJson);
                    FullBridgeTarget = JsonConvert.DeserializeObject<Dictionary<string, double>>(fullBridgeTarget);
                    HalfBridgeTarget = JsonConvert.DeserializeObject<Dictionary<string, double>>(halfBridgeTarget);
                    FullBridgeFactor = JsonConvert.DeserializeObject<Dictionary<string, string>>(fullBridgeFactorJson);
                    HalfBridgeFactor = JsonConvert.DeserializeObject<Dictionary<string, string>>(halfBridgeFactorJson);
                    TsiFactor = JsonConvert.DeserializeObject<Dictionary<string, double>>(tsiFactorJson);
                    TseFactor = JsonConvert.DeserializeObject<Dictionary<string, double>>(tseFactorJson);
                    NvmData = JsonConvert.DeserializeObject<byte[]>(nvmDataJson);
                    RegData = JsonConvert.DeserializeObject<byte[]>(regDataJson);
                }
            }
        }
    }
    
    
}