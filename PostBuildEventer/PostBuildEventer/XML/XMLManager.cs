﻿/*
<License>
Copyright 2015 Virtium Technology
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
    http ://www.apache.org/licenses/LICENSE-2.0
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
</License>
 */

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
