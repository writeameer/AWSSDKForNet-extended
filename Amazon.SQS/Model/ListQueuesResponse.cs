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
 *  API Version: 2009-02-01
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Text;

namespace Amazon.SQS.Model
{
    /// <summary>
    /// Returns a list of queues and related metadata about the request.
    /// </summary>
    [XmlRootAttribute(Namespace = "http://queue.amazonaws.com/doc/2009-02-01/", IsNullable = false)]
    public class ListQueuesResponse
    {
        private ListQueuesResult listQueuesResultField;
        private ResponseMetadata responseMetadataField;

        /// <summary>
        /// Gets and sets the ListQueuesResult property.
        /// Information returned by the ListQueuesRequest, including queue URL.
        /// </summary>
        [XmlElementAttribute(ElementName = "ListQueuesResult")]
        public ListQueuesResult ListQueuesResult
        {
            get { return this.listQueuesResultField ; }
            set { this.listQueuesResultField = value; }
        }

        /// <summary>
        /// Sets the ListQueuesResult property
        /// </summary>
        /// <param name="listQueuesResult">Information returned by the ListQueuesRequest, including queue URL.</param>
        /// <returns>this instance</returns>
        public ListQueuesResponse WithListQueuesResult(ListQueuesResult listQueuesResult)
        {
            this.listQueuesResultField = listQueuesResult;
            return this;
        }

        /// <summary>
        /// Checks if ListQueuesResult property is set
        /// </summary>
        /// <returns>true if ListQueuesResult property is set</returns>
        public bool IsSetListQueuesResult()
        {
            return this.listQueuesResultField != null;
        }

        /// <summary>
        /// Gets and sets the ResponseMetadata property.
        /// Information about the request provided by Amazon SQS.
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
        /// <param name="responseMetadata">Information about the request provided by Amazon SQS.</param>
        /// <returns>this instance</returns>
        public ListQueuesResponse WithResponseMetadata(ResponseMetadata responseMetadata)
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
