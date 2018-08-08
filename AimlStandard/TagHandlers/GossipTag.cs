using System.Xml;

namespace AimlStandard.TagHandlers
{
    using Utilities;

    /// <summary>
    /// The gossip element instructs the AIML interpreter to capture the result of processing the 
    /// contents of the gossip elements and to store these contents in a manner left up to the 
    /// implementation. Most common uses of gossip have been to store captured contents in a separate 
    /// file. 
    /// 
    /// The gossip element does not have any attributes. It may contain any AIML template elements.
    /// </summary>
    public class GossipTag : AIMLTagHandler
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="bot">The bot involved in this request</param>
        /// <param name="user">The user making the request</param>
        /// <param name="request">The request inputted into the system</param>
        /// <param name="result">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public GossipTag(Bot bot, 
                         User user, 
                         Request request, 
                         Result result, 
                         XmlNode templateNode)
        : base (bot,user,request,result,templateNode)
        {}

        protected override string ProcessChange()
        {
            if (this.templateNode.Name.ToLower() == "gossip")
            {
                // gossip is merely logged by the bot and written to log files
                if (this.templateNode.InnerText.Length > 0)
                {
                    this.Bot.WriteToLog("GOSSIP from user: "+this.User.UserID+", '"+this.templateNode.InnerText+"'");
                }
            }
            return string.Empty;
        }
    }
}
