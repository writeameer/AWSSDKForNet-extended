/*******************************************************************************
 *  Copyright 2008-2010 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *  Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *  this file except in compliance with the License. A copy of the License is located at
 *
 *  http://aws.amazon.com/apache2.0
 *
 *  or in the "license" file accompanying this file.
 *  This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *  CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *  specific language governing permissions and limitations under the License.
 * *****************************************************************************
 *    __  _    _  ___
 *   (  )( \/\/ )/ __)
 *   /__\ \    / \__ \
 *  (_)(_) \/\/  (___/
 *
 *  AWS SDK for .NET
 *  API Version: 2006-03-01
 *
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Amazon.S3.Model
{
    /// <summary>
    /// The GetBucketPolicyResponse contains the JSON string representation of the Aspen policy
    /// any headers returned by S3.
    /// </summary>
    public class GetBucketPolicyResponse : S3Response
    {
        #region Private Members

        private string aspenPolicy;

        #endregion

        /// <summary>
        /// Gets and sets the AspenPolicy property.
        /// </summary>
        public String AspenPolicy
        {
            get
            {
                return this.aspenPolicy;
            }
            set
            {
                this.aspenPolicy = value;
            }
        }

        /// <summary>
        /// The request to get the Aspen policy is return as the content
        /// body of the response.
        /// </summary>
        /// <param name="responseBody">The Aspen policy</param>
        internal override void ProcessResponseBody(string responseBody)
        {
            this.AspenPolicy = responseBody;
        }
    }
}