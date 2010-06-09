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
 *  API Version: 2009-04-15
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;

using Amazon.Util;

using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;
using Amazon.SimpleDB.Util;
using Attribute = Amazon.SimpleDB.Model.Attribute;

namespace Amazon.SimpleDB
{
    /// <summary>
    /// AmazonSimpleDBClient is an implementation of AmazonSimpleDB
    /// Amazon SimpleDB is a web service for running queries on structured
    /// data in real time. This service works in close conjunction with Amazon
    /// Simple Storage Service (Amazon S3) and Amazon Elastic Compute Cloud
    /// (Amazon EC2), collectively providing the ability to store, process
    /// and query data sets in the cloud. These services are designed to make
    /// web-scale computing easier and more cost-effective for developers.
    /// Traditionally, this type of functionality has been accomplished with
    /// a clustered relational database that requires a sizable upfront
    /// investment, brings more complexity than is typically needed, and often
    /// requires a DBA to maintain and administer. In contrast, Amazon SimpleDB
    /// is easy to use and provides the core functionality of a database -
    /// real-time lookup and simple querying of structured data without the
    /// operational complexity.  Amazon SimpleDB requires no schema, automatically
    /// indexes your data and provides a simple API for storage and access.
    /// This eliminates the administrative burden of data modeling, index
    /// maintenance, and performance tuning. Developers gain access to this
    /// functionality within Amazon's proven computing environment, are able
    /// to scale instantly, and pay only for what they use.
    /// </summary>
    public class AmazonSimpleDBClient : AmazonSimpleDB, IDisposable
    {
        private string awsAccessKeyId;
        private SecureString awsSecretAccessKey;
        private AmazonSimpleDBConfig config;
        private bool disposed;

        #region Dispose Pattern Implementation

        /// <summary>
        /// Implements the Dispose pattern for the AmazonSimpleDBClient
        /// </summary>
        /// <param name="fDisposing">Whether this object is being disposed via a call to Dispose
        /// or garbage collected.</param>
        protected virtual void Dispose(bool fDisposing)
        {
            if (!this.disposed)
            {
                if (fDisposing)
                {
                    //Remove Unmanaged Resources
                    // I.O.W. remove resources that have to be explicitly
                    // "Dispose"d or Closed
                    if (awsSecretAccessKey != null)
                    {
                        awsSecretAccessKey.Dispose();
                        awsSecretAccessKey = null;
                    }
                }
                this.disposed = true;
            }
        }

        /// <summary>
        /// Disposes of all managed and unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The destructor for the client class.
        /// </summary>
        ~AmazonSimpleDBClient()
        {
            this.Dispose(false);
        }

        #endregion

