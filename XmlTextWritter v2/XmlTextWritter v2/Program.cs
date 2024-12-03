using System;
using System.Xml;

class Program
{
    //задание №4
    static void Main(string[] args)
    {
        string xmlFile = "orders.xml";

        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFile);

            XmlNodeList orders = xmlDoc.GetElementsByTagName("Order");

            foreach (XmlNode order in orders)
            {
                Console.WriteLine($"Заказ № {order.Attributes["OrderId"].Value}");
                Console.WriteLine($"Имя клиента: {order["CustomerName"].InnerText}");
                Console.WriteLine($"Дата заказа: {order["OrderDate"].InnerText}");

                XmlNodeList items = order["Items"].ChildNodes;

                Console.WriteLine("Товары в заказе:");
                foreach (XmlNode item in items)
                {
                    Console.WriteLine($"- Название: {item["Name"].InnerText}, Количество: {item["Quantity"].InnerText}, Цена: {item["Price"].InnerText}");
                }
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        try
        {
            using (XmlTextReader reader = new XmlTextReader(xmlFile))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Order")
                    {
                        string orderId = reader.GetAttribute("OrderId");
                        Console.WriteLine($"Заказ № {orderId}");

                        reader.ReadToFollowing("CustomerName");
                        string customerName = reader.ReadElementContentAsString();
                        Console.WriteLine($"Имя клиента: {customerName}");

                        reader.ReadToFollowing("OrderDate");
                        string orderDate = reader.ReadElementContentAsString();
                        Console.WriteLine($"Дата заказа: {orderDate}");

                        reader.ReadToFollowing("Items");
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Item")
                            {
                                string itemName = reader.ReadElementContentAsString();
                                string quantity = reader.ReadElementContentAsString();
                                string price = reader.ReadElementContentAsString();

                                Console.WriteLine($"Товар: {itemName}, Количество: {quantity}, Цена: {price}");
                            }

                            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Items")
                            {
                                break;
                            }
                        }

                        Console.WriteLine();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении с помощью XmlTextReader: {ex.Message}");
        }
    }
}
