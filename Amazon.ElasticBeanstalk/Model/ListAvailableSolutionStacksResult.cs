/*
 * Copyright 2010-2011 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 * 
 *  http://aws.amazon.com/apache2.0
 * 
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

namespace Amazon.ElasticBeanstalk.Model
{
    /// <summary>
    /// <para>A list of available Amazon ElasticBeanstalk solution stacks.
    /// </para>
    /// </summary>
    public class ListAvailableSolutionStacksResult  
    {
        
        private List<string> solutionStacks = new List<string>();

        /// <summary>
        /// A list of available solution stacks.
        ///  
        /// </summary>
        public List<string> SolutionStacks
        {
            get { return this.solutionStacks; }
            set { this.solutionStacks = value; }
        }
        /// <summary>
        /// Adds elements to the SolutionStacks collection
        /// </summary>
        /// <param name="solutionStacks">The values to add to the SolutionStacks collection </param>
        /// <returns>this instance</returns>
        public ListAvailableSolutionStacksResult WithSolutionStacks(params string[] solutionStacks)
        {
            foreach (string element in solutionStacks)
            {
                this.solutionStacks.Add(element);
            }

            return this;
        }
        
        /// <summary>
        /// Adds elements to the SolutionStacks collection
        /// </summary>
        /// <param name="solutionStacks">The values to add to the SolutionStacks collection </param>
        /// <returns>this instance</returns>
        public ListAvailableSolutionStacksResult WithSolutionStacks(IEnumerable<string> solutionStacks)
        {
            foreach (string element in solutionStacks)
            {
                this.solutionStacks.Add(element);
            }

            return this;
        }

        // Check to see if SolutionStacks property is set
        internal bool IsSetSolutionStacks()
        {
            return this.solutionStacks.Count > 0;       
        }
    }
}
