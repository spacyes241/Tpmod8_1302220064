using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

public class Program
{
    public static void Main(string[] args)
    {
      
        CovidConfig Konfig = new CovidConfig();

        Console.WriteLine("Berapa suhu badan anda saat ini? Dalam nilai " + Konfig.configuration.satuan_suhu);
        double inputSuhu = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala deman?");
        int inputHari = Convert.ToInt32(Console.ReadLine());

        if (Konfig.configuration.satuan_suhu == "celcius")
        {
            if ((inputSuhu >= 36.5 || inputSuhu <= 37.5) && inputHari < Konfig.configuration.batas_hari_demam)
            {
                Console.WriteLine(Konfig.configuration.pesan_diterima);
            }
            else
            {
                Console.WriteLine(Konfig.configuration.pesan_ditolak);
            }
        }
        else if (Konfig.configuration.satuan_suhu == "fahrenheit")
        {
            if ((inputSuhu >= 97.7 || inputSuhu <= 99.5) && inputHari < Konfig.configuration.batas_hari_demam)
            {
                Console.WriteLine(Konfig.configuration.pesan_diterima);
            }
            else
            {
                Console.WriteLine(Konfig.configuration.pesan_ditolak);

            }
        }

        Konfig.UbahSatuan();
        Console.WriteLine();

        Console.WriteLine("Berapa suhu badan anda saat ini? Dalam nilai " + Konfig.configuration.satuan_suhu);
        inputSuhu = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala deman?");
        inputHari = Convert.ToInt32(Console.ReadLine());

        if (Konfig.configuration.satuan_suhu == "celcius")
        {
            if ((inputSuhu >= 36.5 || inputSuhu <= 37.5) && inputHari < Konfig.configuration.batas_hari_demam)
            {
                Console.WriteLine(Konfig.configuration.pesan_diterima);
            }
            else
            {
                Console.WriteLine(Konfig.configuration.pesan_ditolak);
            }
        }
        else if (Konfig.configuration.satuan_suhu == "fahrenheit")
        {
            if ((inputSuhu >= 97.7 || inputSuhu <= 99.5) && inputHari < Konfig.configuration.batas_hari_demam)
            {
                Console.WriteLine(Konfig.configuration.pesan_diterima);
            }
            else
            {
                Console.WriteLine(Konfig.configuration.pesan_ditolak);

            }
        }
    }

    public class Config
    {
        public string satuan_suhu { get; set; }
        public int batas_hari_demam { get; set; }
        public string pesan_ditolak { get; set; }
        public string pesan_diterima { get; set; }

        public Config() { }

        public Config(string suhu, int batasDemam, string pesanDitolak, string pesanDiterima)
        {
            satuan_suhu = suhu;
            batas_hari_demam = batasDemam;
            pesan_ditolak = pesanDitolak;
            pesan_diterima = pesanDiterima;
        }
    }

    public class CovidConfig
    {

        public Config configuration;

        public const string filePath = "E:\\Materi\\SEMESTER 4\\Praktikum KPL\\TPMOD8_1302220064\\tpmodule8_1302220064\\tpmodule8_1302220064\\bin\\Debug\\net8.0\\Covid_config.json";


        public CovidConfig()
        {
            try
            {
                ReadConfigFile();
            }
            catch (Exception)
            {
                SetDefault();
                WriteNewConfigFile();
            }
        }


        public void SetDefault()
        {
            configuration = new Config("celcius", 14, "Anda tidak diperbolehkan masuk ke dalam gedung ini",
                "Anda diperbolehkan masuk ke dalam gedung ini");
        }

        private Config ReadConfigFile()
        {
            String configJsonData = File.ReadAllText(filePath);
            configuration = JsonSerializer.Deserialize<Config>(configJsonData);
            return configuration;
        }

        public void WriteNewConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            String jsonString = JsonSerializer.Serialize(configuration, options);
            File.WriteAllText(filePath, jsonString);
        }

        public void UbahSatuan()
        {
            if (configuration.satuan_suhu == null)
            {
                configuration.satuan_suhu = "celcius";
            }
            else if (configuration.satuan_suhu == "celcius")
            {
                configuration.satuan_suhu = "fahrenheit";
            }
            else if (configuration.satuan_suhu == "fahrenheit")
            {
                configuration.satuan_suhu = "celcius";
            }
        }
    }
}