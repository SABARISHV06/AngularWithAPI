// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//


using System;
using System.IO;
using System.Security.Cryptography;

namespace Security
{
    /// <summary>
    /// Deprecated.  
    /// </summary>
    public class EncryptionUtil
    {
        #region Constant Declarations
        /// <summary>
        /// Assign default encryption key
        /// </summary>
        private const string DEFAULT_ENCRYPTION_KEY = "3AF9D09E-FC80-41b9-B1A5-5B79AAD2E258";
        #endregion

        #region Declarations
        private Byte[] m_Key = new Byte[8];
        private Byte[] m_IV = new Byte[8];

        private string m_EncryptionKey;
        private string EncryptionKey
        {
            get { return m_EncryptionKey; }
            set { m_EncryptionKey = value; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor, this method will use the default Encryption key
        /// </summary>
        public EncryptionUtil()
        {
            EncryptionKey = DEFAULT_ENCRYPTION_KEY;
        }

        /// <summary>
        /// Constructor, this method will use the Encryption key provided
        /// </summary>
        /// <param name="encryptionKeyOverride"></param>
        public EncryptionUtil(string encryptionKeyOverride)
        {
            EncryptionKey = encryptionKeyOverride;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Convert a Memory stream to string
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private static string GetStringFromMemoryStream(MemoryStream m)
        {
            if (m == null || m.Length == 0)
            {
                return null;
            }

            m.Flush();
            m.Position = 0;
            string s = string.Empty;
            using (StreamReader sr = new StreamReader(m))
            {
                s = sr.ReadToEnd();
            }

            return s;
        }

        /// <summary>
        /// we parse out a guid to create Key and Hash
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        private bool InitKey(string Key)
        {
            bool result = false;

            try
            {
                // Convert Key to byte array
                Byte[] bp = new Byte[Key.Length];
                System.Text.ASCIIEncoding aEnc = new System.Text.ASCIIEncoding();

                aEnc.GetBytes(Key, 0, Key.Length, bp, 0);
                using (System.Security.Cryptography.SHA1CryptoServiceProvider sha = new System.Security.Cryptography.SHA1CryptoServiceProvider())
                {
                    Byte[] bpHash = sha.ComputeHash(bp);

                    for (int i = 0; i <= 7; i++)
                    {
                        m_Key[i] = bpHash[i];
                    }

                    for (int i = 8; i <= 15; i++)
                    {
                        m_IV[i - 8] = bpHash[i];
                    }
                }

                result = true;
            }
            catch
            {
            }

            return result;
        }

        public string EncryptDeprecated(string termToEncrypt)
        {
            string result = string.Empty;

            try
            {
                InitKey(EncryptionKey);
                string data;
                using (DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider())
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(m_Key, m_IV), CryptoStreamMode.Write))
                        {
                            using (StreamWriter writer = new StreamWriter(cryptoStream))
                            {
                                writer.Write(termToEncrypt);
                                writer.Flush();
                                cryptoStream.FlushFinalBlock();
                                writer.Flush();
                                data = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
                            }
                        }
                    }
                }

                using (MemoryStream m = new MemoryStream())
                {
                    using (StreamWriter sw = new StreamWriter(m))
                    {
                        sw.Write(data);
                        sw.Flush();

                        result = GetStringFromMemoryStream(m);
                    }
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        public string Decrypt(string termToDecrypt)
        {
            string result = string.Empty;

            try
            {
                InitKey(EncryptionKey);

                using (DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider())
                {
                    using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(termToDecrypt)))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(m_Key, m_IV), CryptoStreamMode.Read))
                        {
                            using (StreamReader reader = new StreamReader(cryptoStream))
                            {
                                using (MemoryStream m = new MemoryStream())
                                {
                                    using (StreamWriter sw = new StreamWriter(m))
                                    {
                                        sw.Write(reader.ReadToEnd());
                                        sw.Flush();

                                        result = GetStringFromMemoryStream(m);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Decrypt Process Failed.", ex);
            }

            return result;
        }

        #endregion
    }
}