        /// <summary>
        /// Constructs AmazonSimpleDBClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        public AmazonSimpleDBClient(string awsAccessKeyId, string awsSecretAccessKey)
            : this(awsAccessKeyId, awsSecretAccessKey, new AmazonSimpleDBConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonSimpleDBClient with AWS Access Key ID, AWS Secret Key and an
        /// AmazonSimpleDB Configuration object
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="config">The AmazonSimpleDB Configuration Object</param>
        public AmazonSimpleDBClient(string awsAccessKeyId, string awsSecretAccessKey, AmazonSimpleDBConfig config)
        {
            if (awsSecretAccessKey != null)
            {
                this.awsSecretAccessKey = new SecureString();
                foreach (char ch in awsSecretAccessKey.ToCharArray())
                {
                    this.awsSecretAccessKey.AppendChar(ch);
                }
                this.awsSecretAccessKey.MakeReadOnly();
            }
            this.awsAccessKeyId = awsAccessKeyId;
            this.config = config;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.UseNagleAlgorithm = false;
        }

        /// <summary>
        /// Constructs an AmazonSimpleDBClient with AWS Access Key ID, AWS Secret Key and an
        /// AmazonSimpleDB Configuration object
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key as a SecureString</param>
        /// <param name="config">The AmazonSimpleDB Configuration Object</param>
        public AmazonSimpleDBClient(string awsAccessKeyId, SecureString awsSecretAccessKey, AmazonSimpleDBConfig config)
        {
            this.awsAccessKeyId = awsAccessKeyId;
            this.awsSecretAccessKey = awsSecretAccessKey;
            this.config = config;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.UseNagleAlgorithm = false;
        }

        #region Public API

        /// <summary>
        /// Create Domain
        /// </summary>
        /// <param name="request">Create Domain  request</param>
        /// <returns>Create Domain  Response from the service</returns>
        /// <remarks>
        /// The CreateDomain operation creates a new domain. The domain name must be unique
        /// among the domains associated with the Access Key ID provided in the request. The CreateDomain
        /// operation may take 10 or more seconds to complete.
        /// </remarks>
        public CreateDomainResponse CreateDomain(CreateDomainRequest request)
        {
            return Invoke<CreateDomainResponse>(ConvertCreateDomain(request));
        }

        /// <summary>
        /// List Domains
        /// </summary>
        /// <param name="request">List Domains  request</param>
        /// <returns>List Domains  Response from the service</returns>
        /// <remarks>
        /// The ListDomains operation lists all domains associated with the Access Key ID. It returns
        /// domain names up to the limit set by MaxNumberOfDomains. A NextToken is returned if there are more
        /// than MaxNumberOfDomains domains. Calling ListDomains successive times with the
        /// NextToken returns up to MaxNumberOfDomains more domain names each time.
        /// </remarks>
        public ListDomainsResponse ListDomains(ListDomainsRequest request)
        {
            return Invoke<ListDomainsResponse>(ConvertListDomains(request));
        }

        /// <summary>
        /// Domain Metadata
        /// </summary>
        /// <param name="request">Domain Metadata  request</param>
        /// <returns>Domain Metadata  Response from the service</returns>
        /// <remarks>
        /// The DomainMetadata operation returns some domain metadata values, such as the
        /// number of items, attribute names and attribute values along with their sizes.
        /// </remarks>
        public DomainMetadataResponse DomainMetadata(DomainMetadataRequest request)
        {
            return Invoke<DomainMetadataResponse>(ConvertDomainMetadata(request));
        }

        /// <summary>
        /// Delete Domain
        /// </summary>
        /// <param name="request">Delete Domain  request</param>
        /// <returns>Delete Domain  Response from the service</returns>
        /// <remarks>
        /// The DeleteDomain operation deletes a domain. Any items (and their attributes) in the domain
        /// are deleted as well. The DeleteDomain operation may take 10 or more seconds to complete.
        /// </remarks>
        public DeleteDomainResponse DeleteDomain(DeleteDomainRequest request)
        {
            return Invoke<DeleteDomainResponse>(ConvertDeleteDomain(request));
        }

        /// <summary>
        /// Put Attributes
        /// </summary>
        /// <param name="request">Put Attributes  request</param>
        /// <returns>Put Attributes  Response from the service</returns>
        /// <remarks>
        /// The PutAttributes operation creates or replaces attributes within an item. You specify new attributes
        /// using a combination of the Attribute.X.Name and Attribute.X.Value parameters. You specify
        /// the first attribute by the parameters Attribute.0.Name and Attribute.0.Value, the second
        /// attribute by the parameters Attribute.1.Name and Attribute.1.Value, and so on.
        /// Attributes are uniquely identified within an item by their name/value combination. For example, a single
        /// item can have the attributes { "first_name", "first_value" } and { "first_name",
        /// second_value" }. However, it cannot have two attribute instances where both the Attribute.X.Name and
        /// Attribute.X.Value are the same.
        /// Optionally, the requestor can supply the Replace parameter for each individual value. Setting this value
        /// to true will cause the new attribute value to replace the existing attribute value(s). For example, if an
        /// item has the attributes { 'a', '1' }, { 'b', '2'} and { 'b', '3' } and the requestor does a
        /// PutAttributes of { 'b', '4' } with the Replace parameter set to true, the final attributes of the
        /// item will be { 'a', '1' } and { 'b', '4' }, replacing the previous values of the 'b' attribute
        /// with the new value.
        /// </remarks>
        public PutAttributesResponse PutAttributes(PutAttributesRequest request)
        {
            return Invoke<PutAttributesResponse>(ConvertPutAttributes(request));
        }

        /// <summary>
        /// Batch Put Attributes
        /// </summary>
        /// <param name="request">Batch Put Attributes  request</param>
        /// <returns>Batch Put Attributes  Response from the service</returns>
        /// <remarks>
        /// The BatchPutAttributes operation creates or replaces attributes within one or more items.
        /// You specify the item name with the Item.X.ItemName parameter.
        /// You specify new attributes using a combination of the Item.X.Attribute.Y.Name and Item.X.Attribute.Y.Value parameters.
        /// You specify the first attribute for the first item by the parameters Item.0.Attribute.0.Name and Item.0.Attribute.0.Value,
        /// the second attribute for the first item by the parameters Item.0.Attribute.1.Name and Item.0.Attribute.1.Value, and so on.
        /// Attributes are uniquely identified within an item by their name/value combination. For example, a single
        /// item can have the attributes { "first_name", "first_value" } and { "first_name",
        /// second_value" }. However, it cannot have two attribute instances where both the Item.X.Attribute.Y.Name and
        /// Item.X.Attribute.Y.Value are the same.
        /// Optionally, the requestor can supply the Replace parameter for each individual value. Setting this value
        /// to true will cause the new attribute value to replace the existing attribute value(s). For example, if an
        /// item 'I' has the attributes { 'a', '1' }, { 'b', '2'} and { 'b', '3' } and the requestor does a
        /// BacthPutAttributes of {'I', 'b', '4' } with the Replace parameter set to true, the final attributes of the
        /// item will be { 'a', '1' } and { 'b', '4' }, replacing the previous values of the 'b' attribute
        /// with the new value.
        /// </remarks>
        public BatchPutAttributesResponse BatchPutAttributes(BatchPutAttributesRequest request)
        {
            return Invoke<BatchPutAttributesResponse>(ConvertBatchPutAttributes(request));
        }

        /// <summary>
        /// Get Attributes
        /// </summary>
        /// <param name="request">Get Attributes  request</param>
        /// <returns>Get Attributes  Response from the service</returns>
        /// <remarks>
        /// Returns all of the attributes associated with the item. Optionally, the attributes returned can be limited to
        /// the specified AttributeName parameter.
        /// If the item does not exist on the replica that was accessed for this operation, an empty attribute is
        /// returned. The system does not return an error as it cannot guarantee the item does not exist on other
        /// replicas.
        /// </remarks>
        public GetAttributesResponse GetAttributes(GetAttributesRequest request)
        {
            return Invoke<GetAttributesResponse>(ConvertGetAttributes(request));
        }

        /// <summary>
        /// Delete Attributes
        /// </summary>
        /// <param name="request">Delete Attributes  request</param>
        /// <returns>Delete Attributes  Response from the service</returns>
        /// <remarks>
        /// Deletes one or more attributes associated with the item. If all attributes of an item are deleted, the item is
        /// deleted.
        /// </remarks>
        public DeleteAttributesResponse DeleteAttributes(DeleteAttributesRequest request)
        {
            return Invoke<DeleteAttributesResponse>(ConvertDeleteAttributes(request));
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="request">Select  request</param>
        /// <returns>Select  Response from the service</returns>
        /// <remarks>
        /// The Select operation returns a set of item names and associate attributes that match the
        /// query expression. Select operations that run longer than 5 seconds will likely time-out
        /// and return a time-out error response.
        /// </remarks>
        public SelectResponse Select(SelectRequest request)
        {
            return Invoke<SelectResponse>(ConvertSelect(request));
        }

        #endregion

        #region Private API

        /**
         * Configure HttpClient with set of defaults as well as configuration
         * from AmazonSimpleDBConfig instance
         */
        private static HttpWebRequest ConfigureWebRequest(int contentLength, AmazonSimpleDBConfig config)
        {
            HttpWebRequest request = WebRequest.Create(config.ServiceURL) as HttpWebRequest;

            if (config.IsSetProxyHost())
            {
                request.Proxy = new WebProxy(config.ProxyHost, config.ProxyPort);
            }
            request.UserAgent = config.UserAgent;
            request.Method = "POST";
            request.Timeout = 50000;
            request.ContentType = AWSSDKUtils.UrlEncodedContent;
            request.ContentLength = contentLength;

            return request;
        }

        /**
         * Invoke request and return response
         */
        private T Invoke<T>(IDictionary<string, string> parameters)
        {
            string actionName = parameters["Action"];
            T response = default(T);
            string responseBody = null;
            HttpStatusCode statusCode = default(HttpStatusCode);

            /* Add required request parameters */
            AddRequiredParameters(parameters);

            string queryString = GetParametersAsString(parameters);

            byte[] requestData = Encoding.UTF8.GetBytes(queryString);
            bool shouldRetry = true;
            int retries = 0;
            do
            {
                HttpWebRequest request = ConfigureWebRequest(requestData.Length, config);
                /* Submit the request and read response body */
                try
                {
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(requestData, 0, requestData.Length);
                    }
                    using (HttpWebResponse httpResponse = request.GetResponse() as HttpWebResponse)
                    {
                        statusCode = httpResponse.StatusCode;
                        using (StreamReader reader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
                        {
                            responseBody = reader.ReadToEnd();
                        }
                    }

                    /* Perform response transformation for GetAttributes and Select operations */
                    if (actionName.Equals("GetAttributes") ||
                        actionName.Equals("Select"))
                    {
                        if (responseBody != null &&
                            responseBody.Trim().EndsWith(String.Concat(actionName, "Response>")))
                        {
                            responseBody = Transform(responseBody, actionName, this.GetType());
                        }
                    }

                    /* Attempt to deserialize response into <Action> Response type */
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    using (XmlTextReader sr = new XmlTextReader(new StringReader(responseBody)))
                    {
                        response = (T)serializer.Deserialize(sr);
                    }
                    shouldRetry = false;
                }
                /* Web exception is thrown on unsucessful responses */
                catch (WebException we)
                {
                    shouldRetry = false;
                    using (HttpWebResponse httpErrorResponse = we.Response as HttpWebResponse)
                    {
                        if (httpErrorResponse == null)
                        {
                            // Abort the unsuccessful request
                            request.Abort();
                            throw new AmazonSimpleDBException(we);
                        }
                        statusCode = httpErrorResponse.StatusCode;
                        using (StreamReader reader = new StreamReader(httpErrorResponse.GetResponseStream(), Encoding.UTF8))
                        {
                            responseBody = reader.ReadToEnd();
                        }

                        // Abort the unsuccessful request
                        request.Abort();
                    }

                    if (statusCode == HttpStatusCode.InternalServerError || statusCode == HttpStatusCode.ServiceUnavailable)
                    {
                        shouldRetry = true;
                        PauseOnRetry(++retries, statusCode);
                    }
                    else
                    {
                        AmazonSimpleDBException se = ReportAnyErrors(responseBody, statusCode);
                        throw se;
                    }
                }
                catch (Exception)
                {
                    // Abort the unsuccessful request
                    request.Abort();
                    throw;
                }
            } while (shouldRetry);

            return response;
        }

        /**
         * Look for additional error strings in the response and return formatted exception
         */
        private static AmazonSimpleDBException ReportAnyErrors(string responseBody, HttpStatusCode status)
        {
            AmazonSimpleDBException ex = null;

            if (responseBody != null && responseBody.StartsWith("<"))
            {
                Match errorMatcherOne = Regex.Match(
                    responseBody,
                    "<RequestId>(.*)</RequestId>.*<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?",
                    RegexOptions.Multiline
                    );
                Match errorMatcherTwo = Regex.Match(
                    responseBody,
                    "<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?.*<RequestID>(.*)</RequestID>",
                    RegexOptions.Multiline
                    );
                Match errorMatcherThree = Regex.Match(
                    responseBody,
                    "<Error><Code>(.*)</Code><Message>(.*)</Message><BoxUsage>(.*)</BoxUsage></Error>.*(<Error>)?.*<RequestID>(.*)</RequestID>",
                    RegexOptions.Multiline);

                if (errorMatcherOne.Success)
                {
                    string requestId = errorMatcherOne.Groups[1].Value;
                    string code = errorMatcherOne.Groups[2].Value;
                    string message = errorMatcherOne.Groups[3].Value;

                    ex = new AmazonSimpleDBException(message, status, code, "Unknown", null, requestId, responseBody);
                }
                else if (errorMatcherTwo.Success)
                {
                    string code = errorMatcherTwo.Groups[1].Value;
                    string message = errorMatcherTwo.Groups[2].Value;
                    string requestId = errorMatcherTwo.Groups[4].Value;

                    ex = new AmazonSimpleDBException(message, status, code, "Unknown", null, requestId, responseBody);
                }
                else if (errorMatcherThree.Success)
                {
                    string code = errorMatcherThree.Groups[1].Value;
                    string message = errorMatcherThree.Groups[2].Value;
                    string boxUsage = errorMatcherThree.Groups[3].Value;
                    string requestId = errorMatcherThree.Groups[5].Value;

                    ex = new AmazonSimpleDBException(message, status, code, "Unknown", boxUsage, requestId, responseBody);
                }
                else
                {
                    ex = new AmazonSimpleDBException("Internal Error", status);
                }
            }
            else
            {
                ex = new AmazonSimpleDBException("Internal Error", status);
            }
            return ex;
        }

        /**
         * Exponential sleep on failed request
         */
        private void PauseOnRetry(int retries, HttpStatusCode status)
        {
            if (retries <= config.MaxErrorRetry)
            {
                int delay = (int)Math.Pow(4, retries) * 100;
                System.Threading.Thread.Sleep(delay);
            }
            else
            {
                throw new AmazonSimpleDBException("Maximum number of retry attempts reached : " + (retries - 1), status);
            }
        }

        /**
         * Add authentication related and version parameters
         */
        private void AddRequiredParameters(IDictionary<string, string> parameters)
        {
            if (String.IsNullOrEmpty(this.awsAccessKeyId))
            {
                throw new AmazonSimpleDBException("The AWS Access Key ID cannot be NULL or a Zero length string");
            }
            parameters.Add("AWSAccessKeyId", this.awsAccessKeyId);
            parameters.Add("Timestamp", AmazonSimpleDBUtil.FormattedCurrentTimestamp);
            parameters.Add("Version", config.ServiceVersion);
            parameters.Add("SignatureVersion", config.SignatureVersion);
            parameters.Add("Signature", SignParameters(parameters, this.awsSecretAccessKey, config));
        }

        /**
         * Convert Dictionary of paremeters to Url encoded query string
         */
        private static string GetParametersAsString(IDictionary<string, string> parameters)
        {
            StringBuilder data = new StringBuilder(512);
            foreach (string key in (IEnumerable<string>)parameters.Keys)
            {
                string value = parameters[key];
                if (value != null)
                {
                    data.Append(key);
                    data.Append('=');
                    data.Append(AWSSDKUtils.UrlEncode(value, false));
                    data.Append('&');
                }
            }
            string result = data.ToString();
            return result.Remove(result.Length - 1);
        }

        /**
         * Computes RFC 2104-compliant HMAC signature for request parameters
         * Implements AWS Signature, as per following spec:
         *
         * If Signature Version is 0, it signs concatenated Action and Timestamp
         *
         * If Signature Version is 1, it performs the following:
         *
         * Sorts all  parameters (including SignatureVersion and excluding Signature,
         * the value of which is being created), ignoring case.
         *
         * Iterate over the sorted list and append the parameter name (in original case)
         * and then its value. It will not URL-encode the parameter values before
         * constructing this string. There are no separators.
         *
         * If Signature Version is 2, string to sign is based on following:
         *
         *    1. The HTTP Request Method followed by an ASCII newline (%0A)
         *    2. The HTTP Host header in the form of lowercase host, followed by an ASCII newline.
         *    3. The URL encoded HTTP absolute path component of the URI
         *       (up to but not including the query string parameters);
         *       if this is empty use a forward '/'. This parameter is followed by an ASCII newline.
         *    4. The concatenation of all query string components (names and values)
         *       as UTF-8 characters which are URL encoded as per RFC 3986
         *       (hex characters MUST be uppercase), sorted using lexicographic byte ordering.
         *       Parameter names are separated from their values by the '=' character
         *       (ASCII character 61), even if the value is empty.
         *       Pairs of parameter and values are separated by the ampersand character (ASCII code 38).
         *
         */
        private static string SignParameters(IDictionary<string, string> parameters, SecureString key, AmazonSimpleDBConfig config)
        {
            string signatureVersion = parameters["SignatureVersion"];

            KeyedHashAlgorithm algorithm = new HMACSHA1();

            string stringToSign = null;
            if ("2".Equals(signatureVersion))
            {
                string signatureMethod = config.SignatureMethod;
                algorithm = KeyedHashAlgorithm.Create(signatureMethod.ToUpper());
                parameters.Add("SignatureMethod", signatureMethod);
                stringToSign = CalculateStringToSignV2(parameters, config);
            }
            else
            {
                throw new AmazonSimpleDBException("Invalid Signature Version specified");
            }

            return Sign(stringToSign, key, algorithm);
        }

        private static string CalculateStringToSignV2(IDictionary<string, string> parameters, AmazonSimpleDBConfig config)
        {
            StringBuilder data = new StringBuilder(512);
            IDictionary<string, string> sorted =
                  new SortedDictionary<string, string>(parameters, StringComparer.Ordinal);
            data.Append("POST");
            data.Append("\n");
            Uri endpoint = new Uri(config.ServiceURL.ToLower());

            data.Append(endpoint.Host);
            data.Append("\n");
            string uri = endpoint.AbsolutePath;
            if (uri == null || uri.Length == 0)
            {
                uri = "/";
            }
            data.Append(AWSSDKUtils.UrlEncode(uri, true));
            data.Append("\n");
            foreach (KeyValuePair<string, string> pair in sorted)
            {
                if (pair.Value != null)
                {
                    data.Append(AWSSDKUtils.UrlEncode(pair.Key, false));
                    data.Append("=");
                    data.Append(AWSSDKUtils.UrlEncode(pair.Value, false));
                    data.Append("&");
                }
            }

            string result = data.ToString();
            return result.Remove(result.Length - 1);
        }

        /// <summary>
        /// Computes RFC 2104-compliant HMAC signature
        /// </summary>
        /// <param name="data">The data to be signed</param>
        /// <param name="key">The secret signing key</param>
        /// <param name="algorithm">The algorithm to sign the data with</param>
        /// <returns>A string representing the HMAC signature</returns>
        private static string Sign(string data, System.Security.SecureString key, KeyedHashAlgorithm algorithm)
        {
            if (key == null)
            {
                throw new AmazonSimpleDBException("The AWS Secret Access Key specified is NULL");
            }

            return AWSSDKUtils.HMACSign(data, key, algorithm);
        }

        /*
         *  Transforms response based on xslt template
         */
        private static string Transform(string responseBody, string action, Type t)
        {
            XslCompiledTransform transformer = new XslCompiledTransform();

            // Build the name of the xslt transform to apply to the response
            char[] seps = { ',' };

            Assembly assembly = Assembly.GetAssembly(t);
            string assemblyName = assembly.FullName;
            assemblyName = assemblyName.Split(seps)[0];

            string ns = t.Namespace;
            string resourceName = String.Concat(
                assemblyName,
                ".",
                ns,
                ".Model.",
                "ResponseTransformer.xslt"
                );

            using (XmlTextReader xmlReader = new XmlTextReader(assembly.GetManifestResourceStream(resourceName)))
            {
                transformer.Load(xmlReader);

                StringBuilder sb = new StringBuilder(1024);
                using (XmlTextReader xmlR = new XmlTextReader(new StringReader(responseBody)))
                {
                    using (StringWriter sw = new StringWriter(sb))
                    {
                        transformer.Transform(xmlR, null, sw);
                        return sb.ToString();
                    }
                }
            }
        }

        /**
         * Convert CreateDomainRequest to name value pairs
         */
        private static IDictionary<string, string> ConvertCreateDomain(CreateDomainRequest request)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Action", "CreateDomain");
            if (request.IsSetDomainName())
            {
                parameters.Add("DomainName", request.DomainName);
            }

            return parameters;
        }

        /**
         * Convert ListDomainsRequest to name value pairs
         */
        private static IDictionary<string, string> ConvertListDomains(ListDomainsRequest request)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Action", "ListDomains");
            if (request.IsSetMaxNumberOfDomains())
            {
                parameters.Add("MaxNumberOfDomains", (request.MaxNumberOfDomains + ""));
            }
            if (request.IsSetNextToken())
            {
                parameters.Add("NextToken", request.NextToken);
            }

            return parameters;
        }

