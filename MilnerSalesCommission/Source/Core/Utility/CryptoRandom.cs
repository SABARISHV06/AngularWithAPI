// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//


using System;
using System.Security.Cryptography;

namespace Utility
{
    /// <summary>
    /// To genarate random number
    /// </summary>
    public class CryptoRandom : IDisposable
    {
        #region Declarations
        /// <summary>
        /// This object has been disposed.
        /// </summary>
        private bool m_Disposed = false;

        /// <summary>
        /// Best practice number generator.
        /// </summary>
        private RNGCryptoServiceProvider m_Generator = new RNGCryptoServiceProvider();

        /// <summary>
        /// The buffer that holds the most recent raw random data.  This is sufficient in size to generate a 32-bit number.
        /// </summary>
        private byte[] m_Data = new byte[4];

        #endregion

        #region Constructor

        /// <summary>
        /// Construct a random number generator.
        /// </summary>
        public CryptoRandom() { }

        #endregion

        #region Methods
        /// <summary>
        /// Get a new random 32-bit number.
        /// </summary>
        /// <returns></returns>
        public Int32 Next()
        {
            m_Generator.GetBytes(m_Data);
            return BitConverter.ToInt32(m_Data, 0) & 0x7FFFFFFF;
        }

        /// <summary>
        /// Get a new random number in the range 0 to maxValue.
        /// </summary>
        /// <param name="maxValue">The maximum integer value to generate.</param>
        /// <returns>An integer in the range 0 to maxValue.</returns>
        public Int32 Next(Int32 maxValue)
        {
            if (maxValue < 0)
            {
                throw new ArgumentOutOfRangeException("maxValue");
            }

            return Next(0, maxValue);
        }

        /// <summary>
        /// Generate a number within a specific range.
        /// </summary>
        /// <param name="minValue">The smallest number to generate.</param>
        /// <param name="maxValue">The largest number to generate.</param>
        /// <returns>An integer in the range minValue to maxValue.</returns>
        public Int32 Next(Int32 minValue, Int32 maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException("minValue");
            }

            if (minValue == maxValue)
            {
                return minValue;
            }

            Int64 diff = maxValue - minValue;
            while (true)
            {
                m_Generator.GetBytes(m_Data);
                UInt32 rand = BitConverter.ToUInt32(m_Data, 0);
                Int64 max = (1 + (Int64)UInt32.MaxValue);
                Int64 remainder = max % diff;
                if (rand < max - remainder)
                {
                    return (Int32)(minValue + (rand % diff));
                }
            }
        }

        #endregion

        #region IDisposable Pattern

        /// <summary>
        /// Implement IDisposable. Only call this on application exit.
        /// Do not make this method virtual. 
        /// A derived class should not be able to override this method. 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method. 
            // Therefore, you should call GC.SupressFinalize to 
            // take this object off the finalization queue 
            // and prevent finalization code for this object 
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implement idisposable. 
        /// </summary>
        /// <param name="disposing">If disposing equals true, the method has been called directly 
        /// or indirectly by a user's code. Managed and unmanaged resources 
        /// can be disposed. 
        /// If disposing equals false, the method has been called by the 
        /// runtime from inside the finalizer and you should not reference 
        /// other objects. Only unmanaged resources can be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if (!this.m_Disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources. 
                if (disposing)
                {
                    // Dispose managed resources here.

                }

                // Call the appropriate methods to clean up 
                // unmanaged resources here.  If disposing is false, only the following code is executed.
                if (m_Generator != null)
                {
                    m_Generator.Dispose();
                    m_Generator = null;
                }

                // Note disposing has been done.
                m_Disposed = true;
            }
        }

        #endregion
    }
}
