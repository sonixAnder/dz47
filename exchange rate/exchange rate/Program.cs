using System;
using System.Linq;
using System.Xml.Linq;
using HtmlAgilityPack;

class Program
{
    static void Main(string[] args)
    {
        string url = "https://www.cbr.ru/currency_base/daily/";

        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = web.Load(url);

        // Найти строки таблицы с курсами валют
        var rows = doc.DocumentNode.SelectNodes("//table[@class='data']//tr");

        if (rows == null || rows.Count == 0)
        {
            Console.WriteLine("Не удалось найти данные о курсах валют.");
            return;
        }

        XDocument xmlDoc = new XDocument(new XElement("Banks"));

        foreach (var row in rows)
        {
            var cells = row.SelectNodes("//table[@class='data']//tr");
            if (cells != null && cells.Count >= 4)
            {
                string bankName = cells[0].InnerText.Trim();
                string buyRate = cells[1].InnerText.Trim();
                string sellRate = cells[2].InnerText.Trim();

                XElement bankElement = new XElement("Bank",
                    new XElement("Name", bankName),
                    new XElement("BuyRate", buyRate),
                    new XElement("SellRate", sellRate)
                );

                xmlDoc.Root.Add(bankElement);
            }
        }

        string outputPath = "output.xml";
        xmlDoc.Save(outputPath);

        Console.WriteLine($"Данные успешно сохранены в файл {outputPath}");
    }
}
