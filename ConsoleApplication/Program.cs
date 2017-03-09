using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlDocument doc = new HtmlDocument();
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            

            doc.LoadHtml(wc.DownloadString("http://lespepitestech.com/french-tech-hub/paris-le-de-france"));
            ProcessPage(doc);

            for (int i = 1; i <= 132; i ++) // 132
            {
                doc.LoadHtml(wc.DownloadString("http://lespepitestech.com/french-tech-hub/paris-le-de-france?page=" + i));
                ProcessPage(doc);
            }

            Console.ReadLine();

        }

        private static void ProcessPage(HtmlDocument doc)
        {
            foreach (HtmlNode n in doc.DocumentNode.SelectNodes("//div[@class='startup-entry card']"))
            {

                HtmlDocument startupEntry = new HtmlDocument();
                startupEntry.LoadHtml(n.InnerHtml);

                string name = HttpUtility.HtmlDecode(startupEntry.DocumentNode.SelectSingleNode("//h3").InnerText.Trim());
                string description = HttpUtility.HtmlDecode(startupEntry.DocumentNode.SelectSingleNode("//div[@class='s-e-slogan-c']").InnerText.Trim());
                string url = startupEntry.DocumentNode.SelectSingleNode("//a[@class='blanklink']").Attributes["href"].Value;

                Console.WriteLine("{0};{1};{2}", name, description, url);
            }
        }
    }
}
