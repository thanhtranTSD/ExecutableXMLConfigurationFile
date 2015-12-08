using PostBuildEventer.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace PostBuildEventer.Factories
{
    public class ActionFactory
    {
        #region Public Static Function
        public static ActionFactory Instance()
        {
            if (null == s_DefinitionActionFactory)
            {
                s_DefinitionActionFactory = new ActionFactory();
            }

            return s_DefinitionActionFactory;
        }
        #endregion

        #region Constructors
        private ActionFactory()
        {
            m_RegisterActionFactory = new Dictionary<string, Type>();
        }
        #endregion Constructors

        #region Public Functions
        public void Initialize()
        {
            IEnumerable<Type> result = FindDerivedTypesFromAssembly(Assembly.GetEntryAssembly(), typeof(IAction), false);
            foreach (Type type in result)
            {
                m_RegisterActionFactory.Add(type.Name, type);
            }
        }

        public IAction CreateActionInstance(string actionName, XmlNode actionNode, bool overwrite)
        {
            IAction returnAction = null;

            if (true == m_RegisterActionFactory.ContainsKey(actionName))
            {
                Type actionType = m_RegisterActionFactory[actionName];
                IAction typeOfAction = (IAction)Activator.CreateInstance(actionType, actionNode, overwrite);
                returnAction = typeOfAction;
            }

            return returnAction;
        }
        #endregion Public Functions

        #region Private Functions
        private static IEnumerable<Type> FindDerivedTypesFromAssembly(Assembly assembly, Type baseType, bool classOnly)
        {
            if (null == assembly)
            {
                throw new ArgumentNullException("assembly", "Assembly must be defined");
            }
            if (null == baseType)
            {
                throw new ArgumentNullException("baseType", "Parent Type must be defined");
            }
            // Get all the types
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                // If classOnly, it must be a class
                if ((true == classOnly) && (false == type.IsClass))
                    continue;
                if (true == baseType.IsInterface)
                {
                    Type interfaceType = type.GetInterface(baseType.FullName);
                    if (null != interfaceType)
                        // Add it to result list
                        yield return type;
                }
                else if (true == type.IsSubclassOf(baseType))
                {
                    // Add it to result list
                    yield return type;
                }
            }
        }
        #endregion

        #region Members
        private Dictionary<string, Type> m_RegisterActionFactory;
        private static ActionFactory s_DefinitionActionFactory;
        #endregion Members

    }
}
