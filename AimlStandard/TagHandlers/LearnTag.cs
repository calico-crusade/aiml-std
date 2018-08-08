using System.Xml;
using System.IO;

namespace AimlStandard.TagHandlers
{
    using Utilities;

    /// <summary>
    /// The learn element instructs the AIML interpreter to retrieve a resource specified by a URI, 
    /// and to process its AIML object contents.
    /// </summary>
    public class LearnTag : AIMLTagHandler
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="bot">The bot involved in this request</param>
        /// <param name="user">The user making the request</param>
        /// <param name="request">The request inputted into the system</param>
        /// <param name="result">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public LearnTag(Bot bot, 
                        User user, 
                        Request request, 
                        Result result, 
                        XmlNode templateNode)
        : base (bot,user,request,result,templateNode)
        {}

        protected override string ProcessChange()
        {
            if (this.templateNode.Name.ToLower() == "learn")
            {
                // currently only AIML files in the local filesystem can be referenced
                // ToDo: Network HTTP and web service based learning
                if (this.templateNode.InnerText.Length > 0)
                {
                    string path = this.templateNode.InnerText;
                    FileInfo fi = new FileInfo(path);
                    if (fi.Exists)
                    {
                        XmlDocument doc = new XmlDocument();
                        try
                        {
                            doc.Load(path);
                            this.Bot.LoadAIMLFromXML(doc, path);
                        }
                        catch
                        {
                            this.Bot.WriteToLog("ERROR! Attempted (but failed) to <learn> some new AIML from the following URI: " + path);
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}
