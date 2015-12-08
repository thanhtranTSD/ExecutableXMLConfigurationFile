using System.Xml;

namespace PostBuildEventer.XML
{
    static class XMLManager
    {
        #region Public Functions
        /// <summary>
        /// Load all the actions in the XML file.
        /// </summary>
        public static XmlDocument LoadXmlFile(string filePath)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);
            return xmlDocument;
        }
        #endregion
    }
}
