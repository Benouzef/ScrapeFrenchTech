using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ScrapeLinkedinWithGoogle
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlDocument doc = new HtmlDocument();

            WebClient wc = new WebClient();
            wc.Encoding = Encoding.Default;

            string str = "GooglePageNumber;url;Name;title;description\r\n";
            StringBuilder sb = new StringBuilder();
            sb.Append(str);

            for (int i = 0; i <= 300; i = i + 10)
            {
                string s = wc.DownloadString("https://www.google.fr/search?q=rci+banque+bank+paris+site:fr.linkedin.com/in&start=" + i);
                Console.WriteLine(doc.Encoding);
                doc.DetectEncodingHtml(s);
                Console.WriteLine(doc.Encoding);
                doc.LoadHtml(s);
                str = ProcessPage(doc, i);
                sb.Append(str);
            }

            System.IO.File.WriteAllText(@"C:\Temp\ProspectsLinkedin.csv", sb.ToString(), Encoding.UTF8);

        }

        private static string ProcessPage(HtmlDocument doc, int pageNumber)
        {
            string str = string.Empty;

            foreach (HtmlNode n in doc.DocumentNode.SelectNodes("//div[@class='g']"))
            {                
                string name = n.SelectSingleNode(".//a").InnerText;
                name = name.Replace(" | LinkedIn", "");
                string url = n.SelectSingleNode(".//cite").InnerText;
                

                HtmlNode subTitleNode = n.SelectSingleNode(".//div[@class='f slp']");
                string subtitle = string.Empty;
                if (subTitleNode != null)
                {
                    subtitle = HttpUtility.HtmlDecode(subTitleNode.InnerText);
                    
                }

                string description = n.SelectSingleNode(".//span[@class='st']").InnerText;
                description = HttpUtility.HtmlDecode(description);
                description = description.Replace(";", "|");
                description = description.Replace("\n", " ");

                /*
                string companyname = n.SelectSingleNode("./a/div/h3").InnerText.Trim().Replace(";", " ");
                string companydescription = n.SelectSingleNode("./a/div/p").InnerText.Trim().Replace(";", " ");
                //ul avec CSS "skill-list cropped jsinit"

                HtmlDocument docDetail = new HtmlDocument();
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                docDetail.LoadHtml(wc.DownloadString("http://www.techinfrance.fr" + url));

                HtmlNode article = docDetail.DocumentNode.SelectSingleNode("//article");
                string website = article.SelectSingleNode("//div[@class='box box-partner']//a").Attributes["href"].Value;



                //p avec CSS "profile-card__fullname"
                string coordonnees = string.Empty;
                foreach (HtmlNode address in article.SelectNodes("//div[@class='box-body']//p"))
                {
                    Console.WriteLine(address.InnerText);
                    coordonnees += address.InnerText.Replace('\r', ' ').Replace('\n', ' ').Replace(";", " ") + " ";
                }

                string contact = string.Empty;
                string rawmail = string.Empty;
                foreach (HtmlNode address in article.SelectNodes("//div[@class='box-body']//span"))
                {
                    Console.WriteLine(address.InnerText);

                    if (address.InnerText.Contains("E-mail"))
                    {
                        contact += " mail ";

                        string jsvar = address.InnerText.Replace("E-mail", "").Replace("//<![CDATA[", "").Replace("//]]>", "").Replace("document.write", "");

                        //string jsvar = System.IO.File.ReadAllText(@"C:\Temp\input.txt");

                        MB.JsEvaluator.Evaluator evaluator = new MB.JsEvaluator.Evaluator();


                        string result = string.Empty;

                        try
                        {
                            result = evaluator.Eval(jsvar);
                        }
                        catch (Exception)
                        {
                        }

                        if (result != string.Empty)
                        {
                            HtmlDocument mail = new HtmlDocument();
                            mail.LoadHtml(result);

                            // "this.href='mailto:elise@wiztopic.com'"

                            rawmail = mail.DocumentNode.SelectSingleNode("/a").Attributes["onmouseover"].Value;
                            rawmail = rawmail.Replace("this.href='mailto:", "").Replace("'", "");

                            Console.WriteLine(mail.DocumentNode.SelectSingleNode("/a").Attributes["onmouseover"].Value);
                        }
                    }
                    else
                    {
                        contact += address.InnerText.Replace('\r', ' ').Replace('\n', ' ').Replace(";", " ") + " ";
                    }
                }
                //string name = HttpUtility.HtmlDecode(profileCard.DocumentNode.SelectSingleNode("//p[@class='profile-card__fullname'").InnerText.Trim());

                //p avec CSS "profile-card__location"
                //string location = HttpUtility.HtmlDecode(profileCard.DocumentNode.SelectSingleNode("//p[@class='profile-card__location']").InnerText.Trim());

                /*
                
                string vote = profileCard.DocumentNode.SelectSingleNode("//div[@class='alternate-votes-display']").InnerText;
                */

                Console.WriteLine(string.Format("{0};{1};{2};{3};{4}\r\n", pageNumber, url, name, subtitle, description));
                str += string.Format("{0};{1};{2};{3};{4}\r\n", pageNumber, url, name, subtitle, description);
                //str += string.Format("{0};{1};{2};{3};{4};{5};{6};{7}\r\n", url, companyname, companydescription, applicationType, website, coordonnees, contact, rawmail);
            }
            return str;
        }

}
}
