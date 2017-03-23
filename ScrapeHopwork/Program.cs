using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ScrapeHopwork
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlDocument doc = new HtmlDocument();
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;


            doc.LoadHtml(wc.DownloadString("https://www.hopwork.fr/s?q=Java&fam=developers_datascientists&f-fam=developers_datascientists&f-cat=backend_developer&sortByRate=true&exp=ENTRY&p=1"));

            string str = ProcessPage(doc);

            StringBuilder sb = new StringBuilder();
            sb.Append(str);

            doc.LoadHtml(wc.DownloadString("https://www.hopwork.fr/s?q=Java&fam=developers_datascientists&f-fam=developers_datascientists&f-cat=backend_developer&sortByRate=true&exp=ENTRY&p=2"));
            str = ProcessPage(doc);
            sb.Append(str);

            System.IO.File.WriteAllText(@"C:\Temp\ProspectsHopwork.csv", sb.ToString(), Encoding.UTF8);

        }

        private static string ProcessPage(HtmlDocument doc)
        {
            string str = string.Empty;

            foreach (HtmlNode n in doc.DocumentNode.SelectNodes("//article[@class='profile-card freelance-linkable ']"))
            //foreach (HtmlNode n in doc.DocumentNode.SelectNodes("//section"))
            {

                HtmlDocument profileCard = new HtmlDocument();
                profileCard.LoadHtml(n.InnerHtml);


                //ul avec CSS "skill-list cropped jsinit"

                //p avec CSS "profile-card__fullname"
                foreach (HtmlNode node in profileCard.DocumentNode.SelectNodes("//p"))
                {
                    Console.WriteLine(node.InnerText);
                }
                //string name = HttpUtility.HtmlDecode(profileCard.DocumentNode.SelectSingleNode("//p[@class='profile-card__fullname'").InnerText.Trim());

                //p avec CSS "profile-card__location"
                //string location = HttpUtility.HtmlDecode(profileCard.DocumentNode.SelectSingleNode("//p[@class='profile-card__location']").InnerText.Trim());

                /*
                string url = profileCard.DocumentNode.SelectSingleNode("//a[@class='blanklink']").Attributes["href"].Value;
                string vote = profileCard.DocumentNode.SelectSingleNode("//div[@class='alternate-votes-display']").InnerText;
                */

                //str += string.Format("{0};{1}\r\n", name, location); //, url, vote);
            }
            return str;
        }
    }
}
