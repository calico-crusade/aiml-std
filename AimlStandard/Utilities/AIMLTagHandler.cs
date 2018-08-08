using System.Xml;

namespace AimlStandard.Utilities
{
    /// <summary>
    /// The template for all classes that handle the AIML tags found within template nodes of a
    /// category.
    /// </summary>
    abstract public class AIMLTagHandler : TextTransformer
    { 
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="bot">The bot involved in this request</param>
        /// <param name="user">The user making the request</param>
        /// <param name="request">The request itself</param>
        /// <param name="result">The result to be passed back to the user</param>
        /// <param name="templateNode">The node to be processed</param>
        public AIMLTagHandler   (   Bot bot, 
                                    User user, 
                                    Request request, 
                                    Result result, 
                                    XmlNode templateNode) :base(bot,templateNode.OuterXml)
        {
            this.User = user;
            this.Request = request;
            this.Result = result;
            this.templateNode = templateNode;
        }

        /// <summary>
        /// Default ctor to use when late binding
        /// </summary>
        public AIMLTagHandler()
        {
        }

        /// <summary>
        /// A representation of the user who made the request
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// A representation of the input into the bot made by the user
        /// </summary>
        public Request Request { get; set; }

        /// <summary>
        /// A representation of the result to be returned to the user
        /// </summary>
        public Result Result { get; set; }

        /// <summary>
        /// The template node to be processed by the class
        /// </summary>
        public XmlNode templateNode { get; set; }

        #region Helper methods

        /// <summary>
        /// Helper method that turns the passed string into an XML node
        /// </summary>
        /// <param name="outerXML">the string to XMLize</param>
        /// <returns>The XML node</returns>
        public static XmlNode GetNode(string outerXML)
        {
            XmlDocument temp = new XmlDocument();
            temp.LoadXml(outerXML);
            return temp.FirstChild;
        }
        #endregion
    }
}
