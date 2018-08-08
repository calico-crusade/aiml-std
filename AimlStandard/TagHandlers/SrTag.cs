using System.Xml;

namespace AimlStandard.TagHandlers
{
    using Utilities;

    /// <summary>
    /// The sr element is a shortcut for: 
    /// 
    /// <srai><star/></srai> 
    /// 
    /// The atomic sr does not have any content. 
    /// </summary>
    public class SrTag : AIMLTagHandler
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="bot">The bot involved in this request</param>
        /// <param name="user">The user making the request</param>
        /// <param name="request">The request inputted into the system</param>
        /// <param name="result">The result to be passed to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public SrTag (Bot bot, 
                        User user, 
                        Request request, 
                        Result result, 
                        XmlNode templateNode)
        : base (bot,user,request,result,templateNode)
        {}

        protected override string ProcessChange()
        {
            if (this.templateNode.Name.ToLower() == "sr")
            {
                XmlNode starNode = GetNode("<star/>");
                StarTag recursiveStar = new StarTag(this.Bot, this.User, this.Request, this.Result, starNode);
                string starContent = recursiveStar.Transform();

                XmlNode sraiNode = GetNode("<srai>"+starContent+"</srai>");
                SraiTag sraiHandler = new SraiTag(this.Bot, this.User, this.Request, this.Result, sraiNode);
                return sraiHandler.Transform();
            }
            return string.Empty;
        }
    }
}
