/*******************************************************************************
 * Copyright 2008-2010 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 * Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 * this file except in compliance with the License. A copy of the License is located at
 *
 * http://aws.amazon.com/apache2.0
 *
 * or in the "license" file accompanying this file. This file is distributed on
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
 * or implied. See the License for the specific language governing permissions and
 * limitations under the License.
 * *****************************************************************************
 *    __  _    _  ___
 *   (  )( \/\/ )/ __)
 *   /__\ \    / \__ \
 *  (_)(_) \/\/  (___/
 *
 *  AWS SDK for .NET
 *  API Version: 2009-03-31
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Text;

namespace Amazon.ElasticMapReduce.Model
{
    /// <summary>
    /// Returns a job flow ID for a successful request and information about the request.
    /// </summary>
    [XmlRootAttribute(Namespace = "http://elasticmapreduce.amazonaws.com/doc/2009-03-31", IsNullable = false)]
    public class RunJobFlowResponse
    {
        private RunJobFlowResult runJobFlowResultField;
        private ResponseMetadata responseMetadataField;

        /// <summary>
        /// Gets and sets the RunJobFlowResult property.
        /// Returns a job flow ID.
        /// </summary>
        [XmlElementAttribute(ElementName = "RunJobFlowResult")]
        public RunJobFlowResult RunJobFlowResult
        {
            get { return this.runJobFlowResultField ; }
            set { this.runJobFlowResultField = value; }
        }

        /// <summary>
        /// Sets the RunJobFlowResult property
        /// </summary>
        /// <param name="runJobFlowResult">Returns a job flow ID.</param>
        /// <returns>this instance</returns>
        public RunJobFlowResponse WithRunJobFlowResult(RunJobFlowResult runJobFlowResult)
        {
            this.runJobFlowResultField = runJobFlowResult;
            return this;
        }

        /// <summary>
        /// Checks if RunJobFlowResult property is set
        /// </summary>
        /// <returns>true if RunJobFlowResult property is set</returns>
        public bool IsSetRunJobFlowResult()
        {
            return this.runJobFlowResultField != null;
        }

        /// <summary>
        /// Gets and sets the ResponseMetadata property.
        /// Information about the request.
        /// </summary>
        [XmlElementAttribute(ElementName = "ResponseMetadata")]
        public ResponseMetadata ResponseMetadata
        {
            get { return this.responseMetadataField ; }
            set { this.responseMetadataField = value; }
        }

        /// <summary>
        /// Sets the ResponseMetadata property
        /// </summary>
        /// <param name="responseMetadata">Information about the request.</param>
        /// <returns>this instance</returns>
        public RunJobFlowResponse WithResponseMetadata(ResponseMetadata responseMetadata)
        {
            this.responseMetadataField = responseMetadata;
            return this;
        }

        /// <summary>
        /// Checks if ResponseMetadata property is set
        /// </summary>
        /// <returns>true if ResponseMetadata property is set</returns>
        public bool IsSetResponseMetadata()
        {
            return this.responseMetadataField != null;
        }

        /// <summary>
        /// XML Representation for this object
        /// </summary>
        /// <returns>XML String</returns>
        public string ToXML()
        {
            StringBuilder xml = new StringBuilder(1024);
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(this.GetType());
            using (StringWriter sw = new StringWriter(xml))
            {
                serializer.Serialize(sw, this);
            }
            return xml.ToString();
        }
    }
}
