// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Utility
{
    /// <summary>
    /// This class may be used to create mutex objects.
    /// </summary>
    public class ManageMutex
    {
        /// <summary>
        /// Maximum number of threads that may share the
        /// mutex.
        /// </summary>
        private int m_MaxCount;

        /// <summary>
        /// Current number of threads that share the mutex.
        /// </summary>
        private int m_Count;

        /// <summary>
        /// Number of threads currently blocked by the mutex.
        /// </summary>
        private int m_NumBlocked;

        /// <summary>
        /// Mutex constructor that sets the maximum number
        /// of threads that may concurrently hold the mutex.
        /// </summary>
        /// 
        /// <param name="count">
        /// The maximum number of threads that may concurrently
        /// hold the mutex.  Zero forces the first locking
        /// thread to block.
        /// </param>
        public ManageMutex(int count)
        {
            // Insure the max count is not less than zero.

            m_MaxCount = (count >= 0) ? count : 0;
            m_Count = m_MaxCount;
            m_NumBlocked = 0;
        }

        /// <summary>
        /// Grab a mutex.  If the number of threads currently
        /// holding the mutex has reached the maximum, this
        /// thread will block.
        /// </summary>
        public void Lock()
        {
            lock (this)
            {
                if ((m_Count == 0) || (m_NumBlocked > 0))
                {
                    ++m_NumBlocked;
                    System.Threading.Monitor.Wait(this);
                    --m_NumBlocked;
                }

                --m_Count;
            }
        }

        /// <summary>
        /// Release a mutex.
        /// </summary>
        public void Unlock()
        {
            lock (this)
            {
                ++m_Count;

                if (m_NumBlocked > 0)
                {
                    System.Threading.Monitor.Pulse(this);
                }
            }
        }
    }
}
