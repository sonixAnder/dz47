using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

class Program
{
    static void Main(string[] args)
    {
        //начало задания 3
        string xmlFile = "orders.xml";  // Путь к XML-файлу
        string xsltFile = "orders.xslt"; // Путь к XSLT-файлу
        string outputHtml = "orders.html"; // Путь к выходному HTML

        try
        {
            // Настраиваем XSLT-преобразование
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltFile);

            // Выполняем преобразование
            xslt.Transform(xmlFile, outputHtml);

            Console.WriteLine($"HTML документ успешно создан: {outputHtml}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        string outputPath = "orders.xml";
        //конец задания 3

        using (XmlTextWriter writer = new XmlTextWriter(outputPath, System.Text.Encoding.UTF8))
        {
            writer.Formatting = Formatting.Indented;

            // Начало документа
            writer.WriteStartDocument();
            writer.WriteStartElement("Orders"); // Корневой элемент

            // Добавляем первый заказ
            writer.WriteStartElement("Order");
            writer.WriteAttributeString("OrderId", "101");
            writer.WriteElementString("CustomerName", "Иван Иванов");
            writer.WriteElementString("OrderDate", "2024-12-03");

            // Добавляем товары для первого заказа
            writer.WriteStartElement("Items");

            AddItem(writer, "Ноутбук Lenovo", 2, 1000.50);
            AddItem(writer, "Вентилятор", 1, 200.00);

            writer.WriteEndElement(); // Закрываем Items
            writer.WriteEndElement(); // Закрываем Order

            // Добавляем второй заказ
            writer.WriteStartElement("Order");
            writer.WriteAttributeString("OrderId", "102");
            writer.WriteElementString("CustomerName", "Мария Петрова");
            writer.WriteElementString("OrderDate", "2024-12-01");

            // Добавляем товары для второго заказа
            writer.WriteStartElement("Items");

            AddItem(writer, "Куртка", 3, 300.75);
            AddItem(writer, "Штаны", 5, 250.00);

            writer.WriteEndElement(); // Закрываем Items
            writer.WriteEndElement(); // Закрываем Order

            // Конец документа
            writer.WriteEndElement(); // Закрываем Orders
            writer.WriteEndDocument();
        }

        Console.WriteLine($"XML файл успешно создан: {outputPath}");
    }

    static void AddItem(XmlTextWriter writer, string itemName, int quantity, double price)
    {
        writer.WriteStartElement("Item");
        writer.WriteElementString("Name", itemName);
        writer.WriteElementString("Quantity", quantity.ToString());
        writer.WriteElementString("Price", price.ToString("F2"));
        writer.WriteEndElement();
    }
}