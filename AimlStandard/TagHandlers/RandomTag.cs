using System;
using System.Xml;
using System.Collections;

namespace AimlStandard.TagHandlers
{
    using Utilities;

    /// <summary>
    /// The random element instructs the AIML interpreter to return exactly one of its contained li 
    /// elements randomly. The random element must contain one or more li elements of type 
    /// defaultListItem, and cannot contain any other elements.
    /// </summary>
    public class RandomTag : AIMLTagHandler
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="bot">The bot involved in this request</param>
        /// <param name="user">The user making the request</param>
        /// <param name="request">The request inputted into the system</param>
        /// <param name="result">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public RandomTag(Bot bot, 
                        User user, 
                        Request request, 
                        Result result, 
                        XmlNode templateNode)
        : base (bot,user,request,result,templateNode)
        {}

        protected override string ProcessChange()
        {
            if (this.templateNode.Name.ToLower() == "random")
            {
                if (this.templateNode.HasChildNodes)
                {
                    // only grab <li> nodes
                    ArrayList listNodes = new ArrayList();
                    foreach (XmlNode childNode in this.templateNode.ChildNodes)
                    {
                        if (childNode.Name == "li")
                        {
                            listNodes.Add(childNode);
                        }
                    }
                    if (listNodes.Count > 0)
                    {
                        Random r = new Random();
                        XmlNode chosenNode = (XmlNode)listNodes[r.Next(listNodes.Count-1)];
                        return chosenNode.InnerText;
                    }
                }
            }
            return string.Empty;
        }
    }
}