        /**
         * Convert DomainMetadataRequest to name value pairs
         */
        private static IDictionary<string, string> ConvertDomainMetadata(DomainMetadataRequest request)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Action", "DomainMetadata");
            if (request.IsSetDomainName())
            {
                parameters.Add("DomainName", request.DomainName);
            }

            return parameters;
        }

        /**
         * Convert DeleteDomainRequest to name value pairs
         */
        private static IDictionary<string, string> ConvertDeleteDomain(DeleteDomainRequest request)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Action", "DeleteDomain");
            if (request.IsSetDomainName())
            {
                parameters.Add("DomainName", request.DomainName);
            }

            return parameters;
        }

        /**
         * Convert PutAttributesRequest to name value pairs
         */
        private static IDictionary<string, string> ConvertPutAttributes(PutAttributesRequest request)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Action", "PutAttributes");
            if (request.IsSetDomainName())
            {
                parameters.Add("DomainName", request.DomainName);
            }
            if (request.IsSetItemName())
            {
                parameters.Add("ItemName", request.ItemName);
            }
            List<ReplaceableAttribute> putAttributesRequestAttributeList = request.Attribute;
            int putAttributesRequestAttributeListIndex = 1;
            foreach (ReplaceableAttribute putAttributesRequestAttribute in putAttributesRequestAttributeList)
            {
                if (putAttributesRequestAttribute.IsSetName())
                {
                    parameters.Add("Attribute" + "."  + putAttributesRequestAttributeListIndex + "." + "Name", putAttributesRequestAttribute.Name);
                }
                if (putAttributesRequestAttribute.IsSetValue())
                {
                    parameters.Add("Attribute" + "."  + putAttributesRequestAttributeListIndex + "." + "Value", putAttributesRequestAttribute.Value);
                }
                if (putAttributesRequestAttribute.IsSetReplace())
                {
                    parameters.Add("Attribute" + "."  + putAttributesRequestAttributeListIndex + "." + "Replace", (putAttributesRequestAttribute.Replace + "").ToLower());
                }

                putAttributesRequestAttributeListIndex++;
            }

            return parameters;
        }

        /**
         * Convert BatchPutAttributesRequest to name value pairs
         */
        private static IDictionary<string, string> ConvertBatchPutAttributes(BatchPutAttributesRequest request)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Action", "BatchPutAttributes");
            if (request.IsSetDomainName())
            {
                parameters.Add("DomainName", request.DomainName);
            }
            List<ReplaceableItem> batchPutAttributesRequestItemList = request.Item;
            int batchPutAttributesRequestItemListIndex = 1;
            foreach (ReplaceableItem batchPutAttributesRequestItem in batchPutAttributesRequestItemList)
            {
                if (batchPutAttributesRequestItem.IsSetItemName())
                {
                    parameters.Add("Item" + "."  + batchPutAttributesRequestItemListIndex + "." + "ItemName", batchPutAttributesRequestItem.ItemName);
                }
                List<ReplaceableAttribute> itemAttributeList = batchPutAttributesRequestItem.Attribute;
                int itemAttributeListIndex = 1;
                foreach (ReplaceableAttribute itemAttribute in itemAttributeList)
                {
                    if (itemAttribute.IsSetName())
                    {
                        parameters.Add("Item" + "."  + batchPutAttributesRequestItemListIndex + "." + "Attribute" + "."  + itemAttributeListIndex + "." + "Name", itemAttribute.Name);
                    }
                    if (itemAttribute.IsSetValue())
                    {
                        parameters.Add("Item" + "."  + batchPutAttributesRequestItemListIndex + "." + "Attribute" + "."  + itemAttributeListIndex + "." + "Value", itemAttribute.Value);
                    }
                    if (itemAttribute.IsSetReplace())
                    {
                        parameters.Add("Item" + "."  + batchPutAttributesRequestItemListIndex + "." + "Attribute" + "."  + itemAttributeListIndex + "." + "Replace", (itemAttribute.Replace + "").ToLower());
                    }

                    itemAttributeListIndex++;
                }

                batchPutAttributesRequestItemListIndex++;
            }

            return parameters;
        }

        /**
         * Convert GetAttributesRequest to name value pairs
         */
        private static IDictionary<string, string> ConvertGetAttributes(GetAttributesRequest request)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Action", "GetAttributes");
            if (request.IsSetDomainName())
            {
                parameters.Add("DomainName", request.DomainName);
            }
            if (request.IsSetItemName())
            {
                parameters.Add("ItemName", request.ItemName);
            }
            List<string> getAttributesRequestAttributeNameList  =  request.AttributeName;
            int getAttributesRequestAttributeNameListIndex = 1;
            foreach  (string getAttributesRequestAttributeName in getAttributesRequestAttributeNameList)
            {
                parameters.Add("AttributeName" + "."  + getAttributesRequestAttributeNameListIndex, getAttributesRequestAttributeName);
                getAttributesRequestAttributeNameListIndex++;
            }

            return parameters;
        }

        /**
         * Convert DeleteAttributesRequest to name value pairs
         */
        private static IDictionary<string, string> ConvertDeleteAttributes(DeleteAttributesRequest request)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Action", "DeleteAttributes");
            if (request.IsSetDomainName())
            {
                parameters.Add("DomainName", request.DomainName);
            }
            if (request.IsSetItemName())
            {
                parameters.Add("ItemName", request.ItemName);
            }
            List<Attribute> deleteAttributesRequestAttributeList = request.Attribute;
            int deleteAttributesRequestAttributeListIndex = 1;
            foreach (Attribute deleteAttributesRequestAttribute in deleteAttributesRequestAttributeList)
            {
                if (deleteAttributesRequestAttribute.IsSetName())
                {
                    parameters.Add("Attribute" + "."  + deleteAttributesRequestAttributeListIndex + "." + "Name", deleteAttributesRequestAttribute.Name);
                }
                if (deleteAttributesRequestAttribute.IsSetValue())
                {
                    parameters.Add("Attribute" + "."  + deleteAttributesRequestAttributeListIndex + "." + "Value", deleteAttributesRequestAttribute.Value);
                }

                deleteAttributesRequestAttributeListIndex++;
            }

            return parameters;
        }

        /**
         * Convert SelectRequest to name value pairs
         */
        private static IDictionary<string, string> ConvertSelect(SelectRequest request)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Action", "Select");
            if (request.IsSetSelectExpression())
            {
                parameters.Add("SelectExpression", request.SelectExpression);
            }
            if (request.IsSetNextToken())
            {
                parameters.Add("NextToken", request.NextToken);
            }

            return parameters;
        }

        #endregion
    }
}
