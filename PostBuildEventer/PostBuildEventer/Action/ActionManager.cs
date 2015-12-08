using PostBuildEventer.XML;
using PostBuildEventer.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PostBuildEventer.Action
{
    static class ActionManager
    {
        #region Public Function
        public static void ExecuteAllAction(XmlDocument xmlDocument, bool overwrite)
        {
            // Select the Action node in the XML file
            XmlNodeList actionList = xmlDocument.SelectNodes(XmlConstants.XML_ACTION_PATH);
            foreach (XmlNode actionNode in actionList)
            {
                string actionName = GetActionName(actionNode);
                string actionType = GetActionType(actionNode);
                IAction action = ActionFactory.Instance().CreateActionInstance(actionType, actionNode, overwrite);

                PrintActionStart(actionName);
                action.Execute();
                PrintActionSuccess(actionName);
            }
        }
        #endregion

        #region Private Function
        private static string GetActionName(XmlNode actionNode)
        {
            return actionNode.Attributes.GetNamedItem(XmlConstants.XML_NAME_ATTRIBUTE).Value;
        }

        private static string GetActionType(XmlNode actionNode)
        {
            string returnType = string.Empty;
            string actionType = actionNode.Attributes.GetNamedItem(XmlConstants.XML_TYPE_ATTRIBUTE).Value;
            if (null != actionType)
            {
                actionType = actionType.Substring(actionType.LastIndexOf(XmlConstants.PATH_SEPARATOR_CHAR) + 1);
                returnType = actionType.Replace("Model", "");
            }

            return returnType;
        }

        private static void PrintActionStart(string actionName)
        {
            Console.WriteLine(String.Format("Execute {0}", actionName));
        }

        private static void PrintActionSuccess(string actionName)
        {
            Console.WriteLine(String.Format("Action {0} success.", actionName));
            Console.WriteLine();
        }
        #endregion
    }
}
