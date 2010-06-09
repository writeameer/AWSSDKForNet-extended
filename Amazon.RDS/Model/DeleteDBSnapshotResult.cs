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
    /// A list of DB Snapshot data types.
    /// </summary>
    [XmlRootAttribute(Namespace = "http://rds.amazonaws.com/admin/2009-10-16/", IsNullable = false)]
    public class DeleteDBSnapshotResult
    {
        private DBSnapshot DBSnapshotField;

        /// <summary>
        /// Gets and sets the DBSnapshot property.
        /// </summary>
        [XmlElementAttribute(ElementName = "DBSnapshot")]
        public DBSnapshot DBSnapshot
        {
            get { return this.DBSnapshotField ; }
            set { this.DBSnapshotField = value; }
        }

        /// <summary>
        /// Sets the DBSnapshot property
        /// </summary>
        /// <param name="DBSnapshot">DBSnapshot property</param>
        /// <returns>this instance</returns>
        public DeleteDBSnapshotResult WithDBSnapshot(DBSnapshot DBSnapshot)
        {
            this.DBSnapshotField = DBSnapshot;
            return this;
        }

        /// <summary>
        /// Checks if DBSnapshot property is set
        /// </summary>
        /// <returns>true if DBSnapshot property is set</returns>
        public bool IsSetDBSnapshot()
        {
            return this.DBSnapshotField != null;
        }

    }
}
