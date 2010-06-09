﻿/*******************************************************************************
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
 *
 */

using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Amazon.Util
{
    /// <summary>
    /// This class defines utilities and constants that can be used by 
    /// all the client libraries of the SDK.
    /// </summary>
    public static class AWSSDKUtils
    {
        #region Internal Constants

        internal const string IfModifiedSinceHeader = "IfModifiedSince";
        internal const string IfMatchHeader = "If-Match";
        internal const string ContentTypeHeader = "Content-Type";
        internal const string ContentLengthHeader = "Content-Length";
        internal const string ContentMD5Header = "Content-MD5";
        internal const string ETagHeader = "ETag";

        #endregion

        #region Public Constants

        /// <summary>
        /// The AWS SDK User Agent
        /// </summary>
        public const string SDKUserAgent = "AWS SDK for .NET/1.0.4";

        /// <summary>
        /// The Set of accepted and valid Url characters. 
        /// Characters outside of this set will be encoded
        /// </summary>
        public const string ValidUrlCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

        /// <summary>
        /// The string representing Url Encoded Content in HTTP requests
        /// </summary>
        public const string UrlEncodedContent = "application/x-www-form-urlencoded; charset=utf-8";

        /// <summary>
        /// The GMT Date Format string. Used when parsing date objects
        /// </summary>
        public const string GMTDateFormat = "ddd, dd MMM yyyy HH:mm:ss \\G\\M\\T";

        /// <summary>
        /// The ISO8601Date Format string. Used when parsing date objects
        /// </summary>
        public const string ISO8601DateFormat = "yyyy-MM-dd\\THH:mm:ss.fff\\Z";

        #endregion

        #region Public Methods and Properties

        /// <summary>
        /// Formats the current date as ISO 8601 timestamp
        /// </summary>
        /// <returns>An ISO 8601 formatted string representation
        /// of the current date and time
        /// </returns>
        public static string FormattedCurrentTimestamp
        {
            get
            {
                DateTime dateTime = DateTime.UtcNow;
                DateTime formatted = new DateTime(
                    dateTime.Year,
                    dateTime.Month,
                    dateTime.Day,
                    dateTime.Hour,
                    dateTime.Minute,
                    dateTime.Second,
                    dateTime.Millisecond,
                    DateTimeKind.Local
                    );
                return formatted.ToString(
                    GMTDateFormat,
                    CultureInfo.InvariantCulture
                    );
            }
        }

        /// <summary>
        /// Computes RFC 2104-compliant HMAC signature
        /// </summary>
        /// <param name="data">The data to be signed</param>
        /// <param name="key">The secret signing key</param>
        /// <param name="algorithm">The algorithm to sign the data with</param>
        /// <exception cref="T:System.ArgumentNullException"/>
        /// <returns>A string representing the HMAC signature</returns>
        public static string HMACSign(string data, System.Security.SecureString key, KeyedHashAlgorithm algorithm)
        {
            if (null == key)
            {
                throw new ArgumentNullException("key", "Please specify a Secret Signing Key.");
            }

            if (String.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data", "Please specify data to sign.");
            }

            if (null == algorithm)
            {
                throw new ArgumentNullException("algorithm", "Please specify a KeyedHashAlgorithm to use.");
            }

            // pointer to hold unmanaged reference to SecureString instance
            IntPtr bstr = IntPtr.Zero;
            char[] charArray = new char[key.Length];
            try
            {
                // Marshal SecureString into byte array
                bstr = Marshal.SecureStringToBSTR(key);
                Marshal.Copy(bstr, charArray, 0, charArray.Length);
                algorithm.Key = Encoding.UTF8.GetBytes(charArray);
                return Convert.ToBase64String(algorithm.ComputeHash(
                    Encoding.UTF8.GetBytes(data.ToCharArray()))
                    );
            }
            finally
            {
                // Make sure that the clear text data is zeroed out
                Marshal.ZeroFreeBSTR(bstr);
                algorithm.Clear();
                Array.Clear(charArray, 0, charArray.Length);
            }
        }

        /// <summary>
        /// URL encodes a string. If the path property is specified,
        /// the accepted path characters {/+:} are not encoded.
        /// </summary>
        /// <param name="data">The string to encode</param>
        /// <param name="path">Whether the string is a URL path or not</param>
        /// <returns></returns>
        public static string UrlEncode(string data, bool path)
        {
            StringBuilder encoded = new StringBuilder(data.Length * 2);
            string unreservedChars = String.Concat(
                AWSSDKUtils.ValidUrlCharacters,
                (path ? "/:+" : "")
                );

            foreach (char symbol in System.Text.Encoding.UTF8.GetBytes(data))
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                {
                    encoded.Append(symbol);
                }
                else
                {
                    encoded.Append("%").Append(String.Format("{0:X2}", (int)symbol));
                }
            }

            return encoded.ToString();
        }

        #endregion
    }
}