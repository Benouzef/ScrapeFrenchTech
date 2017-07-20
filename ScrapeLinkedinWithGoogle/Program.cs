using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ScrapeLinkedinWithGoogle
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlDocument doc = new HtmlDocument();
            string cmdlineArg = "qa+manager+paris+site:fr.linkedin.com/in";
            //cmdlineArg = "pmo+freelance+brussels+site:be.linkedin.com/in";
            //cmdlineArg = "perl+freelance+paris+site:fr.linkedin.com/in";
            //cmdlineArg = "java+oracle+freelance+paris+site:fr.linkedin.com/in";
            //cmdlineArg = "ssis+paris+site:fr.linkedin.com/in";
            //cmdlineArg = "c%23+asp+.net+developpeur+paris+site:fr.linkedin.com/in";
            //cmdlineArg = "php+integration+open+source+freelance+paris+site:fr.linkedin.com/in";
            //cmdlineArg = "ruby+rails+paris+site:fr.linkedin.com/in";
            //cmdlineArg = "developpeur+.net+senior+paris+freelance+site:fr.linkedin.com/in";
            //cmdlineArg = "mongodb+site:fr.linkedin.com/in";
            //cmdlineArg = "developpeur+.net+paris+site:fr.linkedin.com/in";
            //cmdlineArg = "react+paris+site:fr.linkedin.com/in&filter=0";
            //cmdlineArg = "angular+paris+site:fr.linkedin.com/in&filter=0";
            //cmdlineArg = "freelance+alfresco+belgium+site:linkedin.com/in&filter=0";
            cmdlineArg = "freelance+alfresco+luxembourg+site:linkedin.com/in&filter=0";

            if (args.Length > 0)
            {
                Console.WriteLine(args[0]);
                cmdlineArg = args[0];
            } 

            WebClient wc = new WebClient();
            wc.Encoding = Encoding.Default;

            string str = "pageNumber;url;name;region;role;company;title;description;search\r\n";

            StringBuilder sb = new StringBuilder();
            sb.Append(str);

            for (int i = 0; i <= 490; i = i + 10)
            {
                string searchString = "https://www.google.fr/search?q=" + cmdlineArg + "&start=" + i;
                string s = wc.DownloadString(searchString);

                int hasSleptThreeTimes = 0;
                while (s.Contains("Aucun document ne correspond aux termes de recherche"))
                {
                    if (hasSleptThreeTimes == 3)
                    {
                        break;
                    }

                    Thread.Sleep(10000);
                    s = wc.DownloadString(searchString);
                    hasSleptThreeTimes++;
                }

                if (hasSleptThreeTimes == 3)
                {
                    break;
                }

                Console.WriteLine(doc.Encoding);
                doc.DetectEncodingHtml(s);
                Console.WriteLine(doc.Encoding);
                doc.LoadHtml(s);
                str = ProcessPage(doc, i, searchString);
                sb.Append(str);

                Random rnd = new Random();
                int wait = rnd.Next(10, 19);
                int sleep = int.Parse((wait * 10000 * 0.1).ToString());

                Console.WriteLine(sleep);
                Thread.Sleep(sleep);
            }

            System.IO.File.WriteAllText(@"C:\Temp\ProspectsLinkedin_" + cmdlineArg.Replace("/", " ").Replace("&", " ").Replace("+", " ").Replace(":", " ") + "_.csv", sb.ToString(), Encoding.UTF8);

        }

        private static string ProcessPage(HtmlDocument doc, int pageNumber, string searchString)
        {
            string str = string.Empty;

            if (doc.DocumentNode.SelectNodes("//div[@class='g']") != null)
            {


                foreach (HtmlNode n in doc.DocumentNode.SelectNodes("//div[@class='g']"))
                {
                    string name = n.SelectSingleNode(".//a").InnerText;
                    name = name.Replace(" | LinkedIn", "");
                    string url = n.SelectSingleNode(".//cite").InnerText;
                    url = url.Replace("https://fr.", "https://www.");


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

                    string[] splittedTab = subtitle.Replace(" - ", "|").Split('|');
                    string region = string.Empty;
                    string role = string.Empty;
                    string company = string.Empty;
                    if (splittedTab.Length == 3)
                    {
                        region = splittedTab[0].Trim();
                        role = splittedTab[1].Trim();
                        company = splittedTab[2].Trim();
                    }

                    Console.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}\r\n", pageNumber, url, name, region, role, company, subtitle, description, searchString));
                    str += string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}\r\n", pageNumber, url, name, region, role, company, subtitle, description, searchString);
                    //str += string.Format("{0};{1};{2};{3};{4};{5};{6};{7}\r\n", url, companyname, companydescription, applicationType, website, coordonnees, contact, rawmail);
                }
            }
            return str;
        }

}
}
