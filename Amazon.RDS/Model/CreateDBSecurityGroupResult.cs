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
 *  API Version: 2009-10-16
 */

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;

namespace Amazon.RDS.Model
{
    /// <summary>
    /// Details for a particular database security group.
    /// </summary>
    [XmlRootAttribute(Namespace = "http://rds.amazonaws.com/admin/2009-10-16/", IsNullable = false)]
    public class CreateDBSecurityGroupResult
    {
        private DBSecurityGroup DBSecurityGroupField;

        /// <summary>
        /// Gets and sets the DBSecurityGroup property.
        /// </summary>
        [XmlElementAttribute(ElementName = "DBSecurityGroup")]
        public DBSecurityGroup DBSecurityGroup
        {
            get { return this.DBSecurityGroupField ; }
            set { this.DBSecurityGroupField = value; }
        }

        /// <summary>
        /// Sets the DBSecurityGroup property
        /// </summary>
        /// <param name="DBSecurityGroup">DBSecurityGroup property</param>
        /// <returns>this instance</returns>
        public CreateDBSecurityGroupResult WithDBSecurityGroup(DBSecurityGroup DBSecurityGroup)
        {
            this.DBSecurityGroupField = DBSecurityGroup;
            return this;
        }

        /// <summary>
        /// Checks if DBSecurityGroup property is set
        /// </summary>
        /// <returns>true if DBSecurityGroup property is set</returns>
        public bool IsSetDBSecurityGroup()
        {
            return this.DBSecurityGroupField != null;
        }

    }
}
