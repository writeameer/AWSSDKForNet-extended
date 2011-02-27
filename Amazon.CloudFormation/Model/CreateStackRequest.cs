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

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.CloudFormation.Model
{
    /// <summary>
    /// Container for the parameters to the CreateStack operation.
    /// <para> Creates a stack as specified in the template. Once the call
    /// completes successfully, the stack creation starts. You can check the
    /// status of the stack via the DescribeStacks API. </para> <para> AWS
    /// CloudFormation allows you to create and delete related AWS resources
    /// together as a unit called a stack. You define the characteristics of a
    /// stack using a template, which is a JSON-compliant text file. For more
    /// information, go to the AWS CloudFormation User Guide. </para>
    /// <para><b>NOTE:</b> Currently, the quota for stacks is 20 per region.
    /// </para>
    /// </summary>
    /// <seealso cref="Amazon.CloudFormation.AmazonCloudFormation.CreateStack"/>
    public class CreateStackRequest : AmazonWebServiceRequest
    {
        private string stackName;
        private string templateBody;
        private string templateURL;
        private List<Parameter> parameters = new List<Parameter>();
        private bool? disableRollback;
        private int? timeoutInMinutes;
        private List<string> notificationARNs = new List<string>();

        /// <summary>
        /// The name associated with the Stack. The name must be unique within your AWS account. <note> Must contain only alphanumeric characters (case
        /// sensitive) and start with an alpha character. Maximum length of the name is 255 characters. </note>
        ///  
        /// </summary>
        public string StackName
        {
            get { return this.stackName; }
            set { this.stackName = value; }
        }

        /// <summary>
        /// Sets the StackName property
        /// </summary>
        /// <param name="stackName">The value to set for the StackName property </param>
        /// <returns>this instance</returns>
        public CreateStackRequest WithStackName(string stackName)
        {
            this.stackName = stackName;
            return this;
        }
            

        // Check to see if StackName property is set
        internal bool IsSetStackName()
        {
            return this.stackName != null;       
        }

        /// <summary>
        /// Structure containing the template body. (For more information, go to the <a
        /// href="http://docs.amazonwebservices.com/AWSCloudFormation/latest/CFNGuide">AWS CloudFormation User Guide</a>.) <note> You must pass
        /// <c>TemplateBody</c> or <c>TemplateURL</c>. If both are passed, only <c>TemplateBody</c> is used. </note>
        ///  
        /// <para>
        /// <b>Constraints:</b>
        /// <list type="definition">
        ///     <item>
        ///         <term>Length</term>
        ///         <description>1 - </description>
        ///     </item>
        /// </list>
        /// </para>
        /// </summary>
        public string TemplateBody
        {
            get { return this.templateBody; }
            set { this.templateBody = value; }
        }

        /// <summary>
        /// Sets the TemplateBody property
        /// </summary>
        /// <param name="templateBody">The value to set for the TemplateBody property </param>
        /// <returns>this instance</returns>
        public CreateStackRequest WithTemplateBody(string templateBody)
        {
            this.templateBody = templateBody;
            return this;
        }
            

        // Check to see if TemplateBody property is set
        internal bool IsSetTemplateBody()
        {
            return this.templateBody != null;       
        }

        /// <summary>
        /// Location of file containing the template body. (For more information, go to the <a
        /// href="http://docs.amazonwebservices.com/AWSCloudFormation/latest/CFNGuide">AWS CloudFormation User Guide</a>.) <note> You must pass
        /// <c>TemplateURL</c> or <c>TemplateBody</c>. If both are passed, only <c>TemplateBody</c> is used. </note>
        ///  
        /// <para>
        /// <b>Constraints:</b>
        /// <list type="definition">
        ///     <item>
        ///         <term>Length</term>
        ///         <description>1 - 1024</description>
        ///     </item>
        /// </list>
        /// </para>
        /// </summary>
        public string TemplateURL
        {
            get { return this.templateURL; }
            set { this.templateURL = value; }
        }

        /// <summary>
        /// Sets the TemplateURL property
        /// </summary>
        /// <param name="templateURL">The value to set for the TemplateURL property </param>
        /// <returns>this instance</returns>
        public CreateStackRequest WithTemplateURL(string templateURL)
        {
            this.templateURL = templateURL;
            return this;
        }
            

        // Check to see if TemplateURL property is set
        internal bool IsSetTemplateURL()
        {
            return this.templateURL != null;       
        }

        /// <summary>
        /// A list of <c>Parameter</c> structures.
        ///  
        /// </summary>
        public List<Parameter> Parameters
        {
            get { return this.parameters; }
            set { this.parameters = value; }
        }
        /// <summary>
        /// Adds elements to the Parameters collection
        /// </summary>
        /// <param name="parameters">The values to add to the Parameters collection </param>
        /// <returns>this instance</returns>
        public CreateStackRequest WithParameters(params Parameter[] parameters)
        {
            foreach (Parameter element in parameters)
            {
                this.parameters.Add(element);
            }

            return this;
        }
        
        /// <summary>
        /// Adds elements to the Parameters collection
        /// </summary>
        /// <param name="parameters">The values to add to the Parameters collection </param>
        /// <returns>this instance</returns>
        public CreateStackRequest WithParameters(IEnumerable<Parameter> parameters)
        {
            foreach (Parameter element in parameters)
            {
                this.parameters.Add(element);
            }

            return this;
        }

        // Check to see if Parameters property is set
        internal bool IsSetParameters()
        {
            return this.parameters.Count > 0;       
        }

        /// <summary>
        /// Boolean to enable or disable rollback on stack creation failures. Default: <c>false</c>
        ///  
        /// </summary>
        public bool DisableRollback
        {
            get { return this.disableRollback ?? default(bool); }
            set { this.disableRollback = value; }
        }

        /// <summary>
        /// Sets the DisableRollback property
        /// </summary>
        /// <param name="disableRollback">The value to set for the DisableRollback property </param>
        /// <returns>this instance</returns>
        public CreateStackRequest WithDisableRollback(bool disableRollback)
        {
            this.disableRollback = disableRollback;
            return this;
        }
            

        // Check to see if DisableRollback property is set
        internal bool IsSetDisableRollback()
        {
            return this.disableRollback.HasValue;       
        }

        /// <summary>
        /// If the time limit is exceeded, the stack is marked CREATE_FAILED; if <c>RollbackOnFailure</c> is set, the stack will be rolled back.
        ///  
        /// <para>
        /// <b>Constraints:</b>
        /// <list type="definition">
        ///     <item>
        ///         <term>Range</term>
        ///         <description>1 - </description>
        ///     </item>
        /// </list>
        /// </para>
        /// </summary>
        public int TimeoutInMinutes
        {
            get { return this.timeoutInMinutes ?? default(int); }
            set { this.timeoutInMinutes = value; }
        }

        /// <summary>
        /// Sets the TimeoutInMinutes property
        /// </summary>
        /// <param name="timeoutInMinutes">The value to set for the TimeoutInMinutes property </param>
        /// <returns>this instance</returns>
        public CreateStackRequest WithTimeoutInMinutes(int timeoutInMinutes)
        {
            this.timeoutInMinutes = timeoutInMinutes;
            return this;
        }
            

        // Check to see if TimeoutInMinutes property is set
        internal bool IsSetTimeoutInMinutes()
        {
            return this.timeoutInMinutes.HasValue;       
        }

        /// <summary>
        /// The Simple Notification Service (SNS) topic ARNs to publish stack related events. You can find your SNS topic ARNs using the <a
        /// href="http://console.aws.amazon.com/sns">SNS console</a> or your Command Line Interface (CLI).
        ///  
        /// <para>
        /// <b>Constraints:</b>
        /// <list type="definition">
        ///     <item>
        ///         <term>Length</term>
        ///         <description>0 - 5</description>
        ///     </item>
        /// </list>
        /// </para>
        /// </summary>
        public List<string> NotificationARNs
        {
            get { return this.notificationARNs; }
            set { this.notificationARNs = value; }
        }
        /// <summary>
        /// Adds elements to the NotificationARNs collection
        /// </summary>
        /// <param name="notificationARNs">The values to add to the NotificationARNs collection </param>
        /// <returns>this instance</returns>
        public CreateStackRequest WithNotificationARNs(params string[] notificationARNs)
        {
            foreach (string element in notificationARNs)
            {
                this.notificationARNs.Add(element);
            }

            return this;
        }
        
        /// <summary>
        /// Adds elements to the NotificationARNs collection
        /// </summary>
        /// <param name="notificationARNs">The values to add to the NotificationARNs collection </param>
        /// <returns>this instance</returns>
        public CreateStackRequest WithNotificationARNs(IEnumerable<string> notificationARNs)
        {
            foreach (string element in notificationARNs)
            {
                this.notificationARNs.Add(element);
            }

            return this;
        }

        // Check to see if NotificationARNs property is set
        internal bool IsSetNotificationARNs()
        {
            return this.notificationARNs.Count > 0;       
        }
    }
}
    
